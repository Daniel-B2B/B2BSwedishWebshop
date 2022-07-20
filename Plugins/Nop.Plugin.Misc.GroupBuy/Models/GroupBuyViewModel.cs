using FluentValidation.Attributes;
using Nop.Core;
using Nop.Plugin.Misc.GroupBuy.Validators;
using Nop.Web.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.GroupBuy.Models
{
    [Validator(typeof(GroupBuyValidator))]
    public class GroupBuyViewModel : BaseEntity 
    {
        public GroupBuyViewModel()
        {
            ProductList = new List<SelectListItem>();
        }
        [NopResourceDisplayName("Plugins.Misc.GroupBuy.ProductId")]
        public int ProductId { get; set; }

        
        [NopResourceDisplayName("Plugins.Misc.GroupBuy.ProductName")]
        public string ProductName { get; set; }

        
        [DisplayName("Start Date")]
        [NopResourceDisplayName("Admin.Orders.List.StartDate")]
        [UIHint("DateNullable")]
        public DateTime? StartDate { get; set; }

        [DisplayName("End Date")]
        [NopResourceDisplayName("Admin.Orders.List.EndDate")]
        [UIHint("DateNullable")]
        public DateTime? EndDate { get; set; }


        [DisplayName("Product Sku")]
        public string Sku { get; set; }
        
        [NopResourceDisplayName("Plugins.Misc.GroupBuy.SoldQuantity")]
        public int ProductSoldQuantity { get; set; }

       [NotMapped]
        public List<SelectListItem> ProductList { get; set; }
        [NotMapped]
        public List<GroupBuyViewModel> ProductSellList { get; set; }

        

    }
}
