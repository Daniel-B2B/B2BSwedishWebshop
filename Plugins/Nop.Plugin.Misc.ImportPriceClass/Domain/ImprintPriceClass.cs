using Nop.Core;

namespace Nop.Plugin.Misc.ImportPriceClass.Domain
{
    public class ImprintPriceClass : BaseEntity
    {
        public int CountryId { get; set; }
        public string PriceClass { get; set; }
        public string Currency { get; set; }
        public int Quantity { get; set; }
        public double? Price { get; set; }
        public double? Price2 { get; set; }
        public double? Price3 { get; set; }
        public double? Price4 { get; set; }
        public double? Price5 { get; set; }
        public double? Price6 { get; set; }
        public double? Price7 { get; set; }
        public int? Margin { get; set; }
    }
}
