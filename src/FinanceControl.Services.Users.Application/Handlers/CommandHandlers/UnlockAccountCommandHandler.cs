﻿using System.Threading;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Application.Messages.Commands;
using FinanceControl.Services.Users.Application.Messages.DomainEvents;
using FinanceControl.Services.Users.Application.Services.Base;
using FinanceControl.Services.Users.Domain;
using FinanceControl.Services.Users.Domain.Extensions;
using FinanceControl.Services.Users.Infrastructure.Handlers;
using FinanceControl.Services.Users.Infrastructure.MediatR.Bus;
using MediatR;

namespace FinanceControl.Services.Users.Application.Handlers.CommandHandlers
{
    internal sealed class UnlockAccountCommandHandler : AsyncRequestHandler<UnlockAccountCommand>
    {
        private readonly IHandler _handler;
        private readonly IMediatRBus _mediatRBus;
        private readonly IUserService _userService;

        public UnlockAccountCommandHandler(IHandler handler, IMediatRBus mediatRBus, IUserService userService)
        {
            _handler = handler.CheckIfNotEmpty();
            _mediatRBus = mediatRBus.CheckIfNotEmpty();
            _userService = userService.CheckIfNotEmpty();
        }

        protected override async Task Handle(UnlockAccountCommand command, CancellationToken cancellationToken)
        {
            await _handler
                .Run(async () =>
                {
                    await _userService.UnlockAsync(command.UnlockUserId);
                    await _userService.SaveChangesAsync(cancellationToken);
                })
                .OnSuccess(async () =>
                {
                    await _mediatRBus.PublishAsync(
                        new AccountUnlockedDomainEvent(command.Request.Id, command.UserId, command.UnlockUserId),
                        cancellationToken);
                })
                .OnCustomError(async customException =>
                {
                    await _mediatRBus.PublishAsync(
                        new UnlockAccountRejectedDomainEvent(command.Request.Id, command.UserId, command.UnlockUserId,
                            customException.Message, customException.Code), cancellationToken);
                })
                .OnError(async (exception, logger) =>
                {
                    logger.Error($"Error occured when unlocking a account for user with id: {command.UserId}.",
                        exception);
                    await _mediatRBus.PublishAsync(
                        new UnlockAccountRejectedDomainEvent(command.Request.Id, command.UserId, command.UnlockUserId,
                            exception.Message, Codes.Error), cancellationToken);
                })
                .ExecuteAsync();
        }
    }
}