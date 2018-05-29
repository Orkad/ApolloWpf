using ApolloWpf.Model;
using System.Collections.Generic;

namespace ApolloWpf.Business
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
        IEnumerable<User> GetUsersWithGroup();
        User GetUserByName(string name);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}