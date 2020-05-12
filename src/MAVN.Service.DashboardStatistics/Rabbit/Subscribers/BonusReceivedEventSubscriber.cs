using System;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Subscriber;
using MAVN.Service.DashboardStatistics.Domain.Services;
using MAVN.Service.WalletManagement.Contract.Events;

namespace MAVN.Service.DashboardStatistics.Rabbit.Subscribers
{
    public class BonusReceivedEventSubscriber : JsonRabbitSubscriber<BonusReceivedEvent>
    {
        private readonly ICustomerStatisticService _customerStatisticService;

        private readonly ILog _log;

        public BonusReceivedEventSubscriber(
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

        protected override async Task ProcessMessageAsync(BonusReceivedEvent message)
        {
            var context = $"customerId: {message.CustomerId}; transactionId: {message.TransactionId}";

            if (!Guid.TryParse(message.CustomerId, out var customerId))
            {
                _log.Warning("Invalid customer identifier.", context: context);
                return;
            }

            try
            {
                await _customerStatisticService.AddActivityDateAsync(customerId, message.Timestamp);
            }
            catch (Exception exception)
            {
                _log.Error(exception, "An error occurred while precessing bonus received event.", context);
                throw;
            }

            _log.Info("Bonus received event processed.", context: context);
        }
    }
}
