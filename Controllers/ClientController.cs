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
    public class ClientController : ControllerBase
    {
        private readonly IMongoCollection<Client> _client;
        private readonly IMongoCollection<User> _user;

        public ClientController(MongoDbService mongoDbService)
        {
            _client = mongoDbService.GetDatabase.GetCollection<Client>("client");
            _user = mongoDbService.GetDatabase.GetCollection<User>("user");


        }

        [HttpPost]
        public async Task<ActionResult> NewItem(Client client)
        {
            try
            {
                await _client.InsertOneAsync(client);

                return StatusCode(201, client);
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Client>>> Get()
        {
            try
            {
                var clients = await _client.Find(FilterDefinition<Client>.Empty).ToListAsync();
                return Ok(clients);
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Client>> GetById(string id)
        {
            try
            {
                var client = await _client.Find(x => x.Id == id).FirstOrDefaultAsync();

                return Ok(client);
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult<Client>> Update(string id, Client clientUpdated)
        {
            try
            {
                var client = await _client.Find(x => x.Id == id).FirstOrDefaultAsync();

                if (client == null)
                {
                    return NotFound();
                }

                clientUpdated.Id = client.Id;

                await _client.ReplaceOneAsync(x => x.Id == id, clientUpdated);

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
                var client = _client.Find(x => x.Id == id).FirstOrDefaultAsync();

                if (client == null)
                {
                    return NoContent();
                }

                await _client.DeleteOneAsync(x => x.Id == id);

                return NoContent();
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }
    }
}
