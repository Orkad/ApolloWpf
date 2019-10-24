using GalaSoft.MvvmLight.Ioc;
using ApolloWpfCore.Interfaces;
using ApolloWpf.Model;

namespace ApolloWpf.Business
{
    public class ServiceInstaller : IInstaller
    {
        public void Install()
        {
            SimpleIoc.Default.Register<IAuthenticationService, AuthenticationService>();
            SimpleIoc.Default.Register<IUserService, UserService>();
        }
    }
}
