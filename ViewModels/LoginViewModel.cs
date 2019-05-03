using System.ComponentModel.DataAnnotations;

namespace LockServerAPI.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is not specified")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is not specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
