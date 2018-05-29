using ApolloWpf.Model;
using ApolloWpfCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ApolloWpf.Repository
{
    public class UserGroupRepository : IRepository<UserGroup>
    {
        public static List<UserGroup> _userGroupStorage => new List<UserGroup>
        {
            new UserGroup(){Id = 1, Name = "Administrateur"},
            new UserGroup(){Id = 2, Name = "Utilisateur"}
        };

        private int _ai = 2;

        public IEnumerable<UserGroup> GetAll()
        {
            return _userGroupStorage;
        }


        public IEnumerable<UserGroup> Query(Expression<Func<UserGroup, bool>> filter, params string[] includes)
        {
            var users = UserRepository._userStorage;
            var userGroups = _userGroupStorage;
            if (filter != null)
            {
                userGroups = _userGroupStorage.Where(filter.Compile()).ToList();
            }
            foreach (var userGroup in userGroups)
            {
                if (includes.Contains(nameof(UserGroup.Users)))
                {
                    userGroup.Users = users.Where(user => user.GroupId == userGroup.Id).ToList();
                }
            }

            return userGroups;
        }

        public UserGroup GetOne(object id)
        {
            return _userGroupStorage.FirstOrDefault(group => group.Id == (int) id);
        }

        public void Insert(UserGroup entity)
        {
            entity.Id = ++_ai;
            _userGroupStorage.Add(entity);
        }

        public void Update(UserGroup entity)
        {
            var userFound = _userGroupStorage.FirstOrDefault(user => user.Id == entity.Id);
            if (userFound == null)
            {
                throw new Exception($"Impossible de mettre a jour l'entité pour l'identifiant {entity.Id}");
            }
            var index = _userGroupStorage.IndexOf(userFound);
            _userGroupStorage[index] = entity;
        }

        public void Delete(object id)
        {
            var userFound = _userGroupStorage.FirstOrDefault(user => user.Id == (int)id);
            if (userFound == null)
            {
                throw new Exception($"Impossible de supprimer l'entité pour l'identifiant {id}");
            }
            _userGroupStorage.Remove(userFound);
        }
    }
}