using System;
using System.Threading.Tasks;
using AutoMapper;
using Common.Log;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Subscriber;
using Lykke.Service.DashboardStatistics.Domain.Models.LeadStatistic;
using Lykke.Service.DashboardStatistics.Domain.Services;
using Lykke.Service.Referral.Contract.Events;

namespace Lykke.Service.DashboardStatistics.Rabbit.Subscribers
{
    public class LeadStateChangedSubscriber : JsonRabbitSubscriber<LeadStateChangedEvent>
    {
        private readonly ILeadStatisticService _leadStatisticService;
        private readonly IMapper _mapper;

        private readonly ILog _log;

        public LeadStateChangedSubscriber(
            string connectionString,
            string exchangeName,
            string queueName,
            ILogFactory logFactory,
            ILeadStatisticService leadStatisticService,
            IMapper mapper)
            : base(connectionString, exchangeName, queueName, true, logFactory)
        {
            _leadStatisticService = leadStatisticService;
            _mapper = mapper;
            _log = logFactory.CreateLog(this);
        }

        protected override async Task ProcessMessageAsync(LeadStateChangedEvent message)
        {
            var context = $"leadId: {message.LeadId};";

            if (!Guid.TryParse(message.LeadId, out var leadId))
            {
                _log.Warning("Invalid lead identifier", context: context);
                return;
            }

            try
            {
                var leadState = _mapper.Map<LeadState>(message.State);

                await _leadStatisticService.AddAsync(leadId, message.TimeStamp, leadState);
            }
            catch (Exception exception)
            {
                _log.Error(exception, "An error occurred while precessing lead state changed event", context);
                throw;
            }

            _log.Info("Lead state changed event processed.", context: context);
        }
    }
}
