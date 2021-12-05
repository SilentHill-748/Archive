using Microsoft.EntityFrameworkCore;

using Archive.Data.Entities;
using System.Linq;

namespace Archive.Data
{
    public class ArchiveContext : DbContext
    {
        public ArchiveContext(DbContextOptions<ArchiveContext> options)
            : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();

            Documents = (DbSet<Document>)Enumerable.Empty<Document>();
            RefDocuments = (DbSet<ReferenceDocument>)Enumerable.Empty<ReferenceDocument>();
        }


        public DbSet<Document> Documents { get; set; }

        public DbSet<ReferenceDocument> RefDocuments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArchiveContext).Assembly);
        }
    }
}
