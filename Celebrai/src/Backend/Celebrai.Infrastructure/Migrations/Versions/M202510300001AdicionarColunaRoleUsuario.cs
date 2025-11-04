using FluentMigrator;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202510300001, "Adição da coluna Role na tabela Usuario")]
[Profile("Celebrai")]
public class M202510300001AdicionarColunaRoleUsuario : Migration
{
    public override void Up()
    {
        Alter.Table("Usuario")
            .AddColumn("Role")
            .AsString(30)
            .NotNullable()
            .WithDefaultValue("Cliente");
    }

    public override void Down()
    {
        if (Schema.Table("Usuario").Column("Role").Exists())
        {
            Delete.Column("Role").FromTable("Usuario");
        }
    }
}
