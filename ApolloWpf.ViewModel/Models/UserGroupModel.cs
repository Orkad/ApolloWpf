using ApolloWpf.Model;
using ApolloWpfCore.Abstract;

namespace ApolloWpf.ViewModel.Models
{
    public class UserGroupModel : EditableModelBase<UserGroup>
    {
        public UserGroupModel(UserGroup obj) : base(obj)
        {
        }

        public string Name
        {
            get => Instance.Name;
            set => Edit(() => Instance.Name = value);
        }
    }
}
