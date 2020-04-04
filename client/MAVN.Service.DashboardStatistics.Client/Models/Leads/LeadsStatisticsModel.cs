namespace MAVN.Service.DashboardStatistics.Client.Models.Leads
{
    /// <summary>
    /// Represents leads stats model
    /// </summary>
    public class LeadsStatisticsModel
    {
        /// <summary>
        /// Represents the state of the lead
        /// </summary>
        public LeadState State { get; set; }

        /// <summary>
        /// Represents the total count of the leads for the state
        /// </summary>
        public int Count { get; set; }
    }
}
