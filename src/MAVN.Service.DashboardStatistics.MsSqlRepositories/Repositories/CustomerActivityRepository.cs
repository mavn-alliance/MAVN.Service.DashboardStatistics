using System;
using System.Linq;
using System.Threading.Tasks;
using MAVN.Common.MsSql;
using MAVN.Service.DashboardStatistics.Domain.Repositories;
using MAVN.Service.DashboardStatistics.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Service.DashboardStatistics.MsSqlRepositories.Repositories
{
    public class CustomerActivityRepository : ICustomerActivityRepository
    {
        private readonly MsSqlContextFactory<DashboardStatisticsContext> _contextFactory;

        public CustomerActivityRepository(MsSqlContextFactory<DashboardStatisticsContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<int> GetRepeatCountAsync(DateTime startDate, DateTime endDate, Guid[] partnerIds)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var query = context.CustomerActivities
                    .Where(o => startDate <= o.ActivityDate && o.ActivityDate <= endDate);

                if (partnerIds != null && partnerIds.Any())
                    query = query.Where(o => o.PartnerId.HasValue && partnerIds.Contains(o.PartnerId.Value));

                var count = await query
                 .GroupBy(x => new { x.CustomerId, x.ActivityType })
                 .Where(x => x.Key.ActivityType != null && x.Count() >= 2)
                 .Select(o => o.Key.CustomerId)
                 .Distinct()
                 .CountAsync();

                return count;
            }
        }

        public async Task<int> GetCountAsync(DateTime startDate, DateTime endDate, Guid[] partnerIds)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var query = context.CustomerActivities
                    .Where(o => startDate <= o.ActivityDate && o.ActivityDate <= endDate);

                if (partnerIds != null && partnerIds.Any())
                    query = query.Where(o => o.PartnerId.HasValue && partnerIds.Contains(o.PartnerId.Value));

                var count = await query
                    .Select(o => o.CustomerId)
                    .Distinct()
                    .CountAsync();

                return count;
            }
        }

        public async Task InsertAsync(Guid customerId, DateTime activityDate)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                await context.CustomerActivities.AddAsync(new CustomerActivityEntity
                {
                    Id = Guid.NewGuid(),
                    CustomerId = customerId,
                    ActivityDate = activityDate
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
