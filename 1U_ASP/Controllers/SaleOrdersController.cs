using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _1U_ASP.DTO;
using _1U_ASP.Models;
using _1U_ASP.Service.Interface;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<DataServiceMessage> SellGoods([FromBody]  List<SellDto> sellDtos)
        {
            return await _saleOrderService.SellGoods(sellDtos);
        }
        
    }
}
