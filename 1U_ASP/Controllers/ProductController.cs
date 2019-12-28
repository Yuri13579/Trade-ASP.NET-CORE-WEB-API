using _1U_ASP.MiddleTier;
using _1U_ASP.MiddleTier.Interface;
using Dap1U.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace _1U_ASP.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController: Controller
    {
        private readonly IProductService _productService;

        public ProductController(
            IProductService productService
            )
        {
            _productService = productService;
        }

        //[HttpGet]
        //public async Task<Product> Get()
        //{
        //    int id = 1;
        //    var result = await _productService.GetAllProducts();
        //    return result;

        //}


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
                var result = _productService.GetAllProducts();
                return Json(result); //result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

        }


    }
}
