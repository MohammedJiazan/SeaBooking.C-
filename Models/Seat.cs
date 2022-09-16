using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace sea_booking.Models
{
    public partial class Seat
    {
        public Seat()
        {
            SeetBooking = new HashSet<SeetBooking>();
        }
       
        public int Id { get; set; }
        public int ShipClassId { get; set; }
        [Required]
        public string SeatNumber { get; set; }

        public virtual ShipClasses ShipClass { get; set; }
        public virtual ICollection<SeetBooking> SeetBooking { get; set; }
    }
}
