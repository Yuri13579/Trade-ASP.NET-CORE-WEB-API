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
    public class SaleOrdersController : Controller
    {
        private readonly ISaleOrderSevrice _saleOrderService;

        public SaleOrdersController(
            ISaleOrderSevrice saleOrderService

            )
        {
            _saleOrderService = saleOrderService;
        }

        // GET: api/SaleOrders
        [HttpGet("GetSaleOrders")]
        public async Task<IActionResult> GetSaleOrders()
        {
        
          try
          {
              var result = await _saleOrderService.GetAllSaleOrder();
              return Json(result);
          }
          catch (Exception e)
          {
              Console.WriteLine(e.Message);
              throw;
          }
        }

        // GET: api/SaleOrders
        [HttpGet("ALLSales")]
        public async Task<IActionResult> AllSales()
        {
            try
            {
                var result = await _saleOrderService.GetAllSale();
                return Json(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        
        // GET: api/SaleOrders/5
        [HttpGet("GetSaleOrder/{id}")]
        public async Task<ActionResult<SaleOrder>> GetSaleOrder(int id)
        {
            return await _saleOrderService.GetSaleOrder(id);

        }
        
        //SellGoods
        [HttpPost("{SellGoods}")]
        public async Task<string> SellGoods([FromBody]  List<SellDto> sellDtos)
        {

            string resultText;
            try
            {
                 resultText = await _saleOrderService.SellGoods(sellDtos);
            }
            catch (Exception e)
            {
                resultText = e.Message;
                throw;
            }
           
            return resultText;
        }
        
    }
}
