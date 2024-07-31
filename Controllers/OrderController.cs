using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using MinimalAPIMongo.Domains;
using MinimalAPIMongo.Services;
using MinimalAPIMongo.ViewModel;
using MongoDB.Driver;

namespace MinimalAPIMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class OrderController : ControllerBase
    {
        private readonly IMongoCollection<Order> _order;
        private readonly IMongoCollection<Product> _product;
        private readonly IMongoCollection<Client> _client;


        public OrderController(MongoDbService mongoDbService)
        {
            _order = mongoDbService.GetDatabase.GetCollection<Order>("order");
            _product = mongoDbService.GetDatabase.GetCollection<Product>("product");
            _client = mongoDbService.GetDatabase.GetCollection<Client>("client");

        }

        [HttpPost]
        public async Task<ActionResult> NewItem(OrderViewModel orderViewModel)
        {
            try
            {
                Order order = new Order();
                order.Id = orderViewModel.Id;
                order.Date = orderViewModel.Date;
                order.Status = orderViewModel.Status;
                order.ProductId = orderViewModel.ProductId;
                order.ClientId = orderViewModel.ClientId;

                var client = await _client.Find(x => x.Id == order.ClientId).FirstOrDefaultAsync();

                if (client == null)
                {
                    return NotFound();
                }

                order.Client = client;

                await _order!.InsertOneAsync(order);

                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> Get()
        {
            try
            {
                var orders = await _order.Find(FilterDefinition<Order>.Empty).ToListAsync();

                //Verifica se cada pedido possui id de produtos, depois verifica se esses ids estao na collection de produtos
                //depois coloca os produtos nos pedidos
                foreach (var order in orders)
                {
                    if(order.ProductId != null)
                    {
                        var filter = Builders<Product>.Filter.In(x => x.Id, order.ProductId);

                        order.Products = await _product.Find(filter).ToListAsync();
                    }
                }

                return Ok(orders);
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Order>> GetById(string id)
        {
            try
            {
                var order = await _order.Find(x => x.Id == id).FirstOrDefaultAsync();

                return Ok(order);
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult> Put(string id, OrderViewModel orderViewModel)
        {
            try
            {
                var order = _order.Find(x => x.Id == id).FirstOrDefault();

                order.Status = orderViewModel.Status;

                await _order.ReplaceOneAsync(x => x.Id == id, order);
                return Ok();

            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var order = await _order.Find(x => x.Id == id).FirstOrDefaultAsync();

                if(order == null)
                {
                    return NotFound();
                }

                await _order.DeleteOneAsync(x => x.Id == id);

                return NoContent();
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }
    }
}
