using System.ComponentModel.DataAnnotations;

namespace TaxiAppClient.Models
{
    public class LoginModel
    {

        [Required]
        

        public string Username { get; set; }
        [Required]

        [DataType(DataType.Password)]
        [Display(Name = "Password")]    

        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
