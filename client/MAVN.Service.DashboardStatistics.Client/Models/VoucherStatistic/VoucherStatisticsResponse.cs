namespace MAVN.Service.DashboardStatistics.Client.Models.VoucherStatistic
{
    public class VoucherStatisticsResponse
    {
        public string Currency { get; set; }

        public decimal TotalPurchasesCost { get; set; }

        public int TotalPurchasesCount { get; set; }

        public decimal TotalRedeemedVouchersCost { get; set; }

        public int TotalRedeemedVouchersCount { get; set; }
    }
}
