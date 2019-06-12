using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Extras;
using Autofac.Integration.Mvc;
using Autofac.Integration.SignalR;
using Microsoft.AspNet.SignalR;
using Prikhodko.NewsWebsite.Config;

namespace Prikhodko.NewsWebsite.Web
{
    public static class AutofacConfig
    {
        public static void ConfigureContainer()
        {

            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterHubs(typeof(MvcApplication).Assembly);

            builder.AddDataDependencies();
            builder.AddServiceDependencies();

            var container = builder.Build();
            var csl = new Autofac.Extras.CommonServiceLocator.AutofacServiceLocator(container);
            GlobalHost.DependencyResolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(new Autofac.Integration.Mvc.AutofacDependencyResolver(container));
        }
    }
}