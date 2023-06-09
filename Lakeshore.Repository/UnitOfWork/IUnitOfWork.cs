using Lakeshore.DataInterface.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lakeshore.DataInterface.UnitOfWork
{
    public interface IUnitOfWork<TContext> where TContext : DbContext, IDisposable
    {
 
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        int Commit(bool autoHistory = false);
        Task<int> CommitAsync(bool autoHistory = false);

        TContext Context { get; }
    }

    //public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    //{
    //    TContext Context { get; }
    //}
}
