using ApolloWpf.Model;
using ApolloWpfCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ApolloWpf.Repository
{
    public class UserRepository : IRepository<User>
    {
        public static List<User> _userStorage => new List<User>()
        {
            new User(){Id = 1, Username = "Apollo", FirstName = "Apollo", LastName = "Ssc", Age = 20, GroupId = 2},
            new User(){Id = 2, Username = "Orkad", FirstName = "Nicolas", LastName = "Gidon", Age = 25, GroupId = 1},
        };

        private int _ai { get; set; } = 2;

        public IEnumerable<User> GetAll()
        {
            return _userStorage;
        }

        public IEnumerable<User> Query(Expression<Func<User, bool>> filter, params string[] includes)
        {
            var groups = UserGroupRepository._userGroupStorage;
            var users = _userStorage;
            if (filter != null)
            {
                users = users.Where(filter.Compile()).ToList();
            }
            foreach (var user in users)
            {
                if (includes.Contains(nameof(User.Group)))
                {
                    user.Group = groups.FirstOrDefault(g => g.Id == user.GroupId);
                }
            }

            return users;
        }

        public User GetOne(object id)
        {
            return _userStorage.FirstOrDefault(user => user.Id == (int) id);
        }

        public void Insert(User entity)
        {
            entity.Id = ++_ai;
            _userStorage.Add(entity);
        }

        public void Update(User entity)
        {
            var userFound = _userStorage.FirstOrDefault(user => user.Id == entity.Id);
            if (userFound == null)
            {
                throw new Exception($"Impossible de mettre a jour l'entité pour l'identifiant {entity.Id}");
            }
            var index = _userStorage.FindIndex(user => user.Id == entity.Id);
            _userStorage[index] = entity;
        }

        public void Delete(object id)
        {
            var userFound = _userStorage.FirstOrDefault(user => user.Id == (int)id);
            if (userFound == null)
            {
                throw new Exception($"Impossible de supprimer l'entité pour l'identifiant {id}");
            }
            _userStorage.Remove(userFound);
        }
    }
}
