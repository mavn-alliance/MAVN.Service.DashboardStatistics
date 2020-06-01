using JetBrains.Annotations;
using MAVN.Service.DashboardStatistics.Client.Api;

namespace MAVN.Service.DashboardStatistics.Client
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
        /// Tokens API.
        /// </summary>
        ITokensApi TokensApi { get; }
    }
}
