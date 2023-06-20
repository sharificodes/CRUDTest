using CRUDTest.Application.Features.Users.Commands;
using CRUDTest.Application.Features.Users.Queries;
using FluentResults;
using Microsoft.AspNetCore.Mvc;


namespace CRUDTest.Presentation.Controllers;

public class UserController : BaseController
{
    [HttpGet]
    public async Task<Result<string>> Login([FromQuery] LoginUserQuery query)
     => await Mediator.Send(query);

    [HttpPost]
    public async Task<Result> Register(RegisterUserCommand command)
       => await Mediator.Send(command);
}
