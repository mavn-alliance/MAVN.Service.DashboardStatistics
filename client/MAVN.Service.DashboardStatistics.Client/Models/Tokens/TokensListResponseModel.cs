using System.Collections.Generic;
using Falcon.Numerics;

namespace MAVN.Service.DashboardStatistics.Client.Models.Tokens
{
    /// <summary>
    /// Represents a response model for a tokens stats list
    /// </summary>
    public class TokensListResponseModel
    {
        /// <summary>
        /// List of Earn Token separated by day
        /// </summary>
        public IReadOnlyList<TokensStatisticsModel> Earn { get; set; }

        /// <summary>
        /// List of Burn Token separated by day
        /// </summary>
        public IReadOnlyList<TokensStatisticsModel> Burn { get; set; }

        /// <summary>
        /// List of Burn Token separated by day
        /// </summary>
        public IReadOnlyList<TokensStatisticsModel> WalletBalance { get; set; }

        /// <summary>
        /// Total number of earn tokens
        /// </summary>
        public Money18 TotalEarn { get; set; }

        /// <summary>
        /// Total number of burn tokens
        /// </summary>
        public Money18 TotalBurn { get; set; }

        /// <summary>
        /// Total amount of tokens on customers' wallets for the last day of the period 
        /// </summary>
        public Money18 TotalWalletBalance { get; set; }
    }
}
