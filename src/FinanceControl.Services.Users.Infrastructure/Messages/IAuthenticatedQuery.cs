﻿using System;
using System.Runtime.Serialization;

namespace FinanceControl.Services.Users.Infrastructure.Messages
{
    public interface IAuthenticatedQuery : IQuery
    {
        [DataMember]
        Guid UserId { get; }
    }

    public interface IAuthenticatedQuery<out T> : IQuery<T>
    {
        [DataMember]
        Guid UserId { get; }
    }
}