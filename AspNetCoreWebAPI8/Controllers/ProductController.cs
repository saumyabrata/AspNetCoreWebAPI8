using AspNetCoreWebAPI8.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspNetCoreWebAPI8.Service;

namespace AspNetCoreWebAPI8.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductService _productService;
		public ProductController(IProductService productService)
        {
			_productService = productService;
		}

		// GET: /<controller>/  
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet("[action]")]
		public IActionResult ProductAdd()
		{
			var product = new Product
			{
				//Product_Id=1,
				Title = "Apple",
				Rate = (decimal?)11.56,
				TaxCode = 'A',
				CreatedBy = 1

			};
			_productService.Add(product);
			return Ok();
		}

		[HttpPost("[action]")]
		public IActionResult ProductDelete(int id)
		{
			var product = _productService.Get(id);
			_productService.Delete(product);
			return Ok();
		}
		[HttpPost("[action]")]
		public IActionResult ProductUpdate(Product product)
		{
			product.LastModified = DateTime.Now;
			_productService.Update(product);
			return Ok();
		}


	}
}
