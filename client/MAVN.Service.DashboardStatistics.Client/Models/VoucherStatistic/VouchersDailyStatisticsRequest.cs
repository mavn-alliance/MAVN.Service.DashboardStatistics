using System;
using System.ComponentModel.DataAnnotations;

namespace MAVN.Service.DashboardStatistics.Client.Models.VoucherStatistic
{
    /// <summary>
    /// request model for vouchers daily statistics
    /// </summary>
    public class VouchersDailyStatisticsRequest : BasePeriodRequestModel
    {
        /// <summary>
        /// Collection of partner ids
        /// </summary>
        [Required]
        public Guid[] PartnerIds { get; set; }
    }
}
