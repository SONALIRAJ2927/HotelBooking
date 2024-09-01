using HotelBooking.DataInterface;
using HotelBooking.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HotelBooking.Implementation
{
    public class HotelRepository : IHotelRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<HotelModel> _entities;

        public HotelRepository(AppDbContext context)
        {
            _context = context;
            _entities = _context.Set<HotelModel>();
        }

        public async Task<IEnumerable<HotelModel>> GetAllHotel()
        {
            return await _entities.ToListAsync();
        }

        public async Task<HotelModel> GetHotelById(int HotelId)
        {
            return await _entities.FindAsync(HotelId);
        }
        public async Task<HotelModel?> AddHotel(HotelModel hotel)
        {
            if (hotel == null)
            {
                throw new ArgumentNullException(nameof(hotel), "Hotel object is null.");
            }

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "InsertUser";
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding parameters to the command
                        command.Parameters.Add(new SqlParameter("@Id", hotel.HotelId));
                        command.Parameters.Add(new SqlParameter("@Name", hotel.Name));
                        command.Parameters.Add(new SqlParameter("@Address", hotel.Address));
                        command.Parameters.Add(new SqlParameter("@City", hotel.City));
                        command.Parameters.Add(new SqlParameter("@State", hotel.State));
                        command.Parameters.Add(new SqlParameter("@Country", hotel.Country));
                        command.Parameters.Add(new SqlParameter("@ZipCode", hotel.ZipCode));
                        command.Parameters.Add(new SqlParameter("@PhoneNumber", hotel.PhoneNumber));
                        command.Parameters.Add(new SqlParameter("@Rating", hotel.Rating));


                        // Executing the command asynchronously
                        int result = await command.ExecuteNonQueryAsync();

                        if (result < 0)
                        {
                            return hotel;
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
        public async Task<string> UpdateHotel(HotelModel hotel)
        {
            if (hotel == null)
            {
                return "Hotel data cannot be null";
            }

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "UpdateUser";
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding parameters
                        command.Parameters.Add(new SqlParameter("@Id", hotel.HotelId));
                        command.Parameters.Add(new SqlParameter("@Name", hotel.Name));
                        command.Parameters.Add(new SqlParameter("@Address", hotel.Address));
                        command.Parameters.Add(new SqlParameter("@City", hotel.City));
                        command.Parameters.Add(new SqlParameter("@State", hotel.State));
                        command.Parameters.Add(new SqlParameter("@Country", hotel.Country));
                        command.Parameters.Add(new SqlParameter("@ZipCode", hotel.ZipCode));
                        command.Parameters.Add(new SqlParameter("@PhoneNumber", hotel.PhoneNumber));
                        command.Parameters.Add(new SqlParameter("@Rating", hotel.Rating));




                        // Executing command asynchronously
                        int result = await command.ExecuteNonQueryAsync();

                        // Return a message based on the result
                        if (result < 0)
                        {
                            return "Hotel updated successfully";
                        }
                        else
                        {
                            return "Hotel with HotelId " + hotel.HotelId + " doesn't exist";
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
        public async Task DeleteHotel(int id)
        {
            var hotel = await _entities.FirstOrDefaultAsync(e => e.HotelId == id);
            if (hotel != null)
            {
                _entities.Remove(hotel);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<HotelModel>> SearchHotel(string? name, string? address, string? city, string? state, string? country, int? rating, string? zipCode)
        {
            using var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SearchHotel";
            command.CommandType = CommandType.StoredProcedure;

            // Create a method to add parameters to reduce redundancy
            void AddParameter(string paramName, object? value) =>
                command.Parameters.Add(new SqlParameter(paramName, value ?? DBNull.Value));

            // Add all parameters to the command
            AddParameter("@Name", name);
            AddParameter("@Address", address);
            AddParameter("@City", city);
            AddParameter("@State", state);
            AddParameter("@Country", country);
            AddParameter("@Rating", rating);
            AddParameter("@ZipCode", zipCode);

            // Execute the command and map the results to a list of HotelModel
            var hotels = new List<HotelModel>();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                hotels.Add(new HotelModel
                {
                    HotelId = reader.GetInt32(reader.GetOrdinal("HotelId")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Address = reader.GetString(reader.GetOrdinal("Address")),
                    City = reader.GetString(reader.GetOrdinal("City")),
                    State = reader.GetString(reader.GetOrdinal("State")),
                    Country = reader.GetString(reader.GetOrdinal("Country")),
                    ZipCode = reader.GetString(reader.GetOrdinal("ZipCode")),
                    Rating = reader.GetDecimal(reader.GetOrdinal("Rating"))
                });
            }
            return hotels;
        }
    }
}
