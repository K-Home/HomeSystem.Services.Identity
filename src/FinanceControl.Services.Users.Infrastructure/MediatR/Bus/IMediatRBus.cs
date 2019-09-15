﻿using System.Threading;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Infrastructure.Messages;
using MediatR;

namespace FinanceControl.Services.Users.Infrastructure.MediatR.Bus
{
    public interface IMediatRBus
    {
        Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default(CancellationToken))
            where TCommand : IRequest;

        Task<TResult> QueryAsync<TQuery, TResult>(TQuery command,
            CancellationToken cancellationToken = default(CancellationToken)) where TQuery : IQuery<TResult>;

        Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default(CancellationToken))
            where TEvent : INotification;
    }
}