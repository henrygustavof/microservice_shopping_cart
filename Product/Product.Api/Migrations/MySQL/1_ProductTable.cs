using FluentMigrator;

namespace Product.Api.Migrations.MySQL
{
    [Migration(1)]
    public class ProductTable : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("1_ProductTable.sql");
        }

        public override void Down()
        {
        }
    }
}
