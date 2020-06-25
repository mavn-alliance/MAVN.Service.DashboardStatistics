using System;

namespace MAVN.Service.DashboardStatistics.Client.Models.Customers
{
    /// <summary>
    /// Represents a request model for a customers stats list
    /// </summary>
    public class CustomersListRequestModel : BasePeriodRequestModel
    {
        /// <summary>
        /// Ids of partners, used for filtering
        /// </summary>
        public Guid[] PartnerIds { get; set; }

        /// <summary>
        /// Indicates whether to filter by partner ids
        /// </summary>
        public bool FilterByPartnerIds { get; set; }
    }
}
