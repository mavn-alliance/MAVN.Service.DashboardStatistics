using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MAVN.Service.DashboardStatistics.Domain.Enums;
using MAVN.Service.DashboardStatistics.Domain.Models.VoucherStatistic;
using MAVN.Service.DashboardStatistics.Domain.Repositories;
using MAVN.Service.DashboardStatistics.Domain.Services;

namespace MAVN.Service.DashboardStatistics.DomainServices
{
    public class VoucherOperationsStatisticService : IVoucherOperationsStatisticService
    {
        private const int MaxAttemptsCount = 5;
        private readonly IVoucherOperationsStatisticRepository _voucherOperationsStatisticRepository;
        private readonly IRedisLocksService _redisLocksService;
        private readonly TimeSpan _lockTimeOut;

        public VoucherOperationsStatisticService(
            IVoucherOperationsStatisticRepository voucherOperationsStatisticRepository,
            IRedisLocksService redisLocksService,
            TimeSpan lockTimeOut)
        {
            _voucherOperationsStatisticRepository = voucherOperationsStatisticRepository;
            _redisLocksService = redisLocksService;
            _lockTimeOut = lockTimeOut;
        }

        public async Task UpdateVoucherOperationsStatistic(UpdateVoucherOperationsStatistic partnerStatistic)
        {
            var lockValue = $"{partnerStatistic.PartnerId}_{partnerStatistic.OperationType}";
            for (var i = 0; i < MaxAttemptsCount; ++i)
            {
                var locked = await _redisLocksService.TryAcquireLockAsync(
                    lockValue,
                    lockValue,
                    _lockTimeOut);
                if (!locked)
                {
                    await Task.Delay(_lockTimeOut);
                    continue;
                }

                await _voucherOperationsStatisticRepository.UpdateByCurrencyAndOperationType(partnerStatistic);
                await _redisLocksService.ReleaseLockAsync(lockValue, lockValue);
                return;
            }
        }

        public async Task<IList<CurrenciesStatistic>> GetCurrenciesStatistic(Guid[] partnerIds)
        {
            var statistic = await _voucherOperationsStatisticRepository.GetByPartnerIds(partnerIds);

            var groupByCurrencyStatistic = statistic
                .GroupBy(x => x.Currency)
                .ToDictionary(x => x.Key, v => v.ToList());

            return groupByCurrencyStatistic.Select(x =>
                 new CurrenciesStatistic()
                 {
                     Currency = x.Key,
                     TotalPurchasesCost = x.Value
                         .Where(el => el.OperationType == VoucherOperationType.Buy)
                         .Sum(y => y.Amount),
                     TotalPurchasesCount = x.Value
                         .Count(el => el.OperationType == VoucherOperationType.Buy),
                     TotalRedeemedVouchersCost = x.Value
                         .Where(el => el.OperationType == VoucherOperationType.Redeem)
                         .Sum(y => y.Amount),
                     TotalRedeemedVouchersCount = x.Value
                         .Count(el => el.OperationType == VoucherOperationType.Redeem)
                 }).ToList();
        }
    }
}
