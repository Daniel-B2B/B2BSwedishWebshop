using Nop.Core;
using Nop.Web.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.GroupBuy.Domain
{
    //[Validator(typeof(ProductSellModelValidator))]
    public class GroupBuyProduct : BaseEntity 
    {
        public GroupBuyProduct()
        {
            ProductList = new List<SelectListItem>();
        }
       
        public int ProductId { get; set; }

        [NotMapped]
        public string ProductName { get; set; }

        [DisplayName("Start Date")]        
        [NopResourceDisplayName("Admin.Orders.List.StartDate")]
        //[UIHint("DateNullable")]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date")]       
        [NopResourceDisplayName("Admin.Orders.List.EndDate")]
        //[UIHint("DateNullable")]
        public DateTime EndDate { get; set; }   
      
        [DisplayName("Product Sku")]
        public string Sku { get; set; }
        [NotMapped]
        public int ProductSoldQuantity { get; set; }

        [NotMapped]
        public List<SelectListItem> ProductList { get; set; }
        [NotMapped]
        public List<GroupBuyProduct> ProductSellList { get; set; }       
    }
}
