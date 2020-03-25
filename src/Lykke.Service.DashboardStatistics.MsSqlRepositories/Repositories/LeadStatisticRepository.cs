using Lykke.Common.MsSql;
using Lykke.Service.DashboardStatistics.Domain.Models.LeadStatistic;
using Lykke.Service.DashboardStatistics.Domain.Repositories;
using Lykke.Service.DashboardStatistics.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Lykke.Service.DashboardStatistics.MsSqlRepositories.Repositories
{
    public class LeadStatisticRepository : ILeadStatisticRepository
    {
        private readonly MsSqlContextFactory<DashboardStatisticsContext> _contextFactory;
        private readonly IMapper _mapper;

        public LeadStatisticRepository(MsSqlContextFactory<DashboardStatisticsContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<LeadModel>> GetAsync(DateTime fromDate, DateTime toDate)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var leads = await context.LeadStatistics
                    .Where(l => fromDate.Date <= l.TimeStamp.Date && toDate.Date >= l.TimeStamp.Date)
                    .ToListAsync();

                return _mapper.Map<List<LeadModel>>(leads).AsReadOnly();
            }
        }

        public async Task InsertAsync(Guid leadId, DateTime timeStamp, LeadState state)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                await context.AddAsync(new LeadStatisticEntity {LeadId = leadId, TimeStamp = timeStamp, State = state});

                await context.SaveChangesAsync();
            }
        }
    }
}
