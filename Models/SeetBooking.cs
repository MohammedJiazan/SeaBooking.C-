using System;
using System.Collections.Generic;

namespace sea_booking.Models
{
    public partial class SeetBooking
    {
        public int Id { get; set; }
        public int? BookingId { get; set; }
        public int? SeetId { get; set; }
        public int TripId { get; set; }

        public virtual Booking Booking { get; set; }
        public virtual Seat Seet { get; set; }
        public virtual Trip Trip { get; set; }

    }
}
