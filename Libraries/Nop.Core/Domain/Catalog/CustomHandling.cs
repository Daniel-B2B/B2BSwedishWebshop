namespace Nop.Core.Domain.Catalog
{
    public class CustomHandling : BaseEntity
    {
        public string PriceClass { get; set; }
        public string Currency { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public string Margin { get; set; }

    }
}
