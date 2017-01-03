using FlowerShop.Core.Identity;
using FlowerShop.IdentityRoles;
using FlowerShopLibrary.Models;
using FlowerShopLibrary.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using SimpleInjector;
using SimpleInjector.Advanced;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace FlowerShop.App_Start
{
    public class SimpleInjectorConfig
    {
        public static void RegisterDependencyInjection()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();


            //This is how dependency injection is setup when multiple interfaces are inhereted by the same class.
            //var emailServiceRegistration = Lifestyle.Scoped.CreateRegistration(typeof(EmailService), container);
            //container.AddRegistration(typeof(IIdentityEmailService), emailServiceRegistration);
            //container.AddRegistration(typeof(IEmailService), emailServiceRegistration);
            container.Register<IdentitySignInManager>(Lifestyle.Scoped);
            container.Register<IdentityUserManager>(Lifestyle.Scoped);
            container.Register<IdentityRoleManager>(Lifestyle.Scoped);

            //Asp.Net Identity internal dependencies
            container.Register(() =>
                AdvancedExtensions.IsVerifying(container)
                ? new OwinContext(new Dictionary<string, object>()).Authentication
                : HttpContext.Current.GetOwinContext().Authentication,
                Lifestyle.Scoped);
            container.Register<IUserStore<User, int>, UserStore>(Lifestyle.Scoped);
            container.Register<IRoleStore<Role, int>, RoleStore>(Lifestyle.Scoped);
            //DataContext
            var dataContextRegistration = Lifestyle.Scoped.CreateRegistration(typeof(DataContext), container);
            container.AddRegistration(typeof(DataContext), dataContextRegistration);
            container.AddRegistration(typeof(IDataContext), dataContextRegistration);

            RegisterServices(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterMvcIntegratedFilterProvider();

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        public static void RegisterServices(Container container)
        {
            container.Register<IProductService, ProductService>(Lifestyle.Scoped);
            container.Register<ICategoryService, CategoryService>(Lifestyle.Scoped);
            container.Register<IUserService, UserService> (Lifestyle.Scoped);
            container.Register<INavigationService, NavigationService>(Lifestyle.Scoped);
            container.Register<IOrderService, OrderService>(Lifestyle.Scoped);
            container.Register<IOrderDetailsService, OrderDetailsService>(Lifestyle.Scoped);
            container.Register<INewsService, NewsService>(Lifestyle.Scoped);

        }
    }
}