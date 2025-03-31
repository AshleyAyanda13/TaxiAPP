using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClientTaxiApp.Models
{

    [Table("Payments")]
    public class Payments
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string CardName { get; set; }
        [Required]
        public DateOnly ExpirationDate { get; set; }
        [Required]
        public int CVV { get; set; }
        [Required]
        public string BillingAdd { get; set; }

        [DataType(DataType.Currency)]
        [Column("Money")]
        public decimal? Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; }

    }
}
