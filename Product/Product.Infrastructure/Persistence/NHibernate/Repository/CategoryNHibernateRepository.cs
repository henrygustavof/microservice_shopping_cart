namespace Product.Infrastructure.Persistence.NHibernate.Repository
{
    using Product.Infrastructure.Persistence.NHibernate;
    using Product.Domain.Repository;
    using Domain.Entity;

    public class CategoryNHibernateRepository : BaseNHibernateRepository<Category>, ICategoryRepository
    {
        public CategoryNHibernateRepository(UnitOfWorkNHibernate unitOfWork) : base(unitOfWork)
        {
        }
    }
}
