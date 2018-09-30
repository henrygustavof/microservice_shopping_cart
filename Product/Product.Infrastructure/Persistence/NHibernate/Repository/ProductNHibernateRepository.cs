namespace Product.Infrastructure.Persistence.NHibernate.Repository
{
    using Product.Infrastructure.Persistence.NHibernate;
    using Product.Domain.Repository;
    using Domain.Entity;

    public class ProductNHibernateRepository : BaseNHibernateRepository<Product>, IProductRepository
    {
        public ProductNHibernateRepository(UnitOfWorkNHibernate unitOfWork) : base(unitOfWork)
        {
        }
    }
}
