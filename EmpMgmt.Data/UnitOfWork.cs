using EmpMgmt.Core.IRepositories;
using EmpMgmt.Data.Data;
using EmpMgmt.Data.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace EmpMgmt.Data.UnitOfWorkNamespace
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;
        private bool _disposed = false;

        public IEmployeeRepository EmployeeRepository { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            EmployeeRepository = new EmployeeRepository(_context);
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                    _transaction?.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
