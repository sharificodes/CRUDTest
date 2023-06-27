using CRUDTest.Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDTest.Application.Features.Products.Queries
{
    public class GetProductsQuery : IRequest<Result<IEnumerable<ProductDto>>>
    {
        public string? Name { get; set; }
        public DateTime? ProduceDate { get; set; }
        public string? ManufacturePhone { get; set; }
        public string? ManufactureEmail { get; set; }
        public bool IsAvailable { get; set; }
    }
}
