using Nop.Core;

namespace Nop.Plugin.Widgets.SampleRequestForm.Domain
{
    public class SampleRequestFormRecord : BaseEntity
    {
        public string Name { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Product { get; set; }
        public int PotentialOrderQty { get; set; }
        public string Comments { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }

    }
}
