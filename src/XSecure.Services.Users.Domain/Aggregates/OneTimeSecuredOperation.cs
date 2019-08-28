using System;
using XSecure.Services.Users.Domain.Exceptions;
using XSecure.Services.Users.Domain.Extensions;
using XSecure.Services.Users.Domain.Types;
using XSecure.Services.Users.Domain.Types.Base;

namespace XSecure.Services.Users.Domain.Aggregates
{
    public class OneTimeSecuredOperation : EntityBase, ITimestampable
    {
        private bool _consumed;

        public string Type { get; private set; }
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public string Token { get; private set; }
        public string RequesterIpAddress { get; private set; }
        public string RequesterUserAgent { get; private set; }
        public string ConsumerIpAddress { get; private set; }
        public string ConsumerUserAgent { get; private set; }
        public DateTime? ConsumedAt { get; private set; }
        public DateTime Expiry { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public bool Consumed => _consumed = IsConsumed();

        protected OneTimeSecuredOperation()
        {
        }

        public OneTimeSecuredOperation(Guid id, string type,
            Guid userId, string token, DateTime expiry,
            string ipAddress = null, string userAgent = null)
        {
            if (type.IsEmpty())
            {
                throw new DomainException(Codes.InvalidSecuredOperation,
                    "Type can not be empty.");
            }

            if (userId == Guid.Empty)
            {
                throw new DomainException(Codes.InvalidSecuredOperation,
                    "User can not be empty.");
            }

            if (token.IsEmpty())
            {
                throw new DomainException(Codes.InvalidSecuredOperation,
                    "Token can not be empty.");
            }

            Id = id;
            Type = type;
            UserId = userId;
            Token = token;
            Expiry = expiry.ToUniversalTime();
            RequesterIpAddress = ipAddress;
            RequesterUserAgent = userAgent;
            CreatedAt = DateTime.UtcNow;
        }

        public void Consume(string ipAddress = null, string userAgent = null)
        {
            if (!CanBeConsumed())
            {
                throw new DomainException(Codes.InvalidSecuredOperation,
                    "Operation can not be consumed.");
            }

            ConsumerIpAddress = ipAddress;
            ConsumerUserAgent = userAgent;
            ConsumedAt = DateTime.UtcNow;
        }

        public bool CanBeConsumed()
        {
            if (Consumed)
                return false;

            return Expiry > DateTime.UtcNow;
        }

        private bool IsConsumed()
            => ConsumedAt.HasValue;
    }
}