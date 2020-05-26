using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1U_ASP.DTO
{
    public class DataServiceMessage
    {
        public bool Result { get; set; }
        public string MainMessage { get; set; }
        public object Data { get; set; }
    }
}
