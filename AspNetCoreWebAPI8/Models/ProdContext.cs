using Microsoft.EntityFrameworkCore;
namespace AspNetCoreWebAPI8.Models
{
	public class ProdContext : DbContext
	{
		public ProdContext() { }
		public ProdContext(DbContextOptions<ProdContext> options)
			: base(options) { }

		public DbSet<Product> Products { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>(entity =>
			{

				entity.ToTable("Products");
				entity.HasKey(p => p.Product_Id).HasName("PK_Product");
				entity.Property(p => p.Product_Id)
				.HasColumnName("Product_Id")
				.HasColumnType("int").ValueGeneratedNever();
				entity.Property(p => p.Title)
				.HasColumnName("Title");
				entity.Property(p => p.Rate)
				.HasColumnName("Rate")
				.HasColumnType("decimal(8,2)");
				entity.Property(p => p.TaxCode)
				.HasColumnName("TaxCode")
				.HasColumnType("char(2)");
			});

		}
	}
}
