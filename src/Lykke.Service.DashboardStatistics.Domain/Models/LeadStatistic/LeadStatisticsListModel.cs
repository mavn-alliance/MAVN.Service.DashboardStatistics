using System.Collections.Generic;

namespace Lykke.Service.DashboardStatistics.Domain.Models.LeadStatistic
{
    public class LeadStatisticsListModel
    {
        public IReadOnlyCollection<LeadsStatisticsForDayModel> LeadsByDate { get; set; }
        public int TotalCount { get; set; }
    }
}
