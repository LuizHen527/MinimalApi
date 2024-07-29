using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinimalAPIMongo.Domains;
using MinimalAPIMongo.Services;
using MongoDB.Driver;

namespace MinimalAPIMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductController : ControllerBase
    {
        private readonly IMongoCollection<Product> _product;

        /// <summary>
        /// Constructor que recebe como dependencia o obj da classe MongoDbService
        /// </summary>
        /// <param name="mongoDbService"></param>
        public ProductController(MongoDbService mongoDbService)
        {
            _product = mongoDbService.GetDatabase.GetCollection<Product>("product");
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            try
            {
                var products = await _product.Find(FilterDefinition<Product>.Empty).ToListAsync();
                return Ok(products);
            }
            catch (Exception e)
            {

                 return BadRequest(e.Message);
            }
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Product>> GetById(string id)
        {
            try
            {
                var product = await _product.Find(x => x.Id == id).FirstOrDefaultAsync();

                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> NewItem(Product product)
        {
            try
            {
                await _product.InsertOneAsync(product);

                return StatusCode(201, product);
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        [HttpPut("{id:length(24)}")]

        public async Task<IActionResult> Update(string id, Product updatedProduct)
        {
            try
            {
                var product = await _product.Find(x => x.Id == id).FirstOrDefaultAsync();

                updatedProduct.Id = product.Id;

                await _product.ReplaceOneAsync(x => x.Id == id, updatedProduct);

                return Ok();

            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var product = _product.Find(x => x.Id == id).FirstOrDefaultAsync();
            
            if(product == null)
            {
                return NotFound();
            }

            await _product.DeleteOneAsync(x => x.Id == id);

            return NoContent();
        }
    }
}


//Criar a classe user na pasta Domains
//id, name, email, password e atrivbutos adicionais

//Criar a classe Client na pasta Domains
//Id, UserId, Cpf, Phone, Address, atributos adicionais

