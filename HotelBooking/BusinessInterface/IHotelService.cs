using HotelBooking.Model;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.BusinessInterface
{
    public interface IHotelService
    {
        Task<IEnumerable<HotelModel>> GetAllHotel();
        Task<HotelModel> GetHotelById(int HotelId); //direct get data from model basis of id
        Task<HotelModel> AddHotel(HotelModel Hotel); // inserting the data in table

        Task<string> UpdateHotel(HotelModel Hotel); // want to return message string
        Task DeleteHotel(int HotelId); //del Hotel basis of id
        Task<IEnumerable<HotelModel>> SearchHotel(string? name, string? address, string? city, string? state, string? country, int? rating, string? zipCode);
    }
}
