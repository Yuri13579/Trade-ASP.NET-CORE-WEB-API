using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1U_ASP.DTO;
using _1U_ASP.MiddleTier.Interface;
using _1U_ASP.Models;
using _1U_ASP.Repositorys.Interface;

namespace _1U_ASP.MiddleTier
{
    public class SaleOrderSevrices : ISaleOrderSevrice
    {
        private readonly ISaleOrderRepository _saleOrderRepository;
        private readonly ISaleOrderDetailsRepository _saleOrderDetailsRepository;
        
        public SaleOrderSevrices(ISaleOrderRepository saleOrderRepository,
            ISaleOrderDetailsRepository saleOrderDetailsRepository)
        {
            _saleOrderRepository = saleOrderRepository;
            _saleOrderDetailsRepository = saleOrderDetailsRepository;
        }

        public Task<List<SaleOrder>> GetAllSaleOrder()
        {
            return _saleOrderRepository.GetAllSaleOrders();
        }

        public Task<SaleOrder> GetSaleOrderById(int id)
        {
            return _saleOrderRepository.GetSaleOrders(id);
        }

        public  List<SaleDTO> GetAllSaleOrderWithDatails()
        {
            var result = (from saleOrder in _saleOrderRepository.GetAllSaleOrders().Result
                join detail in _saleOrderDetailsRepository.GetAllAsync().Result
                    on saleOrder.SaleOrderID equals detail.SaleOrderId.Value
                select new SaleDTO
                {
                    SaleOrderID = saleOrder.SaleOrderID,
                    Count = detail.Count,
                    DataTime = saleOrder.DataTime,
                    PriseSale = detail.PriseSale
                }).ToList();
                ;

            return result;
        }
    }
}
