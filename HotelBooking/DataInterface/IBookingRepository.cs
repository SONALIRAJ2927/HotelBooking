using HotelBooking.Model;

namespace HotelBooking.DataInterface
{
    public interface IBookingRepository
    {
        Task<IEnumerable<BookingsModel>> GetAllBooking();
        Task<BookingsModel> GetBookingById(int Id);
        Task<BookingsModel?> AddBooking(BookingsModel booking);
        Task<string> UpdateBooking(BookingsModel booking);
        Task DeleteBooking(int id);
        Task<IEnumerable<BookingsModel>> GetAllBookings(int userId);
        Task<IEnumerable<AvailableRoomModel>> GetAvailableRooms(int hotelId, string roomType, DateTime startDate, DateTime endDate);
    }
}




