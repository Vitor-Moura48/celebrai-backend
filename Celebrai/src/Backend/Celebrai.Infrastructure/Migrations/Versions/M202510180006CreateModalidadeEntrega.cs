using FluentMigrator;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202510180006, "Criação da tabela ModalidadeEntrega")]
[Profile("Celebrai")]
public class M202510180006CreateModalidadeEntrega : Migration
{
    public override void Up()
    {
        Create.Table("ModalidadeEntrega")
            .WithColumn("IdModalidadeEntrega").AsInt32().PrimaryKey().Identity()
            .WithColumn("Metodo").AsFixedLengthAnsiString(1).NotNullable();
    }

    public override void Down()
    {
        Delete.Table("ModalidadeEntrega");
    }
}