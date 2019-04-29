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
            builder.Register(c => new UserStore<ApplicationUser>(c.Resolve<ApplicationDbContext>())).As<IUserStore<ApplicationUser>>();
            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).As<IAuthenticationManager>();
            builder.RegisterType<ApplicationUserManager>().As<ApplicationUserManager>().InstancePerLifetimeScope();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ApplicationUserRepository>().As<IRepository<ApplicationUser>>();
            builder.RegisterType<LoginRepository>().As<ILoginRepository>();
            builder.RegisterType<RegisterRepository>().As<IRegisterRepository>();
            builder.RegisterType<AccountManageRepository>().As<IAccountManageRepository>();
            #endregion

            builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<PostRepository>().As<IRepository<Post>>();
            return builder;
        }

        public static ContainerBuilder AddServiceDependencies(this ContainerBuilder builder)
        {
            #region IdentityFramework
            builder.RegisterType<ApplicationUserService>().As<IService<ApplicationUser>>();
            builder.RegisterType<LoginService>().As<ILoginService>();
            builder.RegisterType<RegisterService>().As<IRegisterService>();
            builder.RegisterType<AccountManageService>().As<IAccountManageService>();
            #endregion

            builder.RegisterType<PostService>().As<IService<PostViewModel>>();
            return builder;
        }
    }
}
