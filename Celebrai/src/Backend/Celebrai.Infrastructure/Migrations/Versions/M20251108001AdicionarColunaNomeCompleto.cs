using FluentMigrator;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202511080001, "Adicionar coluna NomeCompleto na tabela PessoaFisica")]
[Profile("Celebrai")]
public class M20251108001AdicionarColunaNomeCompleto : ForwardOnlyMigration
{
    public override void Up()
    {
        Alter.Table("PessoaFisica")
            .AddColumn("NomeCompleto")
            .AsString(50)
            .WithDefaultValue(string.Empty);
    }
}
