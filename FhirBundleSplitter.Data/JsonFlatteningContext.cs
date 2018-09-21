using FhirBundleSplitter.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace FhirBundleSplitter.Data
{
	public class JsonFlatteningContext : DbContext
	{
		public JsonFlatteningContext(DbContextOptions<JsonFlatteningContext> options) : base (options)
		{
		}

		public DbSet<SourceData> SourceData { get; set; }
		public DbSet<FlattenedData> FlattenedData { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<SourceData>().ToTable("SourceData");
			modelBuilder.Entity<FlattenedData>().ToTable("FlattenedData");
		}
	}
}
