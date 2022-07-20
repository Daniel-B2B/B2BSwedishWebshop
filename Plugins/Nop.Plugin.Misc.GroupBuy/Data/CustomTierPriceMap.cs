using Nop.Data.Mapping;
using Nop.Plugin.Misc.GroupBuy.Models;

namespace Nop.Plugin.Misc.GroupBuy.Data
{
    public class CustomTierPriceMap : NopEntityTypeConfiguration<CustomTierPriceModel>
    {
        public CustomTierPriceMap()
        {
            this.ToTable("Akiko_GroupBuy_CustomTierPrice");
            this.HasKey(t => t.Id);
            this.Property(t => t.ProductSellId);
            this.Property(t => t.Quantity);
            this.Property(t => t.Price).HasPrecision(18,6);            
        }
    }
}
