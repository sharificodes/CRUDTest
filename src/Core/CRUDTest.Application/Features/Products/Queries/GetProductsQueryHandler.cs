using AutoMapper;
using CRUDTest.Application.Common.Interfaces;
using CRUDTest.Application.Common.Mappings;
using CRUDTest.Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CRUDTest.Application.Features.Products.Queries
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Result<IEnumerable<ProductDto>>>
    {
        private readonly IApplicationUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public GetProductsQueryHandler(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _uow = applicationUnitOfWork;
            _mapper = mapper;
            _httpContext = httpContext;
        }

        public async Task<Result<IEnumerable<ProductDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var query = _uow.Products.AsQueryable();

            if (!string.IsNullOrEmpty(request.Name))
                query = query.Where(u => u.Name.Contains(request.Name));

            // ...

            var result = await query.Where(u=> u.CreatedUser == CurrentUserId)
                .ProjectToListAsync<ProductDto>(_mapper.ConfigurationProvider);
            return Result.Ok<IEnumerable<ProductDto>>(result);
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
