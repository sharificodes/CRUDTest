using CRUDTest.Application.Features.Products.Commands.Create;
using CRUDTest.Application.Features.Products.Commands.Delete;
using CRUDTest.Application.Features.Products.Commands.Update;
using CRUDTest.Application.Features.Products.Queries;
using CRUDTest.Domain.Entities;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRUDTest.Presentation.Controllers
{
    [Authorize]
    public class ProductController : BaseController
    {
        [HttpGet]
        public async Task<Result<ProductDto>> Get([FromQuery] GetProductQuery query)
            => await Mediator.Send(query);

        [HttpGet]
        public async Task<Result<IEnumerable<ProductDto>>> GetAll([FromQuery] GetProductsQuery query)
            => await Mediator.Send(query);

        [HttpPost]
        public async Task<Result> Post([FromBody] CreateProductCommand command)
            => await Mediator.Send(command);


        [HttpPut]
        public async Task<Result> Put([FromBody] UpdateProductCommand command)
            => await Mediator.Send(command);


        [HttpDelete]
        public async Task<Result> Delete([FromBody] DeleteProductCommand command)
            => await Mediator.Send(command);
    }
}
