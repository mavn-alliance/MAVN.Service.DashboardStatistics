using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lykke.Service.DashboardStatistics.Domain.Models.LeadStatistic;
using Lykke.Service.DashboardStatistics.Domain.Repositories;
using Lykke.Service.DashboardStatistics.Domain.Services;

namespace Lykke.Service.DashboardStatistics.DomainServices
{
    public class LeadStatisticService : ILeadStatisticService
    {
        private readonly ILeadStatisticRepository _leadStatisticRepository;

        public LeadStatisticService(ILeadStatisticRepository leadStatisticRepository)
        {
            _leadStatisticRepository = leadStatisticRepository;
        }

        public async Task<LeadStatisticsListModel> GetAsync(DateTime fromDate, DateTime toDate)
        {
            var leads = await _leadStatisticRepository.GetAsync(fromDate, toDate);

            var leadsDictionary = PrepareLeadsDictionary(fromDate, toDate);
            var totalUnique = new HashSet<string>();
            var leadGroups = leads.GroupBy(i => i.LeadId);
            foreach (var group in leadGroups)
            {
                // Assume timestamp is correct and further timestamp means new state (higher)
                var leadEntries = group.OrderBy(g => g.TimeStamp).ToList();
                var count = leadEntries.Count();
                for (int i = 0; i < count; i++)
                {
                    var lead = leadEntries[i];
                    totalUnique.Add(lead.LeadId);

                    var leadFrom = lead.TimeStamp.Date;
                    var leadTo = toDate.Date;
                    if (i + 1 < count)
                    {
                        leadTo = leadEntries[i + 1].TimeStamp.Date;
                        if (leadFrom == leadTo)
                        {
                            // Don't count same day status change. Next entry will be inserted
                            continue;
                        }

                        // Avoid same day entries with different states
                        // next lead will add entry for last day as first entry with new status
                        leadTo = leadTo.AddDays(-1);
                    }

                    // Ensure total count is correct for the selected period per day
                    foreach (var day in EachDay(leadFrom, leadTo))
                    {
                        // Hashset - one lead will be counted only once per state per day
                        leadsDictionary[day][lead.State].Add(lead.LeadId);
                    }
                }
            }

            return BuildResponseModel(leadsDictionary, totalUnique);
        }

        public Task AddAsync(Guid leadId, DateTime timeStamp, LeadState state)
        {
            return _leadStatisticRepository.InsertAsync(leadId, timeStamp, state);
        }

        private static LeadStatisticsListModel BuildResponseModel(
            IReadOnlyDictionary<DateTime, Dictionary<LeadState, HashSet<string>>> leadsDictionary,
            ICollection<string> totalUnique)
        {
            var leadsForDay = new List<LeadsStatisticsForDayModel>();

            foreach (var (date, leadsInDay) in leadsDictionary)
            {
                var statistics = new List<LeadStatisticModel>();

                var total = new HashSet<string>();
                foreach (var (state, leadsInState) in leadsInDay)
                {
                    statistics.Add(new LeadStatisticModel {Count = leadsInState.Count, State = state});

                    total.UnionWith(leadsInState);
                }

                leadsForDay.Add(new LeadsStatisticsForDayModel {Day = date, Value = statistics, Total = total.Count});
            }

            return new LeadStatisticsListModel {TotalCount = totalUnique.Count, LeadsByDate = leadsForDay};
        }

        private static Dictionary<DateTime, Dictionary<LeadState, HashSet<string>>> PrepareLeadsDictionary(
            DateTime fromDate, DateTime toDate)
        {
            var leadsDictionary = new Dictionary<DateTime, Dictionary<LeadState, HashSet<string>>>();
            foreach (var day in EachDay(fromDate, toDate))
            {
                leadsDictionary.Add(day.Date, new Dictionary<LeadState, HashSet<string>>());

                foreach (var state in (LeadState[]) Enum.GetValues(typeof(LeadState)))
                {
                    leadsDictionary[day.Date].Add(state, new HashSet<string>());
                }
            }

            return leadsDictionary;
        }

        private static IEnumerable<DateTime> EachDay(DateTime fromDate, DateTime toDate)
        {
            for (var day = fromDate.Date; day.Date <= toDate.Date; day = day.AddDays(1))
            {
                yield return day;
            }
        }
    }
}
