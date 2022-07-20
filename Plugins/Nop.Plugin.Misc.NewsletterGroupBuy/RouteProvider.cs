using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc.Routes;
using System.Web.Routing;

namespace Nop.Plugin.Misc.NewsletterGroupBuy
{ 
    public class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            //subscribe newsletters
            routes.MapLocalizedRoute("SubscribeNewslettergroupBuy",
                            "subscribenewslettergroupbuy",
                            new { controller = "NewslettergroupBuy", action = "SubscribeNewsletterGroupBuy" },
                            new[] { "Nop.Plugin.Misc.NewsletterGroupBuy.Controllers" });
        }
        public int Priority
        {
            get
            {
                return 100;
            }
        }
    }
}
