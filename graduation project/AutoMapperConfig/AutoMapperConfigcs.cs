using AutoMapper;
using graduation_project.Models;
using graduation_project.NonDomainModels;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace graduation_project.AutoMapperConfig
{
    public class AutoMapperConfigcs : Profile
    {
        public AutoMapperConfigcs()
        {
            CreateMap<TailorOfferCreation, TailorOffer>();
            CreateMap< AprovedOfferCreationModel,AprovedOffer> ();
            CreateMap<DesignOrderCreationModel,BottomMeasurement>();
            CreateMap<DesignOrderCreationModel,TopMeasurment>();
            CreateMap<DesignOrderCreationModel,DesignOrder>();
            CreateMap<DesignFabricModel, DesignOrderFabric>();
            CreateMap<DesignOrderFabric, DesignOrderFabric>();




        }
    }
}
