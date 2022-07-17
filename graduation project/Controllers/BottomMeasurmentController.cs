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
    public class BottomMeasurementsController : ControllerBase
    {
        private readonly FashionDesignContext _context;

        public BottomMeasurementsController(FashionDesignContext context)
        {
            _context = context;
        }

        // GET: api/BottomMeasurements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BottomMeasurement>>> GetBottomMeasurements()
        {
            if (_context.BottomMeasurements == null)
            {
                return NotFound();
            }
            return await _context.BottomMeasurements.ToListAsync();
        }

        // GET: api/BottomMeasurements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BottomMeasurement>> GetBottomMeasurement(int id)
        {
            if (_context.BottomMeasurements == null)
            {
                return NotFound();
            }
            var bottomMeasurement = await _context.BottomMeasurements.FindAsync(id);

            if (bottomMeasurement == null)
            {
                return NotFound();
            }

            return bottomMeasurement;
        }

        // PUT: api/BottomMeasurements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBottomMeasurement(int id, BottomMeasurement bottomMeasurement)
        {
            if (id != bottomMeasurement.DesignOrderNumber)
            {
                return BadRequest();
            }

            _context.Entry(bottomMeasurement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BottomMeasurementExists(id))
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

        // POST: api/BottomMeasurements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BottomMeasurement>> PostBottomMeasurement(BottomMeasurement bottomMeasurement)
        {
            if (_context.BottomMeasurements == null)
            {
                return Problem("Entity set 'FashionDesignContext.BottomMeasurements'  is null.");
            }
            _context.BottomMeasurements.Add(bottomMeasurement);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BottomMeasurementExists(bottomMeasurement.DesignOrderNumber))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBottomMeasurement", new { id = bottomMeasurement.DesignOrderNumber }, bottomMeasurement);
        }

        // DELETE: api/BottomMeasurements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBottomMeasurement(int id)
        {
            if (_context.BottomMeasurements == null)
            {
                return NotFound();
            }
            var bottomMeasurement = await _context.BottomMeasurements.FindAsync(id);
            if (bottomMeasurement == null)
            {
                return NotFound();
            }

            _context.BottomMeasurements.Remove(bottomMeasurement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BottomMeasurementExists(int id)
        {
            return (_context.BottomMeasurements?.Any(e => e.DesignOrderNumber == id)).GetValueOrDefault();
        }
    }
}
