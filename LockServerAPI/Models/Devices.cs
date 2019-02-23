namespace LockServerAPI.Models
{
    public partial class Devices
    {
        public string Id { get; set; }
        public int? UserId { get; set; }

        public Users User { get; set; }
    }
}
