using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MAVN.Service.DashboardStatistics.Domain.Enums;
using MAVN.Service.DashboardStatistics.Domain.Models.Customers;
using MAVN.Service.DashboardStatistics.Domain.Models.VoucherStatistic;
using MAVN.Service.DashboardStatistics.Domain.Repositories;
using MAVN.Service.DashboardStatistics.Domain.Services;

namespace MAVN.Service.DashboardStatistics.DomainServices
{
    public class CustomerStatisticService : ICustomerStatisticService
    {
        private readonly ICustomerRegistrationRepository _customerRegistrationRepository;
        private readonly ICustomerActivityRepository _customerActivityRepository;
        private readonly IVoucherOperationsStatisticService _voucherOperationsStatisticService;

        public CustomerStatisticService(
            ICustomerRegistrationRepository customerRegistrationRepository,
            ICustomerActivityRepository customerActivityRepository,
            IVoucherOperationsStatisticService voucherOperationsStatisticService)
        {
            _customerRegistrationRepository = customerRegistrationRepository;
            _customerActivityRepository = customerActivityRepository;
            _voucherOperationsStatisticService = voucherOperationsStatisticService;
        }

        public async Task<CustomersStatistic> GetAsync(DateTime startDate, DateTime endDate, Guid[] partnerIds)
        {
            var newCustomersPerDate = new List<CustomersCountAtDate>();

            var totalCustomers = await _customerRegistrationRepository.GetCountSync(endDate, partnerIds);

            if (totalCustomers == 0)
            {
                return new CustomersStatistic
                {
                    NewCustomers = newCustomersPerDate,
                    TotalActiveCustomers = 0,
                    TotalCustomers = 0,
                    TotalNewCustomers = 0,
                    TotalNonActiveCustomers = 0,
                    TotalRepeatCustomers = 0,
                };
            }

            var customersCountPerDayTask = _customerRegistrationRepository.GetCountPerDayAsync(startDate, endDate, partnerIds);
            var activeCustomersCountTask = _customerActivityRepository.GetCountAsync(startDate, endDate, partnerIds);
            var repeaterCustomersCountTask = _customerActivityRepository.GetRepeatCountAsync(startDate, endDate, partnerIds);

            await Task.WhenAll(customersCountPerDayTask, activeCustomersCountTask, repeaterCustomersCountTask);

            for (var date = startDate.Date; date <= endDate.Date; date = date.AddDays(1))
            {
                customersCountPerDayTask.Result.TryGetValue(date, out var customersCount);

                newCustomersPerDate.Add(new CustomersCountAtDate { Date = date, Count = customersCount });
            }

            var statistic = new CustomersStatistic
            {
                TotalCustomers = totalCustomers,
                TotalActiveCustomers = activeCustomersCountTask.Result,
                TotalNonActiveCustomers = totalCustomers - activeCustomersCountTask.Result,
                TotalNewCustomers = customersCountPerDayTask.Result.Sum(o => o.Value),
                NewCustomers = newCustomersPerDate,
                TotalRepeatCustomers = repeaterCustomersCountTask.Result,
            };

            return statistic;
        }

        public async Task AddRegistrationDateAsync(Guid customerId, Guid? partnerId, DateTime registrationDate,
            VoucherOperationType operationType, decimal amount, string currency)
        {
            await _customerRegistrationRepository.InsertIfNotExistsAsync(customerId, partnerId, registrationDate);

            var model = new UpdateVoucherOperationsStatistic()
            {
                PartnerId = partnerId.GetValueOrDefault(),
                Amount = amount,
                OperationType = operationType,
                Currency = currency,
            };

            await _voucherOperationsStatisticService.UpdateVoucherOperationsStatistic(model);
        }

        public Task AddActivityDateAsync(Guid customerId, DateTime activityDate, Guid? partnerId, ActivityType? activityType)
        {
            return _customerActivityRepository.InsertAsync(customerId, activityDate);
        }
    }
}
