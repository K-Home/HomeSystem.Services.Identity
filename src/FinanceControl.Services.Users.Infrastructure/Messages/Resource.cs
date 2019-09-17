﻿namespace FinanceControl.Services.Users.Infrastructure.Messages
{
    public class Resource
    {
        public string Service { get; set; }
        public string Endpoint { get; set; }

        protected Resource()
        {
        }

        protected Resource(string service, string endpoint)
        {
            Service = service.ToLowerInvariant();
            Endpoint = endpoint.ToLowerInvariant();
        }

        public static Resource Create(string service, string endpoint)
        {
            return new Resource(service, endpoint);
        }
    }
}