﻿using Nop.Web.Framework.Mvc;

namespace Nop.Web.Models.Media
{
    public partial class PictureModel : BaseNopModel
    {
        public string ImageUrl { get; set; }

        public string ThumbImageUrl { get; set; }

        public string FullSizeImageUrl { get; set; }

        public string Title { get; set; }

        public string AlternateText { get; set; }


        //akiko
        public string pictureurl { get; set; }
        public string ColorName { get; set; }
    }
}