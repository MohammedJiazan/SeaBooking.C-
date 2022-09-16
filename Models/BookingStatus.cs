using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace sea_booking.Models
{
    public partial class BookingStatus
    {
        public BookingStatus()
        {
            Booking = new HashSet<Booking>();
        }
  
        public int Id { get; set; }
        [Required]
        public string StatusTitle { get; set; }
        public string Note { get; set; }

        public virtual ICollection<Booking> Booking { get; set; }
    }
}
