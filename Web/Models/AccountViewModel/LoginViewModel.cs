using System.ComponentModel.DataAnnotations;

namespace TheGodfatherGM.Web.Models.AccountViewModel
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }

        [Required]
        public bool RememberMe { get; set; }

    }
}
