using HotelBooking.Model;

namespace HotelBooking.DataInterface
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserModel>> GetAllUser();
        Task<UserModel> GetUserById(int EmployeeID);
        Task<UserModel?> AddUser(UserModel user);
        Task<string> UpdateUser(UserModel user);
        Task DeleteUser(int ID);
        Task<bool> ValidateUserLogin(string email, string password);


    }
}
