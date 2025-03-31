using System.ComponentModel.DataAnnotations;
using TaxiAppApi.Model;

namespace TaxiAppApi.DTOs
{
    public class CityDTO
    {
     
        public string Name { get; set; }
        [Required]
        public int ProvinceId { get; set; }
        [Required]
        public int TaxiId { get; set; }


        public Cities Cities() => new Cities
        {
            Name = Name,

        };

    }
}
