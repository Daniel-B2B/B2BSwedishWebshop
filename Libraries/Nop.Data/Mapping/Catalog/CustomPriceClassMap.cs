using Nop.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Catalog
{
    public class CustomPriceClassMap : NopEntityTypeConfiguration<CustomPriceClass>
    {
        public CustomPriceClassMap()
        {
            this.ToTable("Imprint_PriceClasses");
            this.HasKey(p => p.Id);
            this.Property(p => p.PriceClass);
            this.Property(p => p.Currency);
            this.Property(p => p.Price);
            this.Property(p => p.Quantity);
            this.Property(p => p.Margin);
        }
    }
}
