using Nop.Core;
using Nop.Plugin.Misc.TierPriceImport.Services;
using Nop.Services.ExportImport;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Web.Framework.Controllers;
using System;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.TierPriceImport.Controllers
{
    public class TierPriceController : BasePluginController
    {
        private readonly IPermissionService _permissionService;
        private readonly IWorkContext _workContext;
        private readonly IImportManager _importManager;
        private readonly ILocalizationService _localizationService;
        private readonly ITierPriceImportService _tierPriceImportService;

        public TierPriceController(IPermissionService permissionService,IWorkContext workContext,IImportManager importManager,ILocalizationService localizationService,ITierPriceImportService tierPriceImportService)
        {
            _permissionService = permissionService;
            _workContext = workContext;
            _importManager = importManager;
            _localizationService = localizationService;
            _tierPriceImportService = tierPriceImportService;
        }

        public ActionResult Import()
        {
            return View("~/Plugins/Misc.TierPriceImport/Views/TierPriceImport.cshtml");
        }

        [HttpPost]
        public virtual ActionResult ImportExcel()
        {           
            try
            {
                var file = Request.Files["importexcelfile"];
                if (file != null && file.ContentLength > 0)
                {
                    
                    _tierPriceImportService.ImportTierPriceFromXlscx(file.InputStream);
                }
                else
                {
                    ErrorNotification(_localizationService.GetResource("Admin.Common.UploadFile"));
                    return RedirectToAction("Import");
                }
                SuccessNotification("TierPrices Imported Successfully!");
                return RedirectToAction("Import");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("Import");
            }

        }
    }
}
