using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ReserlyAPI.DB;
using ReserlyAPI.Models;

namespace ReserlyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IMongoCollection<Service> _services;

        public ServiceController(MongoDbService mongoDbService)
        {
            _services = mongoDbService?.Database?.GetCollection<Service>("service")
                        ?? throw new ArgumentNullException(nameof(mongoDbService), "Database or collection is null");
        }

        [HttpGet]
        public async Task<IEnumerable<Service>> Get()
        {
            return await _services.Find(FilterDefinition<Service>.Empty).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Service?>> GetById(Int32 id)
        {
            var filter = Builders<Service>.Filter.Eq("_id", id);
            var service = _services.Find(filter).FirstOrDefault();
            return service is not null ? Ok(service) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Service service)
        {
            await _services.InsertOneAsync(service);
            return CreatedAtAction(nameof(GetById), new { id = service.Id }, service);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Service service)
        {
            var filter = Builders<Service>.Filter.Eq(x => x.Id, service.Id);
            await _services.ReplaceOneAsync(filter, service);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Int32 id)
        {
            var filter = Builders<Service>.Filter.Eq("_id", id);
            await _services.DeleteOneAsync(filter);
            return Ok();
        }
    }
}
