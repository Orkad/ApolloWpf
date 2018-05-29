using ApolloWpf.Model;
using ApolloWpfCore.Interfaces;
using GalaSoft.MvvmLight.Ioc;

namespace ApolloWpf.Repository
{
    public static class RepositoryIoc
    {
        public static void Register()
        {
            SimpleIoc.Default.Register<IRepository<User>, UserRepository>();
            SimpleIoc.Default.Register<IRepository<UserGroup>, UserGroupRepository>();
        }
    }
}
