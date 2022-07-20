using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    public  class CustomPriceClass : BaseEntity
    {
        //public string PriceClass { get; set; }
        //public string Currency { get; set; }
        //public string Quantity { get; set; }
        //public string Price { get; set; }
        //public string Margin { get; set; }

        public int CountryId { get; set; }
        public string PriceClass { get; set; }
        public string Currency { get; set; }
        public int? Quantity { get; set; }
        public double Price { get; set; }
        public int? Margin { get; set; }
    }
}
