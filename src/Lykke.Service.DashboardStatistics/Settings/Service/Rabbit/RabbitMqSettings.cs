using JetBrains.Annotations;
using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.DashboardStatistics.Settings.Service.Rabbit
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class RabbitMqSettings
    {
        [AmqpCheck]
        public string ReferralRabbitMqConnectionString { get; set; }

        [AmqpCheck]
        public string CustomerRabbitMqConnectionString { get; set; }

        [AmqpCheck]
        public string WalletManagementRabbitMqConnectionString { get; set; }
    }
}
