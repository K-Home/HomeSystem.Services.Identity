﻿namespace FinanceControl.Services.Users.Infrastructure.Messages
{
    public interface IIntegrationCommand
    {
        Request Request { get; }
    }
}