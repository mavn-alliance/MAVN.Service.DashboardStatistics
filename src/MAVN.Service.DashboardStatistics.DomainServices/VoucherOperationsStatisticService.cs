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

            throw new InvalidOperationException("Couldn't acquire a lock in Redis");
        }

        public async Task<IList<CurrenciesStatistic>> GetCurrenciesStatistic(Guid[] partnerIds)
        {
            var statistic = await _voucherOperationsStatisticRepository.GetByPartnerIds(partnerIds);
            var currencyGroups = statistic
                .GroupBy(x => x.Currency);
            var currenciesStatistics = new List<CurrenciesStatistic>();
            foreach (var currencyGroup in currencyGroups)
            {
                var element = new CurrenciesStatistic() { Currency = currencyGroup.Key };

                foreach (var operationsStatistic in currencyGroup)
                {
                    switch (operationsStatistic.OperationType)
                    {
                        case VoucherOperationType.Redeem:
                            element.TotalRedeemedVouchersCost += operationsStatistic.Amount;
                            element.TotalRedeemedVouchersCount += operationsStatistic.TotalCount;
                            break;
                        case VoucherOperationType.Buy:
                            element.TotalPurchasesCost += operationsStatistic.Amount;
                            element.TotalPurchasesCount += operationsStatistic.TotalCount;
                            break;
                    }
                }

                currenciesStatistics.Add(element);
            }

            return currenciesStatistics;
        }
    }
}
