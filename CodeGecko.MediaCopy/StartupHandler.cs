using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Trees;

namespace CodeGecko.Umbraco.Packages.MediaCopy
{
    public class StartupHandler : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext) {
            MediaTreeController.MenuRendering += MediaTreeController_MenuRendering;
            base.ApplicationStarted(umbracoApplication, applicationContext);
        }

        void MediaTreeController_MenuRendering(TreeControllerBase sender, MenuRenderingEventArgs e)
        {
            var moveMenuItem = e.Menu.Items.Single(p => p.Alias.InvariantEquals("move"));
            var moveMenuItemIndex = e.Menu.Items.IndexOf(moveMenuItem);
            e.Menu.Items.Insert(moveMenuItemIndex + 1, new MenuItem() {
                Alias = "copy",
                Name = "Copy"
            });
        }
    }
}
