using System.ComponentModel.DataAnnotations;

namespace Marvin.IDP.PasswordReset
{
    public class RequestPasswordViewModel
    {
        [Required]
        [MaxLength(200)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }   
    }
}