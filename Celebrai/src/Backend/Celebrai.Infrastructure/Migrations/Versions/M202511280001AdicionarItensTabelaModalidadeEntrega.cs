using FluentMigrator;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202511280001, "Criação da tabela ModalidadeEntrega")]
[Profile("Celebrai")]
public class M202511280001AdicionarItensTabelaModalidadeEntrega : Migration
{
    public override void Up()
    {
        Insert.IntoTable("ModalidadeEntrega")
                .Row(new { metodo = "P" })
                .Row(new { metodo = "F" }); 
    }

    public override void Down()
    {
        Delete.FromTable("ModalidadeEntrega")
                .Row(new { metodo = "P" })
                .Row(new { metodo = "F" });
    }
}
