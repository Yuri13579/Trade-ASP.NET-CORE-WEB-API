using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1U_ASP.Context;
using Dap1U.Models;
using _1U_ASP.Repositorys.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace _1U_ASP.Repositorys
{
    public class ProductRepository : IProductRepository
    {
        private readonly IRepository<Product> _product;
      //  private readonly ApplicationContext _context;
        public ProductRepository(
            //ApplicationContext context)// : base(context)
            IRepository<Product> product

            )
        {
            //_context = context;
            _product = product;
        }


        public Task<Product> GetProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public Product AddProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
