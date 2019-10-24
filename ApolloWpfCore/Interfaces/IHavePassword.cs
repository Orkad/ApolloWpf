using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace ApolloWpfCore.Interfaces
{
    /// <summary>
    /// Stocke un mot de passe en son instance
    /// </summary>
    public interface IHavePassword
    {
        SecureString Password { get; }
    }
}
