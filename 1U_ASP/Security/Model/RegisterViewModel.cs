using System.ComponentModel.DataAnnotations;
using _1U_ASP.Const;

namespace _1U_ASP.Security.Model
{
    public class RegisterViewModel
    {
        [StringLength(NumericConstants.Num40, ErrorMessage = nameof(FirstName) + Attributes.ShouldBeShorter)]
        [Required(ErrorMessage = Attributes.FirstnameIsRequired)]
        public string FirstName { get; set; }
        [StringLength(NumericConstants.Num40, ErrorMessage = nameof(LastName) + Attributes.ShouldBeShorter)]
        [Required(ErrorMessage = Attributes.LastnameIsRequired)]
        public string LastName { get; set; }

        [StringLength(NumericConstants.Num50, ErrorMessage = nameof(Email) + Attributes.ShouldBeShorter)]
        [Required(ErrorMessage = Attributes.EmailIsRequired)]
        public string Email { get; set; }
        [Required(ErrorMessage = Attributes.PasswordIsRequired)]
        public string Password { get; set; }

        [Required(ErrorMessage = Attributes.ConfirmPasswordIsRequired)]
        public string ConfirmPassword { get; set; }

        [StringLength(NumericConstants.Num100, ErrorMessage = nameof(MyRole) + Attributes.ShouldBeShorter)]
        public string MyRole { get; set; }
    }
}
