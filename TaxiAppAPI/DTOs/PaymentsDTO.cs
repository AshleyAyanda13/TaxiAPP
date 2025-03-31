using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaxiAppApi.Model;

namespace TaxiAppApi.DTOs
{
    public class PaymentsDTO
    {
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
        //[Required]
        //public int BookingId { get; set; }

        public Payments Payments() => new Payments
        {
            CardName = CardName,
            ExpirationDate = ExpirationDate,   
            CVV = CVV,
            BillingAdd = BillingAdd,
            Amount = Amount,
            PaymentDate = PaymentDate,
            PaymentStatus = PaymentStatus,

        };
    }
}
