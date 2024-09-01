using HotelBooking.BusinessInterface;
using HotelBooking.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HotelBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _Roomservice;
        public RoomController(IRoomService _roomservice)
        {
            _Roomservice = _roomservice;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomModel>>> GetAllRoom()
        {
            var user = await _Roomservice.GetAllRoom(); //we are storing data in variable
            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoomModel>> GetRoomById(int id)
        {
            var room = await _Roomservice.GetRoomById(id);
            if (room == null)
            {
                //return NotFound();
                throw new Exception("abc");
            }
            return Ok(room);
        }

        [HttpPost]
        public async Task<ActionResult<RoomModel>> AddRoom(RoomModel room)
        {
            var newroom = await _Roomservice.AddRoom(room);
            if (newroom == null)
            {
                return BadRequest("Failed to Add Room .");
            }

            return Ok(newroom);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRoom(int id, RoomModel room)
        {
            if (id != room.Id)
            {
                return BadRequest();
            }

            var updateResult = await _Roomservice.UpdateRoom(room);

            if (updateResult == null)
            {
                return NotFound();
            }

            return Ok(updateResult);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRoom(int id)
        {
            await _Roomservice.DeleteRoom(id);
            return NoContent();
        }

        [HttpGet("RoomTypes/{hotelId}")]
        public async Task<ActionResult<IEnumerable<string>>> GetRoomTypesByHotelId(int hotelId)
        {
            var roomTypes = await _Roomservice.GetRoomTypesByHotelId(hotelId);

            if (roomTypes == null || !roomTypes.Any())
            {
                return NotFound($"No room types found for hotel with ID {hotelId}.");
            }

            return Ok(roomTypes);
        }
    }
}
