using GHQ.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GHQ.Data.EntityConfiguration;

public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntity
{
    private readonly string _schemaName;
    private readonly string _tableName;

    protected BaseEntityConfiguration(string tableName, string schemaName = "dbo")
    {
        _schemaName = schemaName;
        _tableName = tableName;
    }

    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Version)
        .IsRowVersion();

        builder.ToTable(_tableName, _schemaName);
        ConfigureEntity(builder);
    }

    protected abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
}
