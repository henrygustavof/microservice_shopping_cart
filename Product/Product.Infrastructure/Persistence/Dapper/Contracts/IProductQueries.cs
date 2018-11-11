namespace Product.Infrastructure.Persistence.Dapper.Contracts
{
    using Product.Domain.Entity;
    using System.Collections.Generic;

    public interface IProductQueries
    {

       List<Product> GetAll(int page, int pageSize, string sortBy, string sortDirection);

        int CountAll();
    }
}
