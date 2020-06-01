using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAVN.Service.DashboardStatistics.MsSqlRepositories.Entities
{
    [Table("customer_statistics")]
    public class CustomerStatisticEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        
        [Required]
        [Column("customer_id")]
        public Guid CustomerId { get; set; }

        [Column("partner_id")]
        public Guid? PartnerId { get; set; }

        [Column("time_stamp")]
        public DateTime TimeStamp { get; set; }
    }
}
