using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MAVN.Service.DashboardStatistics.Domain.Models.Customers;
using MAVN.Service.DashboardStatistics.Domain.Repositories;
using MAVN.Service.DashboardStatistics.Domain.Services;

namespace MAVN.Service.DashboardStatistics.DomainServices
{
    public class CustomerStatisticService : ICustomerStatisticService
    {
        private readonly ICustomerRegistrationRepository _customerRegistrationRepository;
        private readonly ICustomerActivityRepository _customerActivityRepository;

        public CustomerStatisticService(
            ICustomerRegistrationRepository customerRegistrationRepository,
            ICustomerActivityRepository customerActivityRepository)
        {
            _customerRegistrationRepository = customerRegistrationRepository;
            _customerActivityRepository = customerActivityRepository;
        }

        public async Task<CustomersStatistic> GetAsync(DateTime startDate, DateTime endDate, Guid? partnerId)
        {
            var newCustomersPerDate = new List<CustomersCountAtDate>();

            var totalCustomers = await _customerRegistrationRepository.GetCountSync(endDate, partnerId);

            if (totalCustomers == 0)
            {
                return new CustomersStatistic
                {
                    NewCustomers = newCustomersPerDate,
                    TotalActiveCustomers = 0,
                    TotalCustomers = 0,
                    TotalNewCustomers = 0,
                    TotalNonActiveCustomers = 0,
                };
            }

            var customersCountPerDayTask = _customerRegistrationRepository.GetCountPerDayAsync(startDate, endDate);
            var activeCustomersCountTask = _customerActivityRepository.GetCountAsync(startDate, endDate);

            await Task.WhenAll(customersCountPerDayTask, activeCustomersCountTask);

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
                NewCustomers = newCustomersPerDate
            };

            return statistic;
        }

        public Task AddRegistrationDateAsync(Guid customerId, Guid? partnerId, DateTime registrationDate)
        {
            return _customerRegistrationRepository.InsertIfNotExistsAsync(customerId, partnerId, registrationDate);
        }

        public Task AddActivityDateAsync(Guid customerId, DateTime activityDate)
        {
            return _customerActivityRepository.InsertAsync(customerId, activityDate);
        }
    }
}
