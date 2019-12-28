using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1U_ASP.Context;
using Dap1U.Models;
using _1U_ASP.Repositorys.Interface;
using Microsoft.EntityFrameworkCore;

namespace _1U_ASP.Repositorys
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext _context;
        public ProductRepository(ApplicationContext context)// : base(context)
        {
            _context = context;
        }

        public Task<Product> Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task<Product> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetProduct(int id)
        {
            try
            {
                var res = await _context.Products.FindAsync(id);
                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
           
        }

        public Task<List<Product>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Product> Update(Product entity)
        {
            throw new NotImplementedException();
        }

        Task<Product> IRepository<Product>.Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var res = await _context.Products.ToListAsync();
            return res;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var res = await _context.Products.ToListAsync();
            return res;
        }
    }
}
