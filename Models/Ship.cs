using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sea_booking.Models
{
    public partial class Ship
    {
        public Ship()
        {
            Booking = new HashSet<Booking>();
            ShipClasses = new HashSet<ShipClasses>();
            Trips = new HashSet<Trip>();

        }
       
        public int Id { get; set; }
        [Required]
        public string ShipName { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string Image { get; set; }
        public string Details { get; set; }

        public virtual ICollection<Booking> Booking { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }

        public virtual ICollection<ShipClasses> ShipClasses { get; set; }
    }
}
