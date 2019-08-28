﻿using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using XSecure.Services.Users.Infrastructure.Messages;

// namespace must be the same in services, required by MassTransit library
// https://stackoverflow.com/questions/52477283/masstransit-consume-equal-objects-defined-in-different-namespaces
namespace XSecure.IntegrationMessages.IntegrationEvents
{
    public class SignUpRequestCreatedIntegrationEvent : IIntegrationEvent
    {
        [DataMember]
        public Guid RequestId { get; }

        [DataMember]
        public Guid UserId { get; }

        [DataMember]
        public Resource Resource { get; }

        [DataMember]
        public string Message { get; }

        [JsonConstructor]
        public SignUpRequestCreatedIntegrationEvent(Guid requestId, Guid userId, Resource resource, string message)
        {
            RequestId = requestId;
            UserId = userId;
            Resource = resource;
            Message = message;
        }
    }
}