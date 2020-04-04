using System;

namespace MAVN.Service.DashboardStatistics.Domain.Models.LeadStatistic
{
    public class LeadModel
    {
        public string LeadId { get; set; }

        public DateTime TimeStamp { get; set; }

        public LeadState State { get; set; }
    }
}
