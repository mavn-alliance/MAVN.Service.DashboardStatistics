using AutoMapper;
using Lykke.Service.DashboardStatistics.Domain.Models.LeadStatistic;
using Lykke.Service.DashboardStatistics.MsSqlRepositories.Entities;

namespace Lykke.Service.DashboardStatistics.MsSqlRepositories
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<LeadStatisticEntity, LeadModel>(MemberList.Destination);
        }
    }
}
