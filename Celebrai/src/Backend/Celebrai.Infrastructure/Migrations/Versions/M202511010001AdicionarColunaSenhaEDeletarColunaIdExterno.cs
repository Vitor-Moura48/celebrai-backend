using FluentMigrator;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202511010001, "Adição da coluna Senha e exclusão da coluna IdExterno na tabela Usuario")]
[Profile("Celebrai")]
public class M202511010001AdicionarColunaSenhaEDeletarColunaIdExterno : ForwardOnlyMigration
{
    public override void Up()
    {
        Alter.Table("Usuario")
            .AddColumn("Senha")
            .AsString(255)
            .NotNullable()
            .WithDefaultValue(string.Empty);

        Delete.Column("IdExterno").FromTable("Usuario");
    }
}
