using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Nop.Plugin.Widgets.SampleRequestForm.Models
{
   public class ConfigurationModel : BaseNopModel
    {
        //public ConfigurationModel()
        //{
        //    AvailableZones = new List<SelectListItem>();
        //}
        //[NopResourceDisplayName("ZoneId")]
        //public string ZoneId { get; set; }
        //[NopResourceDisplayName("Available Zones")]
        //public IList<SelectListItem> AvailableZones { get; set; }

        [NopResourceDisplayName("Sample Request Send Email")]
        public bool SampleRequestSendEmail { get; set; }
        [NopResourceDisplayName("Sample Request Email Account")]
        public string SampleRequestEmail { get; set; }
        [NopResourceDisplayName("Email Subject")]
        public string EmailSubject { get; set; }


    }
}
