using Autofac;
using Prikhodko.NewsWebsite.CommonModels;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Prikhodko.NewsWebsite.Data.Contracts.Models;
using Prikhodko.NewsWebsite.Data.EntityFramework;
using Prikhodko.NewsWebsite.Data.EntityFramework.IdentityFramework;
using Prikhodko.NewsWebsite.Data.EntityFramework.Repositories;
using Prikhodko.NewsWebsite.Service;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Service.Contracts.Models;
using Prikhodko.NewsWebsite.Service.IdentityFramework;

namespace Prikhodko.NewsWebsite.Config
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder AddDataDependencies(this ContainerBuilder builder)
        {
            #region IdentityFramework
            builder.Register(c => new IdentityFactoryOptions<ApplicationUserManager>()
            {
                DataProtectionProvider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("NewsWebsite")
            }).As<IdentityFactoryOptions<ApplicationUserManager>>();
            builder.Register(c => HttpContext.Current.GetOwinContext()).As<IOwinContext>();
            builder.Register(c => new UserStore<ApplicationIdentityUser>(c.Resolve<ApplicationDbContext>())).As<IUserStore<ApplicationIdentityUser>>();
            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).As<IAuthenticationManager>();
            builder.RegisterType<ApplicationUserManager>().As<ApplicationUserManager>().InstancePerLifetimeScope();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<LoginRepository>().As<ILoginRepository>();
            builder.RegisterType<RegisterRepository>().As<IRegisterRepository>();
            builder.RegisterType<AccountManageRepository>().As<IAccountManageRepository>();
            #endregion

            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<PostRepository>().As<IPostRepository>();
            builder.RegisterType<PostRateRepository>().As<IPostRateRepository>();
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>();
            builder.RegisterType<CommentRepository>().As<ICommentRepository>();
            builder.RegisterType<TagRepository>().As<ITagRepository>();
            builder.RegisterType<CommentRateRepository>().As<ICommentRateRepository>();
            return builder;
        }

        public static ContainerBuilder AddServiceDependencies(this ContainerBuilder builder)
        {
            #region IdentityFramework
            builder.RegisterType<LoginService>().As<ILoginService>();
            builder.RegisterType<RegisterService>().As<IRegisterService>();
            builder.RegisterType<AccountManageService>().As<IAccountManageService>();
            #endregion

            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<PostService>().As<IPostService>();
            builder.RegisterType<PostRateService>().As<IPostRateService>();
            builder.RegisterType<CategoryService>().As<ICategoryService>();
            builder.RegisterType<CommentService>().As<ICommentService>();
            builder.RegisterType<TagService>().As<ITagService>();
            builder.RegisterType<CommentRateService>().As<ICommentRateService>();
            return builder;
        }
    }
}
