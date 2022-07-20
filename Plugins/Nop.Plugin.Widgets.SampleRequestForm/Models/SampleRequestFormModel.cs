using Nop.Web.Framework.Mvc;
using System.Web.Mvc;

namespace Nop.Plugin.Widgets.SampleRequestForm.Models
{
  public class SampleRequestFormModel : BaseNopModel
    {
        [AllowHtml]
        public string Name { get; set; }
        [AllowHtml]
        public string Company { get; set; }
        [AllowHtml]
        public string Email { get; set; }
        [AllowHtml]
        public string PhoneNumber { get; set; }

        [AllowHtml]
        public int PotentialOrderQty { get; set; }
        [AllowHtml]
        public string Comments { get; set; }

        //Product 
        public int ProductId { get; set; }
        [AllowHtml]
        public string Product { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }


    }
}
