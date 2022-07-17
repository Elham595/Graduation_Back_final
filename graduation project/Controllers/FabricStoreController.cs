using graduation_project.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using graduation_project.Models;

namespace graduation_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FabricStoreController : ControllerBase
    {
        public FashionDesignContext db;
        public FabricStoreController(FashionDesignContext db)
        {
            this.db = db;
        }
        //**********************Get Fabrics of each store by store ID*******************//
        [HttpGet("{id}")]
        public ActionResult getbyid(int id)
        {
            List<FabricOfStore> _Fabricofstore = new List<FabricOfStore>();
            _Fabricofstore = db.FabricOfStores.Where(f => f.StoreId == id).ToList();
            return Ok(_Fabricofstore);

        }
    }
}
