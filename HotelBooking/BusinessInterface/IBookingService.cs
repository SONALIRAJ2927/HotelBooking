using HotelBooking.Model;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.BusinessInterface
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingsModel>> GetAllBooking();
        Task<BookingsModel> GetBookingById(int Id); //direct get data from model basis of id
        Task<BookingsModel> AddBooking(BookingsModel booking); // inserting the data in table

        Task<string> UpdateBooking(BookingsModel booking); // want to return message string
        Task DeleteBooking(int Id); //del Hotel basis of id
        Task<IEnumerable<BookingsModel>> GetAllBookings(int userId);
        Task<IEnumerable<AvailableRoomModel>> GetAvailableRooms(int hotelId, string roomType, DateTime startDate, DateTime endDate);
    }
}
