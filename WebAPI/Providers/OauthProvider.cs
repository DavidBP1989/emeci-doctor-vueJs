using Microsoft.Owin.Security.OAuth;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Providers
{
    public class OauthProvider: OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() => context.Validated());
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            using (var db = new EmeciEntities())
            {
                var q = (from r in db.Registro
                         join m in db.Medico on r.idRegistro equals m.IdRegistro
                         where
                         r.Emeci == context.UserName &&
                         r.clave == context.Password &&
                         r.Status == "V"
                         select new
                         {
                             r.Emeci,
                             m.Idmedico
                         }).FirstOrDefault();
                if (q != null)
                {
                    context.Response.Headers.Add("Access-Control-Expose-Headers", new[] { "dId" });
                    context.Response.Headers.Add("dId", new[] { q.Idmedico.ToString() });

                    identity.AddClaim(new Claim(ClaimTypes.Role, "User"));
                    identity.AddClaim(new Claim(ClaimTypes.Name, q.Emeci));
                    identity.AddClaim(new Claim("LoggedOn", DateTime.Now.ToString()));

                    await Task.Run(() => context.Validated(identity));
                }
                else context.SetError("Wrong credentials", "Provided username or password is incorrect");
                return;
            }
        }
    }
}