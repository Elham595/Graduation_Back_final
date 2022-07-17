using graduation_project.Models;
using graduation_project.Repository;

namespace graduation_project.Data
{
    public class UnitOfWork :  IUnitOfWork 
        
    {
       public  IBaseRepositoryServices<TailorOffer> TailorOffers { get; set; }
        public IBaseRepositoryServices<Design> Designs { get; set; }
        public IBaseRepositoryServices<Clothe> Clothes { get; set; }

        public IBaseRepositoryServices<Color> Colors { get; set; }

        public IBaseRepositoryServices<Fabric> Fabrics { get; set; }

        public IBaseRepositoryServices<TailorOfferFabricMetter> TailorOfferFabricMeters { get; set; }


        public IBaseRepositoryServices<DesignerOfDesign> DesignerOfDesign { get; set; }

        public IBaseRepositoryServices<DesignOrder> DesignOrders { get; set; }

        public IBaseRepositoryServices<AprovedOffer> AprovedOffers { get; set; }

        public DbContextRepositoryServices<AprovedOffer> DbAprovedOffers { get; set; }


        public IBaseRepositoryServices<CompletedOrder> CompletedOrders { get; set; }

        public IBaseRepositoryServices<TopMeasurment> TopMeasurments { get; set; }

        public IBaseRepositoryServices<BottomMeasurement> BottomMeasurments { get; set; }


        public IBaseRepositoryServices<DesignOrderFabric> DesignOrderFabrics { get; set; }


        public IBaseRepositoryServices<Rating> Rates { get; set; }









        private readonly  FashionDesignContext  _context;


        public UnitOfWork(FashionDesignContext context)
        {
            
            _context = context;
            TailorOffers = new DbContextRepositoryServices<TailorOffer>(_context);
            Designs = new DbContextRepositoryServices<Design>(_context);
            Clothes = new DbContextRepositoryServices<Clothe>(_context);
            DesignerOfDesign= new DbContextRepositoryServices<DesignerOfDesign>(_context);
            Colors = new DbContextRepositoryServices<Color>(_context);
            Fabrics = new DbContextRepositoryServices<Fabric>(context);
            TailorOfferFabricMeters = new DbContextRepositoryServices<TailorOfferFabricMetter>(context);
            DesignOrders = new DbContextRepositoryServices<DesignOrder>(_context);
            AprovedOffers= new DbContextRepositoryServices<AprovedOffer>(_context);
            CompletedOrders = new DbContextRepositoryServices<CompletedOrder>(_context);
            TopMeasurments = new DbContextRepositoryServices<TopMeasurment>(_context);
            BottomMeasurments = new DbContextRepositoryServices<BottomMeasurement>(_context);
            DesignOrderFabrics = new DbContextRepositoryServices<DesignOrderFabric>(_context);
            DbAprovedOffers = new DbContextRepositoryServices<AprovedOffer>(_context);
            Rates = new DbContextRepositoryServices<Rating>(_context);

        }

        public int Complete()
        {
           return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
