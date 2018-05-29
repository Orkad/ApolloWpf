using ApolloWpf.Model;
using ApolloWpfCore.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ApolloWpf.Business
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetAll();
        }

        public IEnumerable<User> GetUsersWithGroup()
        {
            return _userRepository.Query(null, nameof(User.Group));
        }

        public User GetUserByName(string name)
        {
            return _userRepository.GetAll().FirstOrDefault(user => user.Username == name);
        }
        
        public void AddUser(User user)
        {
            _userRepository.Insert(user);
        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }

        public void DeleteUser(User user)
        {
            _userRepository.Delete(user.Id);
        }
    }
}
