using HotelBooking.DataInterface;
using HotelBooking.Implementation;
using HotelBooking.Model;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.BusinessInterface
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repository;

        public BookingService(IBookingRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<BookingsModel>> GetAllBooking()
        {
            return await _repository.GetAllBooking();
        }
        public async Task<BookingsModel> GetBookingById(int Id)
        {
            return await _repository.GetBookingById(Id);
        }
        public async Task<BookingsModel> AddBooking(BookingsModel booking)
        {
            return await _repository.AddBooking(booking);
        }
        public async Task<string> UpdateBooking(BookingsModel booking)
        {
            return await _repository.UpdateBooking(booking);
        }

        public async Task DeleteBooking(int Id)
        {
            await _repository.DeleteBooking(Id);
        }
        public async Task<IEnumerable<BookingsModel>> GetAllBookings(int userId)
        {
            return await _repository.GetAllBookings(userId);
        }
        public async Task<IEnumerable<AvailableRoomModel>> GetAvailableRooms(int hotelId, string roomType, DateTime startDate, DateTime endDate)
        {
            return await _repository.GetAvailableRooms(hotelId, roomType, startDate, endDate);
        }
    }
}

