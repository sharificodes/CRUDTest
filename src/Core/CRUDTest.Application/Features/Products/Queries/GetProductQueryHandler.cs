using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRUDTest.Application.Common.Interfaces;
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

namespace CRUDTest.Application.Features.Products.Queries
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Result<ProductDto>>
    {
        private readonly IApplicationUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public GetProductQueryHandler(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _uow = applicationUnitOfWork;
            _mapper = mapper;
            _httpContext = httpContext;
        }

        public async Task<Result<ProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var result = await _uow.Products
                .Where(x => x.Id == request.Id)
                .Where(u => u.CreatedUser == CurrentUserId)
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
            if (result == null)
                return Result.Fail(new Error("product not found!"));
            return Result.Ok(result);
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
