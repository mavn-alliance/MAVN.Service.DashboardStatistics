using System;
using System.Collections.Generic;

namespace MAVN.Service.DashboardStatistics.Domain.Models.LeadStatistic
{
    public class LeadsStatisticsForDayModel
    {
        public DateTime Day { get; set; }

        public IReadOnlyList<LeadStatisticModel> Value { get; set; }

        public int Total { get; set; }
    }
}
