using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiAppApi.Model
{
    [Table("Timeslot")]
    public class Timeslot
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Slot { get; set; }

    }
}
