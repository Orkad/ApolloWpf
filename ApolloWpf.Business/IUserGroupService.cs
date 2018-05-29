using ApolloWpf.Model;
using System.Collections.Generic;

namespace ApolloWpf.Business
{
    public interface IUserGroupService
    {
        IEnumerable<UserGroup> GetUserGroups();
    }
}