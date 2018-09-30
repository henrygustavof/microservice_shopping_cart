namespace Product.Infrastructure.Migrations.MySQL
{
    using FluentMigrator;

    [Migration(1)]
    public class IniTable : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("1_IniTable.sql");
        }

        public override void Down()
        {
        }
    }
}
