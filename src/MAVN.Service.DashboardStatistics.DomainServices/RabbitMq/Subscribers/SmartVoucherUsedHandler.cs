using System.Threading.Tasks;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Subscriber;
using MAVN.Service.DashboardStatistics.Domain.Enums;
using MAVN.Service.DashboardStatistics.Domain.Services;
using MAVN.Service.SmartVouchers.Contract;

namespace MAVN.Service.DashboardStatistics.DomainServices.RabbitMq.Subscribers
{
    public class SmartVoucherUsedHandler : JsonRabbitSubscriber<SmartVoucherUsedEvent>
    {
        private readonly ICustomerStatisticService _customerStatisticService;

        public SmartVoucherUsedHandler(
            ICustomerStatisticService customerStatisticService,
            string connectionString,
            string exchangeName,
            string queueName,
            ILogFactory logFactory)
            : base(connectionString, exchangeName, queueName, logFactory)
        {
            _customerStatisticService = customerStatisticService;
        }

        protected override async Task ProcessMessageAsync(SmartVoucherUsedEvent message)
        {
            await _customerStatisticService.AddRegistrationDateAsync(message.CustomerId, message.PartnerId,
                message.Timestamp, VoucherOperationType.Redeem, message.Amount, message.Currency);
        }
    }
}
