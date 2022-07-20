using Nop.Data.Mapping;
using Nop.Plugin.Misc.ImportPriceClass.Domain;

namespace Nop.PLugin.Misc.ImportPriceClass.Data
{
    public class PriceClassMap : NopEntityTypeConfiguration<ImprintPriceClass>
    {
        public PriceClassMap()
        {
            this.ToTable("Imprint_PriceClasses");  //clipper_products_Local
            this.HasKey(t => t.Id);

           
        }
    }
}
