using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lykke.Service.DashboardStatistics.Domain.Models.LeadStatistic;

namespace Lykke.Service.DashboardStatistics.MsSqlRepositories.Entities
{
    [Table("lead_statistics")]
    public class LeadStatisticEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        
        [Column("lead_id")]
        public Guid LeadId { get; set; }

        [Column("time_stamp")]
        public DateTime TimeStamp { get; set; }

        [Column("state")]
        public LeadState State { get; set; }
    }
}
