using Autofac;
using JetBrains.Annotations;
using Lykke.Sdk;
using Lykke.SettingsReader;
using MAVN.Job.TokensStatistics.Client;
using MAVN.Service.DashboardStatistics.Domain.Services;
using MAVN.Service.DashboardStatistics.DomainServices;
using MAVN.Service.DashboardStatistics.Services;
using MAVN.Service.DashboardStatistics.Settings;
using StackExchange.Redis;

namespace MAVN.Service.DashboardStatistics.Modules
{
    [UsedImplicitly]
    public class ServiceModule : Module
    {
        private readonly AppSettings _settings;

        public ServiceModule(IReloadingManager<AppSettings> settings)
        {
            _settings = settings.CurrentValue;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StartupManager>()
                .As<IStartupManager>()
                .SingleInstance();

            builder.RegisterType<ShutdownManager>()
                .As<IShutdownManager>()
                .AutoActivate()
                .SingleInstance();

            builder.RegisterModule(new DomainServices.AutofacModule());
            builder.RegisterModule(new MsSqlRepositories.AutofacModule(_settings.DashboardStatisticsService.Db.MsSqlConnectionString));

            // Clients

            builder.RegisterTokensStatisticsClient(_settings.TokensStatisticsJobClient, null);

            builder.Register(context =>
            {
                var connectionMultiplexer = ConnectionMultiplexer.Connect(_settings.DashboardStatisticsService.Redis.ConnectionString);
                connectionMultiplexer.IncludeDetailInExceptions = false;
                return connectionMultiplexer;
            })
                .As<IConnectionMultiplexer>()
                .SingleInstance();

            builder.RegisterType<RedisLocksService>()
                .As<IRedisLocksService>()
                .SingleInstance();

            builder.RegisterType<VoucherOperationsStatisticService>()
                .As<IVoucherOperationsStatisticService>()
                .WithParameter(TypedParameter.From(_settings.DashboardStatisticsService.LockTimeOut))
                .AutoActivate()
                .SingleInstance();
        }
    }
}
