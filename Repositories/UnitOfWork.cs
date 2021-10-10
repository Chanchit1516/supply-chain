using Microsoft.Extensions.Logging;
using SupplyChain.Data;
using SupplyChain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplyChain.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed;
        IProductRepository productRepository;
        IUserRepository userRepository;
        //private readonly ILogger _logger;

        public UnitOfWork(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
        }

        public IProductRepository ProductRepository => productRepository ??= new ProductRepository(_context);
        public IUserRepository UserRepository => userRepository ??= new UserRepository(_context);

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
