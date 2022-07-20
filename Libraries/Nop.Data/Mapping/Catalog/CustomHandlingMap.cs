using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public class CustomHandlingMap : NopEntityTypeConfiguration<CustomHandling>
    {
        public CustomHandlingMap()
        {
            this.ToTable("Handling_PriceClass");
            this.HasKey(p => p.Id);
            this.Property(p => p.PriceClass);
            this.Property(p => p.Currency);
            this.Property(p => p.Price);
            this.Property(p => p.Quantity);
            this.Property(p => p.Margin);
        }
    }
}
