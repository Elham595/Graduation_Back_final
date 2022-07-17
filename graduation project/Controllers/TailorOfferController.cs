using AutoMapper;
using graduation_project.Const;
using graduation_project.Data;
using graduation_project.Models;
using graduation_project.NonDomainModels;
using graduation_project.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Linq;
using System.Linq.Expressions;


namespace graduation_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TailorOfferController : ControllerBase
    {
        private IBaseRepositoryServices<TailorOffer> _context;

        private IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private FashionDesignContext _DbContext;

        public TailorOfferController(DbContextRepositoryServices<TailorOffer> context,IMapper mapper,FashionDesignContext dbcontext , IUnitOfWork unitOfWork)
        {
            _context = context;
            _mapper = mapper;
            _DbContext = dbcontext;
            _unitOfWork = unitOfWork;
        }

        /*Get All Tailor offer By Admin*/
        [HttpGet("TailorOfferDetails/All")]
       // [Authorize(Roles ="admin")]
       public async Task<IActionResult> GetAllTailorOffers()
        {
            return Ok(await _unitOfWork.TailorOffers.GetAll());
           //return Ok(await _context.GetAll());
        }


        [HttpGet("TailorOfferDetails/All/pages")]
        // [Authorize(Roles ="admin")]
        public async Task<IActionResult> GetAllTailorOffersPages([FromQuery] int page) {
            if (page > 0)
            {
                var list = await _unitOfWork.TailorOffers.GetAllPagination(page, Paginations.NumberOfItems);
                if (list.Any())
                    return Ok(new { Page = page, Result = list });
                 return NotFound(new {page="Not exist"});
            }
            return BadRequest(new { Message= "Page Number Must Be Large Than Zero" });   
        }

        /*Get All Tailor offer By Date*/
        [HttpGet("TailorOfferDetails/All/{date:?}")]
       // [Authorize(Roles ="admin")]
        public async  Task<IActionResult> GetAllTailorOffersByDate(DateTime date)
        {
           // return Ok(await _context.FindAllAsync(d => d.OfferDate == date));
            return Ok(await _unitOfWork.TailorOffers.FindAllAsync(d => d.OfferDate == date));
        }

        /*Get All Tailor offer Between Two Date */
        [HttpGet("TailorOfferDetails/All/dates")]
        // [Authorize(Roles ="admin")]
        public async Task<IActionResult> GetAllTailorOffersBetweenTwoDates([FromQuery] DateTime start, DateTime end)
        {
            return  Ok(await _unitOfWork.TailorOffers.FindAllAsync(t => (t.OfferDate >= start) && (t.OfferDate <= end)));
        }

        //------------------------------------------------------------------------------------//

        ///*Get All Tailor offer Between Two Date */
        //[HttpGet("TailorOfferDetails/All/datesPages")]
        //// [Authorize(Roles ="admin")]
        //public async Task<IActionResult> GetAllTailorOffersBetweenTwoDatesPages([FromQuery] DateTime start, DateTime end)
        //{
        //    await _unitOfWork.TailorOffers.GetAllPagination(t => (t.OfferDate >= start) && (t.OfferDate <= end))
        //    return Ok(await _unitOfWork.TailorOffers.GetAllPagination(t => (t.OfferDate >= start) && (t.OfferDate <= end)));
        //}

        //-----------------------------------------------------------------------------------//

        /*Get Tailor Offer for Specific Client or Tailor with Details about Offer*/
        /*It Can Used By Tailor , Client and Admin*/
        [HttpGet("TailorOfferDetailsPages")]
        [Authorize(Roles ="tailor,client,admin")]
        public async Task<IActionResult>  TailorOfferDetails(string UserName , string Role,int page)
        {

            if (page > 0)
            {
                PagesInformation pagesInformation = new PagesInformation();
                if (Role.ToLower() == "client")
                {
                    pagesInformation = await _unitOfWork.TailorOffers.GetPageInformation(i => i.ClientUserName == UserName);
                    var list = await _DbContext.Procedures.GetOfferForClientPagesAsync(UserName, page, Paginations.NumberOfItems);

                    if (list.Any())
                    {
                        return Ok(new { Page = page, TotalPages = pagesInformation.TotalPages, TotalItems = pagesInformation.TotalItems, ImageStaticPath = Path.Combine(StaticPath.DesignPath), Result = list });
                    }
                    return NotFound(new { Message = "Page Not Exist" });
                }

                else if (Role.ToLower() == "tailor")
                {
                    pagesInformation = await _unitOfWork.TailorOffers.GetPageInformation(i => i.TailorUserName == UserName);

                    var list = await _DbContext.Procedures.GetOfferForTailorPagesAsync(UserName, page, Paginations.NumberOfItems);
                    if (list.Any())
                    {
                        return Ok(new { Page = page, TotalPages = pagesInformation.TotalPages, TotalItems = pagesInformation.TotalItems, ImageStaticPath = Path.Combine(StaticPath.DesignPath), Result = list });
                    }
                    return NotFound(new { Message = "Page Not Exist" });
                }
                return BadRequest(new { Message = "Role Must Be Client or Tailor" });
            }
            
            return BadRequest(new { Message = "Page Start From 1" });


        }


        //-----------------------------------------------------------------------------------------//
        /*It Can Used By Tailor , Client and Admin*/
        [HttpGet("TailorOfferDetails")]
          [Authorize(Roles ="tailor,client,admin")]
        public async Task<IActionResult> TailorOfferDetails(string UserName, string Role)
        {

                if (Role == "Client")
                {
                    var list = await _DbContext.Procedures.GetOfferForClientAsync(UserName);
                    if(list.Any())
                    return Ok(list);

                }

                else if (Role.ToLower() == "tailor")
                {
                    var list = await _DbContext.Procedures.GetOfferForTailorAsync(UserName);
                     if(list.Any())
                    return Ok(list);
                }        
            return BadRequest(new {Message ="Role Must Be Client or Tailor"});
        }



        //---------------------------------------------------------------------------------------------//

        /*Creat A tailor Offer by Tailor*/
        [HttpPost("Create")]
        [Authorize(Roles ="tailor")]
        public async Task<IActionResult> CreateTailorOffer(TailorOfferCreation model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TailorOffer tailorOffer = _mapper.Map<TailorOffer>(model);
            int status = await _context.Add(tailorOffer);
            if (status < 1) {
               return StatusCode(500);
            }
            int OfferId = tailorOffer.OfferId;
            foreach(FabricMeterCreationModel item in model.FabricMeterList)
            {
                int ColorId = await _unitOfWork.Colors.WhereOne(i => i.ColorName == item.ColorName,i=>i.ColorId);
                int FabricId = await _unitOfWork.Fabrics.WhereOne(i=>i.FabricName==item.FabricName,i=>i.FabricId);
                if(ColorId>0 && FabricId>0)
                {
                    TailorOfferFabricMetter tailorOfferFabricMetter = new TailorOfferFabricMetter
                    {
                        OfferId = OfferId,
                        FabricId = FabricId,
                        ColorId = ColorId,
                        NumberOfMeter = item.NumberOfMeter,
                        DesignOrderNumber = model.DesignOrderNumber
                    };
                   await  _unitOfWork.TailorOfferFabricMeters.Add(tailorOfferFabricMetter);
                }
                
            }
            return Created("TailorOfferDetails",new { Message="Succeed" , ClientUserName= model.ClientUserName });
        }
    }
}
