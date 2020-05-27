using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _1U_ASP.Context;
using _1U_ASP.DTO;
using _1U_ASP.Models;
using _1U_ASP.Service.Interface;

namespace _1U_ASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpGet("{GetAllShops}")]
        public async Task<ActionResult<IEnumerable<Shop>>> GetAllShops()
        {
            return await _shopService.GetAllShops();
            
        }

        [HttpGet("GetShoById/{id}")]
        public async Task<ActionResult<Shop>> GetShop(int id)
        {
            return await _shopService.GetShop(id);
            
        }

        // PUT: api/Shops/5
        [HttpPut("{PutShop}")]
        public async Task<DataServiceMessage> PutShop([FromBody]Shop shop)
        {
            return await _shopService.PutShop(shop);
            
        }

        // POST: api/Shops
        [HttpPost("{PostShop}")]
        public async Task<DataServiceMessage> PostShop(Shop shop)
        {
            return await _shopService.PostShop(shop);
        }

        // DELETE: api/Shops/5
        [HttpDelete("{DeleteShop}/{id}")]
        public async Task<DataServiceMessage> DeleteShop(int id)
        {
            return await _shopService.DeleteShop(id);
            
        }

       
    }
}
