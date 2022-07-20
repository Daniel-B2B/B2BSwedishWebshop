using Autofac;
using Autofac.Core;
using Nop.Core.Configuration;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Plugin.Widgets.SampleRequestForm.Data;
using Nop.Plugin.Widgets.SampleRequestForm.Domain;
using Nop.Plugin.Widgets.SampleRequestForm.Services;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Widgets.SampleRequestForm
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="config">Config</param>
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterType<SampleRequestService>().As<ISampleRequestService>().InstancePerLifetimeScope();

            //data context
            this.RegisterPluginDataContext<CustomObjectContext>(builder, "nop_object_context_sample_request");

            //override required repository with our custom context
            builder.RegisterType<EfRepository<SampleRequestFormRecord>>()
                .As<IRepository<SampleRequestFormRecord>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_context_sample_request"))
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