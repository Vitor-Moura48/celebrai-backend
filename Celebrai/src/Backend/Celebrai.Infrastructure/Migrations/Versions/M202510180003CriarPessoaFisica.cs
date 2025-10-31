using FluentMigrator;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202510180003, "Criação da tabela PessoaFisica")]
[Profile("Celebrai")]
public class M202510180003CriarPessoaFisica : Migration
{
    public override void Up()
    {
        Create.Table("PessoaFisica")
            .WithColumn("Cpf").AsFixedLengthAnsiString(11).PrimaryKey()
            .WithColumn("IdFornecedor").AsGuid().NotNullable();

        Create.ForeignKey("FK_PessoaFisica_Fornecedor")
            .FromTable("PessoaFisica").ForeignColumn("IdFornecedor")
            .ToTable("Fornecedor").PrimaryColumn("IdFornecedor");
    }

    public override void Down()
    {
        Delete.ForeignKey("FK_PessoaFisica_Fornecedor").OnTable("PessoaFisica");
        Delete.Table("PessoaFisica");
    }
}
