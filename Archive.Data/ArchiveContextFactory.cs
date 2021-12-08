using System.IO;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Archive.Data
{
    public class ArchiveContextFactory : IDbContextFactory<ArchiveContext>
    {
        public ArchiveContext CreateDbContext()
        {
            Stream jsonStream = GetJsonStream();

            ConfigurationBuilder builder = new();
            IConfigurationRoot root = builder.AddJsonStream(jsonStream).Build();

            DbContextOptionsBuilder<ArchiveContext> optionsBuilder = new();
            optionsBuilder.UseSqlServer(root.GetConnectionString("MSSQL")).EnableSensitiveDataLogging();

            return new ArchiveContext(optionsBuilder.Options);
        }

        private Stream GetJsonStream()
        {
            Stream? jsonResourceStream = this.GetType().Assembly
                .GetManifestResourceStream("Archive.Data.DataSettings.json");

            if (jsonResourceStream is null)
                throw new FileNotFoundException("Json file isn't found!");

            return jsonResourceStream;
        }
    }
}
