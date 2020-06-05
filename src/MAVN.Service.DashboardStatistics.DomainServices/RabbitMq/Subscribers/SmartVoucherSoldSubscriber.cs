using System.Threading.Tasks;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Subscriber;
using MAVN.Service.DashboardStatistics.Domain.Enums;
using MAVN.Service.DashboardStatistics.Domain.Services;
using MAVN.Service.SmartVouchers.Contract;

namespace MAVN.Service.DashboardStatistics.DomainServices.RabbitMq.Subscribers
{
    public class SmartVoucherSoldSubscriber : JsonRabbitSubscriber<SmartVoucherSoldEvent>
    {
        private readonly ICustomerStatisticService _customerStatisticService;

        public SmartVoucherSoldSubscriber
            (
            ICustomerStatisticService customerStatisticService,
            string connectionString,
            string exchangeName,
            string queueName,
            ILogFactory logFactory)
            : base(connectionString, exchangeName, queueName, logFactory)
        {
            _customerStatisticService = customerStatisticService;
        }


        protected override async Task ProcessMessageAsync(SmartVoucherSoldEvent message)
        {
            await _customerStatisticService.AddRegistrationDateAsync(message.CustomerId, message.PartnerId,
                message.Timestamp, VoucherOperationType.Buy, message.Amount, message.Currency);

            await _customerStatisticService.AddActivityDateAsync(message.CustomerId, message.Timestamp,
                message.PartnerId, ActivityType.VoucherBought);
        }
    }
}
