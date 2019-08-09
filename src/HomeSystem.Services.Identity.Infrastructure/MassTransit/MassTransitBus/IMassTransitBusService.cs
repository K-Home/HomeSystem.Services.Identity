﻿using System.Threading;
using System.Threading.Tasks;
using HomeSystem.Services.Identity.Infrastructure.Messages;
using Microsoft.Extensions.Hosting;

namespace HomeSystem.Services.Identity.Infrastructure.MassTransit.MassTransitBus
{
    public interface IMassTransitBusService : IHostedService
    {
        Task SendAsync<TIntegrationCommand>(TIntegrationCommand integrationCommand,
            CancellationToken cancellationToken = default(CancellationToken))
            where TIntegrationCommand : class, IIntegrationCommand;

        Task PublishAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent,
            CancellationToken cancellationToken = default(CancellationToken))
            where TIntegrationEvent : class, IIntegrationEvent;
    }
}
