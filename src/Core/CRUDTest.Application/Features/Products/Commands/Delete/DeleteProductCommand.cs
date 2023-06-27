using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDTest.Application.Features.Products.Commands.Delete
{
    public class DeleteProductCommand : IRequest<Result>
    {
        public Int64 Id { get; set; }
    }
}
