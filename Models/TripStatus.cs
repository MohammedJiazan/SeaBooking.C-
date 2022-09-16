using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace sea_booking.Models
{
    public partial class TripStatus
    {
        public TripStatus()
        {
            Trip = new HashSet<Trip>();
        }

        public int Id { get; set; }
        [Required]
        public string StatusTitle { get; set; }
        public string Note { get; set; }

        public virtual ICollection<Trip> Trip { get; set; }
    }
}
