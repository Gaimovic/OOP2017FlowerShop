using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace FlowerShop.Util
{
    public class ClaimsAuthorizeAttribute : AuthorizeAttribute
    {
        public string MyRole { get; set; }
         
        
        
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            ClaimsIdentity claimsIdentity;
            if (!httpContext.User.Identity.IsAuthenticated)
                return false;

            claimsIdentity = httpContext.User.Identity as ClaimsIdentity;
            var roleClaims = claimsIdentity.FindFirst("Roles");
            if (roleClaims.Value == null)
                return false;

            //int year; // получаем год
            //if (!Int32.TryParse(yearClaims.Value, out year))
            //    return false;

            // проверяем возраст относительно текущей даты
            //if ((DateTime.Now.Year - year) < this.Age)
            //    return false;
            //string role = roleClaims.Value.ToString();
            //if (role !=  this.Roles.ToString())
            //    return false;

            string role = roleClaims.Value;
            if (role != this.MyRole.ToString())
                return false;

            // обращаемся к методу базового класса
            return base.AuthorizeCore(httpContext);
        }
    }
}