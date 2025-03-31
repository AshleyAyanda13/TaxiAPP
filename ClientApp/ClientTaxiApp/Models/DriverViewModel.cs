using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClientTaxiApp.Models
{
    public class DriverViewModel
    { 
            [StringLength(20)]

            [Required]
            public string DriverName { get; set; }
            [StringLength(20)]

            [Required]
            public string DriverSurname { get; set; }
        
    

    }
}
