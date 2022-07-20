using Nop.Web.Framework.Mvc;

namespace Nop.Web.Models.Catalog
{
    public class HomePageMenuProductViewModel : BaseNopModel
    {
        public int CategoryId { get; set; }

        public string Price { get; set; }

        public string PictureUrl { get; set; }

        public string Seoname { get; set; }

        public bool RootCategory { get; set; }
        public string CustomText { get; set; }
    }
}