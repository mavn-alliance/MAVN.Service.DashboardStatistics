using System;
using System.Threading.Tasks;

namespace MAVN.Service.DashboardStatistics.Domain.Repositories
{
    public interface ICustomerActivityRepository
    {
        Task<int> GetCountAsync(DateTime startDate, DateTime endDate, Guid[] partnerIds, bool filterByPartnerIds);

        Task InsertAsync(Guid customerId, DateTime activityDate);

        Task<int> GetRepeatCountAsync(DateTime startDate, DateTime endDate, Guid[] partnerIds, bool filterByPartnerIds);
    }
}
