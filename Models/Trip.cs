using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace sea_booking.Models
{
    public partial class Trip
    {
        public Trip()
        {
            Booking = new HashSet<Booking>();
            SeetBookings = new HashSet<SeetBooking>();
        }

        public int Id { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public float? Price { get; set; }
        
        [Required]
        public DateTime TripDateTime { get; set; }
        public int? FromId { get; set; }
        public int? ToId { get; set; }
        public DateTime DelayDateTime { get; set; }
        public bool isOpen { get; set; }
        [Required]
        public int ShipId { get; set; }
        public string Note { get; set; }

        public virtual Station From { get; set; }
        public virtual Station To { get; set; }
        public virtual Ship Ship { get; set; }

        public virtual ICollection<Booking> Booking { get; set; }
        public virtual ICollection<SeetBooking> SeetBookings { get; set; }

    }
}
