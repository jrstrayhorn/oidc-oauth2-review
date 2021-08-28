using System.ComponentModel.DataAnnotations;

namespace Marvin.IDP.PasswordReset
{
    public class ResetPasswordViewModel
    {
        [Required]
        [MaxLength(200)]
        [DataType(DataType.Password)]
        [Display(Name = "Your new password")]
        public string Password { get; set; }

        [Required]
        [MaxLength(200)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm your new password")]
        [Compare("Password")] // this makes if don't match will get validation message!!
        public string ConfirmPassword { get; set; }

        public string SecurityCode { get; set; }
    }
}