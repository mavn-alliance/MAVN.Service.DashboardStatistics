using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MAVN.Service.DashboardStatistics.Domain.Models.VoucherStatistic;

namespace MAVN.Service.DashboardStatistics.Domain.Repositories
{
    public interface IPartnerVouchersDailyStatsRepository
    {
        Task UpdateByDateAndCurrencyAndOperationType(UpdateVoucherOperationsStatistic partnerStatistic);

        Task<IReadOnlyList<IPartnerVouchersDailyStats>> GetByPartnerIdsAndPeriod(Guid[] partnerIds, bool filterByPartnerIds, DateTime fromDate,
            DateTime toDate);
    }
}
