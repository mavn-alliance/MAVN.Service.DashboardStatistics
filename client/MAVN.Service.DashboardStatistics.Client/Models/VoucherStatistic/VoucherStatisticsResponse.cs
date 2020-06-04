namespace MAVN.Service.DashboardStatistics.Client.Models.VoucherStatistic
{
    /// <summary>
    /// Voucher statistics response
    /// </summary>
    public class VoucherStatisticsResponse
    {
        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Total purchases cost
        /// </summary>
        public decimal TotalPurchasesCost { get; set; }

        /// <summary>
        /// Total purchases count
        /// </summary>
        public int TotalPurchasesCount { get; set; }

        /// <summary>
        /// Total redeemed vouchers cost
        /// </summary>
        public decimal TotalRedeemedVouchersCost { get; set; }

        /// <summary>
        /// Total redeemed vouchers count
        /// </summary>
        public int TotalRedeemedVouchersCount { get; set; }
    }
}
