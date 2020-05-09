using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

       public SaleOrderServices(
           IRepository<SaleOrder> saleOrder,
           IRepository<SaleOrderDetail> saleOrderDetail,
           IRepository<Product> product
             ,IRepository<Shop> shop
           )
        {
            _saleOrder = saleOrder;
            _saleOrderDetail = saleOrderDetail;
            _product = product;
            _shop = shop;
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
                    PriceCost = detail.PriceCost,
                    PriseSale = detail.PriseSale,
                    Summ = detail.Summ,
                    ShopId = saleOrder.ShopId.GetValueOrDefault(),
                    ShopName = shop.Name,
                    ShopAddress = shop.Address
                }).ToListAsync();
            return result;
        }

        public async Task<string> SellGoods(SellDto sellDto)
        {
            try
            {
             //   int last = _saleOrder.ListAll().LastOrDefault().SaleOrderId;

                var newSaleOrder = await _saleOrder.AddAsync(new SaleOrder {DataTime = DateTime.Now, ShopId = sellDto.ShopId });
                //List<SaleOrderDetail> saleOrderDetailCreate = sellDtos.ConvertAll(
                //    x => new SaleOrderDetail
                //    {
                //        Count = x.Count,
                //        Deleted = false,
                //        PriceCost = x.PriceCost,
                //        PriseSale = x.PriseSale,
                //        ProductId = x.ProductId,
                //        SaleOrderId = 1,// newSaleOrder.SaleOrderID,
                //        Summ = x.PriseSale *  x.Count
                //    });
                //await _saleOrderDetail.AddRangeAsync(saleOrderDetailCreate);
                var currentProduct = _product.GetById(sellDto.ProductId);
                await _saleOrderDetail.AddAsync(new SaleOrderDetail
                {
                    Count = sellDto.Count,
                    Deleted = false,
                    PriceCost = currentProduct.PriceCost,
                    PriseSale = currentProduct.PriseSale,
                    ProductId = sellDto.ProductId,
                    SaleOrderId = newSaleOrder.SaleOrderId,
                    Summ = currentProduct.PriseSale * sellDto.Count
                });
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return "Updated"; 
        }
    }
}
