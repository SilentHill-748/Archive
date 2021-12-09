using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Archive.Data.Entities.Configurations
{
    internal class ReferenceDocumentConfiguration : IEntityTypeConfiguration<ReferenceDocument>
    {
        public void Configure(EntityTypeBuilder<ReferenceDocument> builder)
        {
            builder.HasKey(d => d.Number);
            builder.Property(d => d.Number).HasColumnName("RefNumber");
            builder.Property(d => d.Number).ValueGeneratedNever();
            builder.Property(d => d.Title).IsRequired();
            builder.Property(d => d.Text).IsRequired();
            builder.Property(d => d.Path).IsRequired();
        }
    }
}
