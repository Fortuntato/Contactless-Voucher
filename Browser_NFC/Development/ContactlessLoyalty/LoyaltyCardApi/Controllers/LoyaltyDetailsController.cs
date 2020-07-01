using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoyaltyCardApi.Data;
using LoyaltyCardApi.Models;

namespace LoyaltyCardApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoyaltyDetailsController : ControllerBase
    {
        private readonly LoyaltyCardApiContext _context;

        public LoyaltyDetailsController(LoyaltyCardApiContext context)
        {
            _context = context;
        }

        // GET: api/LoyaltyDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoyaltyDetails>>> GetLoyaltyDetails()
        {
            return await _context.Dashboard.ToListAsync();
        }

        // GET: api/LoyaltyDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoyaltyDetails>> GetLoyaltyDetails(int id)
        {
            var loyaltyDetails = await _context.Dashboard.FindAsync(id);

            if (loyaltyDetails == null)
            {
                return NotFound();
            }

            return loyaltyDetails;
        }

        // PUT: api/LoyaltyDetails/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoyaltyDetails(int id, LoyaltyDetails loyaltyDetails)
        {
            if (id != loyaltyDetails.Id)
            {
                return BadRequest();
            }

            _context.Entry(loyaltyDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoyaltyDetailsExists(id))
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

        // POST: api/LoyaltyDetails
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<LoyaltyDetails>> PostLoyaltyDetails(LoyaltyDetails loyaltyDetails)
        {
            _context.Dashboard.Add(loyaltyDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLoyaltyDetails", new { id = loyaltyDetails.Id }, loyaltyDetails);
        }

        // DELETE: api/LoyaltyDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LoyaltyDetails>> DeleteLoyaltyDetails(int id)
        {
            var loyaltyDetails = await _context.Dashboard.FindAsync(id);
            if (loyaltyDetails == null)
            {
                return NotFound();
            }

            _context.Dashboard.Remove(loyaltyDetails);
            await _context.SaveChangesAsync();

            return loyaltyDetails;
        }

        private bool LoyaltyDetailsExists(int id)
        {
            return _context.Dashboard.Any(e => e.Id == id);
        }
    }
}
