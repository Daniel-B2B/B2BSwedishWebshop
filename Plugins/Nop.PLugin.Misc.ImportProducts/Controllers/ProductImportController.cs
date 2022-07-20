using Nop.PLugin.Misc.ImportProducts.Services;
using Nop.Services.Localization;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace Nop.PLugin.Misc.ImportProducts.Controllers
{
    [AdminAuthorize]
    public class ProductImportController : BasePluginController
    {       
        private readonly ILocalizationService _localizationService;
        private readonly IExcelImportService _excelImportService;
        private readonly IClipperProductsService _clipperProductsService;
        public ProductImportController(ILocalizationService localizationService, 
            IExcelImportService excelImportService,
            IClipperProductsService clipperProductsService)
        {            
            _localizationService = localizationService;
            _excelImportService = excelImportService;
            _clipperProductsService = clipperProductsService;
        }

        public ActionResult Import()
        {
            return View("~/Plugins/Misc.ImportProducts/Views/ProductImport.cshtml");
        }

        [HttpPost]        
        public ActionResult ProductList(DataSourceRequest command,  FormCollection form)
        {
            var products = _clipperProductsService.GetAllProducts(pageIndex: command.Page - 1,
                pageSize: command.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = products,
                Total = products.Count
            };

            return new JsonResult 
            {
                Data = gridModel
                //JsonRequestBehavior= JsonRequestBehavior.AllowGet
            };
        }

        [HttpPost]       
        public virtual ActionResult ImportExcel()
        {
            int countryId = Convert.ToInt32(ConfigurationManager.AppSettings["CountryId"]);
            int siteId = Convert.ToInt32(ConfigurationManager.AppSettings["SiteId"]);
            string database = ConfigurationManager.AppSettings["DatabaseName"];

            if (countryId == 0 || siteId == 0 || string.IsNullOrEmpty(database))
            {
                ErrorNotification(_localizationService.GetResource("Admin.ImportProducts.Config.Missing"));
                return RedirectToAction("Import");
            }

            try
            {
                var file = Request.Files["importexcelfile"];
                if (file != null && file.ContentLength > 0)
                {
                    _excelImportService.ImportProductsFromXlscx(file.InputStream, countryId, siteId, database);
                }
                else
                {
                    ErrorNotification(_localizationService.GetResource("Admin.Common.UploadFile"));
                    return RedirectToAction("Import");
                }
                SuccessNotification("Products Imported Successfully!");
                return RedirectToAction("Import");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("Import");
            }
        }

       
        public virtual ActionResult SyncProducts()
        {
            try
            {
                int countryId = Convert.ToInt32(ConfigurationManager.AppSettings["CountryId"]);
                int siteId = Convert.ToInt32(ConfigurationManager.AppSettings["SiteId"]);
                string database = ConfigurationManager.AppSettings["DatabaseName"];
                var res = _clipperProductsService.ExecuteNopActionProcedure(countryId, siteId, database);
                _clipperProductsService.ClearTable();
                SuccessNotification("Products Synced Successfully!");
            }
            catch(Exception e)
            {
                ErrorNotification(e);
            }
            return RedirectToAction("Import");
        }
    }
}

