﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDTest.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Int64 Id { get; set; }
        public Guid CreatedUser { get; set; }
    }
}
