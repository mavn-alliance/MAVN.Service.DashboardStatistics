using System;
using Falcon.Numerics;

namespace Lykke.Service.DashboardStatistics.Client.Models.Tokens
{
    /// <summary>
    /// Represents the tokens model
    /// </summary>
    public class TokensStatisticsModel
    {
        /// <summary>
        /// The day
        /// </summary>
        public DateTime Day { get; set; }

        /// <summary>
        /// The amount of token for the day
        /// </summary>
        public Money18 Amount { get; set; }
    }
}
