﻿using System;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class PasswordChangedDomainEvent : IDomainEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }

        [JsonConstructor]
        public PasswordChangedDomainEvent(Guid requestId, Guid userId)
        {
            RequestId = requestId;
            UserId = userId;
        }
    }
}