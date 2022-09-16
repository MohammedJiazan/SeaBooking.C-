using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace sea_booking.Models
{
    public partial class Class
    {
        public Class()
        {
            ShipClasses = new HashSet<ShipClasses>();
        }
       
        public int Id { get; set; }
        [Required]
        public string ClassName { get; set; }
        public string Deascription { get; set; }
        [Required]
        public double ClassSeatPrice { get; set; }

        public virtual ICollection<ShipClasses> ShipClasses { get; set; }
    }
}
