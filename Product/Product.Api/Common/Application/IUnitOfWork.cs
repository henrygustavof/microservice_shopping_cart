namespace Product.Api.Common.Application
{
    using System.Data;

    public interface IUnitOfWork
    {
        bool BeginTransaction(IsolationLevel isolationLevel);
        void Commit(bool commit);
        void Rollback(bool rollback);
    }
}
