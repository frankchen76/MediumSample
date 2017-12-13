using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;

namespace BCSOData.ProductV3.Filters
{
    public class IdentityBasicAuthenticationAttribute : BasicAuthenticationAttribute
    {
        protected override async Task<IPrincipal> AuthenticateAsync(string userName, string password, CancellationToken cancellationToken)
        {
            return await Task.Run<IPrincipal>(() => 
            {
                //simply create a GenericIdentity object and GenericPrincipal to return. 
                GenericIdentity id = new GenericIdentity(userName);
                GenericPrincipal principal = new GenericPrincipal(id, null);
                return principal;
            });
            //UserManager<IdentityUser> userManager = CreateUserManager();

            //cancellationToken.ThrowIfCancellationRequested(); // Unfortunately, UserManager doesn't support CancellationTokens.
            //IdentityUser user = await userManager.FindAsync(userName, password);

            //if (user == null)
            //{
            //    // No user with userName/password exists.
            //    return null;
            //}

            // Create a ClaimsIdentity with all the claims for this user.
            //cancellationToken.ThrowIfCancellationRequested(); // Unfortunately, IClaimsIdenityFactory doesn't support CancellationTokens.
            //ClaimsIdentity identity = await userManager.ClaimsIdentityFactory.CreateAsync(userManager, user, "Basic");
            //return new ClaimsPrincipal(identity);
        }

        //private static UserManager<IdentityUser> CreateUserManager()
        //{
        //    return new UserManager<IdentityUser>(new UserStore<IdentityUser>(new UsersDbContext()));
        //}
    }
}