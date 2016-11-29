using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bookstore.Data.Application
{
    
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);
        bool Commit();
        void Rollback();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookStoreContext context;
        private bool disposed;
        private Dictionary<string, object> repositories;
        private ObjectContext _objectContext;
        private DbTransaction _transaction;
        private int _transactionCalls;
       // private readonly IDataContextAsync _dataContext;

        public UnitOfWork(BookStoreContext context)
        {
            this.context = context;
        }

        public UnitOfWork()
        {
            context = new BookStoreContext();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public Repository<T> Repository<T>() where T : BaseEntity
        {
            if (repositories == null)
            {
                repositories = new Dictionary<string, object>();
            }

            var type = typeof(T).Name;

            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), context);
                repositories.Add(type, repositoryInstance);
            }
            return (Repository<T>)repositories[type];
        }


        #region Unit of Work Transactions

        
        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            _objectContext = ((IObjectContextAdapter)context).ObjectContext;
            if (_objectContext.Connection.State != ConnectionState.Open)
                _objectContext.Connection.Open();

            //Only use one transaction at a time
            if (_transaction == null)
                _transaction = _objectContext.Connection.BeginTransaction(isolationLevel);

            //Track number of "transactions" attemtped to be started so we know when to commit
            _transactionCalls++;
        }

        public bool Commit()
        {
            _transactionCalls--;

            //Only commit on call from service that initialized the transaction (ignore nested ones)
            if (_transactionCalls != 0) return true;

            _transaction.Commit();
            _transaction = null;

            return true;
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        #endregion

    }
}
