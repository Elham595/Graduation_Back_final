using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using graduation_project.Models;
using graduation_project.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using graduation_project.Service;

namespace graduation_project.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClotheController : ControllerBase
    {
        public FashionDesignContext db;
        public ClotheController(FashionDesignContext db)
        {
            this.db = db;
        }
        /***********************GET CLOTHES***********************/
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetClothes()
        {
            var model = db.Clothes.Select(c => new
            {
                ClotheId = c.ClotheId,
                ClotheName = c.ClotheName
            });
            return Ok(model);
        }
        /***********************ADD CLOTHES*************************/
        [HttpPost]
        public ActionResult AddClothe(Clothe clo)
        {
            if (clo != null)
            {
                db.Clothes.Add(clo);
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
        public ActionResult DeleteClothe(int id)
        {
            Clothe clothe = db.Clothes.Where(a => a.ClotheId == id).FirstOrDefault();
            if (clothe != null)
            {
                db.Clothes.Remove(clothe);
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
