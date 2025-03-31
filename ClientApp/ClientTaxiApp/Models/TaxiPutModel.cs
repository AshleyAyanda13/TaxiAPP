using System.ComponentModel.DataAnnotations;

namespace ClientTaxiApp.Models
{
    public class TaxiPutModel
    {


        [Required]
        public string BrandName { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string LicensePlate { get; set; }
        [Required]
        public int DriverId { get; set; }
        public string Seats { get; set; }


		public Taxi Taxis() => new Taxi
		{
			BrandName = this.BrandName,
			Type = this.Type,
			LicensePlate = this.LicensePlate,
            Seats = this.Seats
		};


	}
}
