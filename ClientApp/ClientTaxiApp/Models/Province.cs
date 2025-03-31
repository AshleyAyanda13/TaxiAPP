using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClientTaxiApp.Models
{
    [Table("Province")]
    public class Province
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
