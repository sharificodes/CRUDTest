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
    }

    public class SaveChangesResult
    {
        public static SaveChangesResult Failure(SaveChangesResultType type, string message)
            => new()
            {
                Message = message,
                IsSuccess = false,
                ResultType = type
            };

        public static SaveChangesResult Success()
            => new()
            {
                IsSuccess = true,
            };

        public bool IsSuccess { get; set; }
        public SaveChangesResultType ResultType { get; set; }
        public string Message { get; set; }
    }

    public enum SaveChangesResultType : byte
    {
        [Display(Name = "Success")]
        Success = 1,

        [Display(Name = "UpdateException")]
        UpdateException = 2,

        [Display(Name = "UpdateConcurrencyException")]
        UpdateConcurrencyException = 3,
    }
}
