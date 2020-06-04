using System;
using JetBrains.Annotations;
using MAVN.Service.DashboardStatistics.Settings.Service.Db;
using MAVN.Service.DashboardStatistics.Settings.Service.Rabbit;

namespace MAVN.Service.DashboardStatistics.Settings.Service
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class DashboardStatisticsSettings
    {
        public DbSettings Db { get; set; }

        public RabbitMqSettings RabbitMq { get; set; }

        public RedisSettings Redis { set; get; }

        public TimeSpan LockTimeOut { get; set; }
    }
}
