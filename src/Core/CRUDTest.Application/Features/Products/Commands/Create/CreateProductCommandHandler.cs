using AutoMapper;
using CRUDTest.Application.Common.Interfaces;
using CRUDTest.Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CRUDTest.Application.Features.Products.Commands.Create
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result>
    {
        private readonly IApplicationUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public CreateProductCommandHandler(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _uow = applicationUnitOfWork;
            _mapper = mapper;
            _httpContext = httpContext;
        }

        public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);
            product.CreatedUser = CurrentUserId;
            _uow.Products.Add(product);
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
