using HotelBooking.Model;

namespace HotelBooking.BusinessInterface
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetAllUser();
        Task<UserModel> GetUserById(int ID); //direct get data from model basis of id
        Task<UserModel> AddUser(UserModel user); // inserting the data in table

        Task<string> UpdateUser(UserModel user); // want to return message string
        Task DeleteUser(int ID); //del user basis of id
        Task<bool> Login(string email, string password); 


    }
}
