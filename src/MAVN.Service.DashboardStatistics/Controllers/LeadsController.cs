using AutoMapper;
using MAVN.Service.DashboardStatistics.Client.Models.Leads;
using MAVN.Service.DashboardStatistics.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using MAVN.Service.DashboardStatistics.Client.Api;

namespace MAVN.Service.DashboardStatistics.Controllers
{
    [Route("api/leads")]
    [ApiController]
    public class LeadsController : Controller, ILeadsApi
    {
        private readonly ILeadStatisticService _leadStatisticService;
        private readonly IMapper _mapper;

        public LeadsController(ILeadStatisticService leadStatisticService, IMapper mapper)
        {
            _leadStatisticService = leadStatisticService;
            _mapper = mapper;
        }

        /// <response code="200">An list model for all leads statistics</response>
        [HttpGet]
        [ProducesResponseType(typeof(LeadsListResponseModel), (int) HttpStatusCode.OK)]
        public async Task<LeadsListResponseModel> GetAsync([FromQuery] LeadsListRequestModel request)
        {
            var result = await _leadStatisticService.GetAsync(request.FromDate, request.ToDate);

            return _mapper.Map<LeadsListResponseModel>(result);
        }
    }
}
