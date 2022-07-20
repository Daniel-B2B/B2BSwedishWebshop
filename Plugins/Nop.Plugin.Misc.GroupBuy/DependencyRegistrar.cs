using Autofac;
using Autofac.Core;
using Nop.Core.Configuration;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Plugin.Misc.GroupBuy.Data;
using Nop.Plugin.Misc.GroupBuy.Domain;
using Nop.Plugin.Misc.GroupBuy.Models;
using Nop.Plugin.Misc.GroupBuy.Services;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Misc.GroupBuy
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order
        {
            get
            {
                return 1;
            }
        }

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterType<GroupBuyService>().As<IGroupBuyService>().InstancePerLifetimeScope();            

            builder.RegisterType<NewsletterGroupBuyWorkflowMessageService>().As<INewsletterGroupBuyWorkflowMessageService>().InstancePerLifetimeScope();
            builder.RegisterType<NewsLetterSubscriptionGroupBuyService>().As<INewsLetterSubscriptionGroupBuyService>().InstancePerLifetimeScope();
            builder.RegisterType<NewsletterMessageTokenProvider>().As<INewsletterMessageTokenProvider>().InstancePerLifetimeScope();

            //data
            this.RegisterPluginDataContext<CustomObjectContext>(builder, "nop_object_context_GroupBuy");

            //override Required repository wuth our custom context
            builder.RegisterType<EfRepository<GroupBuyProduct>>()
                .As<IRepository<GroupBuyProduct>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_context_GroupBuy"))
                .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<CustomTierPriceModel>>()
                .As<IRepository<CustomTierPriceModel>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_context_GroupBuy"))
                .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<NewsLetterSubscriptionGroupBuy>>()
               .As<IRepository<NewsLetterSubscriptionGroupBuy>>()
               .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_context_GroupBuy"))
               .InstancePerLifetimeScope();
        }
    }
}
