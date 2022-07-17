using graduation_project.Data;
using graduation_project.Models;
using graduation_project.Const;
using graduation_project.NonDomainModels;
using graduation_project.AutoMapperConfig;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;

namespace graduation_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignOrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly FashionDesignContext _context;
        

        public DesignOrderController(IUnitOfWork unitOfWork , IMapper mapper , FashionDesignContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }

        /* Get All Design Order For only Admin with pagination Between Two Dates order by date Desc */
        [HttpGet("DesignOrderAllBetWeenDates")]
       [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllDesigOrder(int page, DateTime start , DateTime end)
        {
            PagesInformation pagesInformation = await _unitOfWork.DesignOrders.GetPageInformation(i => (i.OrderDate >=start) && (i.OrderDate<=end));
            Expression<Func<DesignOrder, object>>[] Includes = { i => i.BottomMeasurement, i => i.TopMeasurment, i => i.DesignOrderFabrics, i => i.Design };
            IEnumerable<DesignOrder> designorders = await _unitOfWork.DesignOrders.GetAllPagination(page, Paginations.NumberOfItems, i => (i.OrderDate >= start) && (i.OrderDate <= end), Includes, i => i.OrderDate, OrderByValues.Descending);
            if (designorders.Count() > 0)
                return Ok(new { Page = page, pagesInformation.TotalPages, pagesInformation.TotalItems, ImageStaticPath = Path.Combine(StaticPath.DesignPath), Result = designorders });
            return NotFound(new { Message = "Page Not Found" });

        }


        //--------------------------------------------------------------------------------------------------//
        /* Get All Design Order For only Admin with pagination in Specific date order by date Desc */
        [HttpGet("DesignOrderAllByDate")]
         [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAllDesigOrder(int page , DateTime date)
        {
            PagesInformation pagesInformation = await _unitOfWork.DesignOrders.GetPageInformation(i=>i.OrderDate==date);
            Expression<Func<DesignOrder, object>>[] Includes = { i => i.BottomMeasurement, i => i.TopMeasurment, i => i.DesignOrderFabrics, i => i.Design };
            IEnumerable<DesignOrder> designorders = await _unitOfWork.DesignOrders.GetAllPagination(page, Paginations.NumberOfItems, i=>i.OrderDate==date, Includes, i => i.OrderDate, OrderByValues.Descending);
            if (designorders.Count() > 0)
                return Ok(new { Page = page, pagesInformation.TotalPages, pagesInformation.TotalItems , ImageStaticPath = Path.Combine(StaticPath.DesignPath), Result = designorders });
            return NotFound(new { Message = "Page Not Found" });

        }


        //---------------------------------------------------------------------------------------------//
        /* Get All Design Order For only Admin with pagination order by date Desc */
        [HttpGet("DesignOrderAll")]
        [Authorize(Roles ="Admin")]

        public async Task<IActionResult> GetAllDesigOrder(int page)
        {
            PagesInformation pagesInformation = await _unitOfWork.DesignOrders.GetPageInformation();
            Expression<Func<DesignOrder, object>>[] Includes = { i => i.BottomMeasurement, i => i.TopMeasurment, i => i.DesignOrderFabrics, i => i.Design };
            IEnumerable<DesignOrder> designorders = await _unitOfWork.DesignOrders.GetAllPagination(page, Paginations.NumberOfItems, Includes, i => i.OrderDate, OrderByValues.Descending);
            if (designorders.Count() > 0)
                return Ok(new { Page = page, pagesInformation.TotalPages, pagesInformation.TotalItems, ImageStaticPath = Path.Combine(StaticPath.DesignPath), Result = designorders });
            return NotFound(new { Message = "Page Not Found" });

        }

        //------------------------------------------------------------------------------------//

        /* Get Design orders For Client and Admin By UserName */
        [HttpGet("DesignOrderByUserName")]
       // [Authorize(Roles ="Client,Admin")]
        public async Task<IActionResult> GetDesignOrderByUserName(string UserName , int page)
        {
            PagesInformation pagesInformation = await _unitOfWork.DesignOrders.GetPageInformation(i => i.UserName == UserName);
            Expression<Func<DesignOrder, object>>[] Includes = { i => i.BottomMeasurement, i => i.TopMeasurment, i => i.DesignOrderFabrics, i => i.Design  };
            IEnumerable<DesignOrder> designorders = await _unitOfWork.DesignOrders.GetAllPagination(page, Paginations.NumberOfItems, i => i.UserName == UserName, Includes ,i => i.OrderDate, OrderByValues.Descending);
            if (designorders.Count() > 0)
                return Ok(new { Page = page, pagesInformation.TotalPages, pagesInformation.TotalItems, ImageStaticPath= Path.Combine(StaticPath.DesignPath)  , Result = designorders });
            return NotFound(new {Message= "Page Not Found"});
        }

        //----------------------------------------------------------------------------------------//
        /* Get Desogn Order Details by Design order Number For Client or Tailor or Admin*/
        [HttpGet("DesignOrderByNumber")]
      //  [Authorize(Roles ="Client,Tailor,Admin")]
        public async Task<IActionResult> GetDesignOrderByNumber(int designOrderNumber)
        {
            Expression<Func<DesignOrder, object>>[] includes = {i => i.Design, i => i.BottomMeasurement , i=>i.TopMeasurment, i=>i.DesignOrderFabrics };
          DesignOrder? designOrder=  await _unitOfWork.DesignOrders.FindOne(i => i.DesignOrderNumber == designOrderNumber, includes);

            if (designOrder is null)
                return NotFound(new { Message = "Can Not Found The Design Order" });
            
            Clothe? Clothe = await _unitOfWork.Clothes.GetById(designOrder.Design.ClothId.Value);

            designOrder.Design.DesignImage = _unitOfWork.DesignOrders.GetImagePath("Design", designOrder.Design.DesignImage);


            return Ok(new { ClotheName = Clothe.ClotheName, DesignOrder = designOrder });
               
            
        }

        //---------------------------------------------------------------------------------------//
        /*Create Design order For Client*/
        [HttpPost("CreateDesignOrder")]
      //  [Authorize(Roles="Client")]
        public async Task<IActionResult> CreateDesignOrder(DesignOrderCreationModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DesignOrder designOrder = _mapper.Map<DesignOrder>(model);

            int status = await _unitOfWork.DesignOrders.Add(designOrder);

            if(status>0)
            {
                model.DesignOrderNumber = designOrder.DesignOrderNumber;
                if(model.Measurment.ToUpper().Contains('B'))
                {
                    BottomMeasurement bottomMeasurement = _mapper.Map<BottomMeasurement>(model);
                    await _unitOfWork.BottomMeasurments.Add(bottomMeasurement);
                }
                if (model.Measurment.ToUpper().Contains('T'))
                {
                    TopMeasurment topMeasurment = _mapper.Map<TopMeasurment>(model);
                    await _unitOfWork.TopMeasurments.Add(topMeasurment);
                }
                foreach(var OrderFabric in model.designFabricModels)
                {
                    DesignOrderFabric designOrderFabric = _mapper.Map<DesignOrderFabric>(OrderFabric);
                    designOrderFabric.DesignOrderNumber = designOrder.DesignOrderNumber;
                    await _unitOfWork.DesignOrderFabrics.Add(designOrderFabric);
                }

                return Created("DesignOrderDetails", new { Message = "Succeed", designOrder });

            }

            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Can Not Create A Design Order" });


        }

        //-----------------------------------------------------------------------------------------//

        /*Update Design order For Client if it Not Has An offer*/
        [HttpPut("UpdateDesignOrder")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> UpdateDesignOrder(DesignOrderCreationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            DesignOrder? designOrder;
            try
            {
                var Includes = new Expression<Func<DesignOrder, object>>[] { i => i.TailorOffers };
                designOrder = await _unitOfWork.DesignOrders.FindOne(i => i.DesignOrderNumber == model.DesignOrderNumber, Includes);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            if (designOrder is null)
                return NotFound();
            if (designOrder.TailorOffers.Count() > 0)
                return BadRequest(new { Message = "Can not Update Design order that Has Offer From Tailor" });


            if (model.Measurment.ToUpper().Contains('B'))
            {
                BottomMeasurement bottomMeasurement = _mapper.Map<BottomMeasurement>(model);
                await _unitOfWork.BottomMeasurments.Update(bottomMeasurement);
            }
            if (model.Measurment.ToUpper().Contains('T'))
            {
                TopMeasurment topMeasurment = _mapper.Map<TopMeasurment>(model);
                await _unitOfWork.TopMeasurments.Update(topMeasurment);
            }
            if (model.designFabricModels.Length > 0)

                await _unitOfWork.DesignOrderFabrics.RemoveRange(i => i.DesignOrderNumber == model.DesignOrderNumber);


            foreach (var OrderFabric in model.designFabricModels)
            {
                DesignOrderFabric designOrderFabric = _mapper.Map<DesignOrderFabric>(OrderFabric);
                designOrderFabric.DesignOrderNumber = model.DesignOrderNumber.Value;
                await _unitOfWork.DesignOrderFabrics.Add(designOrderFabric);
            }

            return Ok(new { Message = "Updated", designOrder });



            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Can Not Create A Design Order" });


        }
        //-------------------------------------------------------------------------------------//

        /*Delete Design order For Client if it Not Has An offer*/
        [HttpDelete("DeleteDesignOrder")]
        [Authorize(Roles = "Client,Admin")]
        public async Task<IActionResult> DeleteDesignOrder(int DesignOrderNumber)
        {

            DesignOrder? designOrder;
            try
            {
                var Includes = new Expression<Func<DesignOrder, object>>[] { i => i.TailorOffers };
                designOrder = await _unitOfWork.DesignOrders.FindOne(i => i.DesignOrderNumber == DesignOrderNumber, Includes);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            if (designOrder is null)
                return NotFound();
            if (designOrder.TailorOffers.Count() > 0)
                return BadRequest(new { Message = "Can not Delete Design order that Has Offer From Tailor" });
            int index = await _unitOfWork.DesignOrders.Remove(designOrder);
            if (index > 0)

                return Ok(new { Message = "Deleted", designOrder });



            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Can Not Create A Design Order" });


        }
    }
}
