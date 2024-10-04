using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ReserlyAPI.DB;
using ReserlyAPI.Models;

namespace ReserlyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IMongoCollection<Reservation> _reservation;

        public ReservationController(MongoDbService mongoDbReservation)
        {
            _reservation = mongoDbReservation?.Database?.GetCollection<Reservation>("Reservation")
                        ?? throw new ArgumentNullException(nameof(mongoDbReservation), "Database or collection is null");
        }

        [HttpGet]
        public async Task<IEnumerable<Reservation>> Get()
        {
            return await _reservation.Find(FilterDefinition<Reservation>.Empty).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation?>> GetById(Int32 id)
        {
            var filter = Builders<Reservation>.Filter.Eq("_id", id);
            var Reservation = _reservation.Find(filter).FirstOrDefault();
            return Reservation is not null ? Ok(Reservation) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Reservation Reservation)
        {
            await _reservation.InsertOneAsync(Reservation);
            return CreatedAtAction(nameof(GetById), new { id = Reservation.Id }, Reservation);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Reservation Reservation)
        {
            var filter = Builders<Reservation>.Filter.Eq(x => x.Id, Reservation.Id);
            await _reservation.ReplaceOneAsync(filter, Reservation);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Int32 id)
        {
            var filter = Builders<Reservation>.Filter.Eq("_id", id);
            await _reservation.DeleteOneAsync(filter);
            return Ok();
        }
    }
}
