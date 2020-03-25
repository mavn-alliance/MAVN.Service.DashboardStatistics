using JetBrains.Annotations;
using Lykke.Job.TokensStatistics.Client;
using Lykke.Sdk.Settings;
using Lykke.Service.DashboardStatistics.Settings.Service;

namespace Lykke.Service.DashboardStatistics.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AppSettings : BaseAppSettings
    {
        public DashboardStatisticsSettings DashboardStatisticsService { get; set; }

        public TokensStatisticsJobClientSettings TokensStatisticsJobClient { get; set; }
    }
}
