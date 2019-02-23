namespace LockServerAPI.Models
{
    public partial class Codes
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int? UserId { get; set; }

        public Users User { get; set; }
    }
}
