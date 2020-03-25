using JetBrains.Annotations;
using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.DashboardStatistics.Settings.Service.Db
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class DbSettings
    {
        [AzureTableCheck]
        public string LogsConnString { get; set; }

        public string MsSqlConnectionString { get; set; }
    }
}
