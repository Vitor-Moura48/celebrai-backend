using FluentMigrator;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202511300001, "Criação da tabela associativa FornecedorPedido")]
[Profile("Celebrai")]
public class M202511300001AdicionarItensTabelaFornecedorPedido : Migration
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
        Delete.ForeignKey("FK_FornecedorPedido_Fornecedor").OnTable("FornecedorPedido");
        Delete.ForeignKey("FK_FornecedorPedido_Pedido").OnTable("FornecedorPedido");
        
        Delete.Table("FornecedorPedido");
    }
}