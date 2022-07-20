using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Misc.GroupBuy.Models
{
    public class NewsletterGroupBuyBoxModel : BaseNopModel
    {
        public string NewsletterEmail { get; set; }
        public bool AllowToUnsubscribe { get; set; }
    }
}
