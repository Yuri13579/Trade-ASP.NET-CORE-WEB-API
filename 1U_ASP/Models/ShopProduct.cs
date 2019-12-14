using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dap1U.Models;

namespace _1U_ASP.Models
{
    public class ShopProduct
    {
        public int ShopProductId { get; set; }
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public bool DELETED { get; set; }
        public int? userActionId { get; set; }


    }
}
