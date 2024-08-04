using AspNetCoreWebAPI8.Models;
namespace AspNetCoreWebAPI8.Repository
{
	public interface IProductRepository
	{
		void Add(Product product);
		void Update(Product product);
		void Delete(Product product);
		Product ProductGetById(int Product_id);
	}
}
