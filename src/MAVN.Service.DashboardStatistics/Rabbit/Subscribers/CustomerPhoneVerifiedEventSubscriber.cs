using Common.Log;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Subscriber;
using MAVN.Service.DashboardStatistics.Domain.Services;
using System;
using System.Threading.Tasks;
using MAVN.Service.CustomerProfile.Contract;

namespace MAVN.Service.DashboardStatistics.Rabbit.Subscribers
{
    public class CustomerPhoneVerifiedEventSubscriber 
        : JsonRabbitSubscriber<CustomerPhoneVerifiedEvent>
    {
        private readonly ICustomerStatisticService _customerStatisticService;

        private readonly ILog _log;
        public CustomerPhoneVerifiedEventSubscriber(
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

        protected override async Task ProcessMessageAsync(CustomerPhoneVerifiedEvent message)
        {
            var context = $"customerId: {message.CustomerId};";

            if (!Guid.TryParse(message.CustomerId, out var customerId))
            {
                _log.Warning("Invalid customer identifier", context: context);
                return;
            }

            try
            {
                await _customerStatisticService.AddRegistrationDateAsync(customerId, message.Timestamp);
            }
            catch (Exception exception)
            {
                _log.Error(exception, "An error occurred while precessing email verification code event", context);
                throw;
            }

            _log.Info("Customer email verification code event processed.", context: context);
        }
    }
}
