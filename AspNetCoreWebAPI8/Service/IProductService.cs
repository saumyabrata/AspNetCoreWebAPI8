using AspNetCoreWebAPI8.Models;
namespace AspNetCoreWebAPI8.Service
{
	public interface IProductService
	{
		void Add(Product product);
		void Update(Product product);
		void Delete(Product product);
		Product Get(int Product_Id);
	}
}
