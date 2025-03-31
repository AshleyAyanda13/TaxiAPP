using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClientTaxiApp.Models
{
    [Table("TaxiMarshal")]
    public class TaxiMarshal
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20)]
        [Required]
        public string MarshalName { get; set; }
        [StringLength(20)]
        [Required]
        public string MarshalSurname { get; set; }
    }
}
