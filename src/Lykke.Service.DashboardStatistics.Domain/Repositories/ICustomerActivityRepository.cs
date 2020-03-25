using System;
using System.Threading.Tasks;

namespace Lykke.Service.DashboardStatistics.Domain.Repositories
{
    public interface ICustomerActivityRepository
    {
        Task<int> GetCountAsync(DateTime startDate, DateTime endDate);

        Task InsertAsync(Guid customerId, DateTime activityDate);
    }
}
