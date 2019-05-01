using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LockServerAPI
{
    public class AuthOptions
    {
        public string ISSUER { get; set; }
        public string AUDIENCE { get; set; }
        public int LIFETIME { get; set; }

        private string KEY;

        public AuthOptions(IConfiguration Configuration)
        {
            var configutationSection = Configuration.GetSection("AppSettings");
            ISSUER = configutationSection.GetValue<string>("ISSUER");
            AUDIENCE = configutationSection.GetValue<string>("AUDIENCE");
            LIFETIME = configutationSection.GetValue<int>("LIFETIME");
            KEY = configutationSection.GetValue<string>("KEY");
        }

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
