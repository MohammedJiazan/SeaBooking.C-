using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace sea_booking.Models
{
    public partial class Passanger
    {
        public Passanger()
        {
            Booking = new HashSet<Booking>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int? Age { get; set; }
        [Required]
        public int PassportNo { get; set; }
        public byte[] Image { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int PhoneNo { get; set; }

        public virtual ICollection<Booking> Booking { get; set; }
    }
}
