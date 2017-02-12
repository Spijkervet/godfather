using System.ComponentModel.DataAnnotations;

namespace TheGodfatherGM.Web.Models.GameViewModel
{
    public class LoginViewModel
    {
        //[Display(Name = "Email")]
        [Required]
        //[EmailAddress]
        public string EmailAddress { get; set; }

        [Display(Name = "Password")]
        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        public bool RememberMe { get; set; }

        [Required]
        public string SocialClub { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
