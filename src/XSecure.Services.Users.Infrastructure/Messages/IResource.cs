﻿namespace XSecure.Services.Users.Infrastructure.Messages
{
    public interface IResource
    {
        string Service { get; }
        string EndPoint { get; }
    }
}