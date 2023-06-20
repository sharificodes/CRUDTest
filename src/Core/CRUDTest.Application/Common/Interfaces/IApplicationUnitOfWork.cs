using CRUDTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CRUDTest.Application.Common.Interfaces
{

    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        /// <summary>
        /// Save all entities in to database.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<SaveChangesResult> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        public DbSet<Product> Products { get; }
        public DbSet<User> Users { get; }
    }
}
