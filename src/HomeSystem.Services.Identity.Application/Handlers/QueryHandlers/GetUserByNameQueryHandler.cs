using AutoMapper;
using HomeSystem.Services.Identity.Application.Dtos;
using HomeSystem.Services.Identity.Application.Messages.Queries;
using HomeSystem.Services.Identity.Application.Services.Base;
using HomeSystem.Services.Identity.Domain.Aggregates;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HomeSystem.Services.Identity.Application.Handlers.QueryHandlers
{
    public class GetUserByNameQueryHandler : IRequestHandler<GetUserByNameQuery, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public GetUserByNameQueryHandler(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }
        
        public async Task<UserDto> Handle(GetUserByNameQuery query, CancellationToken cancellationToken)
        {
            var user = await _userService.GetByNameAsync(query.Name);
            var mappedUser = _mapper.Map<User, UserDto>(user);

            return mappedUser;
        }
    }
}