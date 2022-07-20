using Nop.Core;
using Nop.Plugin.Misc.ImportPriceClass.Domain;
using Nop.Plugin.Misc.ImportPriceClass.Services;
using Nop.Services.ExportImport;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc;
using System;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.ImportPriceClass.Controllers
{
    public class PriceClassController: BasePluginController
    {
        private readonly IPermissionService _permissionService;
        private readonly IWorkContext _workContext;
        private readonly IImportManager _importManager;
        private readonly ILocalizationService _localizationService;
       // private readonly IPriceClassImportService _priceClassImportService;
        private readonly IPriceClassService _priceClassService;

        public PriceClassController(IPermissionService permissionService, 
            IWorkContext workContext, 
            IImportManager importManager, 
            ILocalizationService localizationService, 
           // IPriceClassImportService priceClassImportService,
            IPriceClassService priceClassService)
        {
            _permissionService = permissionService;
            _workContext = workContext;
            _importManager = importManager;
            _localizationService = localizationService;
           // _priceClassImportService = priceClassImportService;
            _priceClassService = priceClassService;
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            //little hack here
            //always set culture to 'en-US' (Telerik has a bug related to editing decimal values in other cultures). Like currently it's done for admin area in Global.asax.cs
            CommonHelper.SetTelerikCulture();

            base.Initialize(requestContext);
        }

        public ActionResult Import()
        {
            return View("~/Plugins/Misc.ImportPriceClass/Views/PriceClassImport.cshtml");
        }

        [HttpPost]
        public ActionResult PriceClassList(DataSourceRequest command, FormCollection form)
        {
            var products = _priceClassService.GetAllPriceClass(pageIndex: command.Page - 1,
                pageSize: command.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = products,
                Total = products.TotalCount
            };

            return new JsonResult
            {
                Data = gridModel
                //JsonRequestBehavior= JsonRequestBehavior.AllowGet
            };
        }

        [HttpPost]
        public virtual ActionResult PriceClassInsert(ImprintPriceClass model)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            //    return AccessDeniedView();           
                        
            //insert mapping
            var priceClass = new ImprintPriceClass
            {
                PriceClass = model.PriceClass,
                Price = model.Price,
                Price2 = model.Price2,
                Price3 = model.Price3,
                Price4 = model.Price4,
                Price5 = model.Price5,
                Price6 = model.Price6,
                Price7 = model.Price7,
                Currency = "DKK",
                CountryId = 3,
                Quantity = model.Quantity                
            };
            _priceClassService.InsertPriceClass(priceClass);

            
            return new NullJsonResult();
        }

        [HttpPost]
        public virtual ActionResult PriceClassUpdate(ImprintPriceClass model)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            //    return AccessDeniedView();           

            var priceClass = _priceClassService.GetPriceClassById(model.Id);
            if (priceClass == null)
                throw new ArgumentException("No price class found with the specified id");


            priceClass.Quantity = model.Quantity;
            priceClass.Price = model.Price;
            priceClass.Price2 = model.Price2;
            priceClass.Price3 = model.Price3;
            priceClass.Price4 = model.Price4;
            priceClass.Price5 = model.Price5;
            priceClass.Price6 = model.Price6;
            priceClass.Price7 = model.Price7;


            _priceClassService.UpdatePriceClass(priceClass);


            return new NullJsonResult();
        }

        public virtual void DeletePriceClass(ImprintPriceClass model)
        {
            if (model == null)
                throw new ArgumentNullException("Price class");

            var priceClass = _priceClassService.GetPriceClassById(model.Id);
            if (priceClass != null)
            {
                _priceClassService.DeletePriceClass(priceClass);
            }
        }


        [HttpPost]
        public virtual ActionResult ImportExcel()
        {
            //try
            //{
            //    var file = Request.Files["importexcelfile"];
            //    if (file != null && file.ContentLength > 0)
            //    {

            //        _priceClassImportService.ImportPriceClassFromXlscx(file.InputStream);
            //    }
            //    else
            //    {
            //        ErrorNotification(_localizationService.GetResource("Admin.Common.UploadFile"));
            //        return RedirectToAction("Import");
            //    }
            //    SuccessNotification("Price Class Imported Successfully!");
            //    return RedirectToAction("Import");
            //}
            //catch (Exception exc)
            //{
            //    ErrorNotification(exc);
            //    return RedirectToAction("Import");
            //}
            return null;
        }
    }
}
