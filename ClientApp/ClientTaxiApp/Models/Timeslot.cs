using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClientTaxiApp.Models
{
        [Table("Timeslot")]
        public class Timeslot
        {
            [Key]
            [Required]
            public int Id { get; set; }
            [Required]
            public TimeOnly Slot { get; set ; }

        }
}
