using HotelBooking.DataInterface;
using HotelBooking.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;

namespace HotelBooking.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<UserModel> _entities;

        public UserRepository(AppDbContext context)
        {
            _context = context;
            _entities = _context.Set<UserModel>();
        }

        public async Task<IEnumerable<UserModel>> GetAllUser()
        {
            return await _entities.ToListAsync();
        }

        public async Task<UserModel> GetUserById(int id)
        {
            return await _entities.FindAsync(id);
        }
        public async Task<UserModel?> AddUser(UserModel user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User object is null.");
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
                        //command.Parameters.Add(new SqlParameter("@EmployeeID", employee.EmployeeID));
                        command.Parameters.Add(new SqlParameter("@FirstName", user.FirstName));
                        command.Parameters.Add(new SqlParameter("@LastName", user.LastName));
                        command.Parameters.Add(new SqlParameter("@Email", user.Email));
                        command.Parameters.Add(new SqlParameter("@Password", user.Password));
                        command.Parameters.Add(new SqlParameter("@PhoneNumber", user.PhoneNumber));


                        // Executing the command asynchronously
                        int result = await command.ExecuteNonQueryAsync();

                        if (result < 0)
                        {
                            return user;
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
        public async Task<string> UpdateUser(UserModel user)
        {
            if (user == null)
            {
                return "User data cannot be null";
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
                        command.Parameters.Add(new SqlParameter("@Id", user.Id));
                        command.Parameters.Add(new SqlParameter("@FirstName", user.FirstName));
                        command.Parameters.Add(new SqlParameter("@LastName", user.LastName));
                        command.Parameters.Add(new SqlParameter("@Email", user.Email));
                        command.Parameters.Add(new SqlParameter("@Password", user.Password));
                        command.Parameters.Add(new SqlParameter("@PhoneNumber", user.PhoneNumber));
                        //command.Parameters.Add(new SqlParameter("@IsActive", user.IsActive));
                        //command.Parameters.Add(new SqlParameter("@CreatedOn", user.CreatedOn));



                        // Executing command asynchronously
                        int result = await command.ExecuteNonQueryAsync();

                        // Return a message based on the result
                        if (result < 0)
                        {
                            return "User updated successfully";
                        }
                        else
                        {
                            return "User with Id " + user.Id + " doesn't exist";
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
        public async Task DeleteUser(int id)
        {
            var user = await _entities.FirstOrDefaultAsync(e => e.Id == id);
            if (user != null)
            {
                _entities.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ValidateUserLogin(string email, string password)
        {
            using (var connection = _context.Database.GetDbConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "ValidateUserLogin";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@Email", email));
                    command.Parameters.Add(new SqlParameter("@Password", password));

                    await connection.OpenAsync();

                    var result = (int)await command.ExecuteScalarAsync();

                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

        }
    }
}
