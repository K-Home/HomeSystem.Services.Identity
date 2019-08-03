﻿using System;
using System.Runtime.Serialization;
using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
{
    public class DisableTwoFactorAuthorization : IAuthenticatedCommand
    {
        [DataMember]
        public Request Request { get; }
        
        [DataMember]
        public Guid UserId { get; }
        
        [DataMember]
        public bool DisableTwoFactorAuthentication { get; }

        [JsonConstructor]
        public DisableTwoFactorAuthorization(Request request, Guid userId, bool disableTwoFactorAuthentication)
        {
            Request = request;
            UserId = userId;
            DisableTwoFactorAuthentication = disableTwoFactorAuthentication;
        }
    }
}
