using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using TaxiAppApi.Model;

namespace TaxiAppApi.DTOs
{
    public class BookingsDTO
    {
        public DateTime BookingDate { get; set; }
        [Required]
        public int PassengerId { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        public int TimeslotId { get; set; }
        [Required]
        public int MarshalId { get; set; }
        public DateTime DateCreated { get; set; }


        public Bookings Bookings() => new Bookings
        {
            BookingDate = BookingDate,
            DateCreated = DateCreated,
        };

    }
}
