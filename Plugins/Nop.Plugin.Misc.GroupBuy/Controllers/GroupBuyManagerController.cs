using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Messages;
using Nop.Plugin.Misc.GroupBuy.Domain;
using Nop.Plugin.Misc.GroupBuy.Helpers;
using Nop.Plugin.Misc.GroupBuy.Models;
using Nop.Plugin.Misc.GroupBuy.Services;
using Nop.Services.Catalog;
using Nop.Services.Customers;
using Nop.Services.Discounts;
using Nop.Services.ExportImport;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Stores;
using Nop.Services.Vendors;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace Nop.Plugin.Misc.GroupBuy.Controllers
{

    public class GroupBuyManagerController : BasePluginController
    {

        #region Fields

        private readonly ICategoryService _categoryService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IManufacturerTemplateService _manufacturerTemplateService;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        private readonly IStoreService _storeService;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IPictureService _pictureService;
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly IExportManager _exportManager;
        private readonly IDiscountService _discountService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IVendorService _vendorService;
        private readonly IAclService _aclService;
        private readonly IPermissionService _permissionService;
        private readonly CatalogSettings _catalogSettings;
        private readonly IWorkContext _workContext;
        private readonly IImportManager _importManager;
        private readonly ICacheManager _cacheManager;
        private readonly IGroupBuyService _groupBuyService;
        private readonly IEmailAccountService _emailAccountService;
        private readonly IEmailSender _emailSender;
        private readonly EmailAccountSettings _emailAccountSettings;
        private readonly INewsLetterSubscriptionGroupBuyService _newsLetterSubscriptionGroupBuyService;
        

        #endregion

        #region Constructors
        public GroupBuyManagerController(ICategoryService categoryService,
            IManufacturerService manufacturerService,
            IManufacturerTemplateService manufacturerTemplateService,
            IProductService productService,
            ICustomerService customerService,
            IStoreService storeService,
            IStoreMappingService storeMappingService,
            IUrlRecordService urlRecordService,
            IPictureService pictureService,
            ILanguageService languageService,
            ILocalizationService localizationService,
            ILocalizedEntityService localizedEntityService,
            IExportManager exportManager,
            IDiscountService discountService,
            ICustomerActivityService customerActivityService,
            IVendorService vendorService,
            IAclService aclService,
            IPermissionService permissionService,
            CatalogSettings catalogSettings,
            IWorkContext workContext,
            IImportManager importManager,
            ICacheManager cacheManager,
            IGroupBuyService groupBuyService,
            IEmailAccountService emailAccountService,
            IEmailSender emailSender,
            EmailAccountSettings emailAccountSettings,
            INewsLetterSubscriptionGroupBuyService newsLetterSubscriptionGroupBuyService)
        {
            this._categoryService = categoryService;
            this._manufacturerTemplateService = manufacturerTemplateService;
            this._manufacturerService = manufacturerService;
            this._productService = productService;
            this._customerService = customerService;
            this._storeService = storeService;
            this._storeMappingService = storeMappingService;
            this._urlRecordService = urlRecordService;
            this._pictureService = pictureService;
            this._languageService = languageService;
            this._localizationService = localizationService;
            this._localizedEntityService = localizedEntityService;
            this._exportManager = exportManager;
            this._discountService = discountService;
            this._customerActivityService = customerActivityService;
            this._vendorService = vendorService;
            this._aclService = aclService;
            this._permissionService = permissionService;
            this._catalogSettings = catalogSettings;
            this._workContext = workContext;
            this._importManager = importManager;
            this._cacheManager = cacheManager;
            this._emailAccountService = emailAccountService;
            this._emailSender = emailSender;
            this._emailAccountSettings = emailAccountSettings;
            this._newsLetterSubscriptionGroupBuyService = newsLetterSubscriptionGroupBuyService;
            this._groupBuyService = groupBuyService;
            
        }

        #endregion

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            //little hack here
            //always set culture to 'en-US' (Telerik has a bug related to editing decimal values in other cultures). Like currently it's done for admin area in Global.asax.cs
            CommonHelper.SetTelerikCulture();

            base.Initialize(requestContext);
        }

        #region Frontend

        public ActionResult TierPricesForProductDetails(string widgetZone, object additionalData = null)
        {
            FrontEndViewModel model = new FrontEndViewModel();
            if (additionalData != null)
            {
                model = _groupBuyService.GetTIerPriceByProductId((Int32)additionalData);
            }
            if (model == null)
            {
                return null;
            }
            return View("~/Plugins/Misc.GroupBuy/Views/CustomTierPrice/TierPriceView.cshtml", model);
        }

        #endregion

        #region Admin Methods

        [AdminAuthorize]
        public ActionResult ManageSellsBasedOnQuantity()
        {
            var model = new GroupBuyViewModel();
            return View("~/Plugins/Misc.GroupBuy/Views/GroupBuyManager/GroupBuyList.cshtml", model);
        }

        [HttpPost]
        [AdminAuthorize]
        public ActionResult List(DataSourceRequest command)
        {
            var groupBuyProducts = _groupBuyService.GetAllGroupBuyProductList();
            var gridModel = new DataSourceResult
            {
                Data = groupBuyProducts,
                Total = groupBuyProducts.TotalCount
            };
            return Json(gridModel);            
        }

        [AdminAuthorize]
        public ActionResult CreateGroupBuyProduct()
        {            
            GroupBuyViewModel model = new GroupBuyViewModel();
            return View("~/Plugins/Misc.GroupBuy/Views/GroupBuyManager/AddGroupBuyProduct.cshtml", model);
        }

        [HttpPost]
        [AdminAuthorize]
        public ActionResult CreateGroupBuyProduct(GroupBuyViewModel groupBuyViewModel)
        {
            if (ModelState.IsValid)
            {
                GroupBuyProduct model = new GroupBuyProduct();
                model.StartDate = groupBuyViewModel.StartDate.Value;
                model.EndDate = groupBuyViewModel.EndDate.Value;
                model.ProductId = Convert.ToInt32(groupBuyViewModel.Sku);
                model.ProductName = groupBuyViewModel.ProductName;

                var product = _productService.GetProductById(model.ProductId);
                if(product!= null)
                {
                    model.Sku = product.Sku;
                    bool isInserted = _groupBuyService.CreateProductQuantity(model);
                    if (isInserted == true)
                        return RedirectToAction("ManageSellsBasedOnQuantity");                   
                   
                }
                else
                {
                    ErrorNotification("Invalid Product Id");
                    return View("~/Plugins/Misc.GroupBuy/Views/GroupBuyManager/AddGroupBuyProduct.cshtml", groupBuyViewModel);
                }
            }
            //ErrorNotification("Invalid data");
            //ErrorNotification("Invalid Product Sku");
            //ProductSellModel model = _manageQuantityService.GetAllProductDropDownList();
            return View("~/Plugins/Misc.GroupBuy/Views/GroupBuyManager/AddGroupBuyProduct.cshtml", groupBuyViewModel);
        }

        [AdminAuthorize]
        public ActionResult EditGroupBuyProduct(int id)
        {
            GroupBuyProduct pmodel = _groupBuyService.GetProductSellQuantityDetails(id);

            GroupBuyViewModel model = new GroupBuyViewModel();
            model.StartDate = pmodel.StartDate;            
            model.EndDate = pmodel.EndDate;
            model.ProductId = pmodel.ProductId;
            model.ProductName = pmodel.ProductName;
            model.Sku = pmodel.Sku;

            return View("~/Plugins/Misc.GroupBuy/Views/GroupBuyManager/EditGroupBuyProduct.cshtml", model);
        }

        [HttpPost]
        [AdminAuthorize]
        public ActionResult EditGroupBuyProduct(GroupBuyViewModel groupBuyViewModel)
        {
            if (ModelState.IsValid)
            {
                var pmodel = _groupBuyService.GetProductSellQuantityDetails(groupBuyViewModel.Id);                     
                pmodel.StartDate = groupBuyViewModel.StartDate.Value;
                pmodel.EndDate = groupBuyViewModel.EndDate.Value;

                _groupBuyService.UpdateProductQunatityAndDates(pmodel);
               
                    return RedirectToAction("ManageSellsBasedOnQuantity");
            }
            return View("~/Plugins/Misc.GroupBuy/Views/GroupBuyManager/EditGroupBuyProduct.cshtml", groupBuyViewModel);
            //return RedirectToAction("EditGroupBuyProduct", new { id = groupBuyViewModel.Id});
        }

        [HttpPost]
        public ActionResult UpdateTierPrice(CustomTierPriceModel productSellModel)
        {
            //var model=new ProductSellModel
            var tierPriceRecord = _groupBuyService.GetTierPriceById(productSellModel.Id);
            tierPriceRecord.Price = productSellModel.Price;
            tierPriceRecord.Quantity = productSellModel.Quantity;
            _groupBuyService.UpdateTierPrice(tierPriceRecord);
            return new NullJsonResult();
            //return RedirectToAction("ManageSellsBasedOnQuantity");
        }

        [AdminAuthorize]
        public ActionResult TierPrice(int id)
        {
            CustomTierPriceModel model = new CustomTierPriceModel();            
            model.ProductSellId = id;           
            return View("~/Plugins/Misc.GroupBuy/Views/GroupBuyManager/TierPrice.cshtml", model);
        }

        [HttpPost]
        [AdminAuthorize]
        public ActionResult TierPrice(GroupBuyTierPriceModel groupBuyTierPriceModel)
        {
            if (ModelState.IsValid)
            {
                var groupBuy = _groupBuyService.GetProductSellQuantityDetails(groupBuyTierPriceModel.Id);
                if (groupBuy != null)
                {
                    var product = _productService.GetProductById(groupBuy.ProductId);
                    if (product != null)
                    {
                        var productTierPrices = product.TierPrices;

                        foreach (var item in productTierPrices)
                        {
                            var tp = new CustomTierPriceModel();
                            tp.Price = decimal.Zero;
                            tp.ProductSellId = groupBuyTierPriceModel.ProductSellId;
                            tp.Quantity = item.Quantity;
                            tp.MaxQuantity = groupBuyTierPriceModel.Quantity;
                            tp.ProductId = product.Id;
                            _groupBuyService.CreateTierPrice(tp);
                        }
                    }
                }                
                return RedirectToAction("TierPrice", new { id = groupBuyTierPriceModel.ProductSellId });
            }
            return View("~/Plugins/Misc.GroupBuy/Views/GroupBuyManager/TierPrice.cshtml", groupBuyTierPriceModel);
        }

        [AdminAuthorize]
        public ActionResult DeleteTierPrice(int id)
        {
            _groupBuyService.DeleteTierPrice(id);            
            return new NullJsonResult();
        }

        [AdminAuthorize]
        public ActionResult DeleteProductAllDetails(int id)
        {
            _groupBuyService.DeleteProductDetails(id);
            return RedirectToAction("ManageSellsBasedOnQuantity");
        }
        [HttpPost]
        [AdminAuthorize]
        public ActionResult TierPriceList(int productSellId)
        {
            var model = _groupBuyService.GetAllTierPriceById(productSellId);
            var gridModel = new DataSourceResult
            {
                Data = model,
                Total = model.TotalCount
            };
            return Json(gridModel);
        }

       

        #region Group Buy Email

        [AdminAuthorize]
        public ActionResult GroupBuyEmail(int id)
        {
            var detailsModel = new GroupBuyEmailModel();
            var groupBuyEmails = _newsLetterSubscriptionGroupBuyService.GetAllNewsLetterSubscriptionsGroupBuy();
            var groupBuyProduct = _groupBuyService.GetAllGroupBuyProductList().Where(a => a.Id == id).FirstOrDefault();
            var productDetails = _productService.GetProductById(groupBuyProduct.ProductId);
            if(productDetails != null)
            {
                detailsModel.ProductId = productDetails.Id.ToString();
                detailsModel.ProductName = productDetails.Name;
            }
            if (groupBuyEmails != null && groupBuyEmails.Count() > 0)
            {
               var activeGroupBuyEmails = groupBuyEmails.Where(x => x.Active == true).ToList();
                string strEmailContent = EmailHelper.RenderRazorViewToString(this, "~/Plugins/Misc.GroupBuy/Views/GroupBuyManager/GroupBuyEmailTemplate.cshtml",detailsModel);

                var defaultEmailAccountId = _emailAccountSettings.DefaultEmailAccountId;
                if (defaultEmailAccountId != 0)
                {
                    var emailAccount = _emailAccountService.GetEmailAccountById(defaultEmailAccountId);
                    if (emailAccount == null)
                        //No email account found with the specified id
                        return RedirectToAction("ManageSellsBasedOnQuantity");
                    try
                    {
                        EmailModel emailcontent = new EmailModel();
                        emailcontent.FromEmail = emailAccount.Email;
                        foreach (var item in activeGroupBuyEmails)
                        {
                            emailcontent.ToAddress = item.Email;
                        }
                        emailcontent.EmailSubject = "Group Buy";
                        emailcontent.EmailBody = strEmailContent;
                        _emailSender.SendEmail(emailAccount, emailcontent.EmailSubject, emailcontent.EmailBody, emailcontent.FromEmail, emailcontent.FromName, emailcontent.ToAddress, null, null, null, null);
                    }
                    catch (Exception e)
                    {

                    }

                }
            }
            return RedirectToAction("ManageSellsBasedOnQuantity");
        }

        #endregion        

        #endregion
    }
}
