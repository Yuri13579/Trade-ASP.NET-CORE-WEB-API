using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1U_ASP.Models;
using Dap1U.Models;

namespace _1U_ASP.DTO
{
    public class DocEnterProductDetailDto
    {
        public int DocEnterProductDetailId { get; set; }
        public int DocEnterProductId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Count { get; set; }
        public float InPrise { get; set; }
        public double Summ { get; set; }
    }
}
