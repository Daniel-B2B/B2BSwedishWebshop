using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Misc.GroupBuy.Models
{
    public class FrontEndViewModel
    {        
        public List<IGrouping<int,CustomTierPriceModel>> CustomTierPriceModelList { get; set; }
        public int MaxQuantity { get; set; }

        public int SoldQuantity { get; set; }
    }
}
