using System.Collections.Generic;

namespace Apollo.Sample.Model.Business.Services
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