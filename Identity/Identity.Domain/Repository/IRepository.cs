namespace Identity.Domain.Repository
{
    using System.Collections.Generic;

    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetAll(int pageNumber, int pageSize,
            string sortBy, string sortDirection);

        int CountGetAll();

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);
    }
}
