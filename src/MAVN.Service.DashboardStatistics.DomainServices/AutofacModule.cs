using Autofac;
using MAVN.Service.DashboardStatistics.Domain.Services;

namespace MAVN.Service.DashboardStatistics.DomainServices
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LeadStatisticService>()
                .As<ILeadStatisticService>()
                .SingleInstance();

            builder.RegisterType<CustomerStatisticService>()
                .As<ICustomerStatisticService>()
                .SingleInstance();
        }
    }
}
