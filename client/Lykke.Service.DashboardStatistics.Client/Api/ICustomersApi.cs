using System.Threading.Tasks;
using JetBrains.Annotations;
using Lykke.Service.DashboardStatistics.Client.Models.Customers;
using Refit;

namespace Lykke.Service.DashboardStatistics.Client.Api
{
    /// <summary>
    /// DashboardStatistics client API interface.
    /// </summary>
    [PublicAPI]
    public interface ICustomersApi
    {
        /// <summary>
        /// Returns a customers statistic model for given time period.
        /// </summary>
        /// <param name="request">Filtering parameter.</param>
        /// <returns>A customer statistics.</returns>
        [Get("/api/customers")]
        Task<CustomersStatisticResponse> GetAsync(CustomersListRequestModel request);
    }
}
