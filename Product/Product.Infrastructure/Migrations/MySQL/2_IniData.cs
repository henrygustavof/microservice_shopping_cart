namespace Product.Infrastructure.Migrations.MySQL
{
    using FluentMigrator;

    [Migration(2)]
    public class IniData : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("2_IniData.sql");
        }

        public override void Down()
        {
        }
    }
}
