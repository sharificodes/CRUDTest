using CRUDTest.Application.Common.Interfaces;
using FluentResults;
using MediatR;

namespace CRUDTest.Application.Features.Users.Commands
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result>
    {
        private readonly IUserAuthenticationService _userAuthentication;
        public RegisterUserCommandHandler(IUserAuthenticationService userAuthentication)
        {
            _userAuthentication = userAuthentication;
        }

        public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userAuthentication.RegisterUserAsync(request.UserName, request.Password) ;
            return result;
        }
    }
}
