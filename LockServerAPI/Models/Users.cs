using System.Collections.Generic;

namespace LockServerAPI.Models
{
    public partial class Users
    {
        public Users()
        {
            Codes = new HashSet<Codes>();
            Devices = new HashSet<Devices>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public ICollection<Codes> Codes { get; set; }
        public ICollection<Devices> Devices { get; set; }
    }
}
