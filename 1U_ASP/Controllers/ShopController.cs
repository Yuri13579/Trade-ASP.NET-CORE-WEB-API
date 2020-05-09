using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _1U_ASP.Context;
using _1U_ASP.Models;

namespace _1U_ASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly ApplicationContext _context;


        public ShopController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet("{GetAllShops}")]
        public async Task<ActionResult<IEnumerable<Shop>>> GetAllShops()
        {
            return await _context.Shops.ToListAsync();
        }

        [HttpGet("GetShoById/{id}")]
        public async Task<ActionResult<Shop>> GetShop(int id)
        {
            var shop = await _context.Shops.FindAsync(id);

            if (shop == null)
            {
                return NotFound();
            }

            return shop;
        }

        // PUT: api/Shops/5
        [HttpPut("{PutShop}")]
        public async Task<string> PutShop([FromBody]Shop shop)
        {
            _context.Entry(shop).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShopExists(shop.ShopId))
                {
                    return "NotFound";
                }
                else
                {
                    return "Error";
                }
            }

            return "updated";
        }

        // POST: api/Shops
        [HttpPost("{PostShop}")]
        public async Task<ActionResult<Shop>> PostShop(Shop shop)
        {
            _context.Shops.Add(shop);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShop", new { id = shop.ShopId }, shop);
        }

        // DELETE: api/Shops/5
        [HttpDelete("{DeleteShop}/{id}")]
        public async Task<ActionResult<Shop>> DeleteShop(int id)
        {
            var shop = await _context.Shops.FindAsync(id);
            if (shop == null)
            {
                return NotFound();
            }

            _context.Shops.Remove(shop);
            await _context.SaveChangesAsync();

            return shop;
        }

        private bool ShopExists(int id)
        {
            return _context.Shops.Any(e => e.ShopId == id);
        }
    }
}
