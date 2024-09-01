using HotelBooking.DataInterface;
using HotelBooking.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HotelBooking.Implementation
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<BookingsModel> _entities;

        public BookingRepository(AppDbContext context)
        {
            _context = context; //
            _entities = _context.Set<BookingsModel>();
        }

        public async Task<IEnumerable<BookingsModel>> GetAllBooking()
        {
            return await _entities.ToListAsync();
        }

        public async Task<BookingsModel> GetBookingById(int Id)
        {
            return await _entities.FindAsync(Id);
        }
        public async Task<BookingsModel?> AddBooking(BookingsModel booking)
        {
            if (booking == null)
            {
                throw new ArgumentNullException(nameof(booking), "Booking object is null.");
            }

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "InsertBooking";
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding parameters to the command
                        command.Parameters.Add(new SqlParameter("@RoomId", booking.RoomId));
                        command.Parameters.Add(new SqlParameter("@UserId", booking.UserId));
                        command.Parameters.Add(new SqlParameter("@StartDate", booking.StartDate));
                        command.Parameters.Add(new SqlParameter("@EndDate", booking.EndDate));
                        command.Parameters.Add(new SqlParameter("@BookingDate", booking.BookingDate));
                        command.Parameters.Add(new SqlParameter("@TotalPrice", booking.TotalPrice));
                        command.Parameters.Add(new SqlParameter("@Status", booking.Status));

                        // Executing the command asynchronously
                        int result = await command.ExecuteNonQueryAsync();

                        if (result < 0)
                        {
                            return booking;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<string> UpdateBooking(BookingsModel booking)
        {
            if (booking == null)
            {
                return "Booking data cannot be null";
            }

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "UpdateBooking";
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding parameters
                        command.Parameters.Add(new SqlParameter("@Id", booking.Id));
                        command.Parameters.Add(new SqlParameter("@RoomId", booking.RoomId));
                        command.Parameters.Add(new SqlParameter("@UserId", booking.UserId));
                        command.Parameters.Add(new SqlParameter("@StartDate", booking.StartDate));
                        command.Parameters.Add(new SqlParameter("@EndDate", booking.EndDate));
                        command.Parameters.Add(new SqlParameter("@BookingDate", booking.BookingDate));
                        command.Parameters.Add(new SqlParameter("@TotalPrice", booking.TotalPrice));
                        command.Parameters.Add(new SqlParameter("@Status", booking.Status));

                        // Executing command asynchronously
                        int result = await command.ExecuteNonQueryAsync();

                        // Return a message based on the result
                        if (result < 0)
                        {
                            return "Booking updated successfully";
                        }
                        else
                        {
                            return "Booking with Id " + booking.Id + " doesn't exist";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception by logging or returning a meaningful message
                Console.WriteLine(ex.Message);
                return "An error occurred: " + ex.Message;
            }
        }

        public async Task DeleteBooking(int id)
        {
            var booking = await _entities.FirstOrDefaultAsync(e => e.Id == id);
            if (booking != null)
            {
                _entities.Remove(booking);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<BookingsModel>> GetAllBookings(int userId)
        {
            var bookings = new List<BookingsModel>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "GetAllBookings";
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding the parameter for the stored procedure
                        command.Parameters.Add(new SqlParameter("@UserId", userId));

                        // Execute the command and read the results
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var booking = new BookingsModel
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    RoomId = reader.GetInt32(reader.GetOrdinal("RoomId")),
                                    UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                                    StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")),
                                    EndDate = reader.GetDateTime(reader.GetOrdinal("EndDate")),
                                    BookingDate = reader.GetDateTime(reader.GetOrdinal("BookingDate")),
                                    TotalPrice = reader.GetDecimal(reader.GetOrdinal("TotalPrice")),
                                    Status = reader.GetString(reader.GetOrdinal("Status"))

                                };

                                bookings.Add(booking);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Handle exception
            }

            return bookings;
        }
        public async Task<IEnumerable<AvailableRoomModel>> GetAvailableRooms(int hotelId, string roomType, DateTime startDate, DateTime endDate)
        {
            var rooms = new List<AvailableRoomModel>();

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "GetAvailableRooms";
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding parameters
                        command.Parameters.Add(new SqlParameter("@HotelId", hotelId));
                        command.Parameters.Add(new SqlParameter("@RoomType", roomType));
                        command.Parameters.Add(new SqlParameter("@StartDate", startDate));
                        command.Parameters.Add(new SqlParameter("@EndDate", endDate));

                        // Execute the command and read the results
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var room = new AvailableRoomModel
                                {
                                    RoomId = reader.GetInt32(reader.GetOrdinal("RoomId")),
                                    RoomType = reader.GetString(reader.GetOrdinal("RoomType")),
                                    RoomNumber = reader.GetString(reader.GetOrdinal("RoomNumber")),
                                    Capacity = reader.GetInt32(reader.GetOrdinal("Capacity")),
                                    PricePerNight = reader.GetDecimal(reader.GetOrdinal("PricePerNight")),
                                    IsAvailable = reader.GetBoolean(reader.GetOrdinal("IsAvailable"))
                                };

                                rooms.Add(room);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Handle exception
            }

            return rooms;
        }
    }
}




