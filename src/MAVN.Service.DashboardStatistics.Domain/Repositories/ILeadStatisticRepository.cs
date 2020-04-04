using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MAVN.Service.DashboardStatistics.Domain.Models.LeadStatistic;

namespace MAVN.Service.DashboardStatistics.Domain.Repositories
{
    public interface ILeadStatisticRepository
    {
        Task<IReadOnlyList<LeadModel>> GetAsync(DateTime fromDate, DateTime toDate);

        Task InsertAsync(Guid leadId, DateTime timeStamp, LeadState state);
    }
}
