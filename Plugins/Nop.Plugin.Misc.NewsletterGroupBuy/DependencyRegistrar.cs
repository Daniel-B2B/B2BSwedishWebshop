using Autofac;
using Autofac.Core;
using Nop.Core.Configuration;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Plugin.Misc.NewsletterGroupBuy.Data;
using Nop.Plugin.Misc.NewsletterGroupBuy.Domain;
using Nop.Plugin.Misc.NewsletterGroupBuy.Services;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Misc.NewsletterGroupBuy
{    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="config">Config</param>
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterType<NewsletterGroupBuyWorkflowMessageService>().As<INewsletterGroupBuyWorkflowMessageService>().InstancePerLifetimeScope();
            builder.RegisterType<NewsLetterSubscriptionGroupBuyService>().As<INewsLetterSubscriptionGroupBuyService>().InstancePerLifetimeScope();
            builder.RegisterType<NewsletterMessageTokenProvider>().As<INewsletterMessageTokenProvider>().InstancePerLifetimeScope();
            //data context
            this.RegisterPluginDataContext<CustomObjectContext>(builder, "nop_object_context_newsletterGroupBuy");

            //override required repository with our custom context
            builder.RegisterType<EfRepository<NewsLetterSubscriptionGroupBuy>>()
                .As<IRepository<NewsLetterSubscriptionGroupBuy>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_context_newsletterGroupBuy"))
                .InstancePerLifetimeScope();
        }

        /// <summary>
        /// Order of this dependency registrar implementation
        /// </summary>
        public int Order
        {
            get { return 1; }
        }
    }
}
