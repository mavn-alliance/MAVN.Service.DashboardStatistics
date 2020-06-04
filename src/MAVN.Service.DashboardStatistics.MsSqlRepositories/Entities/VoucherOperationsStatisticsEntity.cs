using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MAVN.Service.DashboardStatistics.Domain.Enums;

namespace MAVN.Service.DashboardStatistics.MsSqlRepositories.Entities
{
    [Table("voucher_operations_statistics")]
    public class VoucherOperationsStatisticsEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("partner_id")]
        public Guid PartnerId { get; set; }

        [Column("operation_type")]
        public VoucherOperationType OperationType { get; set; }

        [Column("sum")]
        public decimal Sum { get; set; }

        [Column("currency")]
        public string Currency { get; set; }

        [Column("total_count")]
        public int TotalCount { get; set; }
    }
}
