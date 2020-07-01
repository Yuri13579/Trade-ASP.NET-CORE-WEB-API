using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1U_ASP.Models;

namespace _1U_ASP.DTO
{
    public class DocEnterProductDto
    {
        public int DocEnterProductId { get; set; }
        public int? ProviderId { get; set; }
        public string ProviderName { get; set; }
        public DateTime DocDate { get; set; }
    }
}
