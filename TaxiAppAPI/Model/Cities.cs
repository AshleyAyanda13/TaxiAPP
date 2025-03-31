using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiAppApi.Model
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
        public Province Province { get; set; }
        [Required]
        public Taxi Taxi { get; set; }

    }
}
