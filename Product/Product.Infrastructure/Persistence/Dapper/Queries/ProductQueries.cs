namespace Product.Infrastructure.Persistence.Dapper.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using global::Dapper;
    using MySql.Data.MySqlClient;
    using Product.Domain.Entity;
    using Product.Domain.ValueObject;
    using Product.Infrastructure.Persistence.Dapper.Commands;

    public class ProductQueries : IProductQueries
    {
        public List<Product> GetAll(int page, int pageSize, string sortBy, string sortDirection)
        {
  
            string connectionString = Environment.GetEnvironmentVariable("MYSQL_CONECTION_STRING_LOCAL");
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    List<Product> products = connection
                        .Query<Product, Money, dynamic, Product>(ProductSqlCommands.GetAll,
                           (product, money,category) =>
                           {
                               product.Balance = money;
                               product.Category = new Category();
                               product.Category.Id = (int)category.category_id;
                               return product;
                           },
                           new
                           {
                               Offset = (page - 1) * pageSize,
                               PageSize = pageSize,
                               SortBy = sortBy,
                               SortDirection = sortDirection
                           },
                           splitOn: "amount,category_id"
                        ).ToList();
                    return products;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    return new List<Product>();
                }
                finally
                {
                    if (connection.State != System.Data.ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
            }
        }

        public int CountAll()
        {
            string connectionString = Environment.GetEnvironmentVariable("MYSQL_CONECTION_STRING_LOCAL");
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    int count = connection.Query<int>(ProductSqlCommands.CountAll).SingleOrDefault();
                    return count;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    return 0;
                }
                finally
                {
                    if (connection.State != System.Data.ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
            }
        }
    }
}
