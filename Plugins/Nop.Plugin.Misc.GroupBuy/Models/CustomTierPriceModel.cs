using FluentValidation.Attributes;
using Nop.Core;
using Nop.Plugin.Misc.GroupBuy.Validators;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nop.Plugin.Misc.GroupBuy.Models
{
    [Validator(typeof(CustomTierPriceValidator))]
    public class CustomTierPriceModel : BaseEntity
    {
        //public int Id { get; set; }
        [DisplayName("Group Buy Id")]
        public int ProductSellId { get; set; }

        public int ProductId { get; set; }

        public int MaxQuantity { get; set; }


        public int Quantity { get; set; }

        [UIHint("DecimalNullable")]
        public decimal? Price { get; set; }

        [NotMapped]
        public List<CustomTierPriceModel> TierPriceList { get; set; }

        [DisplayName("Product Name")]
        [NotMapped]
        public string ProductName { get; set; }

        [NotMapped]
        public string PriceString { get; set; }
    }
}
