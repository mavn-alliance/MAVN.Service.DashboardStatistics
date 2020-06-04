using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MAVN.Service.DashboardStatistics.Domain.Enums;
using MAVN.Service.DashboardStatistics.Domain.Models.VoucherStatistic;
using MAVN.Service.DashboardStatistics.Domain.Repositories;
using MAVN.Service.DashboardStatistics.Domain.Services;
using MongoDB.Driver;

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

            throw new InvalidOperationException("Couldn't acquire a lock in Redis");
        }

        public async Task<IList<CurrenciesStatistic>> GetCurrenciesStatistic(Guid[] partnerIds)
        {
            var statistic = await _voucherOperationsStatisticRepository.GetByPartnerIds(partnerIds);
            var dict = statistic
                .GroupBy(x => x.Currency)
                .ToDictionary(x => x.Key, v => new CurrenciesStatistic() { Currency = v.Key });

            foreach (var s in statistic)
            {
                var currency = s.Currency;
                var element = dict[currency];

                switch (s.OperationType)
                {
                    case VoucherOperationType.Redeem:
                        element.TotalRedeemedVouchersCost += s.Amount;
                        element.TotalRedeemedVouchersCount++;
                        break;
                    case VoucherOperationType.Buy:
                        element.TotalPurchasesCost += s.Amount;
                        element.TotalPurchasesCount++;
                        break;
                }
            }

            return dict.Values.ToList();
        }
    }
}
