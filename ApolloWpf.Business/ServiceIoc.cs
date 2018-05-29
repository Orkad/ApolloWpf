using ApolloWpf.Repository;
using GalaSoft.MvvmLight.Ioc;

namespace ApolloWpf.Business
{
    public static class ServiceIoc
    {
        public static void Register()
        {
            SimpleIoc.Default.Register<IAuthenticationService, AuthenticationService>();
            SimpleIoc.Default.Register<IUserService, UserService>();
            SimpleIoc.Default.Register<IUserGroupService, UserGroupService>();

            RepositoryIoc.Register();
        }
    }
}
