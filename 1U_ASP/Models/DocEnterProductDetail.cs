﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Dap1U.Models;

namespace _1U_ASP.Models
{
    public class DocEnterProductDetail
    {
        [Key]
        public int DocEnterProductDetailId { get; set; }
        public int? DocEnterProductId { get; set; }
        public DocEnterProduct DocEnterProduct { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }
        public float InPrise { get; set; }
        public double Summ { get; set; }
        public bool Delete { get; set; }
        public int UserActionId { get; set; }
        
    }
}
