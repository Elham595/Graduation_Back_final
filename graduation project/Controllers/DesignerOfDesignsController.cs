//using graduation_project.Data;
//using graduation_project.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace graduation_project.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class DesignerOfDesignsController : ControllerBase
//    {

//        private readonly FashionDesignContext _context;

//        public DesignerOfDesignsController(FashionDesignContext context)
//        {
//            _context = context;

//        }
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<DesignerOfDesign>>> GetDesignerOfDesigns()
//        {
//            if (_context.DesignerOfDesigns == null)
//            {
//                return NotFound();
//            }
//            return await _context.DesignerOfDesigns.ToListAsync();
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<DesignerOfDesign>> GetDesignerOfDesign(string id)
//        {
//            if (_context.DesignerOfDesigns == null)
//            {
//                return NotFound();
//            }
//            var designerOfDesign = await _context.DesignerOfDesigns.FindAsync(id);

//            if (designerOfDesign == null)
//            {
//                return NotFound();
//            }

//            return designerOfDesign;
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutDesignerOfDesign(string id, DesignerOfDesign designerOfDesign)
//        {
//            if (id != designerOfDesign.UserName)
//            {
//                return BadRequest();
//            }

//            _context.Entry(designerOfDesign).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!designerOfDesignExist(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }


//        [HttpPost]
//        public async Task<ActionResult<DesignerOfDesign>> PostDesignerofDesign(DesignerOfDesign designerofDesign)
//        {
//            if (_context.DesignerOfDesigns == null)
//            {
//                return Problem("Entity set 'FashionDesignContext.DesignerOfDesigns'  is null.");
//            }
//            _context.DesignerOfDesigns.Add(designerofDesign);
//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateException)
//            {
//                if (designerOfDesignExist(designerofDesign.UserName))
//                {
//                    return Conflict();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return CreatedAtAction("GetDesignerOfDesign", new { id = designerofDesign.UserName }, designerofDesign);
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteDesignerOfDesign(string id)
//        {
//            if (_context.DesignerOfDesigns == null)
//            {
//                return NotFound();
//            }
//            var designOfDesigner = await _context.DesignerOfDesigns.FindAsync(id);
//            if (designOfDesigner == null)
//            {
//                return NotFound();
//            }

//            _context.DesignerOfDesigns.Remove(designOfDesigner);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }


//        private bool designerOfDesignExist(string id)
//        {
//            return (_context.DesignerOfDesigns?.Any(e => e.UserName == id)).GetValueOrDefault();
//        }



//    }
//}