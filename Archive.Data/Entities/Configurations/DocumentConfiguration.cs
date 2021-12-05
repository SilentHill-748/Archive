using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Archive.Data.Entities.Configurations
{
    internal class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.HasKey(d => d.Number);
            builder.Property(d => d.Number).ValueGeneratedNever();
            builder.Property(d => d.Title).IsRequired();
            builder.Property(d => d.Path).IsRequired();
            builder.Property(d => d.KeyWords).IsRequired();
            builder.Property(d => d.Text).IsRequired();
        }
    }
}
