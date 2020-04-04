using System;

namespace MAVN.Service.DashboardStatistics.Domain.Models.Customers
{
    /// <summary>
    /// Represents the registered customers at specific the date.
    /// </summary>
    public class CustomersCountAtDate
    {
        /// <summary>
        /// The date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The count of registered customers at the <see cref="Date"/>.
        /// </summary>
        public int Count { get; set; }
    }
}
