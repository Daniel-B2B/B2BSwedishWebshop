using Autofac;
using Autofac.Core;
using Nop.Core.Configuration;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.PLugin.Misc.ImportProducts.Data;
using Nop.PLugin.Misc.ImportProducts.Domain;
using Nop.PLugin.Misc.ImportProducts.Services;
using Nop.Web.Framework.Mvc;

namespace Nop.PLugin.Misc.ImportProducts
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
            builder.RegisterType<ExcelImportService>().As<IExcelImportService>().InstancePerLifetimeScope();
            builder.RegisterType<ClipperProductsService>().As<IClipperProductsService>().InstancePerLifetimeScope();

            //builder.RegisterType<NewsletterGroupBuyWorkflowMessageService>().As<INewsletterGroupBuyWorkflowMessageService>().InstancePerLifetimeScope();
            //builder.RegisterType<NewsLetterSubscriptionGroupBuyService>().As<INewsLetterSubscriptionGroupBuyService>().InstancePerLifetimeScope();
            //builder.RegisterType<NewsletterMessageTokenProvider>().As<INewsletterMessageTokenProvider>().InstancePerLifetimeScope();

            //data
            this.RegisterPluginDataContext<CustomObjectContext>(builder, "nop_object_context_ImportProducts");

            //override Required repository wuth our custom context
            builder.RegisterType<EfRepository<CustomProduct>>()
                .As<IRepository<CustomProduct>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_context_ImportProducts"))
                .InstancePerLifetimeScope();
         
        }
    }
}


