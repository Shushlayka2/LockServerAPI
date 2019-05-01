using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Unity;
using Unity.Extension;

namespace LockServerAPI
{
    public class BaseContainerExtension : UnityContainerExtension
    {
        protected IConfiguration Configuration { get; }

        public BaseContainerExtension(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void Initialize()
        {
            Container.RegisterInstance(Container);
            Container.RegisterInstance(Configuration);
            Container.RegisterType<AuthOptions>(TypeLifetime.Singleton);
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            var options = optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")).Options;
            Container.RegisterInstance(new DatabaseContext(options));
        }
    }
}
