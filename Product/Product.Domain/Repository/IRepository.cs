namespace Product.Domain.Repository
{
    using System.Collections.Generic;

    public interface IRepository<T> where T : class
    {
        IReadOnlyList<T> GetAll();

        T Get(long id);
        void Create(T entity);

        void Delete(T entity);
    }
}
