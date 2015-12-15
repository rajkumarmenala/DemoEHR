using Microsoft.AspNet.Authentication;

namespace Monad.EHR.Web.App.Policies
{
    public class TokenAuthOptions : AuthenticationOptions
    {
        public const string Scheme = "TokenAuth";
        public TokenAuthOptions()
        {
            AuthenticationScheme = Scheme;
            AutomaticAuthenticate = true;
        }
    }
}
