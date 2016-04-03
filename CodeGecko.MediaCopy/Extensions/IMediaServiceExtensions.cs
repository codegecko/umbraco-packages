using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.IO;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace CodeGecko.Umbraco.Packages.MediaCopy.Extensions
{
    public static class IMediaServiceExtensions
    {

        private const string UmbFile = "umbracoFile";

        public static IMedia Copy(this IMediaService mediaService, IMedia media, IMedia newLocation, string newName = "", bool copyChildren = false, string relationshipAlias = "")
        {

            var properNewName = string.IsNullOrWhiteSpace(newName) ? media.Name : newName;
            var newMedia = (newLocation == null) ? mediaService.CreateMedia(properNewName, -1, media.ContentType.Alias)
                                                 : mediaService.CreateMedia(properNewName, newLocation, media.ContentType.Alias);

            if (media.HasProperty(UmbFile))
            {
                // We need to call save here to get a media object ID to then put in the file path.
                mediaService.Save(newMedia);

                var fileSystem = FileSystemProviderManager.Current.GetUnderlyingFileSystemProvider("media");
                var oldFilePath = media.GetValue<string>(UmbFile);

                var newFilePath = Regex.Replace(oldFilePath, @"/\d{4,}/", string.Concat(Path.AltDirectorySeparatorChar, newMedia.Id.ToString(), Path.AltDirectorySeparatorChar));

                // Next, retrieve the file if it exists, and clone it using the FSProvider
                if (fileSystem.FileExists(oldFilePath))
                {
                    using (var fs = fileSystem.OpenFile(oldFilePath))
                    {
                        fileSystem.AddFile(newFilePath, fs, false);
                        newMedia.Properties[UmbFile].Value = newFilePath;
                    }
                }
            }

            foreach (var prop in media.Properties.Where(p => p.Alias != UmbFile))
            {
                newMedia.Properties[prop.Alias].Value = prop.Value;
            }

            mediaService.Save(newMedia);

            // Create relationship
            if (!string.IsNullOrWhiteSpace(relationshipAlias))
            {
                var relationService = ApplicationContext.Current.Services.RelationService;
                relationService.Relate(media, newMedia, relationService.GetRelationTypeByAlias(relationshipAlias));
            }

            // Recurse over children
            if (media.Children().Any() && copyChildren)
            {
                foreach (var child in media.Children())
                {
                    mediaService.Copy(child, newMedia, string.Empty, true, relationshipAlias);
                }
            }

            return newMedia;
        }

        public static IMedia Copy(this IMediaService mediaService, IMedia media, int newLocationId, string newName = "", bool copyChildren = false, string relationshipAlias = "")
        {
            return mediaService.Copy(media, mediaService.GetById(newLocationId), newName, copyChildren, relationshipAlias);
        }
    }
}
