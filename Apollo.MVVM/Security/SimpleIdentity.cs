using System.Security;
using System.Security.Principal;

namespace Apollo.MVVM.Security
{
    /// <summary>
    /// Defini l'identité d'un utilisateur pour l'application
    /// </summary>
    public class SimpleIdentity : IIdentity
    {
        public SimpleIdentity()
        {

        }

        public SimpleIdentity(string name, string role)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new SecurityException("Impossible de donnée une identité pour un nom sans caractère");
            }
            Name = name;
            Role = role;
        }

        public string AuthenticationType => "Simple";
        /// <summary>
        /// Ne pas correspond pas à l'authentification, simplement verrifier que l'identité est associé a un nom est n'est pas annonyme
        /// </summary>
        public bool IsAuthenticated => !string.IsNullOrWhiteSpace(Name);
        public string Name { get; }
        public string Role { get; }
    }
}
