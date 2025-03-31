using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TaxiAppApi.Model;

namespace TaxiAppApi.DTOs
{
    public class DriverDelDTO
    {

        public DateTime DeleteDate { get; set; }
        public int DriverId { get; set; }
        public DriverDel ToDriverDel() => new DriverDel
        {
            DeleteDate = DeleteDate,
        };

    }
}
