using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using graduation_project.Models;
using graduation_project.Data;

namespace graduation_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        public FashionDesignContext db;
        public StoresController(FashionDesignContext db)
        {
            this.db = db;
        }
        [HttpGet]
        public ActionResult GetStores()
        {
            return Ok(db.Stores.ToList());
        }

        //**********************Get Stores by Name*******************//
        [HttpGet("EachStore/{name}")]
        public ActionResult getbyname(string name)
        {
            Store _store = db.Stores.Where(T => T.UserName == name).FirstOrDefault();
            _store.UserNameNavigation = db.Users.FirstOrDefault(u => u.UserName.Contains(_store.UserName));
            _store.EmailNavigation = db.Accounts.FirstOrDefault(u => u.Email.Contains(_store.Email));

            if (_store == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(_store);
            }
        }
    }
}
