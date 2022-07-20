using Autofac;
using Autofac.Core;
using Nop.Core.Configuration;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Plugin.Misc.ImportPriceClass.Domain;
using Nop.Plugin.Misc.ImportPriceClass.Services;
using Nop.PLugin.Misc.ImportPriceClass.Data;
using Nop.Web.Framework.Mvc;

namespace Nop.PLugin.Misc.ImportPriceClass
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
            builder.RegisterType<PriceClassService>().As<IPriceClassService>().InstancePerLifetimeScope();
            //builder.RegisterType<ClipperProductsService>().As<IClipperProductsService>().InstancePerLifetimeScope();

            //builder.RegisterType<NewsletterGroupBuyWorkflowMessageService>().As<INewsletterGroupBuyWorkflowMessageService>().InstancePerLifetimeScope();
            //builder.RegisterType<NewsLetterSubscriptionGroupBuyService>().As<INewsLetterSubscriptionGroupBuyService>().InstancePerLifetimeScope();
            //builder.RegisterType<NewsletterMessageTokenProvider>().As<INewsletterMessageTokenProvider>().InstancePerLifetimeScope();

            //data
            this.RegisterPluginDataContext<CustomObjectContext>(builder, "nop_object_context_ImprintPriceClass");

            //override Required repository wuth our custom context
            builder.RegisterType<EfRepository<ImprintPriceClass>>()
                .As<IRepository<ImprintPriceClass>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_context_ImprintPriceClass"))
                .InstancePerLifetimeScope();
         
        }
    }
}


