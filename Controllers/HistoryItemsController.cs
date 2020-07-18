using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaxHistoryApi.models;

namespace TaxHistoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryItemsController : ControllerBase
    {
        private readonly HistoryContext _context;

        public HistoryItemsController(HistoryContext context)
        {
            _context = context;
        }

        // GET: api/HistoryItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoryItem>>> GetHistoryItems()
        {
            return await _context.HistoryItems.ToListAsync();
        }

        // GET: api/HistoryItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HistoryItem>> GetHistoryItem(long id)
        {
            var historyItem = await _context.HistoryItems.FindAsync(id);

            if (historyItem == null)
            {
                return NotFound();
            }

            return historyItem;
        }

        // PUT: api/HistoryItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistoryItem(long id, HistoryItem historyItem)
        {
            if (id != historyItem.id)
            {
                return BadRequest();
            }

            _context.Entry(historyItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistoryItemExists(id))
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

        // POST: api/HistoryItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HistoryItem>> PostHistoryItem(HistoryItem historyItem)
        {
            _context.HistoryItems.Add(historyItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHistoryItem), new { id = historyItem.id }, historyItem);
        }

        // DELETE: api/HistoryItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HistoryItem>> DeleteHistoryItem(long id)
        {
            var historyItem = await _context.HistoryItems.FindAsync(id);
            if (historyItem == null)
            {
                return NotFound();
            }

            _context.HistoryItems.Remove(historyItem);
            await _context.SaveChangesAsync();

            return historyItem;
        }

        private bool HistoryItemExists(long id)
        {
            return _context.HistoryItems.Any(e => e.id == id);
        }
    }
}
