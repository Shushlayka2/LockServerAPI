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
            Container.RegisterInstance<IConfiguration>(Configuration);
        }
    }
}
