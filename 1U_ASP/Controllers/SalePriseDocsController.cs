using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _1U_ASP.Context;
using _1U_ASP.Models;

namespace _1U_ASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalePriseDocsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SalePriseDocsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/SalePriseDocs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalePriseDoc>>> GetSalePriseDocs()
        {
            return await _context.SalePriseDocs.ToListAsync();
        }

        // GET: api/SalePriseDocs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalePriseDoc>> GetSalePriseDoc(int id)
        {
            var salePriseDoc = await _context.SalePriseDocs.FindAsync(id);

            if (salePriseDoc == null)
            {
                return NotFound();
            }

            return salePriseDoc;
        }

        // PUT: api/SalePriseDocs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalePriseDoc(int id, SalePriseDoc salePriseDoc)
        {
            if (id != salePriseDoc.SalePriseDocId)
            {
                return BadRequest();
            }

            _context.Entry(salePriseDoc).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalePriseDocExists(id))
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

        // POST: api/SalePriseDocs
        [HttpPost]
        public async Task<ActionResult<SalePriseDoc>> PostSalePriseDoc(SalePriseDoc salePriseDoc)
        {
            _context.SalePriseDocs.Add(salePriseDoc);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSalePriseDoc", new { id = salePriseDoc.SalePriseDocId }, salePriseDoc);
        }

        // DELETE: api/SalePriseDocs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SalePriseDoc>> DeleteSalePriseDoc(int id)
        {
            var salePriseDoc = await _context.SalePriseDocs.FindAsync(id);
            if (salePriseDoc == null)
            {
                return NotFound();
            }

            _context.SalePriseDocs.Remove(salePriseDoc);
            await _context.SaveChangesAsync();

            return salePriseDoc;
        }

        private bool SalePriseDocExists(int id)
        {
            return _context.SalePriseDocs.Any(e => e.SalePriseDocId == id);
        }
    }
}
