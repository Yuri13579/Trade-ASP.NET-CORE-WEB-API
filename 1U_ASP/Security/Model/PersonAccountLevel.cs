using System.Collections.Generic;

namespace _1U_ASP.Security.Model
{
    public class PersonAccountLevel 
    {
        public string LevelName { get; set; }
        public int LevelCode { get; set; }
        public bool IsSet { get; set; }
        public List<CompanyEmployerProperty> CompanyEmployers { get; set; }
    }

    public class PersonAccountLevelDto
    {
        public List<PersonAccountLevel> Levels { get; set; }
    }

    public class CompanyEmployerProperty
    {
        public string AccountId { get; set; }
        public string CompanyName { get; set; }
        public string EmployerId { get; set; }
        public string EmployerName { get; set; }
        public bool IsSet { get; set; }
    }

    public class ApplyManager
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
