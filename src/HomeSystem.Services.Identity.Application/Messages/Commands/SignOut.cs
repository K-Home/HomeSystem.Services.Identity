﻿using System;
using System.Runtime.Serialization;
using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
{
    public class SignOut : IAuthenticatedCommand
    {
        [DataMember] 
        public Request Request { get; }

        [DataMember] 
        public Guid SessionId { get; }

        [DataMember] 
        public Guid UserId { get; }

        [JsonConstructor]
        public SignOut(Request request, Guid sessionId, Guid userId)
        {
            Request = request;
            SessionId = sessionId;
            UserId = userId;
        }
    }
}