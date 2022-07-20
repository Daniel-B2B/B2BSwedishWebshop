using Nop.Data.Mapping;
using Nop.Plugin.Misc.GroupBuy.Domain;
using Nop.Plugin.Misc.GroupBuy.Models;

namespace Nop.Plugin.Misc.GroupBuy.Data
{
    public class GroupBuyProductMap : NopEntityTypeConfiguration<GroupBuyProduct>
    {
        public GroupBuyProductMap()
        {
            this.ToTable("Akiko_GroupBuy_ProductSellManager");
            this.HasKey(p => p.Id);
            this.Property(p => p.ProductId);            
            this.Property(p => p.StartDate);
            this.Property(p => p.EndDate);
            this.Property(p => p.Sku);

        }
    }
}
