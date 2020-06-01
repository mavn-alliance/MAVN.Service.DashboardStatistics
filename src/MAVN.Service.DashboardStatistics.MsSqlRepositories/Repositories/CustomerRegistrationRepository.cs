using MAVN.Common.MsSql;
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
                    .Select(o => new { Date = o.Key, Count = o.Count() })
                    .ToListAsync();

                return items.ToDictionary(o => o.Date, o => o.Count);
            }
        }

        public async Task<int> GetCountSync(DateTime endDate, Guid? partnerId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var query = context.CustomerStatistics
                    .Where(o => o.TimeStamp <= endDate);

                if (partnerId.HasValue)
                    query = query.Where(o => o.PartnerId == partnerId);

                var count = await query.CountAsync();
                return count;
            }
        }

        public async Task InsertIfNotExistsAsync(Guid customerId, Guid? partnerId, DateTime registrationDate)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var alreadyExists = await context.CustomerStatistics.AnyAsync(x =>
                    x.CustomerId == customerId && partnerId.HasValue && x.PartnerId == partnerId);

                if (alreadyExists)
                    return;

                await context.AddAsync(new CustomerStatisticEntity
                {
                    Id = Guid.NewGuid(),
                    CustomerId = customerId,
                    PartnerId = partnerId,
                    TimeStamp = registrationDate
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
