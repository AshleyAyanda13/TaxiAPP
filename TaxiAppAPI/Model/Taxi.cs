using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaxiAppApi.Model
{

    [Table("Taxi")]
    public class Taxi
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]       
        [Required]
        public string BrandName { get; set; }
        [StringLength(200)]       
        [Required]
        public string Type {  get; set; }
        [StringLength(100)]        
        [Required]
        public string LicensePlate { get; set; }        
        [Required]
        public Driver Driver { get; set; }
        public string Seats { get; set; }

    }
}
