using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using graduation_project.Models;
using graduation_project.Data;

namespace graduation_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesginersController : ControllerBase
    {
        public FashionDesignContext db;
        public DesginersController(FashionDesignContext db)
        {
            this.db = db;
        }
        [HttpGet]
        public ActionResult GetDesginers()
        {
            return Ok(db.Designers.ToList());
        }
        //**********************Get Stores by Name*******************//
        [HttpGet("EachDesginer/{name}")]
        public ActionResult getbyname(string name)
        {
            Designer _Desginer = db.Designers.Where(T => T.UserName == name).FirstOrDefault();
            _Desginer.UserNameNavigation = db.Users.FirstOrDefault(u => u.UserName.Contains(_Desginer.UserName));
            _Desginer.EmailNavigation = db.Accounts.FirstOrDefault(u => u.Email.Contains(_Desginer.Email));

            if (_Desginer == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(_Desginer);
            }
        }
    }
}
