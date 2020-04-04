using Lykke.Common.MsSql;
using MAVN.Service.DashboardStatistics.Domain.Repositories;
using MAVN.Service.DashboardStatistics.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MAVN.Service.DashboardStatistics.MsSqlRepositories.Repositories
{
    public class CustomerStatisticRepository : ICustomerRegistrationRepository
    {
        private readonly MsSqlContextFactory<DashboardStatisticsContext> _contextFactory;

        public CustomerStatisticRepository(MsSqlContextFactory<DashboardStatisticsContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IReadOnlyDictionary<DateTime, int>> GetCountPerDayAsync(DateTime startDate, DateTime endDate)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var items = await context.CustomerStatistics
                    .Where(o => startDate <= o.TimeStamp && o.TimeStamp <= endDate)
                    .GroupBy(o => o.TimeStamp.Date)
                    .Select(o => new {Date = o.Key, Count = o.Count()})
                    .ToListAsync();

                return items.ToDictionary(o => o.Date, o => o.Count);
            }
        }

        public async Task<int> GetCountSync(DateTime endDate)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var count = await context.CustomerStatistics
                    .Where(o => o.TimeStamp <= endDate)
                    .CountAsync();

                return count;
            }
        }

        public async Task InsertAsync(Guid customerId, DateTime registrationDate)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                await context.AddAsync(new CustomerStatisticEntity
                {
                    Id = Guid.NewGuid(), CustomerId = customerId, TimeStamp = registrationDate
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
