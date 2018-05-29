using ApolloWpf.Business;
using ApolloWpf.Model;
using ApolloWpf.ViewModel.Models;
using ApolloWpfCore.Abstract;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ApolloWpf.ViewModel
{
    public class UsersViewModel : NavigableViewModelBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
        private ObservableCollection<UserModel> _users;
        private UserModel _selectedUser;
        private ObservableCollection<UserGroup> _userGroups;

        public UsersViewModel(INavigationService navigationService, 
            IDialogService dialogService,
            IUserService userService,
            IUserGroupService userGroupService,
            IAuthenticationService authenticationService) : base(navigationService, dialogService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
            if (IsInDesignMode)
            {
                
            }
            else
            {
                Users = new ObservableCollection<UserModel>(GetAllUserModels());
                foreach (var userModel in Users)
                {
                    userModel.OnEndEdit += _userService.UpdateUser;
                }
                UserGroups = new ObservableCollection<UserGroup>(userGroupService.GetUserGroups());
            }

            AddUserCommand = new RelayCommand(AddUser);
            RemoveUserCommand = new RelayCommand(RemoveUser);
        }

        protected override async Task<bool> ValidateNavigation()
        {
            var anyUserModified = true;
            foreach (var userModel in Users)
            {
                if (userModel.IsModified)
                {
                    anyUserModified = false;
                }
            }

            if (!anyUserModified)
            {
                if (await AskYesOrNoAsync("Souhaitez vous enregistrer vos modification", "Modifications en cours"))
                {
                    foreach (var user in Users.Where(u => u.IsModified))
                    {
                        user.EndEdit();
                    }
                }
                else
                {
                    foreach (var user in Users.Where(u => u.IsModified))
                    {
                        user.CancelEdit();
                    }
                }
            }
            return true;
        }

        public override void OnBack()
        {
            _authenticationService.Logout();
        }


        public ObservableCollection<UserModel> Users
        {
            get => _users;
            set => Set(ref _users, value);
        }
        public UserModel SelectedUser
        {
            get => _selectedUser;
            set => Set(ref _selectedUser, value);
        }

        public ICommand RemoveUserCommand { get; }
        public ICommand AddUserCommand { get; }

        public ObservableCollection<UserGroup> UserGroups
        {
            get => _userGroups;
            set => Set(ref _userGroups, value);
        }

        private void AddUser()
        {
            var newUser = new UserModel(new User(){Username = "NewUser"});
            Users.Add(newUser);
            newUser.OnEndEdit += _userService.AddUser;
            newUser.OnCancelEdit += user =>
            {
                Users.Remove(newUser);
            };
            newUser.OnEndEdit += user =>
            {
                // Lorsque l'objet est enregistré une première fois les sauvegardes suivantes sont des mise a jour de l'objet
                newUser.OnEndEdit -= _userService.AddUser;
                newUser.OnEndEdit += _userService.UpdateUser;
            };
            SelectedUser = newUser;
            SelectedUser.BeginEdit();
        }

        private void RemoveUser()
        {
            Users.Remove(SelectedUser);
        }

        private IEnumerable<UserModel> GetAllUserModels()
        {
            return _userService.GetUsersWithGroup().Select(user => new UserModel(user));
        }
    }
}
