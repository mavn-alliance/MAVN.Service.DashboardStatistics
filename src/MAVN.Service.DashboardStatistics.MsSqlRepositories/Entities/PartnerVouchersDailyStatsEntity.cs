using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MAVN.Service.DashboardStatistics.Domain.Enums;
using MAVN.Service.DashboardStatistics.Domain.Models.VoucherStatistic;

namespace MAVN.Service.DashboardStatistics.MsSqlRepositories.Entities
{
    [Table("partner_vouchers_daily_stats")]
    public class PartnerVouchersDailyStatsEntity : IPartnerVouchersDailyStats
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Column("partner_id")]
        public Guid PartnerId { get; set; }

        [Required]
        [Column("operation_type")]
        public VoucherOperationType OperationType { get; set; }

        [Required]
        [Column("sum")]
        public decimal Sum { get; set; }

        [Required]
        [Column("currency")]
        public string Currency { get; set; }

        [Required]
        [Column("total_count")]
        public int TotalCount { get; set; }

        [Required]
        [Column("date")]
        public DateTime Date { get; set; }
    }
}
