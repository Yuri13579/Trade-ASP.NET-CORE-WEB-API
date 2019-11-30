using _1U_ASP.Models;
using Dap1U.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dap1U.Models
{
    public class Product 
    {
       [Key]
       public int ProductId { get; set;}
       public double Barcode { get; set; }
       public string Name { get; set; }
       public string Description { get; set; }
       public bool DELETED { get; set; }
       public int userActionId { get; set; }

       public ICollection<DocEnterProduct> DocEnterProducts { get; set; }
       public ICollection<SaleOrderDetail> SaleOrderDetails { get; set; }
       public ICollection<SalePriseDoc> SalePriseDocs { get; set; }
    }
}
