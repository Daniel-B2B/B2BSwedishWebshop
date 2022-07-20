using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Plugin.Misc.NewsletterGroupBuy.Domain;
using Nop.Plugin.Misc.NewsletterGroupBuy.Models;
using Nop.Plugin.Misc.NewsletterGroupBuy.Services;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.NewsletterGroupBuy.Controllers
{
    public class NewslettergroupBuyController : BasePluginController
    {
        #region fields

        private readonly ILocalizationService _localizationService;
        private readonly IWorkContext _workContext;
        private readonly IWorkflowMessageService _workflowMessageService;
        private readonly IStoreContext _storeContext;
        private readonly CustomerSettings _customerSettings;
        private readonly INewsLetterSubscriptionGroupBuyService _newsLetterSubscriptionGroupBuyService;
        private readonly INewsletterGroupBuyWorkflowMessageService _newsletterGroupBuyWorkflowMessageService;

        #endregion

        #region ctor

        public NewslettergroupBuyController(ILocalizationService localizationService,
            IWorkContext workContext,
            INewsLetterSubscriptionGroupBuyService newsLetterSubscriptionGroupBuyService,
            IWorkflowMessageService workflowMessageService,
            IStoreContext storeContext,
            CustomerSettings customerSettings,
            INewsletterGroupBuyWorkflowMessageService newsletterGroupBuyWorkflowMessageService
            )
        {
            this._localizationService = localizationService;
            this._workContext = workContext;
            this._newsLetterSubscriptionGroupBuyService = newsLetterSubscriptionGroupBuyService;
            this._workflowMessageService = workflowMessageService;
            this._storeContext = storeContext;
            this._customerSettings = customerSettings;
            this._newsletterGroupBuyWorkflowMessageService = newsletterGroupBuyWorkflowMessageService;
        }

        #endregion

        #region Methods

        [ChildActionOnly]
        public ActionResult NewsletterGroupBuyBox()
        {
            if (_customerSettings.HideNewsletterBlock)
                return Content("");

            var model = new NewsletterGroupBuyBoxModel()
            {
                AllowToUnsubscribe = _customerSettings.NewsletterBlockAllowToUnsubscribe
            };
            return PartialView("~/Plugins/Misc.NewsletterGroupBuy/Views/NewsletterGroupBuyBox.cshtml", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SubscribeNewsletterGroupBuy(string email, bool subscribe)
        {
            string result;
            bool success = false;

            if (!CommonHelper.IsValidEmail(email))
            {
                result = _localizationService.GetResource("Newsletter.Email.Wrong");
            }
            else
            {
                email = email.Trim();

                var subscription = _newsLetterSubscriptionGroupBuyService.GetNewsLetterSubscriptionGroupBuyByEmailAndStoreId(email, _storeContext.CurrentStore.Id);
                if (subscription != null)
                {
                    if (subscribe)
                    {
                        if (!subscription.Active)
                        {
                            _newsletterGroupBuyWorkflowMessageService.SendNewsLetterGroupBuySubscriptionActivationMessage(subscription, _workContext.WorkingLanguage.Id);
                        }
                        result = _localizationService.GetResource("Newsletter.SubscribeEmailSent");
                    }
                    else
                    {
                        if (subscription.Active)
                        {
                            _newsletterGroupBuyWorkflowMessageService.SendNewsLetterGroupBuySubscriptionDeactivationMessage(subscription, _workContext.WorkingLanguage.Id);
                        }
                        result = _localizationService.GetResource("Newsletter.UnsubscribeEmailSent");
                    }
                }
                else if (subscribe)
                {
                    subscription = new NewsLetterSubscriptionGroupBuy
                    {
                        NewsLetterSubscriptionGuid = Guid.NewGuid(),
                        Email = email,
                        Active = false,
                        StoreId = _storeContext.CurrentStore.Id,
                        CreatedOnUtc = DateTime.UtcNow
                    };
                    _newsLetterSubscriptionGroupBuyService.InsertNewsLetterSubscriptionGroupBuy(subscription);
                    _newsletterGroupBuyWorkflowMessageService.SendNewsLetterGroupBuySubscriptionActivationMessage(subscription, _workContext.WorkingLanguage.Id);

                    result = _localizationService.GetResource("Newsletter.SubscribeEmailSent");
                }
                else
                {
                    result = _localizationService.GetResource("Newsletter.UnsubscribeEmailSent");
                }
                success = true;
            }

            return Json(new
            {
                Success = success,
                Result = result,
            });
        }

        public ActionResult SubscriptionActivation(Guid token, bool active)
        {
            var subscription = _newsLetterSubscriptionGroupBuyService.GetNewsLetterSubscriptionGroupBuyByGuid(token);
            if (subscription == null)
                return RedirectToRoute("HomePage");

            var model = new SubscriptionActivationModel();

            if (active)
            {
                subscription.Active = true;
                _newsLetterSubscriptionGroupBuyService.UpdateNewsLetterSubscriptionGroupBuy(subscription);
            }
            else
                _newsLetterSubscriptionGroupBuyService.DeleteNewsLetterSubscriptionGroupBuy(subscription);

            model.Result = active
                ? _localizationService.GetResource("Newsletter.ResultActivated")
                : _localizationService.GetResource("Newsletter.ResultDeactivated");

            return View(model);
        }

        #endregion

        #region Admin Methods

        [AdminAuthorize]
        public ActionResult List()
        {
            var model = new List<NewsLetterSubscriptionGroupBuy>();
            return View("~/Plugins/Misc.NewsletterGroupBuy/Views/NewsletterAdmin/List.cshtml", model);
        }

        [HttpPost]
        [AdminAuthorize]
        public ActionResult List(DataSourceRequest command)
        {
            var newslettersList = _newsLetterSubscriptionGroupBuyService.GetAllNewsLetterSubscriptionsGroupBuy();
            var gridModel = new DataSourceResult
            {
                Data = newslettersList,
                Total = newslettersList.TotalCount
            };
            return Json(gridModel);
        }

        #endregion
    }
}
