using Autofac;
using JetBrains.Annotations;
using Lykke.Common;
using Lykke.SettingsReader;
using MAVN.Service.DashboardStatistics.DomainServices.RabbitMq.Subscribers;
using MAVN.Service.DashboardStatistics.Settings;
using MAVN.Service.DashboardStatistics.Settings.Service.Rabbit;

namespace MAVN.Service.DashboardStatistics.Modules
{
    [UsedImplicitly]
    public class RabbitMqModule : Module
    {
        private const string QueueName = "dashboard-statistic";
        private const string BonusReceivedExchangeName = "lykke.wallet.bonusreceived";
        private const string PartnersPaymentTokensReservedExchangeName = "lykke.wallet.partnerspaymenttokensreserved";
        private const string SmartVoucherSoldExchangeName = "lykke.smart-vouchers.vouchersold";
        private const string SmartVoucherUsedExchangeName = "lykke.smart-vouchers.voucherused";

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

            builder.RegisterType<PartnersPaymentTokensReservedEventSubscriber>()
                .As<IStartStop>()
                .WithParameter("connectionString", _settings.WalletManagementRabbitMqConnectionString)
                .WithParameter("exchangeName", PartnersPaymentTokensReservedExchangeName)
                .WithParameter("queueName", QueueName);

            builder.RegisterType<SmartVoucherSoldSubscriber>()
                .As<IStartStop>()
                .WithParameter("connectionString", _settings.WalletManagementRabbitMqConnectionString)
                .WithParameter("exchangeName", SmartVoucherSoldExchangeName)
                .WithParameter("queueName", QueueName);

            builder.RegisterType<SmartVoucherUsedSubscriber>()
                .As<IStartStop>()
                .WithParameter("connectionString", _settings.WalletManagementRabbitMqConnectionString)
                .WithParameter("exchangeName", SmartVoucherUsedExchangeName)
                .WithParameter("queueName", QueueName);
        }
    }
}
