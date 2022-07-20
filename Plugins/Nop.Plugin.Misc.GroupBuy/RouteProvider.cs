using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc.Routes;
using System.Web.Routing;

namespace Nop.Plugin.Misc.GroupBuy
{ 
    public class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            //subscribe newsletters
            routes.MapLocalizedRoute("SubscribeNewslettergroupBuy",
                            "subscribenewslettergroupbuy",
                            new { controller = "NewsLetterGroupBuy", action = "SubscribeNewsletterGroupBuy" },
                            new[] { "Nop.Plugin.Misc.GroupBuy.Controllers" });

            routes.MapLocalizedRoute("SubscribeGroupBuyNewsletterActivation",
                              "groupbuynewsletter/subscriptionactivation/{token}/{active}",
                              new { controller = "NewsLetterGroupBuy", action = "SubscriptionActivation" },
                              new { token = new GuidConstraint(false) },
                              new[] { "Nop.Plugin.Misc.GroupBuy.Controllers" });
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
