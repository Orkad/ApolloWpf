﻿using Apollo.MVVM.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Principal;
using System.Threading;

namespace Apollo.Sample.Model.Business.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly List<InternalIdentityWithPassword> _users;

        public AuthenticationService(IUserService userService)
        {
            _userService = userService;
            _users = new List<InternalIdentityWithPassword>()
            {
                new InternalIdentityWithPassword(){
                    Email = null,
                    Username = "admin"
                }.WithClearPassword("admin"),
                new InternalIdentityWithPassword
                {
                    Username = "nicolas.gidon",
                }.WithClearPassword("nicholas")
            };
        }

        private class InternalIdentityWithPassword
        {
            public string Email { get; set; } // GUID
            public string Username { get; set; }
            public SecureString Password { get; set; }
            public string Role { get; set; }

            public bool CheckPassword(SecureString password)
            {
                return Password.IsEqualTo(password);
            }

            public InternalIdentityWithPassword WithClearPassword(string password)
            {
                Password = new SecureString();
                foreach (var c in password)
                {
                    Password.AppendChar(c);
                }
                return this;
            }
        }

        public User Login(string username, SecureString password)
        {
            if (IsLoggedIn())
            {
                throw new Exception("Déjà connecté");
            }
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new BusinessException("Le nom d'utilisateur ne peut être vide");
            }
            if (password == null || password.Length == 0)
            {
                throw new BusinessException("Le mot de passe ne peut être vide");
            }
            var user = _users.FirstOrDefault(d => d.Username == username && d.CheckPassword(password));
            if (user == null)
            {
                throw new BusinessException("Erreur de combinaison nom d'utilisateur / mot de passe");
            }
            Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username, "password"), new[] { "user" });
            //GetApolloPrincipal().Identity = new SimpleIdentity(user.Username, user.Role);
            return GetLoggedUser();
        }

        public void Logout()
        {
            if (!IsLoggedIn())
            {
                throw new BusinessException("Déjà déconnecté");
            }
            Thread.CurrentPrincipal = null;
        }

        public bool IsLoggedIn() => Thread.CurrentPrincipal.Identity.IsAuthenticated;

        public User GetLoggedUser()
        {
            if (!IsLoggedIn())
                return null;
            return _userService.GetUserByName(Thread.CurrentPrincipal.Identity.Name);
        }

        public void Register(string email, string username, SecureString password)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new BusinessException("L'email est obligatoire");
            }
            if (!email.IsEmail())
            {
                throw new BusinessException("Le format de l'email renseigné est incorrect");
            }
            if (username != null && username.Length < 4)
            {
                throw new BusinessException("Le nom d'utilisateur est obligatoire et doit faire 4 caractères minimum");
            }
            if (password.Length < 5)
            {
                throw new BusinessException("Le mot de passe doit faire 5 caractères minimum");
            }
            _users.Add(new InternalIdentityWithPassword()
            {
                Email = email,
                Username = username,
                Password = password,
                Role = "utilisateur"
            });
        }
    }
}