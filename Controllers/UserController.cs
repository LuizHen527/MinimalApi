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
    public class UserController : ControllerBase
    {
        private readonly IMongoCollection<User> _user;

        public UserController(MongoDbService mongoDbService)
        {
            _user = mongoDbService.GetDatabase.GetCollection<User>("user");
        }

        [HttpPost]
        public async Task<ActionResult> NewItem(User user)
        {
            try
            {
                await _user.InsertOneAsync(user);

                return StatusCode(201, user);
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            try
            {
                var users = await _user.Find(FilterDefinition<User>.Empty).ToListAsync();
                return Ok(users);
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<User>> GetById(string id)
        {
            try
            {
                var user = await _user.Find(x => x.Id == id).FirstOrDefaultAsync();

                if(user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, User updatedUser)
        {
            try
            {
                var user = await _user.Find(x => x.Id == id).FirstOrDefaultAsync();

                updatedUser.Id = user.Id;

                await _user.ReplaceOneAsync(x => x.Id == id, updatedUser);

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
            try
            {
                var user = _user.Find(x => x.Id == id).FirstOrDefault();

                if(user == null)
                {
                    return NotFound();
                }

                await _user.DeleteOneAsync(x => x.Id == id);

                return NoContent();
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

    }
}
