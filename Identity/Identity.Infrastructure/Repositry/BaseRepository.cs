namespace Identity.Infrastructure.Repositry
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Repository;
    using Microsoft.EntityFrameworkCore;
    using Utils;
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        protected BaseRepository(DbContext context)
        {
            Context = context;
        }

        public TEntity Get(int id)
        {

            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> GetAll(int pageNumber, int pageSize,
                                           string sortBy, string sortDirection)
        {

            var skip = (pageNumber - 1) * pageSize;
            return Context.Set<TEntity>()
                .OrderBy(sortBy, sortDirection)
                .Skip(skip)
                .Take(pageSize);
        }

        public int CountGetAll()
        {
            return Context.Set<TEntity>().Count();
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }
    }
}
