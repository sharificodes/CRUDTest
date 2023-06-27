using AutoMapper;
using CRUDTest.Application.Common.Interfaces;
using CRUDTest.Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CRUDTest.Application.Features.Products.Commands.Update
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result>
    {
        private readonly IApplicationUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public UpdateProductCommandHandler(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _uow = applicationUnitOfWork;
            _mapper = mapper;
            _httpContext = httpContext;
        }

        public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productSelected = await _uow.Products
                .Where(x => x.Id == request.Id)
                .Where(x => x.CreatedUser == CurrentUserId)
                .FirstOrDefaultAsync(cancellationToken);

            if (productSelected == null)
                return Result.Fail(new Error("no access!"));
            var ProductUpdated = _mapper.Map(request, productSelected);
            _uow.Products.Update(ProductUpdated);
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
