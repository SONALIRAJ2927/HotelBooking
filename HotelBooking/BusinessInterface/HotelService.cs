using HotelBooking.DataInterface;
using HotelBooking.Model;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using System.Net;
using System.Reflection.Emit;

namespace HotelBooking.BusinessInterface
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _repository;

        public HotelService(IHotelRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<HotelModel>> GetAllHotel()
        {
            return await _repository.GetAllHotel();
        }
        public async Task<HotelModel> GetHotelById(int HotelId)
        {
            return await _repository.GetHotelById(HotelId);
        }
        public async Task<HotelModel> AddHotel(HotelModel hotel)
        {
            return await _repository.AddHotel(hotel);
        }
        public async Task<string> UpdateHotel(HotelModel hotel)
        {
            return await _repository.UpdateHotel(hotel);
        }

        public async Task DeleteHotel(int HotelId)
        {
            await _repository.DeleteHotel(HotelId);
        }
      public async  Task<IEnumerable<HotelModel>> SearchHotel(string? name, string? address, string? city, string? state, string? country, int? rating, string? zipCode)
        {
            return await _repository.SearchHotel(name, address, city, state, country, rating, zipCode);
        }
    }
}
