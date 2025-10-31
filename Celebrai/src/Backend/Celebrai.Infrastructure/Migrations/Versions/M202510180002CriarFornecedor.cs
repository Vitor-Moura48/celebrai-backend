using FluentMigrator;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202510180002, "Criação da tabela Fornecedor")]
[Profile("Celebrai")]
public class M202510180002CriarFornecedor : Migration
{
    public override void Up()
    {
        Create.Table("Fornecedor")
            .WithColumn("IdFornecedor").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("IdUsuario").AsGuid().NotNullable()
            .WithColumn("RaioAtuacao").AsInt32().Nullable()
            .WithColumn("AtendimentoPresencial").AsBoolean().NotNullable()
            .WithColumn("DataCriacao").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
            .WithColumn("DataAtualizacao").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
            .WithColumn("Ativo").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("Lograduro").AsString(60).NotNullable() 
            .WithColumn("Numero").AsAnsiString(15).NotNullable()
            .WithColumn("CEP").AsAnsiString(9).NotNullable();

        Create.ForeignKey("FK_Fornecedor_Usuario")
            .FromTable("Fornecedor").ForeignColumn("IdUsuario")
            .ToTable("Usuario").PrimaryColumn("IdUsuario");
    }

    public override void Down()
    {
        Delete.ForeignKey("FK_Fornecedor_Usuario").OnTable("Fornecedor");
        Delete.Table("Fornecedor");
    }
}
