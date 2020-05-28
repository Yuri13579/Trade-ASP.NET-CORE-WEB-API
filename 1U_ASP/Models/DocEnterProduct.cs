using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _1U_ASP.Repositorys;

namespace _1U_ASP.Models
{
    public class DocEnterProduct : BaseEntity
    {
        [Key]
        public int DocEnterProductId { get; set; }
        //public int? ProductId { get; set; }
        //public Product Product { get; set; }
        public int? ProviderId { get; set; }
        public Provider Provider { get; set; }
        public bool Deleted { get; set; }
        public int? UserActionId { get; set; }
        // public DocEnterProductDetail DocEnterProductDetail { get; set; }

        public ICollection<ShopBalanceGood> ShopBalanceGood { get; set; }
        public ICollection<DocEnterProductDetail> DocEnterProductDetails { get; set; }
    }
}
