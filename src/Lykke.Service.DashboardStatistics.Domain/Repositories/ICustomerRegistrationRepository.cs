using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Service.DashboardStatistics.Domain.Repositories
{
    public interface ICustomerRegistrationRepository
    {
        Task<int> GetCountSync(DateTime endDate);

        Task<IReadOnlyDictionary<DateTime, int>> GetCountPerDayAsync(DateTime startDate, DateTime endDate);

        Task InsertAsync(Guid customerId, DateTime registrationDate);
    }
}
