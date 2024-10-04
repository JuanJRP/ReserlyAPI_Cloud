using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using ReserlyAPI.DB;
using ReserlyAPI.Models;

namespace ReserlyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMongoCollection<User> _users;

        public UserController(MongoDbService mongoDbService)
        {
            _users = mongoDbService?.Database?.GetCollection<User>("user")
                      ?? throw new ArgumentNullException(nameof(mongoDbService), "Database or collection is null");
        }

        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _users.Find(FilterDefinition<User>.Empty).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User?>> GetById(Int32 id)
        {
            var filter = Builders<User>.Filter.Eq("_id", id);
            var user = await _users.Find(filter).FirstOrDefaultAsync();
            return user is not null ? Ok(user) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Post(User user)
        {
            await _users.InsertOneAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPut]
        public async Task<ActionResult> Put(User user)
        {
            var filter = Builders<User>.Filter.Eq(x => x.Id, user.Id);
            var result = await _users.ReplaceOneAsync(filter, user);

            return result.ModifiedCount > 0 ? Ok() : NotFound("User not found");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Int32 id)
        {
            var filter = Builders<User>.Filter.Eq("_id",id);
            var result = await _users.DeleteOneAsync(filter);

            return result.DeletedCount > 0 ? Ok() : NotFound();
        }
    }
}
