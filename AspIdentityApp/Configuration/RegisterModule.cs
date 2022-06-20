using AspIdentityApp.Core;
using AspIdentityApp.Services;
using Autofac;

namespace AspIdentityApp.Configuration
{
    public class RegisterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {   
            builder.RegisterType<AuthService>().As<IAuthService>()
                .InstancePerLifetimeScope();
        }
    }
}
