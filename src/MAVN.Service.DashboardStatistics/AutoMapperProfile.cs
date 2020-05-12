using AutoMapper;
using MAVN.Job.TokensStatistics.Client.Models.Responses;
using MAVN.Service.DashboardStatistics.Client.Models.Customers;
using MAVN.Service.DashboardStatistics.Client.Models.Leads;
using MAVN.Service.DashboardStatistics.Client.Models.Tokens;
using MAVN.Service.DashboardStatistics.Domain.Models.Customers;
using MAVN.Service.DashboardStatistics.Domain.Models.LeadStatistic;

namespace MAVN.Service.DashboardStatistics
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Customers
            CreateMap<CustomersStatistic, CustomersStatisticResponse>(MemberList.Source);
            CreateMap<CustomersCountAtDate, CustomerStatisticsByDayResponse>(MemberList.Source)
                .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.Date));

            //Leads
            CreateMap<LeadStatisticsListModel, LeadsListResponseModel>()
                .ForMember(dest => dest.Leads,
                    opt => opt.MapFrom(src => src.LeadsByDate))
                .ForMember(dest => dest.TotalNumber,
                    opt => opt.MapFrom(src => src.TotalCount));
            CreateMap<LeadsStatisticsForDayModel, LeadsStatisticsForDayReportModel>();
            CreateMap<LeadStatisticModel, LeadsStatisticsModel>();

            //Tokens
            CreateMap<TokensListRequestModel, MAVN.Job.TokensStatistics.Client.Models.Requests.PeriodRequest>();
            CreateMap<TokensStatisticResponse, TokensStatisticsModel>();
            CreateMap<TokensStatisticListResponse, TokensListResponseModel>();
        }
    }
}
