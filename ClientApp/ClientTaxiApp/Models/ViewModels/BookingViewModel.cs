using System.ComponentModel.DataAnnotations;

namespace ClientTaxiApp.Models.ViewModels
{
    public class BookingViewModel
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
    }
}
