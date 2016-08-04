namespace DemoWebApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DemoWebApp.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<DemoWebApp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        //protected override void Seed(DemoWebApp.Models.ApplicationDbContext context)
        //{
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        //}

        protected override void Seed(DemoWebApp.Models.ApplicationDbContext context)
        {
            AddUserAndRole(context);

            context.Contacts.AddOrUpdate(p => p.Name,
               new Contacts
               {
                   ContactId = 1,
                   Name = "Mario Desjardins ",
                   Address = "One Microsoft Way",
                   City = "Redmond",
                   State = "WA",
                   Zip = "10999",
                   Email = "mario.desjardins@wss.qc.ca",
               }
               );
        }

        public void AddUserAndRole(DemoWebApp.Models.ApplicationDbContext context)
        {
            IdentityResult IdRoleResult;
            IdentityResult IdUserResult;

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleMgr = new RoleManager<IdentityRole>(roleStore);

            if (!roleMgr.RoleExists("canEdit"))
            {
                IdRoleResult = roleMgr.Create(new IdentityRole { Name = "canEdit" });
            }

            //var userStore = new UserStore<ApplicationUser>(context);
            //var userMgr = new UserManager<ApplicationUser>(userStore);
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var appUser = new ApplicationUser
            {
                UserName = "webstocksolution@gmail.com",
                Email = "webstocksolution@gmail.com"
            };
            IdUserResult = userMgr.Create(appUser, "Pa$$word1");

            if (!userMgr.IsInRole(userMgr.FindByEmail("webstocksolution@gmail.com").Id, "canEdit"))
            {
                //  IdUserResult = userMgr.AddToRole(appUser.Id, "canEdit");
                IdUserResult = userMgr.AddToRole(userMgr.FindByEmail("webstocksolution@gmail.com").Id, "canEdit");
            }
        }
    }
}
