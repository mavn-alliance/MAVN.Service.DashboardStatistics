using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Lykke.Job.TokensStatistics.Client;
using Lykke.Job.TokensStatistics.Client.Models.Requests;
using Lykke.Service.DashboardStatistics.Client.Api;
using Lykke.Service.DashboardStatistics.Client.Models.Tokens;
using Microsoft.AspNetCore.Mvc;

namespace Lykke.Service.DashboardStatistics.Controllers
{
    [Route("api/tokens")]
    [ApiController]
    public class TokensController : Controller, ITokensApi
    {
        private readonly ITokensStatisticsClient _tokensStatisticsApi;
        private readonly IMapper _mapper;

        public TokensController(ITokensStatisticsClient tokensStatistics, IMapper mapper)
        {
            _tokensStatisticsApi = tokensStatistics;
            _mapper = mapper;
        }

        /// <response code="200">An list model for all token statistics</response>
        [HttpGet]
        [ProducesResponseType(typeof(TokensListResponseModel), (int) HttpStatusCode.OK)]
        public async Task<TokensListResponseModel> GetAsync([FromQuery] TokensListRequestModel request)
        {
            var response = await _tokensStatisticsApi.Api.GetByDaysAsync(_mapper.Map<PeriodRequest>(request));

            return _mapper.Map<TokensListResponseModel>(response);
        }
    }
}
