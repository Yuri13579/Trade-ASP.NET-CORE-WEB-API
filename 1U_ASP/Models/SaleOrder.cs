using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _1U_ASP.Models
{
    public class SaleOrder
    {
     //   [Key, ForeignKey("SaleOrderDetail")]
        public int SaleOrderID { get; set; }
        public int? ShopId { get; set; }
        public Shop Shop { get; set; }
        public DateTime DataTime { get; set; }
     //   public int? SaleOrderDetailId { get; set; }
        public SaleOrderDetail SaleOrderDetail { get; set; }
        public bool DELETED { get; set; }
        public int userActionId { get; set; }
    }
}
