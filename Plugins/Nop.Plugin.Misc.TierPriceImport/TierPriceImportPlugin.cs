using Nop.Core.Plugins;
using Nop.Web.Framework.Menu;
using System.Linq;
using System.Web.Routing;

namespace Nop.Plugin.Misc.TierPriceImport
{
    public class TierPriceImportPlugin : BasePlugin,  IAdminMenuPlugin
    {
        //public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        //{
        //    actionName = "Configure";
        //    controllerName = "TirePrice";
        //    routeValues = new RouteValueDictionary()
        //    {
        //        { "Namespaces", "Nop.Plugin.Misc.TierPriceImport" },
        //        { "area", null }
        //    };
        //}

        public override void Install()
        {

            base.Install();
        }

        public override void Uninstall()
        {

            base.Uninstall();
        }

        public void ManageSiteMap(SiteMapNode rootNode)
        {
            var exportimportmenuItem = new SiteMapNode()
            {
                SystemName = "TierPriceImport",
                Title = "Tier Price Import",
                ControllerName = "TierPrice",
                ActionName = "Import",
                Visible = true,
                IconClass = "fa fa-money",
                RouteValues = new RouteValueDictionary() { { "area", null } },
            };

            var topNode = new SiteMapNode()
            {
                SystemName = "AkikoPlugins",
                Title = "Akiko Plugins",
                Visible = true,
                IconClass = "fa fa-th-large",
                RouteValues = new RouteValueDictionary() { { "area", null } }
            };

            //rootNode.ChildNodes.Add(topNode);

            var thirdPartyPluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Third party plugins");
            var akikoPluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "AkikoPlugins");
            if (akikoPluginNode != null)
            {
                akikoPluginNode.ChildNodes.Add(exportimportmenuItem);
                //akikoPluginNode.ChildNodes.Add(exportimportmenuItem1);
            }
            else
            {
                rootNode.ChildNodes.Add(topNode);
                var akikoRootNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "AkikoPlugins");
                akikoRootNode.ChildNodes.Add(exportimportmenuItem);
                // akikoRootNode.ChildNodes.Add(exportimportmenuItem1);
            }                        
        }

        public bool Authenticate()
        {
            return true;
        }
    }
}
