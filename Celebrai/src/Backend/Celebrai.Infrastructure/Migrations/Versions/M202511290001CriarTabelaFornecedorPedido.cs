using FluentMigrator;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202511290001, "Criação da tabela FornecedorPedido")]
[Profile("Celebrai")]
public class M202511290001CriarTabelaFornecedorPedido : Migration
{
    public override void Up()
    {
        Create.Table("FornecedorPedido")
            .WithColumn("IdFornecedor").AsGuid().NotNullable().PrimaryKey()
            .WithColumn("IdPedido").AsInt32().NotNullable().PrimaryKey();

        Create.ForeignKey("FK_FornecedorPedido_Fornecedor")
            .FromTable("FornecedorPedido").ForeignColumn("IdFornecedor")
            .ToTable("Fornecedor").PrimaryColumn("IdFornecedor");

        Create.ForeignKey("FK_FornecedorPedido_Pedido")
            .FromTable("FornecedorPedido").ForeignColumn("IdPedido")
            .ToTable("Pedido").PrimaryColumn("IdPedido");
    }
    public override void Down()
    {
        Delete.Table("FornecedorPedido");
    }
}