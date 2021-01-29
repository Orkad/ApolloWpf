using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace Apollo.Sample.Model.Business
{
    /// <summary>
    /// Défini des constantes fonctionnelle pour l'application
    /// </summary>
    public static class BusinessRules
    {
        public const int USERNAME_MIN_LENGTH = 3;
        public const int USERNAME_MAX_LENGTH = 15;

        public const int PASSWORD_MIN_LENGTH = 5;
        public const int PASSWORD_MAX_LENGTH = 25;

        public static class Validation
        {
            public static bool ValidateUsername(string username)
                => username != null
                && username.Length >= USERNAME_MIN_LENGTH
                && username.Length <= USERNAME_MAX_LENGTH;

            public static bool ValidatePassword(SecureString password)
                => password != null
                && password.Length >= PASSWORD_MIN_LENGTH
                && password.Length <= PASSWORD_MAX_LENGTH;
        }
    }
}
