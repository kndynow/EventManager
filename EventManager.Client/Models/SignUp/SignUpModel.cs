using System.ComponentModel.DataAnnotations;

namespace EventManager.Client.Models.SignUp
{
    public class SignUpModel
    {
        //Username
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required")]
        [MinLength(3, ErrorMessage = "Username must be at least 3 characters long")]
        [MaxLength(24, ErrorMessage = "Username cannot be longer than 16 characters")]
        public string? Username { get; set; }

        //Password
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        [MaxLength(24, ErrorMessage = "Password cannot be longer than 24 characters")]
        public string? Password { get; set; }



        [Required(AllowEmptyStrings = false, ErrorMessage = "Repeat password for verification")]
        [MinLength(6, ErrorMessage = "Repeat password for verification")]
        [MaxLength(24, ErrorMessage = "Repeat password for verification")]
        public string? RepeatPassword { get; set; }
    }
}