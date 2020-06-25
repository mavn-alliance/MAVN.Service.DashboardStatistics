using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MAVN.Common.MsSql;
using MAVN.Service.DashboardStatistics.Domain.Models.VoucherStatistic;
using MAVN.Service.DashboardStatistics.Domain.Repositories;
using MAVN.Service.DashboardStatistics.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Service.DashboardStatistics.MsSqlRepositories.Repositories
{
    public class VoucherOperationsStatisticRepository : IVoucherOperationsStatisticRepository
    {
        private readonly MsSqlContextFactory<DashboardStatisticsContext> _contextFactory;
        private readonly IMapper _mapper;

        public VoucherOperationsStatisticRepository(
            MsSqlContextFactory<DashboardStatisticsContext> contextFactory,
            IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task UpdateByCurrencyAndOperationType(UpdateVoucherOperationsStatistic partnerStatistic)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.VoucherOperationsStatistics
                    .FirstOrDefaultAsync(x =>
                        x.PartnerId == partnerStatistic.PartnerId
                        && x.Currency == partnerStatistic.Currency
                        && x.OperationType == partnerStatistic.OperationType);
                if (entity != null)
                {
                    entity.TotalCount++;
                    entity.Sum += partnerStatistic.Amount;

                    context.Update(entity);
                }
                else
                {
                    var newEntity = new VoucherOperationsStatisticsEntity()
                    {
                        PartnerId = partnerStatistic.PartnerId,
                        Currency = partnerStatistic.Currency,
                        Sum = partnerStatistic.Amount,
                        OperationType = partnerStatistic.OperationType,
                        TotalCount = 1
                    };

                    context.VoucherOperationsStatistics.Add(newEntity);
                }

                await context.SaveChangesAsync();
            }
        }

        public async Task<IList<VoucherOperationsStatistic>> GetByPartnerIds(Guid[] partnerIds)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var query =  context.VoucherOperationsStatistics.AsQueryable();

                if (partnerIds != null && partnerIds.Any())
                    query = query.Where(x => partnerIds.Contains(x.PartnerId));

                var result = await query
                    .Select(x => _mapper.Map<VoucherOperationsStatistic>(x))
                    .ToListAsync();

                return result;
            }
        }
    }
}
