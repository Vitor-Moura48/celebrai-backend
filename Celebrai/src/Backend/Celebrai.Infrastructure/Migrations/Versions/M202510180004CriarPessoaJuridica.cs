using FluentMigrator;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202510180004, "Criação da tabela PessoaJuridica")]
[Profile("Celebrai")]
public class M202510180004CriarPessoaJuridica : Migration
{
    public override void Up()
    {
        Create.Table("PessoaJuridica")
            .WithColumn("Cnpj").AsFixedLengthAnsiString(14).PrimaryKey()
            .WithColumn("Razao_Social").AsAnsiString(50).NotNullable()
            .WithColumn("IdFornecedor").AsGuid().NotNullable();

        Create.ForeignKey("FK_PessoaJuridica_Fornecedor")
            .FromTable("PessoaJuridica").ForeignColumn("IdFornecedor")
            .ToTable("Fornecedor").PrimaryColumn("IdFornecedor");
    }

    public override void Down()
    {
        Delete.ForeignKey("FK_PessoaJuridica_Fornecedor").OnTable("PessoaJuridica");
        Delete.Table("PessoaJuridica");
    }
}
