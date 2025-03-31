using System.ComponentModel.DataAnnotations;

namespace ClientTaxiApp.Models
{
    public class RegisterModel
    {





            [EmailAddress]

            [Required]

            public string Email { get; set; }
            [Required]
            public string UserName { get; set; }
            [Required]
            public string PhoneNumber { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            [Required]
            [DataType(DataType.Password)]
            [Compare(nameof(Password))]
            public string ConfirmPassword { get; set; }
        public string ReturnUrl { get; set; }
    }
}
