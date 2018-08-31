namespace Identity.Domain.Repository
{
    using System;

    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        int Complete();
    }
}
