using System.Security;

namespace Apollo.Sample.Model.Business.Services
{
    public interface IAuthenticationService
    {
        User Login(string username, SecureString password);
        void Logout();
        bool IsLoggedIn();
        User GetLoggedUser();
        void Register(string email, string username, SecureString password);
    }
}