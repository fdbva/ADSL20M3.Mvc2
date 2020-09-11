using System;
using System.Threading.Tasks;
using Data.Infrastructure.Context;
using Domain.Model.Interfaces.UoW;

namespace Infrastructure.Data.UoW
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly BibliotecaContext _bibliotecaContext;
        private bool _disposed;

        public UnitOfWork(
            BibliotecaContext bibliotecaContext)
        {
            _bibliotecaContext = bibliotecaContext;
        }

        public void BeginTransaction()
        {
            _disposed = false;
        }

        public void Commit()
        {
            _bibliotecaContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _bibliotecaContext.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
