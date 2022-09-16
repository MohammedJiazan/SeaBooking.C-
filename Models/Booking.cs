using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace sea_booking.Models
{
    public partial class Booking
    {
        public Booking()
        {
            SeetBooking = new HashSet<SeetBooking>();
        }
      
        public int Id { get; set; }
        [Required]
        public DateTime BookingDateTime { get; set; }
        
        public int TicketId { get; set; }
        
        public int? TripId { get; set; }
        
        public int PassangerId { get; set; }
        
        public int? ShipClassesId { get; set; }
        [Required]
        public double TotalPayment { get; set; }
        [Required]
        public bool isConfirmed  { get; set; }


        public virtual Passanger Passanger { get; set; }
        public virtual ShipClasses ShipClasses { get; set; }

        public virtual Trip Trip { get; set; }
        public virtual ICollection<SeetBooking> SeetBooking { get; set; }
    }
}
