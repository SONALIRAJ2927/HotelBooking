using HotelBooking.DataInterface;
using HotelBooking.Implementation;
using HotelBooking.Model;

namespace HotelBooking.BusinessInterface
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<UserModel>> GetAllUser()
        {
            return await _repository.GetAllUser();
        }
        public async Task<UserModel> GetUserById(int id)
        {
            return await _repository.GetUserById(id);
        }
        public async Task<UserModel> AddUser(UserModel user)
        {
            return await _repository.AddUser(user);
        }
        public async Task<string> UpdateUser(UserModel user)
        {
            return await _repository.UpdateUser(user);
        }

        public async Task DeleteUser(int ID)
        {
            await _repository.DeleteUser(ID);
        }

        public async Task<bool> Login(string email, string password)  // for gtting true orfalse bool
        {
            return await _repository.ValidateUserLogin(email, password); //ValidateUserLogin method
        }

    }
}

