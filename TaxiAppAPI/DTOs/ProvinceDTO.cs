using TaxiAppApi.Model;

namespace TaxiAppApi.DTOs
{
    public class ProvinceDTO
    {
        public string Name { get; set; }

        public Province Province() => new Province
        {
            Name = this.Name
        };
    }
}
