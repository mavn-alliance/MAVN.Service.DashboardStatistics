using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MAVN.Service.DashboardStatistics.Domain.Models.VoucherStatistic;

namespace MAVN.Service.DashboardStatistics.Domain.Repositories
{
    public interface IVoucherOperationsStatisticRepository
    {
        Task UpdateByCurrencyAndOperationType(UpdateVoucherOperationsStatistic partnerStatistic);

        Task<IList<VoucherOperationsStatistic>> GetByPartnerIds(Guid[] partnerIds);
    }
}
