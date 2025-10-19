using FluentMigrator;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202510180001, "Criação da tabela Usuario")]
[Profile("Celebrai")]
public class M202510180001CriarUsuario : Migration
{
    public override void Up()
    {
        Create.Table("Usuario")
            .WithColumn("IdUsuario").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("Nome").AsAnsiString(50).NotNullable()
            .WithColumn("Celular").AsAnsiString(20).Nullable()
            .WithColumn("CpfUsuario").AsFixedLengthAnsiString(11).NotNullable().Unique()
            .WithColumn("IdExterno").AsString(30).NotNullable() 
            .WithColumn("DataNascimento").AsDate().NotNullable()
            .WithColumn("UrlIcon").AsString(150).Nullable() 
            .WithColumn("DataCriacao").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
            .WithColumn("DataAtualizacao").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
            .WithColumn("Ativo").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("Lograduro").AsString(60).Nullable() 
            .WithColumn("Numero").AsAnsiString(15).Nullable()
            .WithColumn("CEP").AsAnsiString(9).Nullable();
    }

    public override void Down()
    {
        Delete.Table("Usuario");
    }
}
