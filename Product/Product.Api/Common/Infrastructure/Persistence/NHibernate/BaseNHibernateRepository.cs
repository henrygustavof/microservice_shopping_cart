using Product.Api.Product.Domain.Repository;

namespace Product.Api.Common.Infrastructure.Persistence.NHibernate
{
    using System.Collections.Generic;
    using System.Linq;


    public abstract class BaseNHibernateRepository<T> : IRepository<T> where T : class
    {
        protected readonly UnitOfWorkNHibernate _unitOfWork;
        protected BaseNHibernateRepository(UnitOfWorkNHibernate unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(T entity)
        {
            SaveOrUpdate(entity);
        }

        public IReadOnlyList<T> GetAll()
        {
            bool status = _unitOfWork.BeginTransaction();
            IReadOnlyList<T> entities = _unitOfWork.GetSession().Query<T>().ToList();
            _unitOfWork.Commit(status);
            return entities;
        }

        public T Get(long id)
        {
            bool status = _unitOfWork.BeginTransaction();
            T entity = _unitOfWork.GetSession().Get<T>(id);
            _unitOfWork.Commit(status);
            return entity;
        }

        public void Update(T entity)
        {
            SaveOrUpdate(entity);
        }

        public void Delete(T entity)
        {
            bool status = _unitOfWork.BeginTransaction();
            _unitOfWork.GetSession().Delete(entity);
            _unitOfWork.Commit(status);
        }

        private void SaveOrUpdate(T entity)
        {
            bool status = _unitOfWork.BeginTransaction();
            _unitOfWork.GetSession().SaveOrUpdate(entity);
            _unitOfWork.Commit(status);
        }
    }
}
