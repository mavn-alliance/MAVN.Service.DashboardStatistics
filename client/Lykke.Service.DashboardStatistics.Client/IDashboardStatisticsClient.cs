using JetBrains.Annotations;
using Lykke.Service.DashboardStatistics.Client.Api;

namespace Lykke.Service.DashboardStatistics.Client
{
    /// <summary>
    /// Dashboard statistics API service client.
    /// </summary>
    [PublicAPI]
    public interface IDashboardStatisticsClient
    {
        /// <summary>
        /// Customers API.
        /// </summary>
        ICustomersApi CustomersApi { get; }

        /// <summary>
        /// Leads API.
        /// </summary>
        ILeadsApi LeadsApi { get; }

        /// <summary>
        /// Tokens API.
        /// </summary>
        ITokensApi TokensApi { get; }
    }
}
