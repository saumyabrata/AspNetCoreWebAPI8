using AspNetCoreWebAPI8.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebAPI8.Repository
{
	public class ProductRepository : IProductRepository
	{
		public void Add(Product product)
		{
			using (var context = new ProdContext())
			{
				var addedEntry = context.Entry<Product>(product);
				addedEntry.State = Microsoft.EntityFrameworkCore.EntityState.Added;
				context.SaveChanges();
			}
		}

		public void Delete(Product product)
		{
			using (var context = new ProdContext())
			{
				var addedEntry = context.Entry<Product>(product);
				addedEntry.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
				context.SaveChanges();
			}
		}

		public Product ProductGetById(int Product_Id)
		{
			using (var context = new ProdContext())
			{
				return context.Set<Product>().SingleOrDefault(x => x.Id == Product_Id);
			}
		}

		public void Update(Product product)
		{
			using (var context = new ProdContext())
			{
				var addedEntry = context.Entry<Product>(product);
				addedEntry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
				context.SaveChanges();
			}
		}
	}
}
