using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using graduation_project.Models;
using graduation_project.Data;

namespace graduation_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public FashionDesignContext db;
        public AdminController(FashionDesignContext db)
        {
            this.db = db;
        }
        ///***********************GET CLOTHES***********************/
        //[HttpGet]
        //public ActionResult GetClothes()
        //{
        //    return Ok(db.Clothes.ToList());
        //}
        /***********************ADD ADMIN*************************/
        [HttpPost]
        public ActionResult AddAdmin(AdminOwner admin)
        {
            if (admin != null)
            {
                db.AdminOwners.Add(admin);
                db.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        /***********************Delete CLOTHES*************************/
        [HttpDelete]
        public ActionResult DeleteClothe(string username)
        {
            AdminOwner admin = db.AdminOwners.Where(a => a.UserName == username).FirstOrDefault();
            if (admin != null)
            {
                db.AdminOwners.Remove(admin);
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
