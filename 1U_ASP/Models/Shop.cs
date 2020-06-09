using System.Collections.Generic;
using _1U_ASP.Repositorys;

namespace _1U_ASP.Models
{
    public class Shop : BaseEntity
    {
      //  [Key]
      public Shop()
      {
          SaleOrders = new HashSet<SaleOrder>();
      }


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
