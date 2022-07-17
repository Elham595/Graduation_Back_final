using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using graduation_project.Models;
using graduation_project.Data;

namespace graduation_project.Controllers
{
   // [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        public FashionDesignContext db;
        public ColorsController(FashionDesignContext db)
        {
            this.db = db;
        }
        /***********************GET COLORS*************************/
        [AllowAnonymous]
        [HttpGet]

        public ActionResult GetColors()
        {
            return Ok(db.Colors.ToList());

        }
        /***********************ADD COLORS*************************/
        [HttpPost]
        public ActionResult AddColors(Color color)
        {
            if(color!= null)
            {
                db.Colors.Add(color);
                db.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
        /***********************DELETE COLORS*************************/
        [HttpDelete]
        public ActionResult DeleteColors(int id)
        {
            Color color = db.Colors.Where(a => a.ColorId == id).FirstOrDefault();
            if (color != null)
            {
                db.Colors.Remove(color);
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
