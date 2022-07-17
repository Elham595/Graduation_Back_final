using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using graduation_project.Models;
using graduation_project.Data;
using Microsoft.AspNetCore.Authorization;

namespace graduation_project.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class FabricController : ControllerBase
    {
        public FashionDesignContext db;
        public FabricController(FashionDesignContext db)
        {
            this.db=db ;
        }
        /***********************GET Fabrics*************************/
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetFabrics()
        {
            return  Ok (db.Fabrics.ToList());
 
        }
        /***********************ADD Fabrics*************************/
        [HttpPost]
        public ActionResult AddFabric(Fabric fab)
        {
            if (fab != null)
            {
                db.Fabrics.Add(fab);
                db.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        /***********************Delete Fabrics*************************/
        [HttpDelete]
        public ActionResult DeleteFabric(int id)
        {
            Fabric fabric = db.Fabrics.Where(a => a.FabricId == id).FirstOrDefault();
            if (fabric!= null)
            {
                db.Fabrics.Remove(fabric);
                db.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

    }
}
