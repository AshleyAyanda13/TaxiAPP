﻿using System.ComponentModel.DataAnnotations;

namespace TaxiAppApi.DTOs
{
    public class LoginDTO
    {



 
        [Required]
        public string Username{ get; set; }
        [Required]

        [DataType(DataType.Password)]


        public string Password { get; set; }
    }


}
