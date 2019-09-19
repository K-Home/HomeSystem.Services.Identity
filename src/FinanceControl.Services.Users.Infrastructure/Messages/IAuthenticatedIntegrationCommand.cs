﻿using System;
using System.Runtime.Serialization;

namespace FinanceControl.Services.Users.Infrastructure.Messages
{
    public interface IAuthenticatedIntegrationCommand : IIntegrationCommand
    {
        [DataMember]
        Guid UserId { get; }
    }
}