using System;
using System.Collections;
using System.Collections.Generic;

namespace MAVN.Service.DashboardStatistics.Client.Models.Leads
{
    /// <summary>
    /// Represent leads for day separated by the state which they are in
    /// </summary>
    public class LeadsStatisticsForDayReportModel
    {
        /// <summary>
        /// The day the model represents
        /// </summary>
        public DateTime Day { get; set; }

        /// <summary>
        /// The list of Leads models for the different states
        /// </summary>
        public IEnumerable<LeadsStatisticsModel> Value { get; set; }

        /// <summary>
        /// Total number of unique leads for the day
        /// </summary>
        public int Total { get; set; }
    }
}
