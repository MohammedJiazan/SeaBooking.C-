using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace sea_booking.Models
{
    public partial class ShipClasses
    {
        public ShipClasses()
        {
            Seat = new HashSet<Seat>();
        }

        public int Id { get; set; }
        [Required]
        public int ShipId { get; set; }
        [Required]
        [Display(Name ="Class")]
        public int ClassId { get; set; }
        [Required]
        public int MaxSeatsNo { get; set; }

        public virtual Class Class { get; set; }
        public virtual Ship Ship { get; set; }
        public virtual ICollection<Seat> Seat { get; set; }
    }
}
