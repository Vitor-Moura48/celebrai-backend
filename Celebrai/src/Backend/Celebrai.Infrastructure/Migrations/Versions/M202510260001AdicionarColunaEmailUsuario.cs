using FluentMigrator;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202510260001, "Adição da coluna Email na tabela Usuario")]
[Profile("Celebrai")]
public class M202510260001AdicionarColunaEmailUsuario : Migration
{
    public override void Up()
    {
        Alter.Table("Usuario")
            .AddColumn("Email")
            .AsString(255)
            .NotNullable();
    }

    public override void Down()
    {
        Delete.Column("Email")
            .FromTable("Usuario");
    }
}
