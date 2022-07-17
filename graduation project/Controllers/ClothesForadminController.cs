using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using graduation_project.Data;
using graduation_project.Models;
using graduation_project.Helpers;
using graduation_project.Extensions;
using graduation_project.DTOs;

namespace graduation_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClothesForadminController : ControllerBase
    {
        private readonly FashionDesignContext _context;

        public ClothesForadminController(FashionDesignContext context)
        {
            _context = context;
        }

        // GET: api/Clothes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clothe>>> GetAllClothes()
        {
            return await _context.Clothes.ToListAsync();
        }

        // GET: api/Clothes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Clothe>> GetClotheByID(int id)
        {
            var clothe = await _context.Clothes.FindAsync(id);

            if (clothe == null)
            {
                return NotFound();
            }

            return clothe;
        }

        // PUT: api/Clothes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClothes(int id, Clothe clothe)
        {
            if (id != clothe.ClotheId)
            {
                return BadRequest();
            }

            _context.Entry(clothe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClotheExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Clothes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Clothe>> PostClothes(Clothe clothe)
        {
            _context.Clothes.Add(clothe);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClotheExists(clothe.ClotheId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetClothes", new { id = clothe.ClotheId }, clothe);
        }

        // DELETE: api/Clothes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClothe(int id)
        {
            var clothe = await _context.Clothes.FindAsync(id);
            if (clothe == null)
            {
                return NotFound();
            }

            _context.Clothes.Remove(clothe);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("DesignersType/{name}")]
        public async Task<ActionResult<Design>> GetDesignByType(string name)
        {
            if (_context.Designs == null)
            {
                return NotFound();
            }

            var Designs = _context.Designs.Where(x => x.Cloth.ClotheName == name).Select(design => new DesignDto
            {
                Id = design.DesignId,
                ClotheType = (_context.Clothes.FirstOrDefault(c => c.ClotheId == design.ClothId)).ClotheName,
                DesignDate = design.DesignDate,
                Status = design.Status,
                ImageUrl = design.DesignImage.UrlFromFilePath("DesginImages", $"{Request.Scheme}://{Request.Host}"),
            });
            return Ok(Designs);

        }

        Design GetClothe(Design design)
        {
            design.Cloth = _context.Clothes.FirstOrDefault(c => c.ClotheId == design.ClothId);
            return design;
        }

        [HttpGet("viewDesigns")]
        public async Task<ActionResult<Design>> GetDesignallDesigns()
        {
            if (_context.Designs == null)
            {
                return NotFound();
            }

            var Designs = await _context.Designs.ToListAsync();

            return Ok(Designs);
        }

        private bool ClotheExists(int id)
        {
            return _context.Clothes.Any(e => e.ClotheId == id);
        }
        private string GetUniqeFileName(string filename)
        {
            filename = Path.GetFileName(filename);
            var fileExtension = Path.GetExtension(filename);
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filename);
            return $"{fileNameWithoutExtension}_{GeneraterandomDigitalCode(4)}_{GeneraterandomDigitalCode(3)}{fileExtension}";
        }

        private string GeneraterandomDigitalCode(int length)
        {
            var random = new SecureRandomNumberGenerator();
            var str = string.Empty;
            for (var i = 0; i < length; i++)
                str = string.Concat(str, random.Next(10).ToString());
            return str;
        }
    }
}
