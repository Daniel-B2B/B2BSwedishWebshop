﻿using Nop.Plugin.Misc.NewsletterGroupBuy.Domain;


namespace Nop.Plugin.Misc.NewsletterGroupBuy.Services
{
    public interface INewsletterGroupBuyWorkflowMessageService
    {
        /// <summary>
        /// Sends a newsletter subscription activation message
        /// </summary>
        /// <param name="subscription">Newsletter subscription</param>
        /// <param name="languageId">Language identifier</param>
        /// <returns>Queued email identifier</returns>
        int SendNewsLetterGroupBuySubscriptionActivationMessage(NewsLetterSubscriptionGroupBuy subscription,
            int languageId);

        /// <summary>
        /// Sends a newsletter subscription deactivation message
        /// </summary>
        /// <param name="subscription">Newsletter subscription</param>
        /// <param name="languageId">Language identifier</param>
        /// <returns>Queued email identifier</returns>
        int SendNewsLetterGroupBuySubscriptionDeactivationMessage(NewsLetterSubscriptionGroupBuy subscription,
            int languageId);      

    }
}