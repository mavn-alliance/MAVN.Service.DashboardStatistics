namespace MAVN.Service.DashboardStatistics.Domain.Models.VoucherStatistic
{
    public class CurrenciesStatistic
    {
        public string Currency { get; set; }

        public decimal TotalPurchasesCost { get; set; }

        public int TotalPurchasesCount { get; set; }

        public decimal TotalRedeemedVouchersCost { get; set; }

        public int TotalRedeemedVouchersCount { get; set; }
    }
}
