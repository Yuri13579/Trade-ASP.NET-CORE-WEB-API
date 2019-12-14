using Dap1U.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _1U_ASP.Models
{
    public class Shop
    {
      //  [Key]
        public int ShopId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool Deleted { get; set; }
        public int? UserActionId { get; set; }
  
        public ICollection<ShopBalanceGood> ShopBalanceGood { get; set; }
        public ICollection<SaleOrder> SaleOrders { get; set; }
        public ICollection<ShopProduct> ShopProducts { get; set; }
    }
}
