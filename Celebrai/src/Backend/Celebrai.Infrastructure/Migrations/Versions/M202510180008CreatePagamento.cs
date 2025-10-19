using FluentMigrator;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202510180008, "Criação da tabela Pagamento")]
[Profile("Celebrai")]
public class M202510180008CreatePagamento : Migration
{
    public override void Up()
    {
        Create.Table("Pagamento")
            .WithColumn("IdPagamento").AsInt32().PrimaryKey().Identity()
            .WithColumn("Valor").AsCurrency().NotNullable()
            .WithColumn("Status").AsFixedLengthAnsiString(1).NotNullable()
            .WithColumn("DataHora").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
            .WithColumn("Metodo").AsFixedLengthAnsiString(1).NotNullable();

        Execute.Sql("ALTER TABLE Pagamento ADD CONSTRAINT CK_Pagamento_Valor CHECK (Valor > 0)");
    }

    public override void Down()
    {
        Execute.Sql("ALTER TABLE Pagamento DROP CONSTRAINT CK_Pagamento_Valor");
        Delete.Table("Pagamento");
    }
}