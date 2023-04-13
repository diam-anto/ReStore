using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class ProductsController : BaseApiController
    {
        // that we want to do in order to use depedency injection, we create a private field inside this class
        // and assign that private fields to the context that we 're adding in our constructor (ctor) here.
        private readonly StoreContext _context;
        public ProductsController(StoreContext context)
        {
                _context = context;
            
        } 

        [HttpGet]
        // we return an action result here (200 response type) and include our products
        // asynchronous we return a Task
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            // without async
            // var products = context.Products.ToList();

            // return Ok(products);

            //async 
            // we get same kind of response
            return await _context.Products.ToListAsync();
        }

        [HttpGet("{id}")] // api/products/3
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null) return NotFound();

            return product;
        }
    }
}