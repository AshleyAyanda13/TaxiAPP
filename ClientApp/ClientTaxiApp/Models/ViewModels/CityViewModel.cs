using System.ComponentModel.DataAnnotations;

namespace ClientTaxiApp.Models.ViewModels
{
    public class CityViewModel
    {
        public string Name { get; set; }
        [Required]
        public int ProvinceId { get; set; }
        [Required]
        public int TaxiId { get; set; }
    }
}
