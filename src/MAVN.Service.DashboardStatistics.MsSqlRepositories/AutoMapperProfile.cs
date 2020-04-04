using AutoMapper;
using MAVN.Service.DashboardStatistics.Domain.Models.LeadStatistic;
using MAVN.Service.DashboardStatistics.MsSqlRepositories.Entities;

namespace MAVN.Service.DashboardStatistics.MsSqlRepositories
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<LeadStatisticEntity, LeadModel>(MemberList.Destination);
        }
    }
}
