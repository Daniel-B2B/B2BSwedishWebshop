using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Widgets.SampleRequestForm.Models
{
  public class CallMeModel  : BaseNopModel
    {
        public string Name { get; set; }

        public string Company { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
