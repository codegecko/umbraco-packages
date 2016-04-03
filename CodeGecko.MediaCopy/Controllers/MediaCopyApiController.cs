using System;
using System.Net;
using System.Net.Http;
using CodeGecko.Umbraco.Packages.MediaCopy.Extensions;
using Umbraco.Web.Editors;
using Umbraco.Web.WebApi;
using Umbraco.Web.WebApi.Filters;

namespace CodeGecko.Umbraco.Packages.MediaCopy.Controllers
{
    public class MediaCopyApiController : ContentControllerBase
    {

        [AngularJsonOnlyConfiguration, UmbracoApplicationAuthorize("media")]
        public HttpResponseMessage Copy(int newParentId, int existingMediaId, string newName = "", bool copyChildren = false, string relationshipAlias = "")
        {
            try {
                var newMedia = Services.MediaService.Copy(Services.MediaService.GetById(existingMediaId), newParentId, newName, copyChildren, relationshipAlias);
                return Request.CreateResponse(HttpStatusCode.Created, newMedia.Id.ToString());
            } catch (Exception ex) {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}
