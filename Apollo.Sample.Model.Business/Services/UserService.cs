using System.Collections.Generic;
using System.Linq;

namespace Apollo.Sample.Model.Business.Services
{
    public class UserService : IUserService
    {
        public static List<User> _userStorage => new List<User>()
        {
            new User(){Id = 3, Username = "admin", LastName = "Administrateur", FirstName = "Administrateur"},
            new User(){Id = 1, Username = "nicolas.gidon", LastName = "Gidon", FirstName = "Nicolas"}
        };

        public IEnumerable<User> GetUsers()
        {
            return _userStorage;
        }

        public User GetUserByName(string name)
        {
            return _userStorage.FirstOrDefault(user => user.Username == name);
        }

        public void AddUser(User user)
        {
            _userStorage.Add(user);
        }

        public void UpdateUser(User user)
        {
            _userStorage.RemoveAll(u => u.Id == user.Id);
            _userStorage.Add(user);
        }

        public void DeleteUser(int id)
        {
            _userStorage.RemoveAll(u => u.Id == id);
        }
    }
}