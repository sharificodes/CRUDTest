using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDTest.Application.Features.Users.Queries
{
    public class LoginUserQuery : IRequest<Result<string>>
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
