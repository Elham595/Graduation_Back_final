using AutoMapper;
using graduation_project.Const;
using graduation_project.Models;
using graduation_project.NonDomainModels;
using graduation_project.Repository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;

namespace graduation_project.Data
{
    public class DbContextRepositoryServices<TEntity> : BaseRepositoryServices<TEntity, DbContext>
        where TEntity : class
    {
        private readonly FashionDesignContext _context;

        public DbContextRepositoryServices(FashionDesignContext tdbcontext) : base(tdbcontext)
        {
            _context = tdbcontext;
            
        }
        // public async Task<IQueryable<object>> GetAllTailorOfferWithFabricMeter()
        // {
        //     _context.DesignOrders.Include(i => i.BottomMeasurement).Include(i => i.TopMeasurment)
        //         .Include(i=>i.DesignOrderFabrics)
        //         .ThenInclude(i=>i.Color)
        //         .Include(i=>i.DesignOrderFabrics)
        //         .ThenInclude(i=>i.Fabric)
        // }
        public IQueryable<object> CompletedorderQuery(string UserName , string Role)
        {
            if (Role.ToLower()=="client")
            {
                var query = from TOF in _context.TailorOffers
                            join AO in _context.AprovedOffers
                            on TOF.OfferId equals AO.OfferId
                            join COM in _context.CompletedOrders
                            on AO.OfferId equals COM.OfferId
                            join DO in _context.DesignOrders
                            on TOF.DesignOrderNumber equals DO.DesignOrderNumber
                            join BO in _context.BottomMeasurements
                            on DO.DesignOrderNumber equals BO.DesignOrderNumber into BottomDesignMeasurement
                            from DOBO in BottomDesignMeasurement.DefaultIfEmpty()
                            join TO in _context.TopMeasurments
                            on DO.DesignOrderNumber equals TO.DesignOrderNumber into TopDesignMeasurement
                            from DOTO in TopDesignMeasurement.DefaultIfEmpty()
                            join D in _context.Designs
                            on DO.DesignId equals D.DesignId
                            join U in _context.Users
                            on TOF.TailorUserName equals U.UserName
                            where 
                            COM.DateOfRecieved <= DateTime.Now
                            &&
                            TOF.ClientUserName==UserName
                            orderby COM.DateOfRecieved descending
                            select new
                            {
                                TOF.OfferId,
                                TOF.NumberOfDays,
                                TOF.Status,
                                TOF.Price,
                                TOF.OfferDate,
                                U.FirstName,
                                U.MiddleName,
                                U.LastName,
                                AO.DateOfAproval,
                                AO.DateOfRecievedFabric,
                                AO.DateOfRecievedOrder,
                                Design = D,
                                Bottom = DOBO,
                                Top = DOTO,
                                

                            };
                return query;
            }
            else
            {

                var query = from TOF in _context.TailorOffers
                            join AO in _context.AprovedOffers
                            on TOF.OfferId equals AO.OfferId
                            join COM in _context.CompletedOrders
                            on AO.OfferId equals COM.OfferId
                            join DO in _context.DesignOrders
                            on TOF.DesignOrderNumber equals DO.DesignOrderNumber
                            join BO in _context.BottomMeasurements
                            on DO.DesignOrderNumber equals BO.DesignOrderNumber into BottomDesignMeasurement
                            from DOBO in BottomDesignMeasurement.DefaultIfEmpty()
                            join TO in _context.TopMeasurments
                            on DO.DesignOrderNumber equals TO.DesignOrderNumber into TopDesignMeasurement
                            from DOTO in TopDesignMeasurement.DefaultIfEmpty()
                            join D in _context.Designs
                            on DO.DesignId equals D.DesignId
                            join U in _context.Users
                            on TOF.ClientUserName equals U.UserName
                            where
                            COM.DateOfRecieved <= DateTime.Now
                            &&
                            TOF.TailorUserName == UserName
                            orderby COM.DateOfRecieved descending
                            select new
                            {
                                TOF.OfferId,
                                TOF.NumberOfDays,
                                TOF.Status,
                                TOF.Price,
                                TOF.OfferDate,
                                U.FirstName,
                                U.MiddleName,
                                U.LastName,
                                AO.DateOfAproval,
                                AO.DateOfRecievedFabric,
                                AO.DateOfRecievedOrder,
                                Design = D,
                                Bottom = DOBO,
                                Top = DOTO

                            };
                return query;

            }
        }
        public IQueryable<object> CompletedorderQuery(bool Rate)
        {
            if(Rate)
            {
                var query = from TOF in _context.TailorOffers
                            join AO in _context.AprovedOffers
                            on TOF.OfferId equals AO.OfferId
                            join COM in _context.CompletedOrders
                            on AO.OfferId equals COM.OfferId
                            join DO in _context.DesignOrders
                            on TOF.DesignOrderNumber equals DO.DesignOrderNumber
                            join BO in _context.BottomMeasurements
                            on DO.DesignOrderNumber equals BO.DesignOrderNumber into BottomDesignMeasurement
                            from DOBO in BottomDesignMeasurement.DefaultIfEmpty()
                            join TO in _context.TopMeasurments
                            on DO.DesignOrderNumber equals TO.DesignOrderNumber into TopDesignMeasurement
                            from DOTO in TopDesignMeasurement.DefaultIfEmpty()
                            join D in _context.Designs
                            on DO.DesignId equals D.DesignId
                            join U in _context.Users
                            on TOF.TailorUserName equals U.UserName
                            where COM.Rating.HasValue &&  COM.DateOfRecieved<= DateTime.Now
                            orderby COM.DateOfRecieved descending
                            select new
                            {
                                TOF.OfferId,
                                TOF.NumberOfDays,
                                TOF.Status,
                                TOF.Price,
                                TOF.OfferDate,
                                U.FirstName,
                                U.MiddleName,
                                U.LastName,
                                AO.DateOfAproval,
                                AO.DateOfRecievedFabric,
                                AO.DateOfRecievedOrder,
                                Design = D,
                                Bottom = DOBO,
                                Top = DOTO

                            };
                return query;
            }
            else {

                var query = from TOF in _context.TailorOffers
                            join AO in _context.AprovedOffers
                            on TOF.OfferId equals AO.OfferId
                            join COM in _context.CompletedOrders
                            on AO.OfferId equals COM.OfferId
                            join DO in _context.DesignOrders
                            on TOF.DesignOrderNumber equals DO.DesignOrderNumber
                            join BO in _context.BottomMeasurements
                            on DO.DesignOrderNumber equals BO.DesignOrderNumber into BottomDesignMeasurement
                            from DOBO in BottomDesignMeasurement.DefaultIfEmpty()
                            join TO in _context.TopMeasurments
                            on DO.DesignOrderNumber equals TO.DesignOrderNumber into TopDesignMeasurement
                            from DOTO in TopDesignMeasurement.DefaultIfEmpty()
                            join D in _context.Designs
                            on DO.DesignId equals D.DesignId
                            join U in _context.Users
                            on TOF.TailorUserName equals U.UserName
                            where  COM.DateOfRecieved <= DateTime.Now
                            orderby COM.DateOfRecieved descending
                            select new
                            {
                                TOF.OfferId,
                                TOF.NumberOfDays,
                                TOF.Status,
                                TOF.Price,
                                TOF.OfferDate,
                                U.FirstName,
                                U.MiddleName,
                                U.LastName,
                                AO.DateOfAproval,
                                AO.DateOfRecievedFabric,
                                AO.DateOfRecievedOrder,
                                Design = D,
                                Bottom = DOBO,
                                Top = DOTO

                            };
                return query;

            }
        }

        public async Task<IEnumerable<object>> GetAllCompletedOffers(int page ,bool Rate)
        {
            var query = CompletedorderQuery(Rate);

            var list = await GetAllPagination(query, page, Paginations.NumberOfItems);

            return list;

        }

        public async Task<IEnumerable<object>> GetAllCompletedOffersByUserName(int page, string UserName , string Role)
        {
            var query = CompletedorderQuery(UserName,Role);

            var list = await GetAllPagination(query, page, Paginations.NumberOfItems);

            return list;
        }
        public IQueryable<object> AprovedOffersQuery(string UserName, string role)
        {
            if (role.ToLower() == "client")
            {
                var query = from TOF in _context.TailorOffers
                            join AO in _context.AprovedOffers
                            on TOF.OfferId equals AO.OfferId
                            join DO in _context.DesignOrders
                            on TOF.DesignOrderNumber equals DO.DesignOrderNumber
                            join BO in _context.BottomMeasurements
                            on DO.DesignOrderNumber equals BO.DesignOrderNumber into BottomDesignMeasurement
                            from DOBO in BottomDesignMeasurement.DefaultIfEmpty()
                            join TO in _context.TopMeasurments
                            on DO.DesignOrderNumber equals TO.DesignOrderNumber into TopDesignMeasurement
                            from DOTO in TopDesignMeasurement.DefaultIfEmpty()
                            join D in _context.Designs
                            on DO.DesignId equals D.DesignId
                            join U in _context.Users
                            on TOF.TailorUserName equals U.UserName
                            where TOF.Status == "A" && TOF.ClientUserName == UserName
                            orderby AO.DateOfAproval descending
                            select new
                            {
                                TOF.OfferId,
                                TOF.NumberOfDays,
                                TOF.Status,
                                TOF.Price,
                                TOF.OfferDate,
                                U.FirstName,
                                U.MiddleName,
                                U.LastName,
                                AO.DateOfAproval,
                                AO.DateOfRecievedFabric,
                                AO.DateOfRecievedOrder,
                                Design = D,
                                Bottom = DOBO,
                                Top = DOTO

                            };
                return query;
            }
            else if (role.ToLower()=="tailor")
            {
                var query = from TOF in _context.TailorOffers
                            join AO in _context.AprovedOffers
                            on TOF.OfferId equals AO.OfferId
                            join DO in _context.DesignOrders
                            on TOF.DesignOrderNumber equals DO.DesignOrderNumber
                            join BO in _context.BottomMeasurements
                            on DO.DesignOrderNumber equals BO.DesignOrderNumber into BottomDesignMeasurement
                            from DOBO in BottomDesignMeasurement.DefaultIfEmpty()
                            join TO in _context.TopMeasurments
                            on DO.DesignOrderNumber equals TO.DesignOrderNumber into TopDesignMeasurement
                            from DOTO in TopDesignMeasurement.DefaultIfEmpty()
                            join D in _context.Designs
                            on DO.DesignId equals D.DesignId
                            join U in _context.Users
                            on TOF.ClientUserName equals U.UserName
                            where TOF.Status == "A" && TOF.TailorUserName == UserName
                            orderby AO.DateOfAproval descending
                            select new
                            {
                                TOF.OfferId,
                                TOF.NumberOfDays,
                                TOF.Status,
                                TOF.Price,
                                TOF.OfferDate,
                                TOF.ClientUserName,
                                U.FirstName,
                                U.MiddleName,
                                U.LastName,
                                AO.DateOfAproval,
                                AO.DateOfRecievedFabric,
                                AO.DateOfRecievedOrder,
                                Design = D,
                                Bottom = DOBO,
                                Top = DOTO

                            };
                return query;
            }
            else 
            {
                var query = from TOF in _context.TailorOffers
                            join AO in _context.AprovedOffers
                            on TOF.OfferId equals AO.OfferId
                            join DO in _context.DesignOrders
                            on TOF.DesignOrderNumber equals DO.DesignOrderNumber
                            join BO in _context.BottomMeasurements
                            on DO.DesignOrderNumber equals BO.DesignOrderNumber into BottomDesignMeasurement
                            from DOBO in BottomDesignMeasurement.DefaultIfEmpty()
                            join TO in _context.TopMeasurments
                            on DO.DesignOrderNumber equals TO.DesignOrderNumber into TopDesignMeasurement
                            from DOTO in TopDesignMeasurement.DefaultIfEmpty()
                            join D in _context.Designs
                            on DO.DesignId equals D.DesignId
            
                            where TOF.Status == "A" 
                            orderby AO.DateOfAproval descending
                            select new
                            {
                                TOF.OfferId,
                                TOF.NumberOfDays,
                                TOF.Status,
                                TOF.Price,
                                TOF.OfferDate,
                                TOF.ClientUserName,
                                TOF.TailorUserName,
                                AO.DateOfAproval,
                                AO.DateOfRecievedFabric,
                                AO.DateOfRecievedOrder,
                                Design = D,
                                Bottom = DOBO,
                                Top = DOTO

                            };
                return query;
            }
        }
        /*Get Aproved Offers Details For  Client or Tailor With Pagination*/ 
        public async Task<IEnumerable<object>> GetAllAprovedOffers(string UserName ,int page , string role)
        {
           var query = AprovedOffersQuery(UserName, role);
            
                var list = await GetAllPagination(query, page, Paginations.NumberOfItems);

                return list;

        }


        public async Task<double?>  CalculateRate(string TailorUserName)
        {
            var AvgRate = await (from TOF in _context.TailorOffers
                       join COM in _context.CompletedOrders
                       on TOF.OfferId equals COM.OfferId
                       where TOF.TailorUserName == TailorUserName && COM.Rating !=null
                       select COM.Rating).AverageAsync();
            return AvgRate;
        }
    }
}
