using JetBrains.Annotations;

namespace MAVN.Service.DashboardStatistics.Settings.Service
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class RedisSettings
    {
        public string ConnectionString { set; get; }
    }
}
