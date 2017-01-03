using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopLibrary.Models
{
    public class UserClaim : IdentityUserClaim<int>
    {
    }

    public class UserLogin : IdentityUserLogin<int>
    {
    }

    public class UserRole : IdentityUserRole<int>
    {
        public Role Role { get; set; }
        public User User { get; set; }
    }

    public class Role : IdentityRole<int, UserRole>
    {
        public Role() : base()
        {
        }

        public Role(string name) : base()
        {
            this.Name = name;
        }
    }

    public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>
    {
       // public int Id { get; set; }

        //public string UserName { get; set; }

       // public string UserSurname { get; set; }

     //   public string Password { get; set; }

        public DateTime UserCreated { get; set; }

        public DateTime? UserModified { get; set; }

        public bool UserIsDeleted { get; set; }

        public User() : base(){}

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager)
        {

            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);


            // Add custom user claims here
            var roleClaim = Claims.FirstOrDefault(c => c.ClaimType == "Roles");
            if (roleClaim != null)
                userIdentity.AddClaim(new Claim(roleClaim.ClaimType, roleClaim.ClaimValue));

            return userIdentity;
        }
    }


}