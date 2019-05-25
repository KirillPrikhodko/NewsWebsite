using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Prikhodko.NewsWebsite.CommonModels;
using Prikhodko.NewsWebsite.Data.Contracts.Models;

namespace Prikhodko.NewsWebsite.Data.EntityFramework
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationIdentityUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", false)
        {
        }

        #region DbSets

        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentRate> CommentRates { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostRate> PostRates { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> AppUsers { get; set; }

        #endregion

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            var applicationIdentityUserConfiguration = builder.Entity<ApplicationIdentityUser>();
            applicationIdentityUserConfiguration.HasRequired(x => x.User)
                .WithRequiredDependent(x => x.ApplicationIdentityUser);

            var userConfiguration = builder.Entity<User>();
            userConfiguration.HasRequired(x => x.ApplicationIdentityUser).WithRequiredPrincipal(x => x.User);
            userConfiguration.HasMany(x => x.Posts).WithRequired(x => x.Author);
            userConfiguration.HasMany(x => x.PostRates).WithRequired(x => x.Author);
            userConfiguration.HasMany(x => x.Comments).WithRequired(x => x.Author);
            userConfiguration.HasMany(x => x.CommentRates).WithRequired(x => x.Author);

            var postConfiguration = builder.Entity<Post>();
            postConfiguration.HasMany(x => x.Tags).WithMany(x => x.Posts);
            postConfiguration.HasMany(x => x.Comments).WithRequired(x => x.Post);
            postConfiguration.HasRequired(x => x.Author).WithMany(x => x.Posts);

            var postRateConfiguration = builder.Entity<PostRate>();
            postRateConfiguration.HasRequired(x => x.Author).WithMany(x => x.PostRates);

            var tagConfiguration = builder.Entity<Tag>();
            tagConfiguration.HasMany(x => x.Posts).WithMany(x => x.Tags);

            var commentConfiguration = builder.Entity<Comment>();
            commentConfiguration.HasRequired(x => x.Author).WithMany(x => x.Comments);
            commentConfiguration.HasRequired(x => x.Post).WithMany(x => x.Comments);
            commentConfiguration.HasMany(x => x.Rates).WithRequired(x => x.Comment);

            var commentrateConfiguration = builder.Entity<CommentRate>();
            commentrateConfiguration.HasRequired(x => x.Comment).WithMany(x => x.Rates);
            commentrateConfiguration.HasRequired(x => x.Author).WithMany(x => x.CommentRates);

            base.OnModelCreating(builder);
        }
    }
}
