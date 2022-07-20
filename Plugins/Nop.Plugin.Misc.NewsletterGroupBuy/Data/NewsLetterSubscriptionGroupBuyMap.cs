using Nop.Data.Mapping;
using Nop.Plugin.Misc.NewsletterGroupBuy.Domain;

namespace Nop.Plugin.Misc.NewsletterGroupBuy.Data
{
   public class NewsLetterSubscriptionGroupBuyMap : NopEntityTypeConfiguration<NewsLetterSubscriptionGroupBuy>
    {
        public NewsLetterSubscriptionGroupBuyMap()
        {
            this.ToTable("Akiko_NewsLetterSubscriptionGroupBuy");
            this.HasKey(nls => nls.Id);

            this.Property(nls => nls.NewsLetterSubscriptionGuid);
            this.Property(nls => nls.Email).IsRequired().HasMaxLength(255);
            this.Property(nls => nls.Active);
            this.Property(nls => nls.StoreId);
            this.Property(nls => nls.CreatedOnUtc);
        }
    }
}