using ApolloWpf.Model;
using System.Security;

namespace ApolloWpf.Business
{
    public interface IAuthenticationService
    {
        User Login(string username, SecureString password);
        void Logout();
        bool IsLoggedIn();
        User GetLoggedUser();
    }
}