using TestApp.Core;
using TestApp.Services;

namespace TestApp.Configuration
{
    public class RegisterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SearchService>().As<ISearchService>()
                .InstancePerLifetimeScope();
        }
    }
}
