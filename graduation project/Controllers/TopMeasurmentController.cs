using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using graduation_project.Data;
using graduation_project.Models;

namespace graduation_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopMeasurmentsController : ControllerBase
    {
        private readonly FashionDesignContext _context;

        public TopMeasurmentsController(FashionDesignContext context)
        {
            _context = context;
        }

        // GET: api/TopMeasurments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TopMeasurment>>> GetTopMeasurments()
        {
            if (_context.TopMeasurments == null)
            {
                return NotFound();
            }
            return await _context.TopMeasurments.ToListAsync();
        }

        // GET: api/TopMeasurments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TopMeasurment>> GetTopMeasurment(int id)
        {
            if (_context.TopMeasurments == null)
            {
                return NotFound();
            }
            var topMeasurment = await _context.TopMeasurments.FindAsync(id);

            if (topMeasurment == null)
            {
                return NotFound();
            }

            return topMeasurment;
        }

        // PUT: api/TopMeasurments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTopMeasurment(int id, TopMeasurment topMeasurment)
        {
            if (id != topMeasurment.DesignOrderNumber)
            {
                return BadRequest();
            }

            _context.Entry(topMeasurment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TopMeasurmentExists(id))
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

        // POST: api/TopMeasurments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TopMeasurment>> PostTopMeasurment(TopMeasurment topMeasurment)
        {
            if (_context.TopMeasurments == null)
            {
                return Problem("Entity set 'FashionDesignContext.TopMeasurments'  is null.");
            }
            _context.TopMeasurments.Add(topMeasurment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TopMeasurmentExists(topMeasurment.DesignOrderNumber))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTopMeasurment", new { id = topMeasurment.DesignOrderNumber }, topMeasurment);
        }

        // DELETE: api/TopMeasurments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTopMeasurment(int id)
        {
            if (_context.TopMeasurments == null)
            {
                return NotFound();
            }
            var topMeasurment = await _context.TopMeasurments.FindAsync(id);
            if (topMeasurment == null)
            {
                return NotFound();
            }

            _context.TopMeasurments.Remove(topMeasurment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TopMeasurmentExists(int id)
        {
            return (_context.TopMeasurments?.Any(e => e.DesignOrderNumber == id)).GetValueOrDefault();
        }
    }
}
   