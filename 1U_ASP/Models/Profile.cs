using _1U_ASP.Repositorys;

namespace _1U_ASP.Models
{
    public class Profile : BaseEntity
    {
        public int ProfileId { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int ProfileTypeSysCodeUniqueId { get; set; }
        public string ProfileTitle { get; set; }
        public bool? Status { get; set; }
        public int? UserActionId { get; set; }
        public bool Deleted { get; set; }
    }
}
