using Autofac;
using JetBrains.Annotations;
using Lykke.Sdk;
using Lykke.SettingsReader;
using MAVN.Job.TokensStatistics.Client;
using MAVN.Service.DashboardStatistics.Services;
using MAVN.Service.DashboardStatistics.Settings;

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
        }
    }
}
