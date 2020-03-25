using System;
using System.Threading.Tasks;
using Lykke.Service.DashboardStatistics.Domain.Models.LeadStatistic;

namespace Lykke.Service.DashboardStatistics.Domain.Services
{
    public interface ILeadStatisticService
    {
        Task<LeadStatisticsListModel> GetAsync(DateTime fromDate, DateTime toDate);

        Task AddAsync(Guid leadId, DateTime timeStamp, LeadState state);
    }
}
