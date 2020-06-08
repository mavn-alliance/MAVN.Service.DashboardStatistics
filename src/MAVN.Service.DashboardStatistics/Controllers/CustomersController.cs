using MAVN.Service.DashboardStatistics.Client.Models.Customers;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using MAVN.Service.DashboardStatistics.Client.Api;
using MAVN.Service.DashboardStatistics.Domain.Services;

namespace MAVN.Service.DashboardStatistics.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : Controller, ICustomersApi
    {
        private readonly ICustomerStatisticService _customerStatisticService;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerStatisticService customStatisticService, IMapper mapper)
        {
            _customerStatisticService = customStatisticService;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns a customers statistic model for given time period.
        /// </summary>
        /// <param name="request">Filtering parameter.</param>
        /// <returns>A customer statistics.</returns>
        /// <response code="200">A customer statistics.</response>
        [HttpPost]
        [ProducesResponseType(typeof(CustomersStatisticResponse), (int) HttpStatusCode.OK)]
        public async Task<CustomersStatisticResponse> GetAsync([FromBody] CustomersListRequestModel request)
        {
            var startDate = request.FromDate.Date;
            var endDate = request.ToDate.Date.AddDays(1).AddMilliseconds(-1);

            var statistic = await _customerStatisticService.GetAsync(startDate, endDate, request.PartnerIds);

            return _mapper.Map<CustomersStatisticResponse>(statistic);
        }
    }
}
