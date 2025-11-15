using FluentMigrator;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202511150001, "Adição de dados na tabela Categoria e SubCategoria")]
[Profile("Celebrai")]
public class M202511150001SeedCategorias : Migration
{
    public override void Up()
    {
        // Categorias principais
        Insert.IntoTable("Categoria").Row(new { IdCategoria = 1, Nome = "Kits" });
        Insert.IntoTable("Categoria").Row(new { IdCategoria = 2, Nome = "Orçamentos" });

        // ===== Categoria Kits =====
        // Casamento
        Insert.IntoTable("SubCategoria").Row(new { IdSubCategoria = 1, IdCategoria = 1, Nome = "Casamento" });
        Insert.IntoTable("SubCategoria").Row(new { IdSubCategoria = 2, IdCategoria = 1, Nome = "Recepção" });
        Insert.IntoTable("SubCategoria").Row(new { IdSubCategoria = 3, IdCategoria = 1, Nome = "Cerimônia" });

        // Aniversário
        Insert.IntoTable("SubCategoria").Row(new { IdSubCategoria = 4, IdCategoria = 1, Nome = "Aniversário" });
        Insert.IntoTable("SubCategoria").Row(new { IdSubCategoria = 5, IdCategoria = 1, Nome = "Infantil" });
        Insert.IntoTable("SubCategoria").Row(new { IdSubCategoria = 6, IdCategoria = 1, Nome = "Adulto" });

        // Corporativos
        Insert.IntoTable("SubCategoria").Row(new { IdSubCategoria = 7, IdCategoria = 1, Nome = "Corporativos" });

        // ===== Categoria Orçamentos =====
        Insert.IntoTable("SubCategoria").Row(new { IdSubCategoria = 8, IdCategoria = 2, Nome = "Casamento" });
        Insert.IntoTable("SubCategoria").Row(new { IdSubCategoria = 9, IdCategoria = 2, Nome = "Aniversário" });
        Insert.IntoTable("SubCategoria").Row(new { IdSubCategoria = 10, IdCategoria = 2, Nome = "Corporativos" });
    }

    public override void Down()
    {
        Delete.FromTable("SubCategoria").AllRows();
        Delete.FromTable("Categoria").AllRows();
    }
}
