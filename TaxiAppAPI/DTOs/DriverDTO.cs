using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TaxiAppApi.Model;

namespace TaxiAppApi.DTOs
{
    public class DriverDTO
    {

        [Required]

        public string DriverName { get; set; }
        [Required]
        public string DriverSurname { get; set; }
        public Driver ToDriver() => new Driver
        {

            DriverName=this.DriverName,
            DriverSurname=this.DriverSurname,


        };


    }
}
