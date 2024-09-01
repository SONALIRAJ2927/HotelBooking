using HotelBooking.DataInterface;
using HotelBooking.Model;

namespace HotelBooking.BusinessInterface
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _repository;

        public RoomService(IRoomRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RoomModel>> GetAllRoom()
        {
            return await _repository.GetAllRoom();
        }

        public async Task<RoomModel> GetRoomById(int id)
        {
            return await _repository.GetRoomById(id);
        }

        public async Task<RoomModel> AddRoom(RoomModel room)
        {
            return await _repository.AddRoom(room); 
        }

        public async Task<string> UpdateRoom(RoomModel room)
        {
            return await _repository.UpdateRoom(room);
        }

        public async Task DeleteRoom(int id)
        {
            await _repository.DeleteRoom(id);
        }
        public async Task<IEnumerable<string>> GetRoomTypesByHotelId(int hotelId)
        {
            return await _repository.GetRoomTypesByHotelId(hotelId);
        }
    }
}

