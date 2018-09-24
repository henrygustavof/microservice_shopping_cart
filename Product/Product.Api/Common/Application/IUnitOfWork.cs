using NHibernate;

namespace Product.Api.Common.Application
{
    using System.Data;

    public interface IUnitOfWork
    {
        bool BeginTransaction();
        ISession GetSession();
        void Commit(bool commit);
        void Rollback(bool rollback);
    }
}
