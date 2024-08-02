using AspNetCoreWebAPI8.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebAPI8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	//ProductController class inherits from ControllerBase.
	public class ProductController : ControllerBase
    {
		//inject the database context through the constructor of the controller
		private readonly ProdContext _dbcontext;
        public ProductController(ProdContext productcontext)
        {
            _dbcontext = productcontext;
        }

        // Get : api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            if (_dbcontext.Products == null)
            {
                return NotFound();
            }
            return await _dbcontext.Products.ToListAsync();
        }

		// Get : api/Products/2
		[HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            if (_dbcontext.Products is null)
            {
                return NotFound();
            }
            var product = await _dbcontext.Products.FindAsync(id);
            if (product is null)
            {
                return NotFound();
            }
            return product;
        }

		// Post : api/Products
		[HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product products)
        {
            _dbcontext.Products.Add(products);
            await _dbcontext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = products.Product_Id }, products);
        }

		// Put : api/Products/2
		[HttpPut]
        public async Task<ActionResult<Product>> PutProduct(int id, Product product)
        {
            if (id != product.Product_Id)
            {
                return BadRequest();
            }
            _dbcontext.Entry(product).State = EntityState.Modified;
            try
            {
                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id)) { return NotFound(); }
                else { throw; }
            }
            return NoContent();
        }

        private bool ProductExists(long id)
        {
            return (_dbcontext.Products?.Any(product => product.Product_Id == id)).GetValueOrDefault();
        }

		// Delete : api/Products/2
		[HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            if (_dbcontext.Products is null)
            {
                return NotFound();
            }
            var product= await _dbcontext.Products.FindAsync(id);
            if (product is null)
            {
                return NotFound();
            }
          _dbcontext.Products.Remove(product);
            await _dbcontext.SaveChangesAsync();
            return NoContent();
        }


    }
}
