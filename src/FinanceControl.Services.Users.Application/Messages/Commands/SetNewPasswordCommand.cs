﻿using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class SetNewPasswordCommand : ICommand
    {
        [DataMember]
        public Request Request { get; }

        [DataMember]
        public string Email { get; }

        [DataMember]
        public string Token { get; }

        [DataMember]
        public string Password { get; }

        [JsonConstructor]
        public SetNewPasswordCommand(string email, string token, string password)
        {
            Request = Request.New<SetNewPasswordCommand>();
            Email = email;
            Token = token;
            Password = password;
        }
    }
}