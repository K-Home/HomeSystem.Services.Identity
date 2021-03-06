﻿using System.Threading;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Domain.Extensions;
using FinanceControl.Services.Users.Infrastructure.Messages;
using MassTransit;

namespace FinanceControl.Services.Users.Infrastructure.MassTransit.MassTransitBus
{
    public class MassTransitBusService : IMassTransitBusService
    {
        private readonly IBusControl _busControl;
        private readonly IBus _bus;

        public MassTransitBusService(IBusControl busControl, IBus bus)
        {
            _busControl = busControl.CheckIfNotEmpty();
            _bus = bus.CheckIfNotEmpty();
        }

        public async Task SendAsync<TIntegrationCommand>(TIntegrationCommand integrationCommand)
            where TIntegrationCommand : class, IIntegrationCommand
        {
            await _bus.Publish(integrationCommand);
        }

        public async Task SendAsync<TIntegrationCommand>(TIntegrationCommand integrationCommand,
            CancellationToken cancellationToken)
            where TIntegrationCommand : class, IIntegrationCommand
        {
            await _bus.Publish(integrationCommand, cancellationToken);
        }

        public async Task PublishAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent)
            where TIntegrationEvent : class, IIntegrationEvent
        {
            await _bus.Publish(integrationEvent);
        }

        public async Task PublishAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent,
            CancellationToken cancellationToken)
            where TIntegrationEvent : class, IIntegrationEvent
        {
            await _bus.Publish(integrationEvent, cancellationToken);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _busControl.StartAsync(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _busControl.StopAsync(cancellationToken);
        }
    }
}