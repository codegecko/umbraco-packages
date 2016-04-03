using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CodeGecko.Packages.Umbraco.uLogDash.Models;
using Umbraco.Core.Persistence;
using Umbraco.Web.WebApi;
using Umbraco.Web.WebApi.Filters;

namespace CodeGecko.Packages.Umbraco.uLogDash.WebApi {
    [AngularJsonOnlyConfiguration, UmbracoApplicationAuthorize("developer")]
    public class uLogDashApiController : UmbracoAuthorizedApiController {

        [HttpGet]
        public IEnumerable<ExceptionLog> Log() {
            return ApplicationContext.DatabaseContext.Database.Fetch<ExceptionLog>(string.Empty);
        }
    }
}