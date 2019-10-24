using ApolloWpf.Model;
using ApolloWpfCore.Extensions;
using ApolloWpfCore.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading;

namespace ApolloWpf.Business
{
    class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly List<InternalIdentityWithPassword> _users;

        public AuthenticationService(IUserService userService)
        {
            _userService = userService;
            _users = new List<InternalIdentityWithPassword>()
            {
                // DB storage
                new InternalIdentityWithPassword("Orkad", "apollo", "Utilisateur"),
                new InternalIdentityWithPassword("Apollo", "admin", "Administrateur")
            };
        }

        private class InternalIdentityWithPassword
        {
            public string Username { get; }
            private SecureString Password { get; }
            public string Role { get; }

            public InternalIdentityWithPassword(string username, string password,  string role)
            {
                Username = username;
                Role = role;
                Password = new SecureString();
                foreach (var c in password)
                {
                    Password.AppendChar(c);
                }
            }

            public bool CheckPassword(SecureString password)
            {
                return Password.IsEqualTo(password);
            }
        }

        private SimplePrincipal GetApolloPrincipal()
        {
            var principal = Thread.CurrentPrincipal as SimplePrincipal;
            if (principal == null)
                throw new SecurityException("The application's default thread principal must be set to a SimplePrincipal");
            return principal;
        }

        public User Login(string username, SecureString password)
        {
            if (IsLoggedIn())
            {
                throw new Exception("Déjà connecté");
            }
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new Exception("Le nom d'utilisateur ne peut être vide");
            }
            if (password == null || password.Length == 0)
            {
                throw new Exception("Le mot de passe ne peut être vide");
            }
            var user = _users.FirstOrDefault(d => d.Username == username && d.CheckPassword(password));
            if (user == null)
            {
                throw new Exception("Erreur de combinaison nom d'utilisateur / mot de passe");
            }
            GetApolloPrincipal().Identity = new SimpleIdentity(user.Username, user.Role);
            return GetLoggedUser();
        }

        public void Logout()
        {
            if (!IsLoggedIn())
            {
                throw new Exception("Déjà déconnecté");
            }
            GetApolloPrincipal().Identity = new SimpleIdentity();
        }

        public bool IsLoggedIn() => Thread.CurrentPrincipal.Identity.IsAuthenticated;

        public User GetLoggedUser()
        {
            if (GetApolloPrincipal() == null)
                return null;
            return _userService.GetUserByName(Thread.CurrentPrincipal.Identity.Name);
        }
    }
}