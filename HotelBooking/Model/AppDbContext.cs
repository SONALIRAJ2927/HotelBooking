using HotelBooking.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HotelBooking.Model
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<HotelModel> Hotels { get; set; }
        public DbSet<RoomModel> Rooms { get; set; }
        public DbSet<BookingsModel> Bookings { get; set; }
        public DbSet<PaymentModel> Payment { get; set; }
        public DbSet<AvailableRoomModel> AvailableRoom { get; set; }

    }
}
  
      