using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using graduation_project.Models;
using graduation_project.Data;

namespace graduation_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class TailorsController : ControllerBase
    {
        List<Tailor> _tailors = new List<Tailor>();
        public FashionDesignContext db;
        public TailorsController(FashionDesignContext db)
        {
            this.db = db;
        }
        //**********************Get All Tailors*******************//
        [HttpGet]
        public ActionResult GetTailors()
        {
            return Ok(db.Tailors.ToList());
        }

        //**********************Get Tailor by Name*******************//
        [HttpGet("eachtailor/{name}")]
        public ActionResult getbyname(string name)
        {
            Tailor tailor = db.Tailors.Where(T => T.UserName == name).FirstOrDefault();
            tailor.UserNameNavigation = db.Users.FirstOrDefault(u => u.UserName.Contains(tailor.UserName));
            tailor.EmailNavigation = db.Accounts.FirstOrDefault(u => u.Email.Contains(tailor.Email));

            if (tailor == null)
            {
                return BadRequest();
            }
            else             
            {
                return Ok(tailor);
            }
        }
    }
}

