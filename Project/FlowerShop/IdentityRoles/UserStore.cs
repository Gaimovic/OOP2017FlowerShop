using FlowerShopLibrary.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FlowerShop.IdentityRoles
{
    public class UserStore : UserStore<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public UserStore(DataContext context) : base(context) { }
    }
}