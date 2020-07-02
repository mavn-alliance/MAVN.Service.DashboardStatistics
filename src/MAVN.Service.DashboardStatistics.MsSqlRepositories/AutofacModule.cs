using Autofac;
using MAVN.Persistence.PostgreSQL.Legacy;
using MAVN.Service.DashboardStatistics.Domain.Repositories;
using MAVN.Service.DashboardStatistics.MsSqlRepositories.Repositories;

namespace MAVN.Service.DashboardStatistics.MsSqlRepositories
{
    public class AutofacModule : Module
    {
        private readonly string _connectionString;

        public AutofacModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterPostgreSQL(_connectionString,
                connectionString => new DashboardStatisticsContext(connectionString, false),
                dbConnection => new DashboardStatisticsContext(dbConnection));

            builder.RegisterType<CustomerActivityRepository>()
                .As<ICustomerActivityRepository>()
                .SingleInstance();

            builder.RegisterType<CustomerStatisticRepository>()
                .As<ICustomerRegistrationRepository>()
                .SingleInstance();

            builder.RegisterType<VoucherOperationsStatisticRepository>()
                .As<IVoucherOperationsStatisticRepository>()
                .SingleInstance();

            builder.RegisterType<PartnerVouchersDailyStatsRepository>()
                .As<IPartnerVouchersDailyStatsRepository>()
                .SingleInstance();
        }
    }
}
