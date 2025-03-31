using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClientTaxiApp.Models
{
    [Table("Bookings")]
    public class Bookings
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        [Required]
        public Passenger Passenger { get; set; }
        [Required]
        public Cities City { get; set; }
        [Required]
        public Timeslot Timeslot { get; set; }
        [Required]
        public TaxiMarshal Marshal { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
