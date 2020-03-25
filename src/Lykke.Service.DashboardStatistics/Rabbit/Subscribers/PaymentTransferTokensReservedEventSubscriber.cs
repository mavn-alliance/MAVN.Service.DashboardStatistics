using System;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Subscriber;
using Lykke.Service.DashboardStatistics.Domain.Services;
using Lykke.Service.PaymentTransfers.Contract;

namespace Lykke.Service.DashboardStatistics.Rabbit.Subscribers
{
    public class PaymentTransferTokensReservedEventSubscriber : JsonRabbitSubscriber<PaymentTransferTokensReservedEvent>
    {
        private readonly ICustomerStatisticService _customerStatisticService;

        private readonly ILog _log;

        public PaymentTransferTokensReservedEventSubscriber(
            string connectionString,
            string exchangeName,
            string queueName,
            ILogFactory logFactory,
            ICustomerStatisticService customerStatisticService)
            : base(connectionString, exchangeName, queueName, true, logFactory)
        {
            _customerStatisticService = customerStatisticService;
            _log = logFactory.CreateLog(this);
        }

        protected override async Task ProcessMessageAsync(PaymentTransferTokensReservedEvent message)
        {
            var context = $"customerId: {message.CustomerId}; transferId: {message.TransferId}";

            if (!Guid.TryParse(message.CustomerId, out var customerId))
            {
                _log.Warning("Invalid customer identifier", context: context);
                return;
            }

            try
            {
                await _customerStatisticService.AddActivityDateAsync(customerId, message.Timestamp);
            }
            catch (Exception exception)
            {
                _log.Error(exception, "An error occurred while precessing payment transfer tokens reserved event",
                    context);
                throw;
            }

            _log.Info("Payment transfer tokens reserved event processed.", context: context);
        }
    }
}
