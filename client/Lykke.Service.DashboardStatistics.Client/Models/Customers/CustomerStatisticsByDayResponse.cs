using System;
using JetBrains.Annotations;

namespace Lykke.Service.DashboardStatistics.Client.Models.Customers
{
    /// <summary>
    /// Represents the registered customers at specific the date.
    /// </summary>
    [PublicAPI]
    public class CustomerStatisticsByDayResponse
    {
        /// <summary>
        /// The date.
        /// </summary>
        public DateTime Day { get; set; }

        /// <summary>
        /// The count of registered customers at the <see cref="Day"/>.
        /// </summary>
        public int Count { get; set; }
    }
}
