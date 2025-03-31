using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClientTaxiApp.Models
{
    [Table("DriverDel")]
    public class DriverDel
    {
        [Key]
        public int Id { get; set; }
        public DateTime DeleteDate { get; set; }
        [Required]
        public Driver driver { get; set; }

    }
}
