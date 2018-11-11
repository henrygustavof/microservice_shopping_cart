namespace Product.Infrastructure.Persistence.Dapper.Commands
{
    public struct ProductSqlCommands
    {
        public static string GetAll = @"select 
                           p.product_id as 'id',
                           p.name,
                           p.picture_url as 'pictureUrl',
                           p.description,
                           p.unit,
                           p.price as 'amount',
                           p.currency,
                           p.category_id

                           from product p
                            join (SELECT p2.product_id FROM product p2 ORDER BY @SortBy @SortDirection LIMIT @Offset, @PageSize)
                             AS p3 ON p.product_id = p3.product_id ORDER BY @SortBy @SortDirection;";


        public static string CountAll = @"select  count(p.product_id) from product p;";

    }
}
