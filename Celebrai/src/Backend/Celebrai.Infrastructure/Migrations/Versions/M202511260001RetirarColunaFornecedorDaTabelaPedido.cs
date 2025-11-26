using FluentMigrator;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202511260001, "Retirar Coluna Fornecedor da Tabela Pedido")]
[Profile("Celebrai")]
public class M202511260001RetirarColunaFornecedorDaTabelaPedido : ForwardOnlyMigration
{
    public override void Up()
    {
        Delete.ForeignKey("FK_Pedido_Fornecedor").OnTable("Pedido");

        Delete.Column("IdFornecedor").FromTable("Pedido");
    }
}
