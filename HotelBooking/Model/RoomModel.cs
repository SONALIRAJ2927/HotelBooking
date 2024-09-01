namespace HotelBooking.Model
{
    public class RoomModel
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public required string RoomType { get; set; }
        public required string RoomNumber { get; set; }
        public int Capacity { get; set; }
        public required decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
        public string? Description { get; set; }
        public required string Facilities { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
