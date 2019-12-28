using _1U_ASP.Repositorys.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dap1U.Models;
using _1U_ASP.MiddleTier.Interface;
using Microsoft.EntityFrameworkCore;

namespace _1U_ASP.MiddleTier
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(
            IProductRepository productRepository
        )
        {
            _productRepository = productRepository;
        }

        public async Task<Product> GetProductById(int id)
        {
           return await _productRepository.GetProduct(id);
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _productRepository.GetAllProducts();
        }


    }
}
