using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celebrai.Infrastructure.DataAccess.MapEntities;
public class BaseMap<T> : IEntityTypeConfiguration<T> where T : class
{
    private readonly string _tableName;
    public BaseMap(string tableName)
    {
        _tableName = tableName;
    }
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        if (!string.IsNullOrWhiteSpace(_tableName))
            builder.ToTable(_tableName);
    }
}
