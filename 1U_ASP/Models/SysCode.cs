using System;
using _1U_ASP.Repositorys;

namespace _1U_ASP.Models
{
    public class SysCode : BaseEntity
    {
        public int SysCodeId { get; set; }
        public int UniqueId { get; set; }
        public string SysCode1 { get; set; }
        public string CodeId { get; set; }
        public int OrderId { get; set; }
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
        public string Comments { get; set; }
        public bool? Group1 { get; set; }
        public bool Group2 { get; set; }
        public bool Group3 { get; set; }
        public bool Group4 { get; set; }
        public bool Group5 { get; set; }
        public bool Group6 { get; set; }
        public DateTime AddDate { get; set; }
        public string AddUser { get; set; }
        public DateTime ChangeDate { get; set; }
        public string ChangeUser { get; set; }
        public bool Deleted { get; set; }
    }
}
