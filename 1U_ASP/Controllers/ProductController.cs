using Dap1U.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using _1U_ASP.Service.Interface;

namespace _1U_ASP.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController: TradeBaseController
    {
        private readonly IProductService _productService;

        public ProductController(
            IProductService productService
            )
        {
            _productService = productService;
        }
        
        [HttpGet("GetSaleOrderById/{id}")]
        public async Task<Product> GetProductById(int id)
        {
            try
            {
                var result = await _productService.GetProductById(id);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
           
        }
        
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts() 
        {
            try
            {
                var token = GetTokenJwt();
                var result = await _productService.GetAllProducts(token);
                return Json(result); //result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        //AddProduct
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            try
            {
                var result = await _productService.AddProduct(product);
                return Json(result); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        //Delete
        [HttpDelete("Delete/{id}")]
        public async Task<bool> Delete(int id)
        {
            try
            {
                var result = await _productService.DeleteProductById(id);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

        }
        //PutProduct
        [HttpPut("PutProduct")]
        public async Task<IActionResult> PutProduct([FromBody] Product product)
        {
            try
            {
                var result = await _productService.PutProduct(product);
                return Json(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }


    }
}
