using HotelBooking.BusinessInterface;
using HotelBooking.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _HotelService;
        public HotelController(IHotelService _hotelService)
        {
            _HotelService = _hotelService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelModel>>> GetAllHotel()
        {
            var user = await _HotelService.GetAllHotel(); //we are storing data in variable
            return Ok(user);
        }

        [HttpGet("{HotelId}")]
        public async Task<ActionResult<HotelModel>> GetHotelById(int HotelId)
        {
            var hotel = await _HotelService.GetHotelById(HotelId);
            if (hotel == null)
            {
                //return NotFound();
                throw new Exception("abc");
            }
            return Ok(hotel);
        }

        [HttpPost]
        public async Task<ActionResult<HotelModel>> AddHotel(HotelModel hotel)
        {
            var newhotel = await _HotelService.AddHotel(hotel);
            if (newhotel == null)
            {
                return BadRequest("Failed to Add Hotel .");
            }

            return Ok(newhotel);
        }


        [HttpPut("{HotelId}")]
        public async Task<ActionResult> UpdateHotel(int HotelId, HotelModel hotel)
        {
            if (HotelId != hotel.HotelId)
            {
                return BadRequest();
            }

            var UpdateHotel = await _HotelService.UpdateHotel(hotel);

            if (UpdateHotel == null)
            {
                return NotFound();
            }

            return Ok(UpdateHotel);
        }

        [HttpDelete("{HotelId}")]
        public async Task<ActionResult> DeleteHotel(int HotelId)
        {
            await _HotelService.DeleteHotel(HotelId);
            return NoContent();
        }

        [HttpGet("SearchHotel")]
        public async Task<ActionResult<IEnumerable<HotelModel>>> SearchHotel(string? name=null,string? address=null,string? city = null,string? state= null,string? country = null,int? rating= null,string? zipCode = null)
        {
            var hotels = await _HotelService.SearchHotel(name, address, city, state, country, rating, zipCode);

            if (hotels == null || !hotels.Any())
            {
                return NotFound("No hotels found with the provided criteria.");
            }

            return Ok(hotels);
        }
    }
}

