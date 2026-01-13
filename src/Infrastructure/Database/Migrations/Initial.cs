using FluentMigrator;

namespace Infrastructure.Database.Migrations;

[Migration(001)]
public class Initial : Migration
{
    public override void Up()
    {
        Execute.Sql(@"CREATE TYPE role AS ENUM ('passenger', 'driver', 'admin');");

        Create.Table("users")
            .WithColumn("user_name").AsString().PrimaryKey()
            .WithColumn("user_password").AsString().NotNullable()
            .WithColumn("user_role").AsCustom("role").NotNullable();
    }

    public override void Down()
    {
        Delete.Table("users");
        Execute.Sql("DROP TYPE IF EXISTS role;");
    }
}