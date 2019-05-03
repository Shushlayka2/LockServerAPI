using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LockServerAPI.Models.Code
{
    public partial class Code
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CodeVal { get; set; }
        public string LockId { get; set; }

        public void GenerateSecretCode()
        {
            var random = new Random();
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var result = new StringBuilder(11);
            for (int i = 0; i < 11; i++)
            {
                if (i == 3 || i == 7)
                {
                    result.Append('-');
                }
                else
                {
                    result.Append(characters[random.Next(characters.Length)]);
                }
            }
            CodeVal = result.ToString();
        }

        public void GenerateLockId()
        {
            LockId = Guid.NewGuid().ToString();
        }
    }
}
