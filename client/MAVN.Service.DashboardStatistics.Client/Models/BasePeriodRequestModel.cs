using System;

namespace MAVN.Service.DashboardStatistics.Client.Models
{
    /// <summary>
    /// Base period request model
    /// </summary>
    public abstract class BasePeriodRequestModel
    {
        /// <summary>From date</summary>
        public DateTime FromDate { get; set; }

        /// <summary>To date</summary>
        public DateTime ToDate { get; set; }
    }
}
