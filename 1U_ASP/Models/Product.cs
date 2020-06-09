using _1U_ASP.Models;
using System.Collections.Generic;
using _1U_ASP.Repositorys;

namespace Dap1U.Models
{
    public class Product : BaseEntity
    {
     //  [Key]
       public int ProductId { get; set;}
       public double Barcode { get; set; }
       public string Name { get; set; }
       public string Description { get; set; }
       public int CategoryId { get; set; }
       public double PriceCost { get; set; }
       public double PriseSale { get; set; }
       public bool Deleted { get; set; }
       public int? UserActionId { get; set; }


       public ICollection<ShopBalanceGood> ShopBalanceGood { get; set; }
       public ICollection<DocEnterProductDetail> DocEnterProductDetail { get; set; }
       public ICollection<SaleOrderDetail> SaleOrderDetails { get; set; }
       public ICollection<SalePriseDoc> SalePriseDocs { get; set; }
       public ICollection<ShopProduct> ShopProducts { get; set; }
    }
}
