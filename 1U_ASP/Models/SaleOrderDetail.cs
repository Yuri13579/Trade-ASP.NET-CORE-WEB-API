using Dap1U.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _1U_ASP.Models
{
    public class SaleOrderDetail
    {
    //    [Key, ForeignKey("SaleOrder")]
        public int SaleOrderDetailId { get; set; }
        public int? SaleOrderId { get; set; }
        public SaleOrder SaleOrder { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }
        public double PriceCost { get; set; }
        public double PriseSale { get; set; }
        public double Summ { get; set; }
        public bool Deleted { get; set; }
        public int? UserActionId { get; set; }



    }
}
