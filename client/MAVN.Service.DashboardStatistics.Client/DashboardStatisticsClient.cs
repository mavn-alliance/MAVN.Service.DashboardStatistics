using Lykke.HttpClientGenerator;
using MAVN.Service.DashboardStatistics.Client.Api;

namespace MAVN.Service.DashboardStatistics.Client
{
    /// <inheritdoc />
    public class DashboardStatisticsClient : IDashboardStatisticsClient
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DashboardStatisticsClient"/> with <param name="httpClientGenerator"></param>.
        /// </summary> 
        public DashboardStatisticsClient(IHttpClientGenerator httpClientGenerator)
        {
            CustomersApi = httpClientGenerator.Generate<ICustomersApi>();
            LeadsApi = httpClientGenerator.Generate<ILeadsApi>();
            TokensApi = httpClientGenerator.Generate<ITokensApi>();
        }

        /// <inheritdoc />
        public ICustomersApi CustomersApi { get; }

        /// <inheritdoc />
        public ILeadsApi LeadsApi { get; }

        /// <inheritdoc />
        public ITokensApi TokensApi { get; }
    }
}
