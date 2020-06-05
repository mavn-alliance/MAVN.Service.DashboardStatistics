using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MAVN.Service.DashboardStatistics.Domain.Enums;

namespace MAVN.Service.DashboardStatistics.Domain.Models.VoucherStatistic
{
    public interface IPartnerVouchersDailyStats
    {
        Guid PartnerId { get; set; }
        VoucherOperationType OperationType { get; set; }
        decimal Sum { get; set; }
        string Currency { get; set; }
        int TotalCount { get; set; }
        DateTime Date { get; set; }
    }
}
