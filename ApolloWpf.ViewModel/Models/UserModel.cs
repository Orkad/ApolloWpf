using ApolloWpf.Model;
using ApolloWpfCore.Abstract;

namespace ApolloWpf.ViewModel.Models
{
    public class UserModel : EditableModelBase<User>
    {
        public string Username
        {
            get => Instance.Username;
            set => Edit(() => Instance.Username = value);
        }

        public string FirstName
        {
            get => Instance.FirstName;
            set => Edit(() => Instance.FirstName = value);
        }

        public string LastName
        {
            get => Instance.LastName;
            set => Edit(() => Instance.LastName = value);
        }

        public int Age
        {
            get => Instance.Age;
            set => Edit(() => Instance.Age = value);
        }

        public int GroupId
        {
            get => Instance.GroupId;
            set => Edit(() => { Instance.GroupId = value; });
        }

        public UserModel(User obj) : base(obj)
        {
        }
    }
}
