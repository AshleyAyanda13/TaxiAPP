using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiAppApi.Model
{
    [Table("Province")]
    public class Province
    {
        [Key]
        public int Id { get; set; } 
        public string Name { get; set; }

    }
}
