using ApolloWpf.Model;
using ApolloWpfCore.Interfaces;
using System.Collections.Generic;

namespace ApolloWpf.Business
{
    public class UserGroupService : IUserGroupService
    {
        private readonly IRepository<UserGroup> _userGroupRepository;

        public UserGroupService(IRepository<UserGroup> userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }

        public IEnumerable<UserGroup> GetUserGroups()
        {
            return _userGroupRepository.GetAll();
        }
    }
}
