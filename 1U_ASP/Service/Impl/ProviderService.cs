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
    public class ProviderService : IProviderService
    {
      
        private readonly ApplicationContext _context;
       
        public ProviderService(ApplicationContext context
           
        )
        {
            _context = context;
        }
        
        public async Task<ActionResult<IEnumerable<Provider>>> GetProviders()
        {
            return await _context.Provider.ToListAsync();
        }

        public async Task<ActionResult<Provider>> GetProvider(int id)
        {
            var provider = await _context.Provider.FindAsync(id);
            
            return provider;
        }

        public async Task<DataServiceMessage> PutProvider(Provider provider)
        {
            _context.Entry(provider).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return await Task.FromResult(new DataServiceMessage
                {
                    Result = false,
                    MainMessage = e.Message
            });
              
            }

            return await Task.FromResult(new DataServiceMessage
            {
                Result = true,
                MainMessage = GoodResponses.UpdatedSuccessfully
            });
        }

        public async Task<string> PostProvider(Provider provider)
        {
            try
            {
                _context.Provider.Add(provider);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return GoodResponses.AddedSuccessfully;
            
        }

        public async Task<string> DeleteProvider(int id)
        {
            var provider = await _context.Provider.FindAsync(id);
            if (provider == null)
            {
                return BadResponses.PersonIsnTFound;
            }

            _context.Provider.Remove(provider);
            await _context.SaveChangesAsync();

            return GoodResponses.DeletedSuccessfully;
        }
    }
}
