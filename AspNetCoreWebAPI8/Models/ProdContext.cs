using Microsoft.EntityFrameworkCore;
using AspNetCoreWebAPI8.Enums;
using AspNetCoreWebAPI8.Utils;
using System.Security.AccessControl;
namespace AspNetCoreWebAPI8.Models
{
	public class ProdContext : DbContext
	{
		//Constructor
		//public ProdContext() { }
		//public ProdContext(DbContextOptions<ProdContext> options)
		//	: base(options) { }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=LENOVO-IDEAPAD5;Database=Hospitalitydb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
			base.OnConfiguring(optionsBuilder);
		}
		public override int SaveChanges()
		{
			BeforeSaveChanges();
			return base.SaveChanges();
		}
		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
		{
			BeforeSaveChanges();
			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}

		private void BeforeSaveChanges()
		{
			ChangeTracker.DetectChanges();
			var auditEntries = new List<AuditEntry>();
			foreach (var entry in ChangeTracker.Entries())
			{
				if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
					continue;
				var auditEntry = new AuditEntry(entry);
				auditEntry.TableName = entry.Entity.GetType().Name;
				auditEntries.Add(auditEntry);
				foreach (var property in entry.Properties)
				{
					string propertyName = property.Metadata.Name;
					if (property.Metadata.IsPrimaryKey())
					{
						auditEntry.KeyValues[propertyName] = property.CurrentValue;
						continue;
					}
					switch (entry.State)
					{
						case EntityState.Added:
							auditEntry.AuditType = AuditType.Create;
							auditEntry.NewValues[propertyName] = property.CurrentValue;
							auditEntry.UserId = entry.Property("CreatedBy").CurrentValue != null ? entry.Property("CreatedBy").CurrentValue.ToString() : "Null";
							break;
						case EntityState.Deleted:
							auditEntry.AuditType = AuditType.Delete;
							auditEntry.OldValues[propertyName] = property.OriginalValue;
							auditEntry.UserId = entry.Property("LastModifiedBy").CurrentValue != null ? entry.Property("LastModifiedBy").CurrentValue.ToString() : "Null";
							break;
						case EntityState.Modified:
							if (property.IsModified)
							{
								auditEntry.ChangedColumns.Add(propertyName);
								auditEntry.AuditType = AuditType.Update;
								auditEntry.OldValues[propertyName] = property.OriginalValue;
								auditEntry.NewValues[propertyName] = property.CurrentValue;
								auditEntry.UserId = entry.Property("LastModifiedBy").CurrentValue != null ? entry.Property("LastModifiedBy").CurrentValue.ToString() : "Null";
							}
							break;
					}
				}
			}
			foreach (var auditEntry in auditEntries)
			{
				AuditLogs.Add(auditEntry.ToAudit());
			}
		}

		public DbSet<Product> Products { get; set; }
		public DbSet<Audit> AuditLogs { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>(entity =>
			{

				entity.ToTable("Products");
				entity.HasKey(p => p.Product_Id).HasName("PK_Product");
				entity.Property(p => p.Product_Id)
				.HasColumnName("Product_Id")
				.HasColumnType("int").ValueGeneratedOnAdd();
				entity.Property(p => p.Title)
				.HasColumnName("Title")
				.HasColumnType("nvarchar(max)");
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
