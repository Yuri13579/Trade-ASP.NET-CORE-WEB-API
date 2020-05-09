using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using _1U_ASP.Repositorys;
using Microsoft.AspNetCore.Identity;

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
