namespace Product.Api.Common.Infrastructure.Persistence
{
 
    public interface IUnitOfWork
    {
        bool BeginTransaction();
        void Commit(bool commit);
        void Rollback(bool rollback);
    }
}
