using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using Prikhodko.NewsWebsite.Data.Contracts.Models;

namespace Prikhodko.NewsWebsite.Data.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Prikhodko.NewsWebsite.Data.EntityFramework.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Prikhodko.NewsWebsite.Data.EntityFramework.ApplicationDbContext context)
        {
            context.Categories.AddOrUpdate(new Category() {Name = "C#"});
            context.Categories.AddOrUpdate(new Category() {Name = "Java" });
            context.Categories.AddOrUpdate(new Category() {Name = "Machine Learning" });
            context.Categories.AddOrUpdate(new Category() {Name = "Algorithms" });

            context.Roles.AddOrUpdate(new IdentityRole("Readonly"));
            context.Roles.AddOrUpdate(new IdentityRole("Writer"));
            context.Roles.AddOrUpdate(new IdentityRole("Admin"));
        }
    }
}
