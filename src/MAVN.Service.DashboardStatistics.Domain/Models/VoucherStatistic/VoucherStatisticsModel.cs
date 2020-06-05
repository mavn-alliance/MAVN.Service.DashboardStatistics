using System;

namespace MAVN.Service.DashboardStatistics.Domain.Models.VoucherStatistic
{
    public class VoucherStatisticsModel
    {
        public DateTime Date { get; set; }
        public string Currency { get; set; }
        public decimal Sum { get; set; }
        public int Count { get; set; }
    }
}
