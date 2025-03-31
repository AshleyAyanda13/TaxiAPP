using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClientTaxiApp.Models
{
    [Table("Cities")]
    public class Cities
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public Province ProvinceName { get; set; }
        [Required]
        public Taxi TaxiName { get; set; }

    }
}
