using NHibernate;
using Product.Api.Common.Application;
using Product.Api.Product.Domain.Repository;

namespace Product.Api.Common.Infrastructure.Persistence.NHibernate
{
    using System.Collections.Generic;
    using System.Linq;


    public abstract class BaseNHibernateRepository<T> : IRepository<T> where T : class
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected BaseNHibernateRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(T entity)
        {
            SaveOrUpdate(entity);
        }

        public IReadOnlyList<T> GetAll()
        {
            IReadOnlyList<T> entities = _unitOfWork.GetSession().Query<T>().ToList();
            return entities;
        }

        public T Get(long id)
        {
            T entity = _unitOfWork.GetSession().Get<T>(id);
            return entity;
        }

        public void Update(T entity)
        {
            SaveOrUpdate(entity);
        }

        public void Delete(T entity)
        {
            _unitOfWork.GetSession().Delete(entity);
        }

        private void SaveOrUpdate(T entity)
        {
            _unitOfWork.GetSession().SaveOrUpdate(entity);
        }
    }
}
