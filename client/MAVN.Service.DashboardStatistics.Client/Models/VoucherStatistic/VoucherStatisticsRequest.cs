using System;

namespace MAVN.Service.DashboardStatistics.Client.Models.VoucherStatistic
{
    /// <summary>
    /// Request model to get voucher statistics
    /// </summary>
    public class VoucherStatisticsRequest
    {
        /// <summary>
        /// Collection of partner ids
        /// </summary>
        public Guid[] PartnerIds { get; set; }

        /// <summary>
        /// Indicates whether we should filter by partner ids or not
        /// </summary>
        public bool FilterByPartnerIds { get; set; }
    }
}
