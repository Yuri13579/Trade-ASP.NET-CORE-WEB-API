using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using _1U_ASP.Const;
using _1U_ASP.Context;
using _1U_ASP.DTO;
using _1U_ASP.MiddleTier.Interface;
using _1U_ASP.Models;
using _1U_ASP.Repositorys.Interface;
using Dap1U.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _1U_ASP.MiddleTier
{
    public class SaleOrderServices : ISaleOrderSevrice
    {
        private readonly IRepository<SaleOrder> _saleOrder;
        private readonly IRepository<SaleOrderDetail> _saleOrderDetail;
        private readonly IRepository<Product> _product;
        private readonly IRepository<Shop> _shop;
        private readonly ApplicationContext _applicationContext;

        public SaleOrderServices(
           IRepository<SaleOrder> saleOrder,
           IRepository<SaleOrderDetail> saleOrderDetail,
           IRepository<Product> product
           ,IRepository<Shop> shop,
           ApplicationContext applicationContext
           )
        {
            _saleOrder = saleOrder;
            _saleOrderDetail = saleOrderDetail;
            _product = product;
            _shop = shop;
            _applicationContext = applicationContext;
        }

        public async Task<List<SaleOrder>> GetAllSaleOrder()
        {
            return await _saleOrder.ListAllAsync();
        }

        public async Task<SaleOrder> GetSaleOrderById(int id)
        {
            return await _saleOrder.GetByIdAsync(id);
        }

        public async Task<List<SaleDTO>> GetAllSale()
        {
            var products = _product.List(x => x.Deleted == false).ToList();
            var saleOrders =  _saleOrder.ListAll().ToList();
            var details = await _saleOrderDetail.ListAllAsync();

            var result = await (from saleOrder in _saleOrder.ListAll()
                         join detail in _saleOrderDetail.ListAll()
                             on saleOrder.SaleOrderId equals detail.SaleOrderId.GetValueOrDefault()
                         join product in _product.ListAll()
                          on detail.ProductId equals product.ProductId
                         join shop in _shop.ListAll()
                             on saleOrder.ShopId equals shop.ShopId
                         select new SaleDTO
                {
                    SaleOrderID = saleOrder.SaleOrderId,
                    SaleOrderDetailId = detail.SaleOrderDetailId,
                    ProductId = product.ProductId,
                    ProductName = product.Name,
                    ProductDescription = product.Description,
                    ProductBarcode = product.Barcode,
                    Count = detail.Count,
                    DataTime = saleOrder.DataTime,
                    PriceCost = detail.PriceCost.GetValueOrDefault(),
                    PriseSale = detail.PriseSale.GetValueOrDefault(),
                    Summ = detail.Summ.GetValueOrDefault(),
                    ShopId = saleOrder.ShopId.GetValueOrDefault(),
                    ShopName = shop.Name,
                    ShopAddress = shop.Address
                }).ToListAsync();
            return result;
        }

        private static bool StoredProcedureExists(string sp)
        {
            var connString = GlobalVariables.ConnectionStringMainDatabase;
            var query = string.Format("SELECT COUNT(0) FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = '{0}'", sp);
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(query, conn))
                {
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        public async Task<string> SellGoods(List<SellDto> sellDtos)
        {
            try
            {
               
                var newSaleOrder = await _saleOrder.AddAsync(new SaleOrder {DataTime = DateTime.Now, ShopId = sellDtos[0].ShopId });
                var products = _product.ListAll();
                List<SaleOrderDetail> saleOrderDetailCreate = sellDtos.ConvertAll(
                    x => new SaleOrderDetail
                    {
                        Count = x.Count,
                        Deleted = false,
                        PriceCost = products.LastOrDefault(z=> z.ProductId == x.ProductId)?.PriceCost,
                        PriseSale = products.LastOrDefault(z => z.ProductId == x.ProductId)?.PriseSale,
                        ProductId = x.ProductId,
                        SaleOrderId = newSaleOrder.SaleOrderId, //  1,
                        Summ = products.LastOrDefault(z => z.ProductId == x.ProductId)?.PriseSale * x.Count
                    });

                bool isProcedureExist = StoredProcedureExists(SqlNames.SpAddSell);
                if (isProcedureExist)
                {
                    string sql = @"exec spAddSell @Count, @PriceCost, @PriseSale, @ProductId, @SaleOrderId, @Summ";
                    foreach (var el in saleOrderDetailCreate)
                    {
                        var replacements = new Dictionary<string, string> { { "@Count", el.Count.ToString() }, { "@PriceCost", el.PriceCost.ToString() }, { "@PriseSale", el.PriseSale.ToString() }, { "@ProductId" , el.ProductId.ToString() }, { "@SaleOrderId", el.SaleOrderId.ToString() }, { "@Summ", el.Summ.ToString() } };

                        var output = replacements.Aggregate(sql, (current, replacement) => current.Replace(replacement.Key, replacement.Value));
                        _applicationContext.Database.ExecuteSqlCommand(output);
                    }
                }
                else
                {
                    await _saleOrderDetail.AddRangeAsync(saleOrderDetailCreate);
                }
                
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return "Updated"; 
        }
    }
}
