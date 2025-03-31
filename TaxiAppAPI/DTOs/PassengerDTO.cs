using System.ComponentModel.DataAnnotations;
using TaxiAppApi.Model;

namespace TaxiAppApi.DTOs
{
    public class PassengerDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Numbers { get; set; }

       public string appUserId { get; set; }


        public Passenger Passengers() => new Passenger
        {
            PassengerName = this.Name,
            Surname = this.Surname,
            CellphoneNr = this.Numbers,
            
        };
    }
}