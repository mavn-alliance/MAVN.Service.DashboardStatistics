using System;
using System.Threading.Tasks;
using MAVN.Service.DashboardStatistics.Domain.Models.Customers;

namespace MAVN.Service.DashboardStatistics.Domain.Services
{
    public interface ICustomerStatisticService
    {
        Task<CustomersStatistic> GetAsync(DateTime fromDate, DateTime toDate, Guid? partnerId);

        Task AddRegistrationDateAsync(Guid customerId,Guid? partnerId, DateTime registrationDate);

        Task AddActivityDateAsync(Guid customerId, DateTime activityDate);
    }
}
