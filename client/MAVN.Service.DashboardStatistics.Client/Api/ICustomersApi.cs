using System.Threading.Tasks;
using JetBrains.Annotations;
using MAVN.Service.DashboardStatistics.Client.Models.Customers;
using Refit;

namespace MAVN.Service.DashboardStatistics.Client.Api
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
        [Post("/api/customers")]
        Task<CustomersStatisticResponse> GetAsync(CustomersListRequestModel request);
    }
}
