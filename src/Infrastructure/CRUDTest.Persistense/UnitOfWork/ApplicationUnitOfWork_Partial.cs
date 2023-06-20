using CRUDTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDTest.Persistense.UnitOfWork
{
    public partial class ApplicationUnitOfWork
    {
        public DbSet<Product> Products => _context.Set<Product>();
        public DbSet<User> Users => _context.Set<User>();
    }
}
