﻿using CRUDTest.Application.Common;
using CRUDTest.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDTest.Persistense.UnitOfWork
{
    public partial class ApplicationUnitOfWork : IApplicationUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUnitOfWork(ApplicationDbContext applicationDbContext)
           => _context = applicationDbContext;

        public async Task<SaveChangesResult> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                return SaveChangesResult.Success();
            }
            catch (DbUpdateConcurrencyException e)
            {
                return SaveChangesResult.Failure(SaveChangesResultType.UpdateConcurrencyException, e.Message);
            }
            catch (DbUpdateException e)
            {
                return SaveChangesResult.Failure(SaveChangesResultType.UpdateException, e.Message);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}
