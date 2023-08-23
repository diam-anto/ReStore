using System.Text.Json;
using API.Data;
using API.Entities;
using API.Extensions;
using API.RequestHelpers;
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
        // with [FromQuery] , controller know where to look for parameters
        public async Task<ActionResult<PagedList<Product>>> GetProducts([FromQuery]ProductParams productParams)
        {
            // without async
            // var products = context.Products.ToList();

            // return Ok(products);

            //async 
            // we get same kind of response
            //return await _context.Products.ToListAsync();

            // this is a type of IQueryable<Product>
            // nothing has been executed here
            // Repository method
            var query = _context.Products
                .Sort(productParams.OrderBy)
                .Search(productParams.SearchTerms)
                .Filter(productParams.Brands, productParams.Types)
                .AsQueryable();

            var products = await PagedList<Product>.ToPagedList(query, productParams.PageNumber, productParams.PageSize);

            // these product parameters is we 're going to return them in our response headers
            // and get access to our pagination from our response headers
            //Response.Headers.Add("Pagination", JsonSerializer.Serialize(products.MetaData));

            Response.AddPaginationHeader(products.MetaData);

            // here it goes to database
            return products;
        }

        [HttpGet("{id}")] // api/products/3
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null) return NotFound();

            return product;
        }

        [HttpGet("filters")]
        public async Task<IActionResult> GetFilters()
        {
            var brands = await _context.Products.Select(c => c.Brand).Distinct().ToListAsync();
            var types = await _context.Products.Select(c => c.Type).Distinct().ToListAsync();

            // to create an anonymous object
            // this gives 2 lists
            return Ok(new {brands, types});
        }
    }
}