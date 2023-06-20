using CRUDTest.Application.Common.Interfaces;
using CRUDTest.Application.Common.Models;
using CRUDTest.Application.Features.Users.Commands;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDTest.Application.Features.Users.Queries
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, Result<string>>
    {
        private readonly IUserAuthenticationService _userAuthentication;
        public LoginUserQueryHandler(IUserAuthenticationService userAuthentication)
        {
            _userAuthentication = userAuthentication;
        }

        public async Task<Result<string>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var validatedUser = await _userAuthentication.ValidateUserAsync(request.UserName, request.Password);
            if (!validatedUser)
                return Result.Fail(new Error("user is not valid"));

            string token = await _userAuthentication.CreateTokenAsync();
            return Result.Ok<string>(token);
        }
    }
}
