using Nop.Core;
using Nop.Web.Framework;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nop.Plugin.Misc.GroupBuy.Models
{
    public class GroupBuyTierPriceModel : BaseEntity
    {
        
        [DisplayName("Group Buy Id")]
        public int ProductSellId { get; set; }


        public int Quantity { get; set; }


        public string Price { get; set; }

        [NotMapped]
        public List<CustomTierPriceModel> TierPriceList { get; set; }

        [NopResourceDisplayName("Plugins.Misc.GroupBuy.ProductName")]
        [NotMapped]
        public string ProductName { get; set; }
    }
}
