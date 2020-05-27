using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1U_ASP.Const;
using _1U_ASP.Context;
using _1U_ASP.DTO;
using _1U_ASP.Models;
using _1U_ASP.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _1U_ASP.Service.Impl
{
    public class ShopService : IShopService
    {
        private readonly ApplicationContext _context;


        public ShopService(ApplicationContext context)
        {
            _context = context;
        }
        
        public async Task<ActionResult<IEnumerable<Shop>>> GetAllShops()
        {
            return await _context.Shops.ToListAsync();
        }

        public async Task<ActionResult<Shop>> GetShop(int id)
        {
            var shop = await _context.Shops.FindAsync(id);
            return shop;
        }
        
        public async Task<DataServiceMessage> PutShop(Shop shop)
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
                    return new DataServiceMessage
                    {
                        Result = false,
                        MainMessage = BadResponses.ShopIsnTFound
                    }; 
                }
                else
                { 
                    return new DataServiceMessage
                    {
                        Result = false,
                        MainMessage = BadResponses.Error
                    }; 
                }
            }

            return new DataServiceMessage
            {
                Result = true,
                MainMessage = GoodResponses.UpdatedSuccessfully
            };
        }

        public async Task<DataServiceMessage> PostShop(Shop shop)
        {
            try
            {
                _context.Shops.Add(shop);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new DataServiceMessage
                {
                    Result = false,
                    MainMessage = e.Message
                };
            }
            
            return new DataServiceMessage
            {
                Result = true,
                MainMessage = GoodResponses.AddedSuccessfully
            };
        }
        
        public async Task<DataServiceMessage> DeleteShop(int id)
        {
            var shop = await _context.Shops.FindAsync(id);
            if (shop == null)
            {
                return new DataServiceMessage
                {
                    Result = false,
                    MainMessage = BadResponses.ShopIsnTFound
                };
            }

            _context.Shops.Remove(shop);
            await _context.SaveChangesAsync();

            return new DataServiceMessage
            {
                Result = true,
                MainMessage = GoodResponses.DeletedSuccessfully
            };
        }

        private bool ShopExists(int id)
        {
            return _context.Shops.Any(e => e.ShopId == id);
        }

    }
}
