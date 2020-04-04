using System;
using System.Threading.Tasks;
using MAVN.Service.DashboardStatistics.Domain.Models.LeadStatistic;

namespace MAVN.Service.DashboardStatistics.Domain.Services
{
    public interface ILeadStatisticService
    {
        Task<LeadStatisticsListModel> GetAsync(DateTime fromDate, DateTime toDate);

        Task AddAsync(Guid leadId, DateTime timeStamp, LeadState state);
    }
}
