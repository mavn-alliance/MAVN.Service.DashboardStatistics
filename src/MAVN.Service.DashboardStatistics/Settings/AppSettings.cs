using JetBrains.Annotations;
using Lykke.Job.TokensStatistics.Client;
using Lykke.Sdk.Settings;
using MAVN.Service.DashboardStatistics.Settings.Service;

namespace MAVN.Service.DashboardStatistics.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AppSettings : BaseAppSettings
    {
        public DashboardStatisticsSettings DashboardStatisticsService { get; set; }

        public TokensStatisticsJobClientSettings TokensStatisticsJobClient { get; set; }
    }
}
