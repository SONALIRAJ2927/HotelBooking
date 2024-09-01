using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Model
{
    public class HotelModel
    {
        [Key]
        public int HotelId { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public string? City { get; set; }
        public required string State { get; set; }
        public string? Country { get; set; }
        public string? ZipCode { get; set; }
        public  string? PhoneNumber { get; set; }
        public decimal Rating { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}