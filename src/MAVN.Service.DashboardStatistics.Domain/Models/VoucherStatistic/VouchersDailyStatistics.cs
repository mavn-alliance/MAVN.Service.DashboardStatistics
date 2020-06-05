using System;
using System.Collections.Generic;

namespace MAVN.Service.DashboardStatistics.Domain.Models.VoucherStatistic
{
    public class VouchersDailyStatistics
    {
        public List<VoucherStatisticsModel> BoughtVoucherStatistics { get; set; }
        public List<VoucherStatisticsModel> UsedVoucherStatistics { get; set; }
    }
}
