using graduation_project.Data;
using graduation_project.Models;
using graduation_project.NonDomainModels;
using graduation_project.Const;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace graduation_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompletedOrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompletedOrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }

        [HttpGet("CompletedOfferDetailsByUserName/All")]
         [Authorize(Roles = "Client,Admin")]
        public async Task<IActionResult> GetCompletedOfferDetails(int page, string UserName , string Role)
        {
            PagesInformation pagesInformation=new PagesInformation();
            if (Role.ToLower()=="client")
            {
                pagesInformation = await _unitOfWork.AprovedOffers.GetPageInformation(i => (i.Offer.ClientUserName==UserName)&& (i.DateOfRecievedOrder<=DateTime.Now));
            }

            else if(Role.ToLower()=="tailor")
            {
                pagesInformation = await _unitOfWork.AprovedOffers.GetPageInformation(i => (i.Offer.TailorUserName == UserName) && (i.DateOfRecievedOrder <= DateTime.Now));
            }


            var list = await _unitOfWork.DbAprovedOffers.GetAllCompletedOffersByUserName(page, UserName,Role);
            if (list.Any())
                return Ok(new { Page = page, TotalPages = pagesInformation.TotalPages, TotalItems = pagesInformation.TotalItems, Result = list });
            return NotFound(new { Message = "Page Not Exist" });

        }
        //------------------------------------------------------------------------------//

        /*Get All Completed Offer with details*/
        [HttpGet("CompletedOfferDetails/All")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCompletedOfferDetails(int page ,bool Rate)
        {
            PagesInformation pagesInformation;
            if (Rate)
            {
                 pagesInformation =  await _unitOfWork.CompletedOrders.GetPageInformation(i => (i.DateOfRecieved <= DateTime.Now) && (i.Rating.HasValue));
            }

            else
            {
                 pagesInformation = await _unitOfWork.CompletedOrders.GetPageInformation(i => i.DateOfRecieved <= DateTime.Now);
            }


            var list = await _unitOfWork.DbAprovedOffers.GetAllCompletedOffers(page, Rate);
            if (list.Any())
                return Ok(new { Page = page, TotalPages = pagesInformation.TotalPages, TotalItems = pagesInformation.TotalItems, Result = list });
            return NotFound(new { Message = "Page Not Exist" });

        }


        //-------------------------------------------------------------------------------------------//

        /*Get All Completed Offer*/
        [HttpGet("CompletedOffer/All")]
       [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCompletedOffer(int page)
        {
           PagesInformation pagesInformation= await _unitOfWork.CompletedOrders.GetPageInformation();
           var list= await  _unitOfWork.CompletedOrders.GetAllPagination(page, Paginations.NumberOfItems, i => i.DateOfRecieved, OrderByValues.Descending);
            if (list.Any())
                return Ok(new { Page = page, TotalPages = pagesInformation.TotalPages, TotalItems = pagesInformation.TotalItems, Result = list });
            return NotFound(new { Message = "Page Not Exist" });
        }
        //----------------------------------------------------------------------------------//

        /*Get All Completed Offer With Rate*/
        [HttpGet("CompletedOffer/AllRating")]
         [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCompletedOfferRating(int page)
        {
            PagesInformation pagesInformation = await _unitOfWork.CompletedOrders.GetPageInformation(i => i.Rating.HasValue);
            var list = await _unitOfWork.CompletedOrders.GetAllPagination(page, Paginations.NumberOfItems,i=>i.Rating.HasValue, i => i.DateOfRecieved, OrderByValues.Descending);
            if (list.Any())
                return Ok(new { Page = page, TotalPages = pagesInformation.TotalPages, TotalItems = pagesInformation.TotalItems, Result = list });
            return NotFound(new { Message = "Page Not Exist" });
        }

        //--------------------------------------------------------------------------------------//

        /*Get All Completed Offer With Rate*/
        [HttpGet("CompletedOffer/AllCompletedRate")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCompletedOfferRatingDate(int page)
        {
            PagesInformation pagesInformation = await _unitOfWork.CompletedOrders.GetPageInformation(i => (i.DateOfRecieved <= DateTime.Now) && (i.Rating.HasValue));
            var list = await _unitOfWork.CompletedOrders.GetAllPagination(page, Paginations.NumberOfItems, i => (i.DateOfRecieved <= DateTime.Now) && (i.Rating.HasValue), i => i.DateOfRecieved, OrderByValues.Descending);
            if (list.Any())
                return Ok(new { Page = page, TotalPages = pagesInformation.TotalPages, TotalItems = pagesInformation.TotalItems, Result = list });
            return NotFound(new { Message = "Page Not Exist" });
        }

        //-------------------------------------------------------------------------------------//
        /*Get All Completed Offer*/
        [HttpGet("CompletedOffer/AllCompleted")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCompletedOfferDate(int page)
        {
            PagesInformation pagesInformation = await _unitOfWork.CompletedOrders.GetPageInformation(i => i.DateOfRecieved <= DateTime.Now);
            var list = await _unitOfWork.CompletedOrders.GetAllPagination(page, Paginations.NumberOfItems, i => i.DateOfRecieved <= DateTime.Now, i => i.DateOfRecieved, OrderByValues.Descending);
            if (list.Any())
                return Ok(new { Page = page, TotalPages = pagesInformation.TotalPages, TotalItems = pagesInformation.TotalItems, Result = list });
            return NotFound(new { Message = "Page Not Exist" });
        }



        //----------------------------------------------------------------------------------------//

        /*Completed Offer By Client*/
        [HttpPut("CompleteOrder")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> CompleteOrder(float Rate, int OfferId)
        {
            if (Rate < 0 || Rate > 5)
                return BadRequest(new { Message = "Rate Must Be From 0 To 5" });
            CompletedOrder? completedOrder = await _unitOfWork.CompletedOrders.GetById(OfferId);
            if (completedOrder is null)
                return NotFound(new { Message = " Can not Found The Offer" });
            if (completedOrder.DateOfRecieved > DateTime.Now)
                return BadRequest(new { Message = "Date of Recieved Not Now " });
            if (completedOrder.Rating.HasValue)
                return BadRequest(new { Message = "Offer Already has A Rate" });
            completedOrder.Rating = Rate;
            await _unitOfWork.CompletedOrders.Update(completedOrder);

            var Tailor = await _unitOfWork.TailorOffers.
                FindOne(i => i.OfferId == OfferId, new Expression<Func<TailorOffer, object>>[] { i => i.TailorUserNameNavigation });
            if (Tailor is not null)
            {
                var AvgRate = await _unitOfWork.DbAprovedOffers.CalculateRate(Tailor.TailorUserName);
                Rating? RateAccount = await _unitOfWork.Rates.WhereOne(i => i.Email == Tailor.TailorUserNameNavigation.Email);
                if (RateAccount is null)
                {
                    Rating newOne = new Rating() { Email = Tailor.TailorUserNameNavigation.Email, RateNumber = AvgRate.Value };
                    await _unitOfWork.Rates.Add(newOne);
                }
                else
                {
                    RateAccount.RateNumber = AvgRate.Value;
                    await _unitOfWork.Rates.Update(RateAccount);
                }
                return Ok(new { Message = "Order Completed ", OrderRate = Rate });
            }
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "error During Connection" });

        }

    }
}
