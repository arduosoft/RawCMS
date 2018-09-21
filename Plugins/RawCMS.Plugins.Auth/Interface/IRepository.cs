using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace RawCMS.Plugins.Auth.Interfaces
{
    /// <summary>
    /// Basic interface with a few methods for adding, deleting, and querying data.
    /// </summary>
    public interface IRepository
    {
        IMongoDatabase GetDatabase();
        System.Linq.IQueryable<T> All<T>() where T : class, new();
        IQueryable<T> Where<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : class, new();
        T Single<T>(Expression<Func<T, bool>> expression) where T : class, new();
        void Delete<T>(Expression<Func<T, bool>> expression) where T : class, new();
        void Add<T>(T item) where T : class, new();
        void Add<T>(IEnumerable<T> items) where T : class, new();
        bool CollectionExists<T>() where T : class, new();
    }
}
