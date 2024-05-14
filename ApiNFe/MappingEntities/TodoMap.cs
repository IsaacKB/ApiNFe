using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace ApiNFe.MappingEntities
{
    public class TodoMap : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.HasKey(t => t.Id);

            builder
                .Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .Property(t => t.IsComplete)
                .HasDefaultValue(false);

            builder.ToTable("Todos");
        }
    }
}
