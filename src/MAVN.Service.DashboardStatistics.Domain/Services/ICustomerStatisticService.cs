using System;
using System.Threading.Tasks;
using MAVN.Service.DashboardStatistics.Domain.Models.Customers;

namespace MAVN.Service.DashboardStatistics.Domain.Services
{
    public interface ICustomerStatisticService
    {
        Task<CustomersStatistic> GetAsync(DateTime fromDate, DateTime toDate);

        Task AddRegistrationDateAsync(Guid customerId, DateTime registrationDate);

        Task AddActivityDateAsync(Guid customerId, DateTime activityDate);
    }
}
