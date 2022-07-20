using Nop.Core.Plugins;
using Nop.Plugin.Misc.NewsletterGroupBuy.Data;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Web.Framework.Menu;
using System.Linq;
using System.Web.Routing;

namespace Nop.Plugin.Misc.NewsletterGroupBuy
{
    public class NewsletterGroupBuy : BasePlugin, IAdminMenuPlugin,IMiscPlugin
    {
        #region Fields

        private readonly ISettingService _settingService;
        private readonly ILanguageService _languageService;
        private readonly CustomObjectContext _objectContext;

        #endregion

        #region Ctor

        public NewsletterGroupBuy(ISettingService settingService,
            ILanguageService languageService,
            CustomObjectContext objectContext)
        {
            this._settingService = settingService;
            this._languageService = languageService;
            this._objectContext = objectContext;
        }

        #endregion

        #region Methods

        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "NewsletterGroupBuy";
            routeValues = new RouteValueDictionary { { "Namespaces", "Nop.Plugin.Misc.NewsletterGroupBuy.Controllers" }, { "area", null } };
        }

        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            this.AddOrUpdatePluginLocaleResource("Plugins.Misc.NewsletterGroupBuy.NewsletterEmail", "Newsletter Email");
            this.AddOrUpdatePluginLocaleResource("Plugins.Misc.NewsletterGroupBuy.AllowToUnsubscribe", "Allow To Unsubscribe");

            //data
            _objectContext.Install();
            base.Install();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            this.DeletePluginLocaleResource("Plugins.Misc.NewsletterGroupBuy.NewsletterEmail");
            this.DeletePluginLocaleResource("Plugins.Misc.NewsletterGroupBuy.AllowToUnsubscribe");

            _objectContext.Uninstall();


            base.Uninstall();
        }

        #endregion

        #region Admin Menu

        public void ManageSiteMap(SiteMapNode rootNode)
        {
            var newsletterGroupBuymenuItem = new SiteMapNode()
            {
                SystemName = "Misc.NewsletterGroupBuy",
                Title = "Newsletter Subsciption Group Buy",
                Visible = true,
                IconClass = "fa fa-money",
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

            newsletterGroupBuymenuItem.ChildNodes.Add(newsletterGroupBuyItem);

            var topNode = new SiteMapNode()
            {
                SystemName = "AkikoPlugins",
                Title = "Akiko Plugins",
                Visible = true,
                IconClass = "fa fa-th-large",
                RouteValues = new RouteValueDictionary() { { "area", null } }
            };
            var akikoPluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "AkikoPlugins");
            if (akikoPluginNode != null)
            {
                akikoPluginNode.ChildNodes.Add(newsletterGroupBuyItem);
            }
            else
            {
                rootNode.ChildNodes.Add(topNode);
                var akikoRootNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "AkikoPlugins");
                akikoRootNode.ChildNodes.Add(newsletterGroupBuyItem);
            }
        }


        #endregion
    }
}
