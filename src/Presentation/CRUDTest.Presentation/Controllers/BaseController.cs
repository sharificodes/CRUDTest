using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CRUDTest.Presentation.Controllers;

[Route("api/[controller]/[action]"), ApiController]
public abstract class BaseController : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
