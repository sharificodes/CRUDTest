using AutoMapper;
using CRUDTest.Application.Common.Interfaces;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CRUDTest.Application.Features.Products.Commands.Delete
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result>
    {
        private readonly IApplicationUnitOfWork _uow;
        private readonly IHttpContextAccessor _httpContext;

        public DeleteProductCommandHandler(IApplicationUnitOfWork applicationUnitOfWork, IHttpContextAccessor httpContext)
        {
            _uow = applicationUnitOfWork;
            _httpContext = httpContext;
        }

        public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productSelected = await _uow.Products
                .Where(x => x.Id == request.Id)
                .Where(x => x.CreatedUser == CurrentUserId)
                .FirstOrDefaultAsync(cancellationToken);

            if (productSelected == null)
                return Result.Fail(new Error("no access!"));
            
            _uow.Products.Remove(productSelected);
            await _uow.SaveChangesAsync(cancellationToken);
            return Result.Ok();
        }

        protected Guid CurrentUserId
        {
            get
            {
                var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
                var userDataClaim = claimsIdentity?.FindFirst(ClaimTypes.UserData);
                return userDataClaim == null ? Guid.Empty : Guid.Parse(userDataClaim.Value);
            }
        }
    }
}
