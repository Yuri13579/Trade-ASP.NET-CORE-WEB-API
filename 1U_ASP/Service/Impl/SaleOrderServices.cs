﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using _1U_ASP.Const;
using _1U_ASP.Context;
using _1U_ASP.DTO;
using _1U_ASP.Models;
using _1U_ASP.Repositorys.Interface;
using _1U_ASP.Service.Interface;
using Dap1U.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace _1U_ASP.Service.Impl
{
    public class SaleOrderServices : ISaleOrderSevrice
    {
        private readonly IRepository<SaleOrder> _saleOrder;
        private readonly IRepository<SaleOrderDetail> _saleOrderDetail;
        private readonly IRepository<Product> _product;
        private readonly IRepository<Shop> _shop;
        //private readonly ApplicationContext _applicationContext;
        private readonly IMemoryCache _memoryCache;

        public SaleOrderServices(
           IRepository<SaleOrder> saleOrder,
           IRepository<SaleOrderDetail> saleOrderDetail,
           IRepository<Product> product,
           IRepository<Shop> shop,
           //ApplicationContext applicationContext,
           IMemoryCache memoryCache,
           IServiceProvider scopeFactory
           )
        {
            _saleOrder = saleOrder;
            _saleOrderDetail = saleOrderDetail;
            _product = product;
            _shop = shop;
            //_applicationContext = applicationContext;
            _memoryCache = memoryCache;
            CacheUpload().Wait();
        }

        public async Task CacheUpload()
        {
            if (!_memoryCache.TryGetValue("sell_list", out object value))
            {
                value = await LoadData();
                // Вариант 1
                // Сохранение в кэш без определения времени жизни записи. Кэш является общим для всех пользователей.
                _memoryCache.Set("sell_list", value);

                // Вариант 2
                // Cохранение в кэш на 10 секунд (использование абсолютного времени устаревания). 
                //memoryCache.Set("saved_list", value, TimeSpan.FromSeconds(10));

                // Вариант 3
                // Сохранение в кэш на 5 секунд (используя скользящее время устаревания). Данные удалятся из кэш, если последнее обращение произошло более 5 секунд назад.
                //memoryCache.Set("saved_list", value, new MemoryCacheEntryOptions() { SlidingExpiration = TimeSpan.FromSeconds(5) });
            }
        }
        
        public async Task<List<SaleDTO>> LoadData()
        {
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


        public async Task<List<SaleOrder>> GetAllSaleOrder()
        {
           // await CacheUpload();
           

            return await _saleOrder.ListAllAsync();
        }

        public async Task<SaleOrder> GetSaleOrderById(int id)
        {
            return await _saleOrder.GetByIdAsync(id);
        }

        public async Task<List<SaleDTO>> GetAllSale()
        {
           //var result = await (from saleOrder in _saleOrder.ListAll()
           //              join detail in _saleOrderDetail.ListAll()
           //                  on saleOrder.SaleOrderId equals detail.SaleOrderId.GetValueOrDefault()
           //              join product in _product.ListAll()
           //               on detail.ProductId equals product.ProductId
           //              join shop in _shop.ListAll()
           //                  on saleOrder.ShopId equals shop.ShopId
           //              select new SaleDTO
           //     {
           //         SaleOrderID = saleOrder.SaleOrderId,
           //         SaleOrderDetailId = detail.SaleOrderDetailId,
           //         ProductId = product.ProductId,
           //         ProductName = product.Name,
           //         ProductDescription = product.Description,
           //         ProductBarcode = product.Barcode,
           //         Count = detail.Count,
           //         DataTime = saleOrder.DataTime,
           //         PriceCost = detail.PriceCost.GetValueOrDefault(),
           //         PriseSale = detail.PriseSale.GetValueOrDefault(),
           //         Summ = detail.Summ.GetValueOrDefault(),
           //         ShopId = saleOrder.ShopId.GetValueOrDefault(),
           //         ShopName = shop.Name,
           //         ShopAddress = shop.Address
           //     }).ToListAsync();
           var result = await AllSale();
           result.ForEach(x=> x.DisplayData = x.DataTime.ToString(CultureInfo.InvariantCulture));
            return result;
        }

        private async Task<List<SaleDTO>> AllSale()
        {
            var result =_memoryCache.Get("sell_list");
            return (List<SaleDTO>)result;
        }

        public async Task<List<SaleDTO>> MonthlySalesOfGoods()
        {
            var all = await AllSale();
            var c1 = all.Sum(x => x.Summ);
            var result = all.GroupBy(x=> new { x.DataTime.Month, x.DataTime.Year, x.ProductId}).Select(g => new SaleDTO
            {
                DisplayData = g.Key.Year.ToString() + " "+ CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key.Month), //ToString("MMMM")
                ProductName = all.FirstOrDefault(x=> x.ProductId == g.Key.ProductId)?.ProductName,
                Count = g.Sum(x=> x.Count),
                Summ = g.Sum(x=>x.Summ),
                PriceCost = all.FirstOrDefault(x => x.ProductId == g.Key.ProductId).PriceCost,
                PriseSale = all.FirstOrDefault(x => x.ProductId == g.Key.ProductId).PriseSale,
                Sort = DateTime.ParseExact((CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key.Month) + " " + g.Key.Year.ToString() ), "MMMM yyyy", CultureInfo.InvariantCulture)
            }).OrderByDescending(x=>x.Sort).ToList();
            var c2 = result.Sum(x => x.Summ);
            return result;

        }

        public async Task<List<SaleDTO>> YearlySalesOfGoods()
        {
            var all = await AllSale();
            var c1 = all.Sum(x => x.Count);
            var result = all.GroupBy(x => new { x.DataTime.Year, x.ProductId }).Select(g => new SaleDTO
            {
                DisplayData = g.Key.Year.ToString(), //ToString("MMMM")
                ProductName = all.FirstOrDefault(x => x.ProductId == g.Key.ProductId)?.ProductName,
                Count = g.Sum(x => x.Count),
                Summ = g.Sum(x => x.Summ),
                PriceCost = all.FirstOrDefault(x => x.ProductId == g.Key.ProductId).PriceCost,
                PriseSale = all.FirstOrDefault(x => x.ProductId == g.Key.ProductId).PriseSale

            }).OrderBy(x => x.DataTime).ToList();
            var c2 = result.Sum(x => x.Summ);
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

        public async Task<DataServiceMessage> SellGoods(List<SellDto> sellDtos)
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
                //if (isProcedureExist)
                //{
                //    string sql = @"exec spAddSell @Count, @PriceCost, @PriseSale, @ProductId, @SaleOrderId, @Summ";
                //    foreach (var el in saleOrderDetailCreate)
                //    {
                //        var replacements = new Dictionary<string, string> { { "@Count", el.Count.ToString() }, { "@PriceCost", el.PriceCost.ToString() }, { "@PriseSale", el.PriseSale.ToString() }, { "@ProductId" , el.ProductId.ToString() }, { "@SaleOrderId", el.SaleOrderId.ToString() }, { "@Summ", el.Summ.ToString() } };

                //        var output = replacements.Aggregate(sql, (current, replacement) => current.Replace(replacement.Key, replacement.Value));
                //        _applicationContext.Database.ExecuteSqlCommand(output);
                //    }
                //}
                //else
                //{
                    await _saleOrderDetail.AddRangeAsync(saleOrderDetailCreate);
                //}

                _memoryCache.Remove("sell_list");
                await CacheUpload();
            }
            catch (Exception e)
            {
                return new DataServiceMessage
                {
                    Result = false,
                    MainMessage = e.Message
                };
            }

            return new DataServiceMessage
            {
                Result = true,
                MainMessage = GoodResponses.AddedSuccessfully
            }; 
        }

        public async Task<ActionResult<SaleOrder>> GetSaleOrder(int id)
        {
            var saleOrder = await _saleOrder.GetByIdAsync(id);
            return saleOrder;
        }

    }
}
