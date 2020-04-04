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

        public async Task<CustomersStatistic> GetAsync(DateTime startDate, DateTime endDate)
        {
            var totalCustomersTask = _customerRegistrationRepository.GetCountSync(endDate);
            var customersCountPerDayTask = _customerRegistrationRepository.GetCountPerDayAsync(startDate, endDate);
            var activeCustomersCountTask = _customerActivityRepository.GetCountAsync(startDate, endDate);

            await Task.WhenAll(totalCustomersTask, customersCountPerDayTask, activeCustomersCountTask);

            var newCustomersPerDate = new List<CustomersCountAtDate>();

            for (var date = startDate.Date; date <= endDate.Date; date = date.AddDays(1))
            {
                customersCountPerDayTask.Result.TryGetValue(date, out var customersCount);

                newCustomersPerDate.Add(new CustomersCountAtDate {Date = date, Count = customersCount});
            }

            var statistic = new CustomersStatistic
            {
                TotalCustomers = totalCustomersTask.Result,
                TotalActiveCustomers = activeCustomersCountTask.Result,
                TotalNonActiveCustomers = totalCustomersTask.Result - activeCustomersCountTask.Result,
                TotalNewCustomers = customersCountPerDayTask.Result.Sum(o => o.Value),
                NewCustomers = newCustomersPerDate
            };

            return statistic;
        }

        public Task AddRegistrationDateAsync(Guid customerId, DateTime registrationDate)
        {
            return _customerRegistrationRepository.InsertAsync(customerId, registrationDate);
        }

        public Task AddActivityDateAsync(Guid customerId, DateTime activityDate)
        {
            return _customerActivityRepository.InsertAsync(customerId, activityDate);
        }
    }
}
