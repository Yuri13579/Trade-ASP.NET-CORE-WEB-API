using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Dap1U.Models;

namespace _1U_ASP.Models
{
    public class DocEnterProduct
    {
        [Key]
        public int DocEnterProductId { get; set; }
        //public int? ProductId { get; set; }
        //public Product Product { get; set; }
        public int? ProviderId { get; set; }
        public Provider Provider { get; set; }
        public bool Delete { get; set; }
        public int UserActionId { get; set; }
        // public DocEnterProductDetail DocEnterProductDetail { get; set; }

        public ICollection<ShopBalanceGood> ShopBalanceGood { get; set; }
        public ICollection<DocEnterProductDetail> DocEnterProductDetails { get; set; }
    }
}
