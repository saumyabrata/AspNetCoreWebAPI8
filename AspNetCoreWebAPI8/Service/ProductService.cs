using AspNetCoreWebAPI8.Models;
using AspNetCoreWebAPI8.Repository;
namespace AspNetCoreWebAPI8.Service
{
	public class ProductService : IProductService
	{
		private IProductRepository _productRepository;
		public ProductService(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}
		public void Add(Product product)
		{
			_productRepository.Add(product);
		}

		public void Delete(Product product)
		{
			_productRepository.Delete(product);
		}

		public Product Get(int Product_Id)
		{
			return _productRepository.ProductGetById(Product_Id);
		}

		public void Update(Product product)
		{
			_productRepository.Update(product);
		}
	}
}
