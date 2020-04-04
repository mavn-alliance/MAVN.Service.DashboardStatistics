using JetBrains.Annotations;
using Lykke.SettingsReader.Attributes;

namespace MAVN.Service.DashboardStatistics.Client 
{
    /// <summary>
    /// DashboardStatistics client settings.
    /// </summary>
    [PublicAPI]
    public class DashboardStatisticsServiceClientSettings 
    {
        /// <summary>Service url.</summary>
        [HttpCheck("api/isalive")]
        public string ServiceUrl {get; set;}
    }
}
