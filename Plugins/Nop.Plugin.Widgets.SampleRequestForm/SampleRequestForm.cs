using Nop.Core.Plugins;
using Nop.Plugin.Widgets.SampleRequestForm.Data;
using Nop.Services.Cms;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Web.Framework.Menu;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;

namespace Nop.Plugin.Widgets.SampleRequestForm
{
    public class SampleRequestForm : BasePlugin, IMiscPlugin, IWidgetPlugin //IAdminMenuPlugin,
    {
        #region Fields

        private readonly ISettingService _settingService;
        private readonly ILanguageService _languageService;
        private readonly SampleRequestFormSettings _sampleRequestFormSettingssettings;
        private readonly CustomObjectContext _objectContext;

        #endregion

        #region Ctor
        public SampleRequestForm(ISettingService settingService,
            ILanguageService languageService,
            SampleRequestFormSettings sampleRequestFormSettingssettings,
             CustomObjectContext objectContext)
        {
            this._settingService = settingService;
            this._languageService = languageService;
            this._sampleRequestFormSettingssettings = sampleRequestFormSettingssettings;
            this._objectContext = objectContext;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Gets a route for provider configuration
        /// </summary>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "SampleRequestForm";
            routeValues = new RouteValueDictionary { { "Namespaces", "Nop.Plugin.POWebshopConnector.Controllers" }, { "area", null } };
        }

        public IList<string> GetWidgetZones()
        {
            return !string.IsNullOrWhiteSpace(_sampleRequestFormSettingssettings.WidgetZone)
                       ? new List<string>() { _sampleRequestFormSettingssettings.WidgetZone }
                       : new List<string>() { "SampleRequestForm_Widget" };
        }

        public void GetDisplayWidgetRoute(string widgetZone, out string actionName, out string controllerName,
         out RouteValueDictionary routeValues)
        {
            actionName = "FrontEndView";
            controllerName = "SampleRequestForm";
            routeValues = new RouteValueDictionary
            {
                {"Namespaces", "Nop.Plugin.POWebshopConnector.Controllers"},
                {"area", null},
                {"widgetZone", widgetZone}
            };
        }

        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            //settings
            var settings = new SampleRequestFormSettings
            {
                SampleRequestEmail = null,
                SampleRequestSendEmail = false,
                WidgetZone = null,
                EmailSubject=null,
            };
            _settingService.SaveSetting(settings);

            //data
            _objectContext.Install();
            base.Install();
        }
        public override void Uninstall()
        {
            //settings
            _settingService.DeleteSetting<SampleRequestFormSettings>();

            //data
            _objectContext.Uninstall();
            base.Uninstall();
        }
        //public void ManageSiteMap(SiteMapNode rootNode)
        //{
        //    var SampleRequestAdminMenuItem = new SiteMapNode()
        //    {
        //        SystemName = "Widgets.SampleRequestForm",
        //        Title = "Sample Request Forms",
        //        ControllerName = "SampleRequestForm",
        //        ActionName = "SendSampleRequest",
        //        Visible = true,
        //        IconClass = "fa fa-desktop",
        //        RouteValues = new RouteValueDictionary() { { "area", null } },
        //    };
        //    var SampleRequestMenuItem = new SiteMapNode()
        //    {
        //        SystemName = "Widgets.SampleRequestForm",
        //        Title = "Settings",
        //        ControllerName = "SampleRequestForm",
        //        ActionName = "Configure",
        //        Visible = true,
        //        IconClass = "fa fa-cog",
        //        RouteValues = new RouteValueDictionary() { { "area", null } },
        //    };
        //    var topNode = new SiteMapNode()
        //    {
        //        SystemName = "AkikoPlugins",
        //        Title = "Akiko Plugins",
        //        Visible = true,
        //        IconClass = "fa fa-dot-circle-o",
        //        RouteValues = new RouteValueDictionary() { { "area", null } }
        //    };

        //    var akikoPluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "AkikoPlugins");
        //    if (akikoPluginNode != null)
        //    {
        //        akikoPluginNode.ChildNodes.Add(SampleRequestAdminMenuItem);
        //    }
        //    else
        //    {
        //        rootNode.ChildNodes.Add(topNode);
        //        var akikoRootNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "AkikoPlugins");
        //        akikoRootNode.ChildNodes.Add(SampleRequestAdminMenuItem);
        //    }
        //}

        #endregion
    }
}
