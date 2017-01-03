
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using FlowerShopLibrary.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace FlowerShop.Core.Identity
{
    // Configure the application sign-in manager which is used in this application.
    public class IdentitySignInManager : SignInManager<User, int>
    {
        public IdentitySignInManager(IdentityUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return user.GenerateUserIdentityAsync((IdentityUserManager)UserManager);
        }

        public static IdentitySignInManager Create(IdentityFactoryOptions<IdentitySignInManager> options, IOwinContext context)
        {
            return new IdentitySignInManager(context.GetUserManager<IdentityUserManager>(), context.Authentication);
        }
    }
}