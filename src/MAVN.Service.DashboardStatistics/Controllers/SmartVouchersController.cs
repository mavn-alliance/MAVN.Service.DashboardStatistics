using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using MAVN.Service.DashboardStatistics.Client.Api;
using MAVN.Service.DashboardStatistics.Client.Models.VoucherStatistic;
using MAVN.Service.DashboardStatistics.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace MAVN.Service.DashboardStatistics.Controllers
{
    [Route("/api/smartvouchers")]
    public class SmartVouchersController : Controller, ISmartVouchersApi
    {
        private readonly IVoucherOperationsStatisticService _partnerStatisticService;
        private readonly IMapper _mapper;

        public SmartVouchersController(
            IVoucherOperationsStatisticService partnerStatisticService,
            IMapper mapper)
        {
            _partnerStatisticService = partnerStatisticService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("totals")]
        [ProducesResponseType(typeof(IList<VoucherStatisticsResponse>), (int)HttpStatusCode.OK)]
        public async Task<IList<VoucherStatisticsResponse>> GetTotalStatisticsAsync(Guid[] partnerIds)
        {
            var currenciesStatistic = await _partnerStatisticService.GetCurrenciesStatistic(partnerIds);

            return currenciesStatistic.Select(x => _mapper.Map<VoucherStatisticsResponse>(x)).ToList();
        }

        [HttpPost]
        [Route("period")]
        [ProducesResponseType(typeof(VoucherDailyStatisticsResponse), (int)HttpStatusCode.OK)]
        public async Task<VoucherDailyStatisticsResponse> GetPeriodStatsAsync(VouchersDailyStatisticsRequest request)
        {
            var statistics = await _partnerStatisticService.GetPartnerDailyVoucherStatistic(request.PartnerIds, request.FromDate, request.ToDate);

            return _mapper.Map<VoucherDailyStatisticsResponse>(statistics);
        }
    }
}
