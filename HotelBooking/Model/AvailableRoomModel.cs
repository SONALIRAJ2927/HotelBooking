using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Model
{
    public class AvailableRoomModel
    {
        [Key]
        public int RoomId { get; set; }
        public string? RoomType { get; set; }
        public string? RoomNumber { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
    }
}
