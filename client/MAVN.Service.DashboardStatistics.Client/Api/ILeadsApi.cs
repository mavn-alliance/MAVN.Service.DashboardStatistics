using System.Threading.Tasks;
using JetBrains.Annotations;
using MAVN.Service.DashboardStatistics.Client.Models.Leads;
using Refit;

namespace MAVN.Service.DashboardStatistics.Client.Api
{
    /// <summary>
    /// DashboardStatistics client API interface.
    /// </summary>
    [PublicAPI]
    public interface ILeadsApi
    {
        /// <summary>
        /// Returns a leads list for given time period.
        /// </summary>
        /// <param name="request">Filtering parameter.</param>
        /// <returns>LeadsListResponseModel</returns>
        [Get("/api/leads")]
        Task<LeadsListResponseModel> GetAsync(LeadsListRequestModel request);
    }
}
