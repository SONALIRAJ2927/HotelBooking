using HotelBooking.BusinessInterface;
using HotelBooking.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _BookingService;
        public BookingsController(IBookingService _bookingService)
        {
            _BookingService = _bookingService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingsModel>>> GetAllBooking()
        {
            var bookings = await _BookingService.GetAllBooking(); // Correct variable naming
            return Ok(bookings);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<BookingsModel>> GetBookingById(int Id)
        {
            var booking = await _BookingService.GetBookingById(Id);
            if (booking == null)
            {
                return NotFound(); // Better error handling
            }
            return Ok(booking);
        }

        [HttpPost]
        public async Task<ActionResult<BookingsModel>> AddBooking(BookingsModel booking)
        {
            var newBooking = await _BookingService.AddBooking(booking);
            if (newBooking == null)
            {
                return BadRequest("Failed to add booking.");
            }

            return Ok(newBooking);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateBooking(int Id, BookingsModel booking)
        {
            if (Id != booking.Id)
            {
                return BadRequest("Booking ID mismatch.");
            }

            var updatedBooking = await _BookingService.UpdateBooking(booking);

            if (updatedBooking == null)
            {
                return NotFound("Booking not found.");
            }

            return Ok(updatedBooking); //storing data 
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteBooking(int Id)
        {
            await _BookingService.DeleteBooking(Id);
            return NoContent();
        }
        [HttpGet("GetAllBookings/{userId}")]
        public async Task<ActionResult<IEnumerable<BookingsModel>>> GetAllBookings(int userId)
        {
            var bookings = await _BookingService.GetAllBookings(userId);
            return Ok(bookings);
        }
        [HttpGet("GetAvailableRooms")]
        public async Task<IActionResult> GetAvailableRooms(int hotelId, string roomType, DateTime startDate, DateTime endDate)
        {
            // Validate parameters
            if (startDate >= endDate)  //startDate is greater than or equal to the endDate
            {
                return BadRequest("Start date must be earlier than end date.");
            }

            try
            {
                var availableRooms = await _BookingService.GetAvailableRooms(hotelId, roomType, startDate, endDate);

                if (availableRooms == null || !availableRooms.Any())  // 
                {
                    return NotFound("No available rooms found for the specified criteria.");
                }

                return Ok(availableRooms);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
    }
}


           
