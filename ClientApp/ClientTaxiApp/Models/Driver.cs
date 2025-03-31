using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClientTaxiApp.Models
{
    [Table("Driver")]
    public class Driver
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20)]

        [Required]
        public string DriverName { get; set; }
        [StringLength(20)]

        [Required]
        public string DriverSurname { get; set; }
    }
}
