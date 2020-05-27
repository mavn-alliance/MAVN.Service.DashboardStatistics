using Autofac;
using JetBrains.Annotations;
using Lykke.Common;
using Lykke.SettingsReader;
using MAVN.Service.DashboardStatistics.Rabbit.Subscribers;
using MAVN.Service.DashboardStatistics.Settings;
using MAVN.Service.DashboardStatistics.Settings.Service.Rabbit;

namespace MAVN.Service.DashboardStatistics.Modules
{
    [UsedImplicitly]
    public class RabbitMqModule : Module
    {
        private const string QueueName = "dashboard-statistic";
        private const string BonusReceivedExchangeName = "lykke.wallet.bonusreceived";
        private const string LeadStateChangedExchangeName = "lykke.bonus.leadstatechanged";
        private const string PartnersPaymentTokensReservedExchangeName = "lykke.wallet.partnerspaymenttokensreserved";
        private const string CustomerPhoneVerifiedEventExchangeName = "lykke.customer.phoneverified";

        private readonly RabbitMqSettings _settings;

        public RabbitMqModule(IReloadingManager<AppSettings> settings)
        {
            _settings = settings.CurrentValue.DashboardStatisticsService.RabbitMq;
        }

        protected override void Load(ContainerBuilder builder)
        {
            // Subscribers

            builder.RegisterType<BonusReceivedEventSubscriber>()
                .As<IStartStop>()
                .WithParameter("connectionString", _settings.WalletManagementRabbitMqConnectionString)
                .WithParameter("exchangeName", BonusReceivedExchangeName)
                .WithParameter("queueName", QueueName);

            builder.RegisterType<LeadStateChangedSubscriber>()
                .As<IStartStop>()
                .WithParameter("connectionString", _settings.ReferralRabbitMqConnectionString)
                .WithParameter("exchangeName", LeadStateChangedExchangeName)
                .WithParameter("queueName", QueueName);

            builder.RegisterType<PartnersPaymentTokensReservedEventSubscriber>()
                .As<IStartStop>()
                .WithParameter("connectionString", _settings.WalletManagementRabbitMqConnectionString)
                .WithParameter("exchangeName", PartnersPaymentTokensReservedExchangeName)
                .WithParameter("queueName", QueueName);

            builder.RegisterType<CustomerPhoneVerifiedEventSubscriber>()
                .As<IStartStop>()
                .WithParameter("connectionString", _settings.CustomerRabbitMqConnectionString)
                .WithParameter("exchangeName", CustomerPhoneVerifiedEventExchangeName)
                .WithParameter("queueName", QueueName);
        }
    }
}
