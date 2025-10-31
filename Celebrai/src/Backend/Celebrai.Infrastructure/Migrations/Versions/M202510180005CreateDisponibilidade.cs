using FluentMigrator;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202510180005, "Criação da tabela Disponibilidade")]
[Profile("Celebrai")]
public class M202510180005CreateDisponibilidade : Migration
{
    public override void Up()
    {
        Create.Table("Disponibilidade")
            .WithColumn("IdDisponibilidade").AsInt32().PrimaryKey().Identity()
            .WithColumn("DiaSemana").AsInt32().NotNullable()
            .WithColumn("HoraInicio").AsTime().NotNullable()
            .WithColumn("HoraFim").AsTime().NotNullable()
            .WithColumn("IdFornecedor").AsGuid().NotNullable();

        Create.ForeignKey("FK_Disponibilidade_Fornecedor")
            .FromTable("Disponibilidade").ForeignColumn("IdFornecedor")
            .ToTable("Fornecedor").PrimaryColumn("IdFornecedor");
    }

    public override void Down()
    {
        Delete.ForeignKey("FK_Disponibilidade_Fornecedor").OnTable("Disponibilidade");
        Delete.Table("Disponibilidade");
    }
}