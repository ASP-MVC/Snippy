namespace Snippy.App
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using Snippy.Data;
    using Snippy.Data.Migrations;
    using Snippy.Infrastructure.Mappings;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SnippyDbContext, Configuration>());
            this.LoadAutoMapper();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void LoadAutoMapper()
        {
            var autoMapper = new AutoMapperConfig(new List<Assembly>() { Assembly.GetExecutingAssembly() });
            autoMapper.Execute();
        }
    }
}