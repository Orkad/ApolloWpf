using ApolloWpf.Model;
using ApolloWpfCore.Interfaces;
using GalaSoft.MvvmLight.Ioc;

namespace ApolloWpf.Repository
{
    public class RepositoryInstaller : IInstaller
    {
        public void Install()
        {
            SimpleIoc.Default.Register<IRepository<User>, UserRepository>();
            SimpleIoc.Default.Register<IRepository<UserGroup>, UserGroupRepository>();
        }
    }
}
