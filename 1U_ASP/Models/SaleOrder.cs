using System;
using System.Collections.Generic;
using _1U_ASP.Repositorys;

namespace _1U_ASP.Models
{
    public class SaleOrder : BaseEntity
    {
        public int SaleOrderId { get; set; }
        public int? ShopId { get; set; }
        public virtual Shop Shop { get; set; }
        public DateTime DataTime { get; set; }
        public bool Deleted { get; set; }
        public int? UserActionId { get; set; }

        public ICollection<SaleOrderDetail> SaleOrderDetails { get; set; }
    }
}
