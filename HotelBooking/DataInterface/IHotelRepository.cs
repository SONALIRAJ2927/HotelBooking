using HotelBooking.Model;

namespace HotelBooking.DataInterface
{
    public interface IHotelRepository
    {
        Task<IEnumerable<HotelModel>> GetAllHotel();
        Task<HotelModel> GetHotelById(int HotelId);
        Task<HotelModel?> AddHotel(HotelModel Hotel);
        Task<string> UpdateHotel(HotelModel Hotel);
        Task DeleteHotel(int HotelId);
        Task<IEnumerable<HotelModel>> SearchHotel(string? name, string? address, string? city, string? state, string? country, int? rating, string? zipCode);
    }
}
