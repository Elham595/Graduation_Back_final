using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using graduation_project.Data;
using graduation_project.Models;
using graduation_project.NonDomainModels;
using graduation_project.Repository;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using graduation_project.Const;
using System.Collections;
using System.Linq.Expressions;

namespace graduation_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AprovedOffersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AprovedOffersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //--------------------------------------------------------------------------------//

        /*Get All Aproved Offers Details  With Pagination By Admin*/
        [HttpGet("AprovedOfferDetails/All")]
          [Authorize(Roles="Admin")]
        public async Task<IActionResult> GetAllAprovedOfferDetails(int page)
        {
            PagesInformation pagesInformation = await _unitOfWork.TailorOffers.GetPageInformation(i => i.Status.ToUpper() == "A");
            var list = await _unitOfWork.DbAprovedOffers.GetAllAprovedOffers("All", page, "Admin");
            if (list.Any())
            {
                return Ok(new { Page = page, TotalPages = pagesInformation.TotalPages, TotalItems = pagesInformation.TotalItems, ImageStaticPath = Path.Combine(StaticPath.DesignPath), Result = list });
            }
            return NotFound(new { Message = "Page Not Exist" });

        }


        //------------------------------------------------------------------------------------//
        /*Get All Aproved Offers   With Pagination By Admin*/
        [HttpGet("AprovedOffer/All")]
          [Authorize(Roles="Admin")]
        public async Task<IActionResult> GetAllAprovedOffer(int page)
        {
            PagesInformation pagesInformation = await _unitOfWork.TailorOffers.GetPageInformation(i => i.Status.ToUpper() == "A");
            var list = await _unitOfWork.TailorOffers.GetAllPagination
                (page,
                Paginations.NumberOfItems,
                i => i.Status.ToUpper() == "A",
                new Expression<Func<TailorOffer, object>>[] { i => i.AprovedOffer },
                i => i.OfferDate,
                OrderByValues.Descending
                );
            if (list.Any())
            {
                return Ok(new { Page = page, TotalPages = pagesInformation.TotalPages, TotalItems = pagesInformation.TotalItems, ImageStaticPath = Path.Combine(StaticPath.DesignPath), Result = list });
            }
            return NotFound(new { Message = "Page Not Exist" });
        }



        //-------------------------------------------------------------------------------------------------//
        /*Get Aproved Offers Details For  Client or Tailor With Pagination*/
        [HttpGet("AprovedOffer")]
      //  [Authorize(Roles="Client,Tailor,Admin")]
        public async Task<IActionResult> GetAllAprovedOffer(string UserName, string Role ,int page)
        {
            PagesInformation pagesInformation = new PagesInformation();
            if (Role.ToLower()=="client")
            {
                 pagesInformation = await _unitOfWork.TailorOffers.GetPageInformation(i => (i.ClientUserName == UserName) && (i.Status.ToUpper() == "A"));
            }
            else if (Role.ToLower() == "tailor")
            {
                pagesInformation = await _unitOfWork.TailorOffers.GetPageInformation(i => (i.TailorUserName == UserName) && (i.Status.ToUpper() == "A"));
            }
            var list = await _unitOfWork.DbAprovedOffers.GetAllAprovedOffers(UserName, page,Role);
            if(list.Any())
            {
                return Ok(new { Page= page, TotalPages = pagesInformation.TotalPages, TotalItems= pagesInformation.TotalItems, ImageStaticPath = Path.Combine(StaticPath.DesignPath), Result= list });
            }
            return NotFound(new { Message = "Page Not Exist" });

        }

        //------------------------------------------------------------------------------------------------//
        /*Create Aproved Offer with Creation of Completed Offer  */
        [HttpPost("AprovedOffer")]
        [Authorize(Roles ="Client")]
        public async Task<IActionResult> CreateAprovedOffer(AprovedOfferCreationModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TailorOffer? tailorOffer = await _unitOfWork.TailorOffers.GetById(model.OfferId);
            if (tailorOffer == null)
                return BadRequest(new { Message = "Offer Not Exist" });
           int Exist= await  _unitOfWork.TailorOffers.WhereOne(i => (i.DesignOrderNumber == tailorOffer.DesignOrderNumber) && (i.Status.ToUpper() == "A"),i=>i.OfferId);
            if (Exist > 0)
                return BadRequest(new { Message = "Design Order Already IN Aproved  For Another Tailor Offer" });
            AprovedOffer aprovedOffer = _mapper.Map<AprovedOffer>(model);
            DateTime dateTime = aprovedOffer.DateOfRecievedFabric.
                AddDays(await _unitOfWork.TailorOffers.WhereOne(i => i.OfferId == model.OfferId, i => i.NumberOfDays));
            aprovedOffer.DateOfRecievedOrder= dateTime;
            

            int status =await _unitOfWork.AprovedOffers.Add(aprovedOffer);
            if (status > 0)
            {
                CompletedOrder completedOrder = new CompletedOrder { OfferId= aprovedOffer.OfferId , DateOfRecieved=dateTime };
               await _unitOfWork.CompletedOrders.Add(completedOrder);
               // TailorOffer tailorOffer = await _unitOfWork.TailorOffers.GetById(aprovedOffer.OfferId);
                tailorOffer.Status = "A";
              int index=  await  _unitOfWork.TailorOffers.Update(tailorOffer);
                if (index > 0)
                    return Created("AprovedOfferDetails", aprovedOffer);
                else
                {
                    await _unitOfWork.AprovedOffers.Remove(aprovedOffer);
                    await _unitOfWork.CompletedOrders.Remove(completedOrder);   

                }
            }
            return StatusCode(500,"Some thing Error Can Not Aproved The offer");


        }

        //--------------------------------------------------------------------------------------------//

        /*Update Aproved Offer with Update of Completed Offer  */
        [HttpPut("AprovedOffer")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> UpdateAprovedOffer(AprovedOfferCreationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AprovedOffer? aprovedOffer = await  _unitOfWork.AprovedOffers.GetById(model.OfferId);
            if(aprovedOffer == null)
                return NotFound(new { Message="Offer Not Exist" });
            if(aprovedOffer.DateOfRecievedOrder<=DateTime.Now)
                return BadRequest(new { Message = "this is Completed Offer Can Not update" });
            if (model.DateOfRecievedFabric <= aprovedOffer.DateOfRecievedFabric)
                return BadRequest(new { Message = " Only Can Upadte the Date of Recieved Fabric to new one After the Current Date" });
            aprovedOffer.DateOfRecievedFabric = model.DateOfRecievedFabric;
            DateTime dateTime = model.DateOfRecievedFabric.
                AddDays(await _unitOfWork.TailorOffers.WhereOne(i => i.OfferId == model.OfferId, i => i.NumberOfDays));
            aprovedOffer.DateOfRecievedOrder = dateTime;
            aprovedOffer.RecievedMethod = model.RecievedMethod;

            int status =  _unitOfWork.Complete();
            if (status > 0)
            {
                CompletedOrder? completedOrder = await _unitOfWork.CompletedOrders.GetById(aprovedOffer.OfferId);
                if(completedOrder != null)
                {
                    completedOrder.DateOfRecieved = dateTime;
                    _unitOfWork.Complete();
                    return Ok(new { Message = "Updated", aprovedOffer });
                }
                              
            }
            return StatusCode(500, "Some thing Error Can Not Udpate The offer");

        }

        //------------------------------------------------------------------------------------------------------//


        /*Update Aproved Offer with Update of Completed Offer By Design order Number  */
        [HttpPut("AprovedOfferByDesignOrder")]
       [Authorize(Roles = "Client")]
        public async Task<IActionResult> UpdateAprovedOffer(AprrovedOfferUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           int OfferId = await _unitOfWork.TailorOffers.WhereOne(i => (i.DesignOrderNumber == model.DesignOrderNumber) && (i.Status.ToUpper()=="A") , i => i.OfferId);
            if(OfferId == 0)
                return NotFound(new { Message = "Offer Not Exist" });
            AprovedOffer? aprovedOffer = await _unitOfWork.AprovedOffers.GetById(OfferId);
            if (aprovedOffer == null)
                return NotFound(new { Message = "Offer Not Exist" });
            if (aprovedOffer.DateOfRecievedOrder <= DateTime.Now)
                return BadRequest(new { Message = "this is Completed Offer Can Not update" });
            if (model.DateOfRecievedFabric <= aprovedOffer.DateOfRecievedFabric)
                return BadRequest(new { Message = " Only Can Upadte the Date of Recieved Fabric to new one After the Current Date" });
            aprovedOffer.DateOfRecievedFabric = model.DateOfRecievedFabric;
            DateTime dateTime = model.DateOfRecievedFabric.
                AddDays(await _unitOfWork.TailorOffers.WhereOne(i => i.OfferId == OfferId, i => i.NumberOfDays));
            aprovedOffer.DateOfRecievedOrder = dateTime;
            aprovedOffer.RecievedMethod = model.RecievedMethod;
            int status = _unitOfWork.Complete();
            if (status > 0)
            {
                CompletedOrder? completedOrder = await _unitOfWork.CompletedOrders.GetById(aprovedOffer.OfferId);
                if (completedOrder != null)
                {
                    completedOrder.DateOfRecieved = dateTime;
                    _unitOfWork.Complete();
                    return Ok(new { Message = "Updated", aprovedOffer });
                }

            }
            return StatusCode(500, "Some thing Error Can Not Update The offer");

        }

    }
}
