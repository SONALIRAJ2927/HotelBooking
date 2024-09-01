using HotelBooking.Model;

namespace HotelBooking.DataInterface
{
    public interface IRoomRepository
    {
        Task<IEnumerable<RoomModel>> GetAllRoom();
        Task<RoomModel> GetRoomById(int id);
        Task<RoomModel?> AddRoom(RoomModel room);
        Task<string> UpdateRoom(RoomModel room);
        Task DeleteRoom(int id);
        Task<IEnumerable<string>> GetRoomTypesByHotelId(int hotelId);

    }
}
