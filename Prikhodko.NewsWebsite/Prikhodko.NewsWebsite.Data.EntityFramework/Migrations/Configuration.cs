using System.Collections.Generic;
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
            context.Categories.AddRange(new List<Category>
            {
                new Category() {Name = "C#"},
                new Category() {Name = "Java"},
                new Category() {Name = "Python"}
            });
        }
    }
}
