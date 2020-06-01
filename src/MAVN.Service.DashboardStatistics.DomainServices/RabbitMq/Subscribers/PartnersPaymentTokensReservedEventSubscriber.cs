using System;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Subscriber;
using MAVN.Service.DashboardStatistics.Domain.Services;
using MAVN.Service.PartnersPayments.Contract;

namespace MAVN.Service.DashboardStatistics.DomainServices.RabbitMq.Subscribers
{
    public class PartnersPaymentTokensReservedEventSubscriber : JsonRabbitSubscriber<PartnersPaymentTokensReservedEvent>
    {
        private readonly ICustomerStatisticService _customerStatisticService;

        private readonly ILog _log;

        public PartnersPaymentTokensReservedEventSubscriber(
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

        protected override async Task ProcessMessageAsync(PartnersPaymentTokensReservedEvent message)
        {
            var context = $"customerId: {message.CustomerId}; paymentRequestId: {message.PaymentRequestId}";

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
                _log.Error(exception, "An error occurred while precessing partners payment tokens reserved event",
                    context);
                throw;
            }

            _log.Info("Partners payment tokens reserved event processed.", context: context);
        }
    }
}
