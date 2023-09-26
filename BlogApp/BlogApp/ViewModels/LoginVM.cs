using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels
{
    public class LoginVM
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
        public bool RememberMe { get; set; }

    }
}
