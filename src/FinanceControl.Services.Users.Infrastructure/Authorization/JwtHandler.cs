using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using FinanceControl.Services.Users.Domain.Extensions;
using FinanceControl.Services.Users.Infrastructure.Extensions;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace FinanceControl.Services.Users.Infrastructure.Authorization
{
    public class JwtTokenHandler : IJwtTokenHandler
    {
        private const string RoleClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
        private const string StateClaim = "state";
        private const string SessionClaim = "session";
        private const string IpAddressClaim = "ipAddress";
        private const string UserAgentClaim = "userAgent";

        private readonly ILogger _logger;
        private readonly JwtTokenSettings _settings;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private TokenValidationParameters _tokenValidationParameters;
        private SecurityKey _issuerSigningKey;
        private SigningCredentials _signingCredentials;
        private JwtHeader _jwtHeader;

        public JwtTokenHandler(ILogger logger, JwtTokenSettings settings)
        {
            _logger = logger.CheckIfNotEmpty();
            _settings = settings.CheckIfNotEmpty();

            InitializeHmac();
            InitializeJwtParameters();
        }

        private void InitializeHmac()
        {
            _issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
            _signingCredentials = new SigningCredentials(_issuerSigningKey, SecurityAlgorithms.HmacSha256);
        }

        private void InitializeJwtParameters()
        {
            _jwtHeader = new JwtHeader(_signingCredentials);
            _tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidIssuer = _settings.Issuer,
                ValidateIssuer = _settings.ValidateIssuer,
                IssuerSigningKey = _issuerSigningKey
            };
        }

        public JwtBasic Create(Guid userId, Guid sessionId, string role, string state, string ipAddress,
            string userAgent)
        {
            var now = DateTime.UtcNow;
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToTimestamp().ToString()),
                new Claim(ClaimTypes.Role, role),
                new Claim(SessionClaim, sessionId.ToString()),
                new Claim(StateClaim, state),
                new Claim(IpAddressClaim, ipAddress),
                new Claim(UserAgentClaim, userAgent)
            };
            var expires = now.AddDays(_settings.ExpiryDays);
            var jwt = new JwtSecurityToken(
                _settings.Issuer,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: _signingCredentials
            );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtBasic
            {
                Token = token,
                Expires = expires.ToTimestamp()
            };
        }

        public string GetFromAuthorizationHeader(string authorizationHeader)
        {
            if (authorizationHeader.IsEmpty())
            {
                return null;
            }

            var data = authorizationHeader.Trim().Split(' ');
            if (data.Length != 2 || data.Any(x => x.IsEmpty()))
            {
                return null;
            }

            if (data[0].ToLowerInvariant() != "bearer")
            {
                return null;
            }

            return data[1];
        }

        public JwtDetails Parse(string token)
        {
            try
            {
                _jwtSecurityTokenHandler.ValidateToken(token, _tokenValidationParameters,
                    out var validatedSecurityToken);

                if (validatedSecurityToken is JwtSecurityToken validatedJwt)
                {
                    return new JwtDetails
                    {
                        Subject = validatedJwt.Subject,
                        Claims = validatedJwt.Claims,
                        Role = validatedJwt.Claims.FirstOrDefault(x => x.Type == RoleClaim)?.Value,
                        State = validatedJwt.Claims.FirstOrDefault(x => x.Type == StateClaim)?.Value,
                        IpAddress = validatedJwt.Claims.First(x => x.Type == IpAddressClaim)?.Value,
                        UserAgent = validatedJwt.Claims.First(x => x.Type == UserAgentClaim)?.Value,
                        SessionId = validatedJwt.Claims.First(x => x.Type == SessionClaim)?.Value,
                        Expires = validatedJwt.ValidTo.ToTimestamp()
                    };
                }

                return null;
            }
            catch (Exception exception)
            {
                _logger.Error(exception, $"JWT Token parser error. {exception.Message}");

                return null;
            }
        }
    }
}