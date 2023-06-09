using Lakeshore.DataInterface.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lakeshore.DataInterface.RepositoryFactory
{
    public interface IRepositoryFactory<TContext>
    {
        IRepository<T> GetRepository<T>() where T : class;
        
    }
}
