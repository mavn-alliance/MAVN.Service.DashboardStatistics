using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.DashboardStatistics.Domain.Models.LeadStatistic;

namespace Lykke.Service.DashboardStatistics.Domain.Repositories
{
    public interface ILeadStatisticRepository
    {
        Task<IReadOnlyList<LeadModel>> GetAsync(DateTime fromDate, DateTime toDate);

        Task InsertAsync(Guid leadId, DateTime timeStamp, LeadState state);
    }
}
