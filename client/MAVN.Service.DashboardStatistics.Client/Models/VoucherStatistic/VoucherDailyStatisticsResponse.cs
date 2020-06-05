using System.Collections.Generic;

namespace MAVN.Service.DashboardStatistics.Client.Models.VoucherStatistic
{
    /// <summary>
    /// response model
    /// </summary>
    public class VoucherDailyStatisticsResponse
    {
        /// <summary>
        /// Statistics for bought vouchers per day and currency
        /// </summary>
        public IReadOnlyList<VoucherDailyStatisticsModel> BoughtVoucherStatistics { get; set; }
        /// <summary>
        /// Statistics for used vouchers per day and currency
        /// </summary>
        public IReadOnlyList<VoucherDailyStatisticsModel> UsedVoucherStatistics { get; set; }
    }
}
