using FlowerShopLibrary.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FlowerShop.IdentityRoles
{
    public class RoleStore : RoleStore<Role, int, UserRole>
    {
        public RoleStore(DataContext context) : base(context) { }
    }
}