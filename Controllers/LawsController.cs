using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Apka2.Entities;
using Apka2.Data;

namespace Apka2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LawsController : ControllerBase
    {
        private readonly Context _context;

        public LawsController(Context context)
        {
            _context = context;
        }

        // GET: api/Laws
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Law>>> GetLaws()
        {
            return await _context.Laws.ToListAsync();
        }

        // GET: api/Laws/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Law>> GetLaw(int id)
        {
            var law = await _context.Laws.FindAsync(id);

            if (law == null)
            {
                return NotFound();
            }

            return law;
        }

        // PUT: api/Laws/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLaw(int id, Law law)
        {
            if (id != law.Id)
            {
                return BadRequest();
            }

            _context.Entry(law).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LawExists(id))
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

        // POST: api/Laws
        [HttpPost]
        public async Task<ActionResult<Law>> PostLaw(Law law)
        {
            _context.Laws.Add(law);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLaw", new { id = law.Id }, law);
        }

        // DELETE: api/Laws/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Law>> DeleteLaw(int id)
        {
            var law = await _context.Laws.FindAsync(id);
            if (law == null)
            {
                return NotFound();
            }

            _context.Laws.Remove(law);
            await _context.SaveChangesAsync();

            return law;
        }

        private bool LawExists(int id)
        {
            return _context.Laws.Any(e => e.Id == id);
        }
    }
}
