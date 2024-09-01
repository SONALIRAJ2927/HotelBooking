namespace HotelBooking.Model
{
    public class BookingsModel
    {
            public int Id { get; set; }
            public int RoomId { get; set; }
            public int UserId { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public DateTime BookingDate { get; set; }
            public decimal TotalPrice { get; set; }
            public string Status { get; set; }
        }

    }

