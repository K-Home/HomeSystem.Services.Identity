﻿using System;
using HomeSystem.Services.Identity.Infrastructure.Messages;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
{
    public class SignUp : ICommand
    {
        public Guid Id { get; }
        public string Name { get; }
        public DateTime When { get; }

        public SignUp(Guid id, string name, DateTime when)
        {
            Id = id;
            Name = name;
            When = when;
        }
    }
}
