using System;

namespace MAVN.Service.DashboardStatistics.Client.Models.VoucherStatistic
{
    /// <summary>
    /// Request to get voucher statistics
    /// </summary>
    public class VoucherStatisticsRequest
    {
        /// <summary>
        /// Collection of partner ids
        /// </summary>
        public Guid[] PartnerIds { get; set; }
    }
}
