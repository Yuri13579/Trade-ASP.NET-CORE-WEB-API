namespace _1U_ASP.Security.Model
{
    public class SignOutModel
    {
        public int PersonId { get; set; }
        public int DiscriptionUniqueId { get; set; }
        public string Comment { get; set; }
        public int ProfileId { get; set; }
        public bool DeleteByProfileId { get; set; }
    }
}
