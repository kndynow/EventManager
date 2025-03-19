using System.ComponentModel.DataAnnotations;

namespace EventManager.Client.Models.SignUp
{
    public class SignUpModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required")]
        public string? Username { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Repeat password for verification")]
        public string? RepeatPassword { get; set; }
    }
}
