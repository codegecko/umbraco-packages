using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeGecko.Packages.Umbraco.uLogDash.Models;
using Umbraco.Core;
using Umbraco.Core.Persistence;

namespace CodeGecko.Packages.Umbraco.uLogDash
{
    public class StartupHandler : ApplicationEventHandler {

        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            var db = applicationContext.DatabaseContext.Database;
            if (!db.TableExist("ExceptionLog")) db.CreateTable<ExceptionLog>();

            base.ApplicationStarted(umbracoApplication, applicationContext);
        }
    }
}