using AutoMapper;
using MAVN.Job.TokensStatistics.Client.Models.Responses;
using MAVN.Service.DashboardStatistics.Client.Models.Customers;
using MAVN.Service.DashboardStatistics.Client.Models.Tokens;
using MAVN.Service.DashboardStatistics.Client.Models.VoucherStatistic;
using MAVN.Service.DashboardStatistics.Domain.Models.Customers;
using MAVN.Service.DashboardStatistics.Domain.Models.VoucherStatistic;
using MAVN.Service.DashboardStatistics.MsSqlRepositories.Entities;

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

            //Tokens
            CreateMap<TokensListRequestModel, MAVN.Job.TokensStatistics.Client.Models.Requests.PeriodRequest>();
            CreateMap<TokensStatisticResponse, TokensStatisticsModel>();
            CreateMap<TokensStatisticListResponse, TokensListResponseModel>();

            CreateMap<CurrenciesStatistic, VoucherStatisticsResponse>();
            CreateMap<VoucherOperationsStatisticsEntity, VoucherOperationsStatistic>()
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Sum));
        }
    }
}
