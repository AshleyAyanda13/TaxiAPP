﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClientTaxiApp.Models
{
    [Table("Passenger")]
    public class Passenger
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string PassengerName { get; set; }
        [Required]
        [StringLength(100)]
        public string Surname { get; set; }
        [Required]
        [StringLength(13)]
        public string CellphoneNr { get; set; }
        [Required]
        public AppUser AppUser { get; set; }

    }
}
