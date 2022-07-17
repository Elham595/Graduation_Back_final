using graduation_project.Const;
using graduation_project.Data;
using graduation_project.Models;
using graduation_project.NonDomainModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace graduation_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignsController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _Environment;
        public DesignsController(IUnitOfWork unitOfWork, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _Environment = environment;
        }

        //----------------------------------------------------------------------------------------------------//
        [HttpGet("AllDesignByClotheName/pages")]
        // [Authorize(Roles ="Client,Admin")]
        public async Task<IActionResult> GetAllDesignByClothe(string ClotheName, int page)
        {
            var Includes = new Expression<Func<Design, object>>[] { i => i.Cloth };
            PagesInformation pagesInformation = await _unitOfWork.Designs.GetPageInformation(i => i.Cloth.ClotheName == ClotheName, Includes);
            var list = await _unitOfWork.Designs.GetAllPagination(page, Paginations.NumberOfItems, i => i.Cloth.ClotheName == ClotheName, Includes, i => i.DesignDate, OrderByValues.Descending);
            if (list.Any())
            {
                return Ok(new { Page = page, pagesInformation.TotalPages, pagesInformation.TotalItems, ImageStaticPath = Path.Combine(StaticPath.DesignPath), Result = list });

            }
            return NotFound(new { Message = "Page Not Exist" });
        }
        //-----------------------------------------------------------------------------------------------------//
        [HttpGet("AllCleintDesignByClotheName/pages")]
        // [Authorize(Roles ="Client,Admin")]
        public async Task<IActionResult> GetAllClientDesignByClothe(string ClotheName, int page)
        {
            var Includes = new Expression<Func<Design, object>>[] { i => i.Cloth };
            PagesInformation pagesInformation = await _unitOfWork.Designs.GetPageInformation(i => (i.Cloth.ClotheName == ClotheName) && (i.Status.ToUpper() == "C"), Includes);
            var list = await _unitOfWork.Designs.GetAllPagination(page, Paginations.NumberOfItems, i => (i.Cloth.ClotheName == ClotheName) && (i.Status.ToUpper() == "C"), Includes, i => i.DesignDate, OrderByValues.Descending);
            if (list.Any())
            {
                return Ok(new { Page = page, pagesInformation.TotalPages, pagesInformation.TotalItems, ImageStaticPath = Path.Combine(StaticPath.DesignPath), Result = list });

            }
            return NotFound(new { Message = "Page Not Exist" });
        }

        //-------------------------------------------------------------------------------------//
        [HttpGet("ViewAllDesignByClotheName/pages")]
        // [Authorize(Roles ="Client,Admin")]

        public async Task<IActionResult> GetAllAdminDesignByClothe(string ClotheName, int page)
        {
            var Includes = new Expression<Func<Design, object>>[] { i => i.Cloth };
            PagesInformation pagesInformation = await _unitOfWork.Designs.GetPageInformation(i => (i.Cloth.ClotheName == ClotheName) && (i.Status.ToUpper() == "A"), Includes);
            var list = await _unitOfWork.Designs.GetAllPagination(page, Paginations.NumberOfItems, i => (i.Cloth.ClotheName == ClotheName) && (i.Status.ToUpper() == "A"), Includes, i => i.DesignDate, OrderByValues.Descending);
            if (list.Any())
            {
                return Ok(new { Page = page, pagesInformation.TotalPages, pagesInformation.TotalItems, ImageStaticPath = Path.Combine(StaticPath.DesignPath), Result = list });

            }
            return NotFound(new { Message = "Page Not Exist" });
        }

        [HttpPost("UploadDesign")]
        // [Authorize(Roles="Admin,Client")]
        public async Task<IActionResult> CreateDesign([FromForm] DesignCreationModel model)
        {
            try
            {

                //if (!ModelState.IsValid)
                //    return BadRequest(ModelState);
                var ClotheId = await _unitOfWork.Clothes.WhereOne(c => c.ClotheName == model.ClotheName, c => c.ClotheId);
                if (ClotheId != 0)
                {
                    Design design = new Design { ClothId = (Int32)ClotheId, Status = model.Status };
                    int index = await _unitOfWork.Designs.Add(design);
                    if (index < 1)
                        throw new Exception("Some thing Wrong ,Can Not Save Design");
                    int DesignId = design.DesignId;
                    string ImageName = DesignId.ToString();
                    string ImageExtenstion = Path.GetExtension(model.Image.FileName);
                    string DesignImage = String.Concat(ImageName, ImageExtenstion);
                    design.DesignImage = DesignImage;
                    index = await _unitOfWork.Designs.Update(design);
                    if (index < 1)
                    {
                        await _unitOfWork.Designs.Remove(design);
                        throw new Exception("Some thing Wrong ,Can Not Save Design");
                    }

                    var path = Path.Combine(_Environment.WebRootPath, StaticPath.DesignPath, design.DesignImage);
                    using (FileStream file = new FileStream(path, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(file);
                    }
                    if (model.UserName is not null)
                    {
                        foreach (var userName in model.UserName)
                        {
                            await _unitOfWork.DesignerOfDesign.Add(new DesignerOfDesign { DesignId = DesignId, UserName = userName });

                        }
                    }

                    return Created("CreateDesignOrder", new { Message = "Success", DesignId = design.DesignId ,designImage=design.DesignImage , model });
                }
                return BadRequest(new { message = "Clothe Type Not Found" });
            }
            catch (Exception? err)
            {
                return BadRequest(err);
            }

        }
    }
}
