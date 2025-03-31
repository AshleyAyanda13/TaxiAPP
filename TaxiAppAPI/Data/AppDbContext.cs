using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaxiAppApi.Model;


namespace TaxiAppApi.Data
{
    
    public class AppDbContext : IdentityDbContext<AppUser>
    {
    //create empty constructor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        //field for booking items
        public DbSet<Bookings> Bookings { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Taxi> Taxi {get;set;}
        public DbSet<TaxiMarshal> TaxiMarshals { get; set; }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<Timeslot> Timeslot { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<Province> Province { get; set; }
        public DbSet<Driver> Driver { get; set; }
        public DbSet<DriverDel> DriverDel {get;set;}
        public DbSet<AppUser> AppUser { get; set; }
    }
}
