using ApolloWpf.Model;
using System.Collections.Generic;

namespace ApolloWpf.Business
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
        User GetUserByName(string name);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }
}
