﻿using CRUDTest.Application.Common.Mappings;
using CRUDTest.Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDTest.Application.Features.Products.Commands.Update
{
    public class UpdateProductCommand : IRequest<Result> , IMapTo<Product>
    {
        public Int64 Id { get; set; }
        public required string Name { get; set; }
        public DateTime ProduceDate { get; set; }
        public required string ManufacturePhone { get; set; }
        public required string ManufactureEmail { get; set; }
        public bool IsAvailable { get; set; }
    }
}
