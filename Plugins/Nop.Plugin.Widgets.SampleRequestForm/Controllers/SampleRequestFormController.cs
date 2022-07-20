using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Messages;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Widgets.SampleRequestForm.Domain;
using Nop.Plugin.Widgets.SampleRequestForm.Helpers;
using Nop.Plugin.Widgets.SampleRequestForm.Models;
using Nop.Plugin.Widgets.SampleRequestForm.Services;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Nop.Plugin.Widgets.SampleRequestForm.Controllers
{
   public class SampleRequestFormController : BasePluginController
    {
        #region Fields

        private readonly IStoreContext _storeContext;
        private readonly IWorkContext _workContext;
        private readonly IProductService _productService;
        private readonly IEmailAccountService _emailAccountService;
        private readonly IEmailSender _emailSender;
        private readonly EmailAccountSettings _emailAccountSettings;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly SampleRequestFormSettings _sampleRequestFormSettings;
        private readonly ISettingService _settingService;
        private readonly ILocalizationService _localizationService;
        private readonly IProductAttributeService _productAttributeService;
        private readonly IProductAttributeFormatter _productAttributeFormatter;
        private readonly ShoppingCartSettings _shoppingCartSettings;
        private readonly CatalogSettings _catalogSettings;
        private readonly IProductAttributeParser _productAttributeParser;
        private readonly ICustomerService _customerService;
        private readonly IPictureService _pictureService;
        private readonly MediaSettings _mediaSettings;
        private readonly IQueuedEmailService _queuedEmailService;
        private readonly IDownloadService _downloadService;
        private readonly ILogger _loggerService;
        private readonly ISampleRequestService _sampleRequestService;
        #endregion

        #region Ctor

        public SampleRequestFormController(IWorkContext workContext,
            IStoreContext storeContext,
            IProductService productService,
            IEmailAccountService emailAccountService,
            IEmailSender emailSender,
            EmailAccountSettings emailAccountSettings,
            IShoppingCartService shoppingCartService,
            SampleRequestFormSettings sampleRequestFormSettings,
            ISettingService settingService,
            ILocalizationService localizationService,
            IProductAttributeService productAttributeService,            
            IProductAttributeFormatter productAttributeFormatter,
            ShoppingCartSettings shoppingCartSettings,
            CatalogSettings catalogSettings,
            IProductAttributeParser productAttributeParser,
            ICustomerService customerService,
            IPictureService pictureService,
            MediaSettings mediaSettings,
            IQueuedEmailService queuedEmailService,
            IDownloadService downloadService,
            ILogger loggerService,
            ISampleRequestService sampleRequestService 
           )
        {
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._productService = productService;
            this._emailAccountService = emailAccountService;
            this._emailSender = emailSender;
            this._emailAccountSettings = emailAccountSettings;
            this._shoppingCartService = shoppingCartService;
            this._sampleRequestFormSettings = sampleRequestFormSettings;
            this._settingService = settingService;
            this._localizationService = localizationService;
            this._productAttributeService = productAttributeService;           
            this._productAttributeFormatter = productAttributeFormatter;
            this._shoppingCartSettings = shoppingCartSettings;
            this._catalogSettings = catalogSettings;
            this._productAttributeParser = productAttributeParser;
            this._customerService = customerService;
            this._pictureService = pictureService;
            this._mediaSettings = mediaSettings;
            this._queuedEmailService = queuedEmailService;
            this._downloadService = downloadService;
            this._loggerService = loggerService;
            this._sampleRequestService = sampleRequestService;
        }

        #endregion

        #region Configuration Methods

        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure()
        {
            var model = new ConfigurationModel();
            return View("~/Plugins/Widgets.SampleRequestForm/Views/Configure.cshtml", model);
        }

        [HttpPost]
        [ChildActionOnly]
        [AdminAuthorize]
        [FormValueRequired("save")]
        public ActionResult Configure(ConfigurationModel model)
        {           
            //save settings            
            _sampleRequestFormSettings.EmailSubject = model.EmailSubject;
            //_sampleRequestFormSettings.WidgetZone = model.ZoneId;
            _sampleRequestFormSettings.SampleRequestEmail = model.SampleRequestEmail;
            _sampleRequestFormSettings.SampleRequestSendEmail = model.SampleRequestSendEmail;
            _settingService.SaveSetting(_sampleRequestFormSettings);
            SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }
        #endregion

        #region Methods
        public ActionResult FrontEndView(string widgetZone, object additionalData = null)
        {
            var model = new SampleRequestFormModel();
            model.ProductId = (int)additionalData;
            return View("~/Plugins/Widgets.SampleRequestForm/Views/FrontEndView.cshtml", model);
        }

        public ActionResult SendSampleRequest()
        {
            return View();
        }

       [HttpPost]
        public ActionResult SendSampleRequest(SampleRequestFormModel sampleRequestFormModel)
        {
            var sampleRequestFormRecord = new SampleRequestFormRecord();
            var product = _productService.GetProductById(sampleRequestFormModel.ProductId);
            if(product != null)
            {
                sampleRequestFormModel.ProductId =  product.Id ;
                sampleRequestFormModel.Product = product.Name;
            }
            sampleRequestFormRecord.Comments = sampleRequestFormModel.Comments;
            sampleRequestFormRecord.Company = sampleRequestFormModel.Company;
            sampleRequestFormRecord.Email = sampleRequestFormModel.Email;
            sampleRequestFormRecord.Name = sampleRequestFormModel.Name;
            sampleRequestFormRecord.PhoneNumber = sampleRequestFormModel.PhoneNumber;
            sampleRequestFormRecord.Address = sampleRequestFormModel.Address;
            sampleRequestFormRecord.City = sampleRequestFormModel.City;
            sampleRequestFormRecord.ZipCode = sampleRequestFormModel.ZipCode;
            sampleRequestFormRecord.PotentialOrderQty = sampleRequestFormModel.PotentialOrderQty;
            sampleRequestFormRecord.Product ="Product Id: " +  sampleRequestFormModel.ProductId + "," +"Product Name: " + sampleRequestFormModel.Product;
            _sampleRequestService.AddSampleRequestFormRecord(sampleRequestFormRecord);

            //Email funstion
            try
            {
                bool sampleRequestSendEmail = _sampleRequestFormSettings.SampleRequestSendEmail;
                if (ModelState.IsValid)
                {
                    try
                    {
                       // sampleRequestFormModel.Name = sampleRequestFormModel.Name;
                        if (sampleRequestSendEmail)
                        {
                            #region Email

                            string EnquiryToEmail = "";
                            var defaultEmailAccountId = _emailAccountSettings.DefaultEmailAccountId;
                            if (!string.IsNullOrEmpty(_sampleRequestFormSettings.SampleRequestEmail))
                            {
                                EnquiryToEmail = _sampleRequestFormSettings.SampleRequestEmail;
                            }
                            else
                            {                              
                                if (defaultEmailAccountId != 0)
                                {
                                    var emailAccount = _emailAccountService.GetEmailAccountById(defaultEmailAccountId);
                                    EnquiryToEmail = emailAccount.Email.ToString();
                                }
                            }

                            if (!string.IsNullOrEmpty(EnquiryToEmail))
                            {
                                string strEmailContent = EmailHelper.RenderRazorViewToString(this, "~/Plugins/Widgets.SampleRequestForm/Views/SampleRequestEmailTemplate.cshtml", sampleRequestFormModel);

                                if (defaultEmailAccountId != 0)
                                {
                                    var emailAccount = _emailAccountService.GetEmailAccountById(defaultEmailAccountId);
                                    if (emailAccount == null)
                                        return Json(new
                                        {
                                            html = "Der opstod et problem med at sende din anmodning."
                                        });

                                    #region "generate enquiry Email"
                                    try
                                    {
                                        //Enquiry mail
                                        EmailModel emailcontent = new EmailModel();
                                        emailcontent.FromEmail = sampleRequestFormModel.Email; ;
                                        emailcontent.FromName = sampleRequestFormModel.Name;
                                        emailcontent.ToAddress = EnquiryToEmail;
                                        if(!string.IsNullOrEmpty(_sampleRequestFormSettings.EmailSubject))
                                        {
                                            emailcontent.EmailSubject = _sampleRequestFormSettings.EmailSubject;
                                        }
                                        else
                                        {
                                            emailcontent.EmailSubject = "Forespørgsel på vareprøve (" + sampleRequestFormModel.Product + ") ";
                                        }
                                        emailcontent.EmailBody = strEmailContent;
                                        var bccAdrresses = new List<string>();
                                        bccAdrresses.Add(emailcontent.FromEmail);

                                        _queuedEmailService.InsertQueuedEmail(new QueuedEmail
                                        {
                                            From = emailcontent.FromEmail,
                                            FromName = emailcontent.FromName,
                                            To = emailcontent.ToAddress,
                                            ToName = "",
                                            Priority = QueuedEmailPriority.High,
                                            Subject = emailcontent.EmailSubject,
                                            Body = emailcontent.EmailBody,
                                            CreatedOnUtc = DateTime.UtcNow,
                                            EmailAccountId = emailAccount.Id,
                                            Bcc = emailcontent.FromEmail
                                        });
                                        emailcontent = null;
                                        return Json(new
                                        {
                                            html = "Din anmodning er blevet sendt, med en kopi videresendt til din egen indbakke."
                                        });
                                    }

                                    catch (Exception ex)
                                    {
                                        _loggerService.Error(ex.Message, ex);
                                        LogException(ex);
                                        return Json(new
                                        {
                                            html = "Din anmodning blev ikke sendt!"
                                        });
                                    }

                                    #endregion
                                }
                            }
                            else
                            {
                                return Json(new
                                {
                                    html = "Der opstod et problem med at sende en forespørgsel."
                                });
                            }

                            #endregion
                        }
                        else
                        {
                            return Json(new
                            {
                                html = "Din anmodning blev sendt med succes!"
                            });
                        }
                    }
                    catch (Exception e)
                    {
                        _loggerService.Error(e.Message, e);
                    }
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(ex.Message, ex);
            }
            return Json(new
            {
                html = "Din anmodning blev ikke sendt!"
            });
        }

        #endregion

        [HttpPost]
        public ActionResult CallMeRequest(CallMeModel callMeModel)
        {
            try
            {
                bool sampleRequestSendEmail = _sampleRequestFormSettings.SampleRequestSendEmail;
                if (ModelState.IsValid)
                {
                    try
                    {
                        if (sampleRequestSendEmail)
                        {
                            #region Email

                            string EnquiryToEmail = "";
                            var defaultEmailAccountId = _emailAccountSettings.DefaultEmailAccountId;
                            if (!string.IsNullOrEmpty(_sampleRequestFormSettings.SampleRequestEmail))
                            {
                                EnquiryToEmail = _sampleRequestFormSettings.SampleRequestEmail;
                            }
                            else
                            {
                                if (defaultEmailAccountId != 0)
                                {
                                    var emailAccount = _emailAccountService.GetEmailAccountById(defaultEmailAccountId);
                                    EnquiryToEmail = emailAccount.Email.ToString();
                                }
                            }

                            if (!string.IsNullOrEmpty(EnquiryToEmail))
                            {
                                string strEmailContent = EmailHelper.RenderRazorViewToString(this, "~/Plugins/Widgets.SampleRequestForm/Views/CallMeEmailTemplate.cshtml", callMeModel);

                                if (defaultEmailAccountId != 0)
                                {
                                    var emailAccount = _emailAccountService.GetEmailAccountById(defaultEmailAccountId);
                                    if (emailAccount == null)
                                        return Json(new
                                        {
                                            html = "Der opstod et problem med at sende din anmodning."
                                        });

                                    #region "generate enquiry Email"
                                    try
                                    {
                                        //Enquiry mail
                                        EmailModel emailcontent = new EmailModel();
                                        emailcontent.FromEmail = callMeModel.Email;
                                        emailcontent.FromName = callMeModel.Name;
                                        emailcontent.ToAddress = EnquiryToEmail;
                                        emailcontent.EmailSubject = "Call Me Request";
                                        emailcontent.EmailBody = strEmailContent;
                                        var bccAdrresses = new List<string>();
                                        bccAdrresses.Add(emailcontent.FromEmail);

                                        _queuedEmailService.InsertQueuedEmail(new QueuedEmail
                                        {
                                            From = emailcontent.FromEmail,
                                            FromName = emailcontent.FromName,
                                            To = emailcontent.ToAddress,
                                            ToName = "",
                                            Priority = QueuedEmailPriority.High,
                                            Subject = emailcontent.EmailSubject,
                                            Body = emailcontent.EmailBody,
                                            CreatedOnUtc = DateTime.UtcNow,
                                            EmailAccountId = emailAccount.Id,
                                            Bcc = emailcontent.FromEmail
                                        });
                                        emailcontent = null;
                                        return Json(new
                                        {
                                            html = "Din anmodning er blevet sendt, med en kopi videresendt til din egen indbakke."
                                        });
                                    }

                                    catch (Exception ex)
                                    {
                                        _loggerService.Error(ex.Message, ex);
                                        LogException(ex);
                                        return Json(new
                                        {
                                            html = "Din anmodning blev ikke sendt!"
                                        });
                                    }

                                    #endregion
                                }
                            }
                            else
                            {
                                return Json(new
                                {
                                    html = "Der opstod et problem med at sende en forespørgsel."
                                });
                            }

                            #endregion
                        }
                        else
                        {
                            return Json(new
                            {
                                html = "Din anmodning blev sendt med succes!"
                            });
                        }
                    }
                    catch (Exception e)
                    {
                        _loggerService.Error(e.Message, e);
                    }
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(ex.Message, ex);
            }
            return Json(new
            {
                html = "Din anmodning blev ikke sendt!"
            });
        }
    }
}
