using graduation_project.Data;
using graduation_project.Models;
using graduation_project.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;


namespace graduation_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IBaseRepositoryServices<Color> _dbContextRepositoryServices;

        private FashionDesignContext _Context;
        private IUnitOfWork _UnitOfWork;
        public TestController(DbContextRepositoryServices<Color> dbContextRepositoryServices, FashionDesignContext fashionDesginContext, IUnitOfWork unitOfWork)
        {
            // _dbContextRepositoryServices = dbContextRepositoryServices;
            _Context = fashionDesginContext;
            _UnitOfWork = unitOfWork;
        }
        [HttpGet("test")]
        public async Task<IActionResult> GetColor()
        {
            Design? d = await _Context.Designs.FindAsync(18);

            FileResult Image = new VirtualFileResult($"{d.DesignImage}", "Image/jpeg");
            return Ok(new { DesignImage = Image , DesignId = d.DesignId});
            
        }
        [HttpPost("test1")]
        public async Task<IActionResult> SetColor(Color color)
        {
            int id = color.ColorId;
            _Context.Colors.Add(color);
            id  = color.ColorId;
           await _Context.SaveChangesAsync();
            id= color.ColorId;
            return Ok();
        }

    }
}
