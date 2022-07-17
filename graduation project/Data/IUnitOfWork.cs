using graduation_project.Models;
using graduation_project.Repository;

namespace graduation_project.Data
{
    public interface IUnitOfWork : IDisposable 
    {
        IBaseRepositoryServices<TailorOffer> TailorOffers { get; }
        IBaseRepositoryServices<Design> Designs { get; }
        IBaseRepositoryServices<Clothe> Clothes { get; }

        IBaseRepositoryServices<DesignerOfDesign> DesignerOfDesign { get; }


        IBaseRepositoryServices<Color> Colors { get; }

         IBaseRepositoryServices<Fabric> Fabrics { get; }

         IBaseRepositoryServices<TailorOfferFabricMetter> TailorOfferFabricMeters { get; }



         IBaseRepositoryServices<DesignOrder> DesignOrders { get; }

         IBaseRepositoryServices<AprovedOffer> AprovedOffers { get; }


         IBaseRepositoryServices<CompletedOrder> CompletedOrders { get; }

         IBaseRepositoryServices<TopMeasurment> TopMeasurments { get;  }

         IBaseRepositoryServices<BottomMeasurement> BottomMeasurments { get;  }


         IBaseRepositoryServices<DesignOrderFabric> DesignOrderFabrics { get; }

        IBaseRepositoryServices<Rating> Rates { get;  }


        DbContextRepositoryServices<AprovedOffer> DbAprovedOffers { get; }



        int Complete();
    }  
}
