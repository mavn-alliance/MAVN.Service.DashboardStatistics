namespace Lykke.Service.DashboardStatistics.Client.Models.Leads
{
    /// <summary>
    /// Represents a state of a referral lead
    /// </summary>
    public enum LeadState
    {
        /// <summary>
        /// Lead is created and its pending confirmation
        /// </summary>
        Pending,
        /// <summary>
        /// Lead is confirmed and its waiting approval
        /// </summary>
        Confirmed,
        /// <summary>
        /// Lead is approved
        /// </summary>
        Approved,
        /// <summary>
        /// Lead is rejected
        /// </summary>
        Rejected
    }
}
