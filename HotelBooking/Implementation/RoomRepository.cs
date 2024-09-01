using HotelBooking.DataInterface;
using HotelBooking.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HotelBooking.Implementation
{
    public class RoomRepository : IRoomRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<RoomModel> _entities;

        public RoomRepository(AppDbContext context)
        {
            _context = context;
            _entities = _context.Set<RoomModel>();
        }

        public async Task<IEnumerable<RoomModel>> GetAllRoom()
        {
            return await _entities.ToListAsync();
        }

        public async Task<RoomModel> GetRoomById(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<RoomModel?> AddRoom(RoomModel room)
        {
            if (room == null)
            {
                throw new ArgumentNullException(nameof(room), "Room object is null.");
            }

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "InsertRoom";
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding parameters to the command
                        command.Parameters.Add(new SqlParameter("@HotelId", room.HotelId));
                        command.Parameters.Add(new SqlParameter("@RoomType", room.RoomType));
                        command.Parameters.Add(new SqlParameter("@RoomNumber", room.RoomNumber));
                        command.Parameters.Add(new SqlParameter("@Capacity", room.Capacity));
                        command.Parameters.Add(new SqlParameter("@PricePerNight", room.PricePerNight));
                        command.Parameters.Add(new SqlParameter("@IsAvailable", room.IsAvailable));
                        command.Parameters.Add(new SqlParameter("@Description", room.Description ?? (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@Facilities", room.Facilities));
                        command.Parameters.Add(new SqlParameter("@CreatedOn", room.CreatedOn));
                        command.Parameters.Add(new SqlParameter("@ModifiedOn", room.ModifiedOn));

                        // Executing the command asynchronously
                        int result = await command.ExecuteNonQueryAsync();

                        if (result > 0)
                        {
                            return room;
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

        public async Task<string> UpdateRoom(RoomModel room)
        {
            if (room == null)
            {
                return "Room data cannot be null";
            }

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "UpdateRoom";
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding parameters
                        command.Parameters.Add(new SqlParameter("@Id", room.Id));
                        command.Parameters.Add(new SqlParameter("@HotelId", room.HotelId));
                        command.Parameters.Add(new SqlParameter("@RoomType", room.RoomType));
                        command.Parameters.Add(new SqlParameter("@RoomNumber", room.RoomNumber));
                        command.Parameters.Add(new SqlParameter("@Capacity", room.Capacity));
                        command.Parameters.Add(new SqlParameter("@PricePerNight", room.PricePerNight));
                        command.Parameters.Add(new SqlParameter("@IsAvailable", room.IsAvailable));
                        command.Parameters.Add(new SqlParameter("@Description", room.Description ?? (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@Facilities", room.Facilities));
                        command.Parameters.Add(new SqlParameter("@CreatedOn", room.CreatedOn));
                        command.Parameters.Add(new SqlParameter("@ModifiedOn", room.ModifiedOn));

                        // Executing command asynchronously
                        int result = await command.ExecuteNonQueryAsync();

                        // Return a message based on the result
                        if (result < 0)
                        {
                            return "Room updated successfully";
                        }
                        else
                        {
                            return "Room with Id " + room.Id + " doesn't exist";
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

        public async Task DeleteRoom(int id)
        {
            var room = await _entities.FirstOrDefaultAsync(e => e.Id == id);
            if (room != null)
            {
                _entities.Remove(room);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<string>> GetRoomTypesByHotelId(int hotelId)
        {
            using var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "GetRoomTypesByHotelId"; // Stored procedure name
            command.CommandType = CommandType.StoredProcedure;

            // Add hotelId as a parameter
            command.Parameters.Add(new SqlParameter("@HotelId", hotelId));

            var roomTypes = new List<string>();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                roomTypes.Add(reader.GetString(reader.GetOrdinal("RoomType")));
            }

            return roomTypes;
        }
    }
}
