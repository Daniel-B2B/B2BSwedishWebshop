using Nop.Core.Plugins;
using Nop.Plugin.Misc.GroupBuy.Data;
using Nop.Services.Cms;
using Nop.Services.Common;
using Nop.Services.Localization;
using Nop.Web.Framework.Menu;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;

namespace Nop.Plugin.Misc.GroupBuy
{
    public class GroupBuyPlugin : BasePlugin, IAdminMenuPlugin, IMiscPlugin, IWidgetPlugin
    {

        #region Fields
        private readonly CustomObjectContext _objectContext;

        #endregion


        #region Ctor
        public GroupBuyPlugin(CustomObjectContext objectContext)
        {
            _objectContext = objectContext;
        }

        #endregion

        public void GetDisplayWidgetRoute(string widgetZone, out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "TierPricesForProductDetails";
            controllerName = "GroupBuyManager";
            routeValues = new RouteValueDictionary
            {
                {"Namespaces","Nop.Plugin.Misc.GroupBuy.Controllers" },
                {"area", null },
                {"WidgetZone",widgetZone }
            };
        }

        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "NewsletterGroupBuy";
            routeValues = new RouteValueDictionary { { "Namespaces", "Nop.Plugin.Misc.GroupBuy.Controllers" }, { "area", null } };
        }


        public IList<string> GetWidgetZones()
        {          
                
               return  new List<string>() { "productdetails_after_tierprices" };
        }



        #region Install/Uninstall

        public override void Install()
        {
            this.AddOrUpdatePluginLocaleResource("Plugins.Misc.NewsletterGroupBuy.NewsletterEmail", "Newsletter Email");
            this.AddOrUpdatePluginLocaleResource("Plugins.Misc.NewsletterGroupBuy.AllowToUnsubscribe", "Allow To Unsubscribe");

            this.AddOrUpdatePluginLocaleResource("Plugins.Misc.GroupBuy.GroupBuyProduct", "Group Buy Product");
            this.AddOrUpdatePluginLocaleResource("Plugins.Misc.GroupBuy.AddGroupBuyProduct", "Add Group Buy Product");
            this.AddOrUpdatePluginLocaleResource("Plugins.Misc.GroupBuy.EditGroupBuyProduct", "Edit Group Buy Product");
            this.AddOrUpdatePluginLocaleResource("Plugins.Misc.GroupBuy.SoldQuantity", "Sold Qunatity");
            this.AddOrUpdatePluginLocaleResource("Plugins.Misc.GroupBuy.TierPrice", "Tier Price");
            this.AddOrUpdatePluginLocaleResource("Plugins.Misc.GroupBuy.ProductName", "Product Name");
            this.AddOrUpdatePluginLocaleResource("Plugins.Misc.GroupBuy.ProductId", "Product Id");
            _objectContext.Install();
            base.Install();
        }

        public override void Uninstall()
        {
            this.DeletePluginLocaleResource("Plugins.Misc.NewsletterGroupBuy.NewsletterEmail");
            this.DeletePluginLocaleResource("Plugins.Misc.NewsletterGroupBuy.AllowToUnsubscribe");

            this.DeletePluginLocaleResource("Plugins.Misc.GroupBuy.GroupBuyProduct");
            this.DeletePluginLocaleResource("Plugins.Misc.GroupBuy.AddGroupBuyProduct");
            this.DeletePluginLocaleResource("Plugins.Misc.GroupBuy.EditGroupBuyProduct");
            this.DeletePluginLocaleResource("Plugins.Misc.GroupBuy.SoldQuantity");
            this.DeletePluginLocaleResource("Plugins.Misc.GroupBuy.TierPrice");
            this.DeletePluginLocaleResource("Plugins.Misc.GroupBuy.ProductName");
            this.DeletePluginLocaleResource("Plugins.Misc.GroupBuy.ProductId");

            _objectContext.Uninstall();
            base.Uninstall();
        }

        #endregion

        public void ManageSiteMap(SiteMapNode rootNode)
        {
            var adminMainNode = new SiteMapNode()
            {
                SystemName = "Misc.GroupBuy",
                Title = "Group Buy",                
                Visible = true,
                IconClass = "fa fa-money",
                RouteValues = new RouteValueDictionary() { { "area", null } },
            };
            var nodeItemProducts = new SiteMapNode()
            {
                SystemName = "Misc.GroupBuy",
                Title = "Manage Group Buy Products",
                ControllerName = "GroupBuyManager",
                ActionName = "ManageSellsBasedOnQuantity",
                Visible = true,
                IconClass = "fa fa-desktop",
                RouteValues = new RouteValueDictionary() { { "area", null } },
            };

            var newsletterGroupBuyItem = new SiteMapNode()
            {
                SystemName = "Misc.NewsletterGroupBuy",
                Title = "Newsletter Subscribers Group Buy",
                ControllerName = "NewslettergroupBuy",
                ActionName = "List",
                Visible = true,
                IconClass = "fa fa-dot-circle-o",
                RouteValues = new RouteValueDictionary() { { "area", null } },
            };

            var nodeItemSettings = new SiteMapNode()
            {
                SystemName = "Misc.GroupBuy",
                Title = "Configure",
                ControllerName = "GroupBuyManager",
                ActionName = "Settings",
                Visible = true,
                IconClass = "fa fa-cog",
                RouteValues = new RouteValueDictionary() { { "area", null } },
            };

            adminMainNode.ChildNodes.Add(nodeItemProducts);
            adminMainNode.ChildNodes.Add(newsletterGroupBuyItem);
            // adminMainNode.ChildNodes.Add(nodeItemSettings);

            var topNode = new SiteMapNode()
            {
                SystemName = "AkikoPlugins",
                Title = "Akiko Plugins",
                Visible = true,
                IconClass = "fa fa-th-large",
                RouteValues = new RouteValueDictionary() { { "area", null } }
            };



            var thirdPartyPluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Third party plugins");
            var akikoPluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "AkikoPlugins");
            if (akikoPluginNode != null)
            {
                akikoPluginNode.ChildNodes.Add(adminMainNode);

            }
            else
            {
                rootNode.ChildNodes.Add(topNode);
                var akikoRootNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "AkikoPlugins");
                akikoRootNode.ChildNodes.Add(adminMainNode);

            }
        }

        //public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
