﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Configuration;

namespace AppHarbor
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
           // filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            // Use LocalDB for Entity Framework by default - Use same connect string, allowing the appharbor injection
            string defaultString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            Database.DefaultConnectionFactory = new SqlConnectionFactory(defaultString);
            
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            if (!Roles.RoleExists("User"))
                Roles.CreateRole("User");
            if (Membership.FindUsersByName("adminstrador").Count == 0)
            {
                MembershipUser user = Membership.CreateUser("adminstrador", "password4@DM1N");
                if (!Roles.RoleExists("Admin"))
                    Roles.CreateRole("Admin");
                Roles.AddUserToRole("adminstrador", "Admin");
                user.Comment = "MainAdmin";
                Membership.UpdateUser(user);
            }
        }
    }
}