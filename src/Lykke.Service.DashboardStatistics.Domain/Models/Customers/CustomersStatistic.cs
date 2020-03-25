using System.Collections.Generic;

namespace Lykke.Service.DashboardStatistics.Domain.Models.Customers
{
    /// <summary>
    /// Represents a customers statistics in a specific period.
    /// </summary>
    public class CustomersStatistic
    {
        /// <summary>
        /// The count of customers since beginning to the end of the period. 
        /// </summary>
        public int TotalCustomers { get; set; }
        
        /// <summary>
        /// The count of active customers in a specific period.
        /// </summary>
        public int TotalActiveCustomers { get; set; }

        /// <summary>
        /// The count of non active customers in a specific period.
        /// </summary>
        public int TotalNonActiveCustomers { get; set; }

        /// <summary>
        /// The count of registered customers in a specific period.
        /// </summary>
        public int TotalNewCustomers { get; set; }

        /// <summary>
        /// The list of registered customers per days in a specific period.
        /// </summary>
        public IReadOnlyList<CustomersCountAtDate> NewCustomers { get; set; }
    }
}
