using System;
using System.ComponentModel.DataAnnotations;
using _1U_ASP.Const;

namespace _1U_ASP.Security.Model
{
    public class RefreshToken
    {
        [Range(NumericConstants.Num1, Int32.MaxValue, ErrorMessage =
            Attributes.TheValueMustBeAPositiveNumberNotExceeding)]
        [Required(ErrorMessage = Attributes.AccountLevelIsRequired)]
        public int AccountLevel { get; set; }
        public string AccountId { get; set; }
        public string EmployerId { get; set; }
        public string ReturnUrl { get; set; }
    }
}
