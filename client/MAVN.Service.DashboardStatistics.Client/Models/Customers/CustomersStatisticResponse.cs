using System.Collections.Generic;
using JetBrains.Annotations;

namespace MAVN.Service.DashboardStatistics.Client.Models.Customers
{
    /// <summary>
    /// Represents a response model for a customers statistic
    /// </summary>
    [PublicAPI]
    public class CustomersStatisticResponse
    {
        /// <summary>
        /// Represents a total count of all customer for the given period
        /// </summary>
        public int TotalCustomers { get; set; }
        
        /// <summary>
        /// Represents a total for active customers
        /// </summary>
        public int TotalActiveCustomers { get; set; }

        /// <summary>
        /// Represents a total for non active customers
        /// </summary>
        public int TotalNonActiveCustomers { get; set; }

        /// <summary>
        /// Represents a total count of new registered customers for selected period
        /// </summary>
        public int TotalNewCustomers { get; set; }

        /// <summary>
        /// The count of repeat active customers in a specific period.
        /// </summary>
        public int TotalRepeatCustomers { get; set; }

        /// <summary>
        /// Represents a list of new registered customers by days for selected period
        /// </summary>
        public IReadOnlyList<CustomerStatisticsByDayResponse> NewCustomers { get; set; }

    }
}
