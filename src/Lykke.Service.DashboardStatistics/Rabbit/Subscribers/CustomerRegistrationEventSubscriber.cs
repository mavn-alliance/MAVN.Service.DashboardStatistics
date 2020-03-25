using System;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Subscriber;
using Lykke.Service.CustomerManagement.Contract.Events;
using Lykke.Service.DashboardStatistics.Domain.Services;

namespace Lykke.Service.DashboardStatistics.Rabbit.Subscribers
{
    public class CustomerRegistrationEventSubscriber : JsonRabbitSubscriber<CustomerRegistrationEvent>
    {
        private readonly ILog _log;

        public CustomerRegistrationEventSubscriber(
            string connectionString,
            string exchangeName,
            string queueName,
            ILogFactory logFactory)
            : base(connectionString, exchangeName, queueName, true, logFactory)
        {
            _log = logFactory.CreateLog(this);
        }

        protected override async Task ProcessMessageAsync(CustomerRegistrationEvent message)
        {
            var context = $"customerId: {message.CustomerId};";

            _log.Info("Customer registration event processed.", context: context);
        }
    }
}
