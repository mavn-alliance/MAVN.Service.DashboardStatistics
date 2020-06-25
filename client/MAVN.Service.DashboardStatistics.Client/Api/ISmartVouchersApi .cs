using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MAVN.Service.DashboardStatistics.Client.Models.VoucherStatistic;
using Refit;

namespace MAVN.Service.DashboardStatistics.Client.Api
{
    /// <summary>
    /// Smart vouchers Api
    /// </summary>
    [PublicAPI]
    public interface ISmartVouchersApi
    {
        /// <summary>
        /// Get total voucher operations statistics per Partner
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Post("/api/smartvouchers/totals")]
        Task<IList<VoucherStatisticsResponse>> GetTotalStatisticsAsync([Body] VoucherStatisticsRequest request);

        /// <summary>
        /// Get daily voucher statistics
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Post("/api/smartvouchers/period")]
        Task<VoucherDailyStatisticsResponse> GetPeriodStatsAsync([Body] VouchersDailyStatisticsRequest request);
    }
}
