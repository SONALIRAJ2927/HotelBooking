using HotelBooking.Model;

namespace HotelBooking.BusinessInterface
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomModel>> GetAllRoom();
        Task<RoomModel> GetRoomById(int id); //direct get data from model basis of id
        Task<RoomModel> AddRoom(RoomModel room); // inserting the data in table

        Task<string> UpdateRoom(RoomModel room); // want to return message string
        Task DeleteRoom(int id); //del Hotel basis of id
        Task<IEnumerable<string>> GetRoomTypesByHotelId(int hotelId);

    }
}
