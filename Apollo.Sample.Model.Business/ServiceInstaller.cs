using Apollo.MVVM.IOC;
using Apollo.Sample.Model.Business.Services;
using GalaSoft.MvvmLight.Ioc;

namespace Apollo.Sample.Model.Business
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