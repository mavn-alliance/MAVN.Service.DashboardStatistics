using System;
using MAVN.Service.DashboardStatistics.Domain.Enums;

namespace MAVN.Service.DashboardStatistics.Domain.Models.VoucherStatistic
{
    public class UpdateVoucherOperationsStatistic
    {
        public Guid PartnerId { get; set; }

        public string Currency { get; set; }

        public VoucherOperationType OperationType { get; set; }

        public decimal Amount { get; set; }
    }
}
