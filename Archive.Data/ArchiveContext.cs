using Microsoft.EntityFrameworkCore;

using Archive.Data.Entities;

namespace Archive.Data
{
    public class ArchiveContext : DbContext
    {
        public ArchiveContext(DbContextOptions<ArchiveContext> options)
            : base(options)
        {
#if DEBUG
            Database.EnsureDeleted();
#endif
            Database.EnsureCreated();
        }


        public DbSet<Document>? Documents { get; set; }

        public DbSet<ReferenceDocument>? RefDocuments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArchiveContext).Assembly);
        }
    }
}
