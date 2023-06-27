using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDTest.Application.Features.Products.Queries
{
    public class GetProductQuery : IRequest<Result<ProductDto>>
    {
        public Int64 Id { get; set; }
    }
}
