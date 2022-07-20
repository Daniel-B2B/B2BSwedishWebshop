using Nop.Core.Domain.Messages;
using Nop.Core.Domain.Stores;
using Nop.Plugin.Misc.NewsletterGroupBuy.Domain;
using Nop.Services.Messages;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.NewsletterGroupBuy.Services
{
   public interface INewsletterMessageTokenProvider
    {
        /// <summary>
        /// Add newsletter subscription tokens
        /// </summary>
        /// <param name="tokens">List of already added tokens</param>
        /// <param name="subscription">Newsletter subscription</param>
        void AddNewsLetterSubscriptionGroupByTokens(IList<Token> tokens, NewsLetterSubscriptionGroupBuy subscription);

        void AddStoreTokens(IList<Token> tokens, Store store, EmailAccount emailAccount);

    }
}
