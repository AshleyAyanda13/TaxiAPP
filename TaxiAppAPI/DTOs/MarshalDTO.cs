using TaxiAppApi.Model;

namespace TaxiAppApi.DTOs
{
    public class MarshalDTO
    {
        public string MarshalName { get; set; }
        public string MarshalSurname { get; set; }

        public TaxiMarshal TaxiMarshals() => new TaxiMarshal
        {
            MarshalName = this.MarshalName,
            MarshalSurname = this.MarshalSurname

        };
    }
}
