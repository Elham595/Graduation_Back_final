using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using graduation_project.Models;
using graduation_project.Data;
using Newtonsoft.Json.Linq;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using IdentityModel;
using graduation_project.Service;
using graduation_project.Validators;
using graduation_project.DTOs;

namespace graduation_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public IConfiguration _configuration;
        public FashionDesignContext db;
        private readonly JWT _jwt;
        public AccountController(FashionDesignContext db, IOptions<JWT> jwt)
        {
            this.db = db;
            this._jwt = jwt.Value;

        }
        /****************************************************REGISTRATION METHOD*********************************************************/
        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] JObject obj)//JObject 
        {

            /************desining users of the site***************/
            var account = obj["account"].ToObject<Account>();
            var user = obj["user"].ToObject<User>();

            if (user != null && account != null)
            {
                Dictionary<string, string> message = new Dictionary<string, string>();

                if (user != null && account != null)
                {

                    CreateAccountValidator validator = new CreateAccountValidator(db);
                    var result = validator.Validate(obj);

                    if (!result.IsValid)
                        return BadRequest(result.ToDictionary());

                    db.Users.Add(user);
                    db.Accounts.Add(account);

                    var id = account.RoleId;
                    /****************check the role of the user*************/
                    if (id == 1)
                    {
                        var adminowner = obj["adminowner"].ToObject<AdminOwner>();
                        db.AdminOwners.Add(adminowner);
                        message.Add("Status", "Success");
                    }
                    if (id == 2)
                    {
                        var tailor = obj["tailor"].ToObject<Tailor>();
                        db.Tailors.Add(tailor);
                        message.Add("Status", "Success");
                    }
                    if (id == 3)
                    {
                        var designer = obj["designer"].ToObject<Designer>();
                        db.Designers.Add(designer);
                        message.Add("Status", "Success");
                    }
                    if (id == 4)
                    {
                        var client = obj["client"].ToObject<Client>();
                        db.Clients.Add(client);
                        message.Add("Status", "Success");
                    }
                    if (id == 5)
                    {
                        var store = obj["store"].ToObject<Store>();
                        db.Stores.Add(store);
                        await db.SaveChangesAsync();
                        var storeFabrics = obj["StoreFabrics"]?.ToObject<IEnumerable<FabricOfStore>>();
                        storeFabrics = storeFabrics.Select(storeFabric =>
                        {
                            storeFabric.StoreId = store.StoreId;
                            return storeFabric;
                        });
                        await db.FabricOfStores.AddRangeAsync(storeFabrics);
                        message.Add("Status", "Success");
                    }

                    await db.SaveChangesAsync();

                    return Ok(message);
                }

            }
            return BadRequest();

        }
        /****************************************************LOGIN METHOD*********************************************************/

        [HttpPost("signin")]
        public ActionResult LoginAccount(LoginDTO model)
        {
            Account account = db.Accounts.Where(a => a.Email == model.Email && a.Password == model.Password).FirstOrDefault();

            //UserRole role = db.UserRoles.Where(a => a.RoleId == account.RoleId).FirstOrDefault();

            Dictionary<string, string> message = new Dictionary<string, string>();
            if (account != null)
            {

                string username = "";
                UserRole role = db.UserRoles.Where(a => a.RoleId == account.RoleId).FirstOrDefault();
                if (role.RoleName.ToLower() == "client")
                {
                    username = db.Clients.Where(a => a.Email == account.Email).Select(x => x.UserName).FirstOrDefault();
                }
                else if (role.RoleName.ToLower() == "tailor")
                {
                    username = db.Tailors.Where(a => a.Email == account.Email).Select(x => x.UserName).FirstOrDefault();
                }
                else if (role.RoleName.ToLower() == "desginer")
                {
                    username = db.Designers.Where(a => a.Email == account.Email).Select(x => x.UserName).FirstOrDefault();
                }
                else if (role.RoleName.ToLower() == "store")
                {
                    username = db.Stores.Where(a => a.Email == account.Email).Select(x => x.UserName).FirstOrDefault();
                }
                else if (role.RoleName.ToLower() == "admin")
                {
                    username = db.AdminOwners.Where(a => a.Email == account.Email).Select(x => x.UserName).FirstOrDefault();
                }

                var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));//"user_signature_12345678"

                var credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);//securitykey
                /****************store RoleId*********************/
                var data = new List<Claim>();
                data.Add(new Claim(JwtClaimTypes.Role, role.RoleName));
                data.Add(new Claim(JwtClaimTypes.Email, account.Email));
                data.Add(new Claim(JwtClaimTypes.PreferredUserName, username));
                //where we want to read the roleId from the token
                //var identity = User.Identity as ClaimsIdentity;
                //List<Claim> accounts=identity.Claims.ToList();
                //accounts[0].Value;
                var token = new JwtSecurityToken(
                    issuer: _jwt.Issuer,
                    audience: _jwt.Audience,
                    claims: data,
                    expires: DateTime.Now.AddHours(_jwt.DurationInHours),
                    signingCredentials: credentials);
                message.Add("Status", "Success");

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            else
            {
                message.Add("Status", "Email Was Not Found");
                return Unauthorized();
            }
            //return Ok(message);

        }

        /**********************************************UPDATE ACCOUNT& USER METHOD***************************************************/
        [HttpPut]
        public ActionResult UpdateAccountEmail([FromQuery]string email,[FromQuery] string username,JObject obj)
        {
            //NEW DATA
            var user = obj["User"].ToObject<User>();
            var account = obj["Account"].ToObject<Account>();
            //OLD DATA
            Account oldaccount = db.Accounts.Where(a => a.Email == email).FirstOrDefault();
            User olduser = db.Users.Where(a => a.UserName == username).FirstOrDefault();


            if (oldaccount != null && olduser != null)
            {
                //oldaccount.Email = account.Email;
                //olduser.UserName = user.UserName;
                olduser.FirstName = user.FirstName;
                olduser.LastName = user.LastName;
                olduser.MiddleName = user.MiddleName;
                olduser.MobileNumber = user.MobileNumber;
                olduser.BirthDate = user.BirthDate;
                //oldaccount.Email = account.Email;
                //olduser.UserName = user.UserName;
            

                db.SaveChanges();
                int id = oldaccount.RoleId;
                /*********check for the RoleId to update additional data not in the account and user ***********/
                if (id == 2)
                {
                    var tailor = obj["Tailor"].ToObject<Tailor>();
                    Tailor oldtailor = db.Tailors.Where(a => a.UserName == username).FirstOrDefault();
                    oldtailor.City = tailor.City;
                    oldtailor.Address = tailor.Address;
                    oldtailor.ExperienceYears = tailor.ExperienceYears;
                    oldtailor.Bio = tailor.Bio;
                    db.SaveChanges();
                }
                if (id == 3)
                {
                    var designer = obj["Designer"].ToObject<Designer>();
                    Designer oldadesigner = db.Designers.Where(a => a.UserName == username).FirstOrDefault();
                    oldadesigner.City = designer.City;
                    oldadesigner.Address = designer.Address;
                    oldadesigner.ExperienceYear = designer.ExperienceYear;
                    oldadesigner.Bio = designer.Bio;
                    db.SaveChanges();
                }
                if (id == 4)
                {
                    var client = obj["Client"].ToObject<Client>();
                    Client oldclient = db.Clients.Where(a => a.UserName == username).FirstOrDefault();
                    oldclient.City = client.City;
                    oldclient.Address = client.Address;
                    db.SaveChanges();
                }
                if (id == 5)
                {
                    var store = obj["Store"].ToObject<Store>();
                    Store oldstore = db.Stores.Where(a => a.Email == email).FirstOrDefault();
                    oldstore.StoreName = store.StoreName;
                    oldstore.City = store.City;
                    oldstore.Address = store.Address;
                    db.SaveChanges();
                }
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
        ///UPDATE ACCOUNT PASSORD & USE IT IN FORGET PASSWORD
        [HttpPut("updatepassword")]
        public ActionResult ChangePassword(string email,ChangePasswordDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Account account = db.Accounts.Where(a => a.Email == email).FirstOrDefault();
            string _password = account.Password;
            if(_password == model.OldPassword)
            //if (account != null)
            {
                account.Password = model.NewPassword;
                db.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        /**********************************************DELET ACCOUNT&USER METHOD***************************************************/
        [HttpDelete]
        public ActionResult DeleteAccount(string email, string username)
        {
            Account account = db.Accounts.Where(a => a.Email == email).FirstOrDefault();
            User user = db.Users.Where(a => a.UserName == username).FirstOrDefault();
            if (account != null && user != null)
            {
                db.Accounts.Remove(account);
                db.Users.Remove(user);
                int id = account.RoleId;
                /*********check for the RoleId to delete corresponding user ***********/
                if (id == 2)
                {
                    Tailor tailor = db.Tailors.Where(a => a.UserName == username && a.Email == email).FirstOrDefault();
                    db.Tailors.Remove(tailor);
                }
                if (id == 3)
                {
                    Designer designer = db.Designers.Where(a => a.UserName == username && a.Email == email).FirstOrDefault();
                    db.Designers.Remove(designer);
                }
                if (id == 4)
                {
                    Client client = db.Clients.Where(a => a.UserName == username && a.Email == email).FirstOrDefault();
                    db.Clients.Remove(client);
                }
                if (id == 5)
                {
                    Store store = db.Stores.Where(a => a.UserName == username && a.Email == email).FirstOrDefault();
                    db.Stores.Remove(store);
                }
                db.SaveChanges();
                return Ok("Removed");
            }
            else
            {
                return BadRequest();
            }
        }


    }

}
