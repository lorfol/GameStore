using System.Web.Mvc;
using System.Web.Routing;
using GameStore.Web;
using GameStore.Web.IoC;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using NUnit.Framework;

namespace GameStore.Tests
{
    [TestFixture]
    public class AppStartTest
    {
        [Test]
        public void Init()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }

        [Test]
        public void NinjectTest()
        {
            NinjectModule registrations = new NinjectRegistrations();
            var kernel = new StandardKernel(registrations);
            kernel.Unbind<ModelValidatorProvider>();
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
