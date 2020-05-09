using System;
using _1U_ASP.Repositorys.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dap1U.Models;
using _1U_ASP.MiddleTier.Interface;

namespace _1U_ASP.MiddleTier
{
    public class ProductService: IProductService
    {
        
        private readonly IRepository<Product> _product;

        public ProductService(
            IRepository<Product> product
        )
        {
            _product = product;
        }
            
        public async Task<Product> GetProductById(int id)
        {
           return await _product.GetByIdAsync(id);
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _product.ListAllAsync();
        }

        public async Task<bool> AddProduct(Product product)
        {
            await _product.AddAsync(product);
            return true;
        }

        public async Task<bool> DeleteProductById(int id)
        {
            await _product.DeleteAsyncById(id);
            return true;
        }

        public async Task<bool> PutProduct(Product product)
        {
           var old = await _product.GetByIdAsync(product.ProductId);
           if (old != null &&
                (string.Equals(old.Name, product.Name, StringComparison.CurrentCultureIgnoreCase) == false 
                || string.Equals(old.Description, product.Description, StringComparison.CurrentCultureIgnoreCase) == false
                || old.Barcode.Equals(product.Barcode) == false
                || old.PriceCost.Equals(product.PriceCost) == false
                || old.PriseSale.Equals(product.PriseSale) == false
               ))
            {
                old.Name = product.Name;
                old.Description= product.Description;
                old.Barcode = product.Barcode;
                old.PriceCost = product.PriceCost;
                old.PriseSale = product.PriseSale;
                await _product.UpdateAsync(old);
                return true;
            }

            if (old == null)
            {
                product.ProductId = 0;
                await AddProduct(product);
            }
          

            return true;
        }
        
    }
}
