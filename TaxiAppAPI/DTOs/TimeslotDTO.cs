using TaxiAppApi.Model;

namespace TaxiAppApi.DTOs
{
    public class TimeslotDTO
    {
        public string Slot { get; set; }

        public Timeslot Timeslot() => new Timeslot
        {
            Slot = this.Slot,
           

        };
    }
}
