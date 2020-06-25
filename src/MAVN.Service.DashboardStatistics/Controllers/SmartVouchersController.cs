using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public async Task<IList<VoucherStatisticsResponse>> GetTotalStatisticsAsync([FromBody] VoucherStatisticsRequest request)
        {
            if (request.FilterByPartnerIds && request.PartnerIds == null)
                return new List<VoucherStatisticsResponse>();

            var currenciesStatistic = await _partnerStatisticService.GetCurrenciesStatistic(request.PartnerIds, request.FilterByPartnerIds);

            return currenciesStatistic.Select(x => _mapper.Map<VoucherStatisticsResponse>(x)).ToList();
        }

        [HttpPost]
        [Route("period")]
        [ProducesResponseType(typeof(VoucherDailyStatisticsResponse), (int)HttpStatusCode.OK)]
        public async Task<VoucherDailyStatisticsResponse> GetPeriodStatsAsync([FromBody] VouchersDailyStatisticsRequest request)
        {
            if (request.FilterByPartnerIds && request.PartnerIds == null)
                return new VoucherDailyStatisticsResponse();

            var statistics = await _partnerStatisticService.GetPartnerDailyVoucherStatistic(request.PartnerIds, request.FilterByPartnerIds, request.FromDate, request.ToDate);

            return _mapper.Map<VoucherDailyStatisticsResponse>(statistics);
        }
    }
}
