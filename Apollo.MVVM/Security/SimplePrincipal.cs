using System.Security.Principal;

namespace Apollo.MVVM.Security
{
    public class SimplePrincipal : IPrincipal
    {
        private SimpleIdentity _identity;

        public SimplePrincipal()
        {
            _identity = new SimpleIdentity();
        }

        public SimplePrincipal(SimpleIdentity identity)
        {
            _identity = identity;
        }

        public bool IsInRole(string role) => _identity.Role == role;

        public IIdentity Identity
        {
            get => _identity;
            set => _identity = value as SimpleIdentity;
        }
    }
}
