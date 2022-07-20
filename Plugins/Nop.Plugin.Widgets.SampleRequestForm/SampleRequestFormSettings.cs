using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.SampleRequestForm
{
   public class SampleRequestFormSettings : ISettings
    {
        public bool SampleRequestSendEmail { get; set; }

        public string SampleRequestEmail { get; set; }

        public string WidgetZone { get; set; }

        public string EmailSubject { get; set; }
    }
}
