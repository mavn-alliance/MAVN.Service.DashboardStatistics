using Autofac;
using Common;
using JetBrains.Annotations;
using MAVN.Service.DashboardStatistics.Rabbit.Subscribers;
using MAVN.Service.DashboardStatistics.Settings;
using Lykke.SettingsReader;
using MAVN.Job.TokensStatistics.Client;

namespace MAVN.Service.DashboardStatistics
{
    [UsedImplicitly]
    public class AutofacModule : Module
    {
        private const string QueueName = "dashboard-statistic";
        private const string BonusReceivedExchangeName = "lykke.wallet.bonusreceived";
        private const string LeadStateChangedExchangeName = "lykke.bonus.leadstatechanged";
        private const string CustomerRegisteredExchangeName = "lykke.customer.registration";
        private const string PartnersPaymentTokensReservedExchangeName = "lykke.wallet.partnerspaymenttokensreserved";
        private const string CustomerPhoneVerifiedEventExchangeName = "lykke.customer.phoneverified";

        private readonly AppSettings _settings;

        public AutofacModule(IReloadingManager<AppSettings> settings)
        {
            _settings = settings.CurrentValue;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new DomainServices.AutofacModule());
            builder.RegisterModule(new MsSqlRepositories
                .AutofacModule(_settings.DashboardStatisticsService.Db.MsSqlConnectionString));

            var rabbitMqSettings = _settings.DashboardStatisticsService.RabbitMq;

            // Subscribers

            builder.RegisterType<BonusReceivedEventSubscriber>()
                .As<IStartable>()
                .As<IStopable>()
                .WithParameter("connectionString", rabbitMqSettings.WalletManagementRabbitMqConnectionString)
                .WithParameter("exchangeName", BonusReceivedExchangeName)
                .WithParameter("queueName", QueueName);

            builder.RegisterType<LeadStateChangedSubscriber>()
                .As<IStartable>()
                .As<IStopable>()
                .WithParameter("connectionString", rabbitMqSettings.ReferralRabbitMqConnectionString)
                .WithParameter("exchangeName", LeadStateChangedExchangeName)
                .WithParameter("queueName", QueueName);

            builder.RegisterType<CustomerRegistrationEventSubscriber>()
                .As<IStartable>()
                .As<IStopable>()
                .WithParameter("connectionString", rabbitMqSettings.CustomerRabbitMqConnectionString)
                .WithParameter("exchangeName", CustomerRegisteredExchangeName)
                .WithParameter("queueName", QueueName);

            builder.RegisterType<PartnersPaymentTokensReservedEventSubscriber>()
                .As<IStartable>()
                .As<IStopable>()
                .WithParameter("connectionString", rabbitMqSettings.WalletManagementRabbitMqConnectionString)
                .WithParameter("exchangeName", PartnersPaymentTokensReservedExchangeName)
                .WithParameter("queueName", QueueName);
            
            builder.RegisterType<CustomerPhoneVerifiedEventSubscriber>()
                .As<IStartable>()
                .As<IStopable>()
                .WithParameter("connectionString", rabbitMqSettings.CustomerRabbitMqConnectionString)
                .WithParameter("exchangeName", CustomerPhoneVerifiedEventExchangeName)
                .WithParameter("queueName", QueueName);

            // Clients

            builder.RegisterTokensStatisticsClient(_settings.TokensStatisticsJobClient, null);
        }
    }
}
