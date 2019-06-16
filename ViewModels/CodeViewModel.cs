using System.ComponentModel.DataAnnotations;

namespace LockServerAPI.ViewModels
{
    public class CodeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Lock identifier is not specified")]
        public string LockId { get; set; }

        [Required(ErrorMessage = "Connection configuration is not specified")]
        public string Config { get; set; }
    }
}
