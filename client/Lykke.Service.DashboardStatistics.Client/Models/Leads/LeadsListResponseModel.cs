using System.Collections;
using System.Collections.Generic;

namespace Lykke.Service.DashboardStatistics.Client.Models.Leads
{
    /// <summary>
    /// Represents a response model for a leads stats list
    /// </summary>
    public class LeadsListResponseModel
    {
        /// <summary>
        /// List of Leads separated by day and state
        /// </summary>
        public IEnumerable<LeadsStatisticsForDayReportModel> Leads { get; set; }

        /// <summary>
        /// The Total number of all leads for the day
        /// </summary>
        public int TotalNumber { get; set; }
    }
}
