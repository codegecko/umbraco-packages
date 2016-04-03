using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeGecko.Packages.Umbraco.Sitemap.Properties;
using Umbraco.Core;

namespace CodeGecko.Packages.Umbraco.Sitemap
{
    public class StartupHandler : ApplicationEventHandler
    {

        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            if (applicationContext.DatabaseContext.IsDatabaseConfigured)
            {
                applicationContext.DatabaseContext.Database.Execute(Resources.SqlForSitemapView);
            }
            base.ApplicationStarted(umbracoApplication, applicationContext);
        }
    }
}
