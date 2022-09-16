using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace sea_booking.Models
{
    public partial class Station
    {
 

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Country { get; set; }
        public string Note { get; set; }

   
    }
}
