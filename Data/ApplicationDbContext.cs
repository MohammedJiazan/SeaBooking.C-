using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using sea_booking.Models;

namespace sea_booking.Data
{
    public partial class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Booking> Booking { get; set; }
        public virtual DbSet<BookingStatus> BookingStatus { get; set; }
        public virtual DbSet<Class> Class { get; set; }
        public virtual DbSet<Passanger> Passanger { get; set; }
        public virtual DbSet<Seat> Seat { get; set; }
        public virtual DbSet<SeetBooking> SeetBooking { get; set; }
        public virtual DbSet<Ship> Ship { get; set; }
        public virtual DbSet<ShipClasses> ShipClasses { get; set; }
        public virtual DbSet<Station> Station { get; set; }
        public virtual DbSet<Trip> Trip { get; set; }
        public virtual DbSet<TripStatus> TripStatus { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<Ship>().Ignore(m => m.ImageFile);
        //    //modelBuilder.Entity<Booking>(entity =>
        //    //{
        //    //    entity.Property(e => e.Id)
        //    //        .HasColumnName("ID")
        //    //        .ValueGeneratedNever();

        //    //    entity.Property(e => e.BookingDateTime).HasColumnType("datetime");

        //    //    entity.Property(e => e.ClassId).HasColumnName("ClassID");

        //    //    entity.Property(e => e.PassangerId).HasColumnName("PassangerID");

        //    //    entity.Property(e => e.ShipId).HasColumnName("ShipID");

        //    //    entity.Property(e => e.StatusId).HasColumnName("StatusID");

        //    //    entity.Property(e => e.TicketId).HasColumnName("TicketID");

        //    //    entity.Property(e => e.TotalPayment).HasColumnType("money");

        //    //    entity.Property(e => e.TripId).HasColumnName("TripID");

        //    //    entity.HasOne(d => d.Passanger)
        //    //        .WithMany(p => p.Booking)
        //    //        .HasForeignKey(d => d.PassangerId)
        //    //        .OnDelete(DeleteBehavior.ClientSetNull)
        //    //        .HasConstraintName("FK_Booking_Passanger");

        //    //    entity.HasOne(d => d.Ship)
        //    //        .WithMany(p => p.Booking)
        //    //        .HasForeignKey(d => d.ShipId)
        //    //        .OnDelete(DeleteBehavior.ClientSetNull)
        //    //        .HasConstraintName("FK_Booking_Ship");

        //    //    entity.HasOne(d => d.Status)
        //    //        .WithMany(p => p.Booking)
        //    //        .HasForeignKey(d => d.StatusId)
        //    //        .OnDelete(DeleteBehavior.ClientSetNull)
        //    //        .HasConstraintName("FK_Booking_BookingStatus");

        //    //    entity.HasOne(d => d.Trip)
        //    //        .WithMany(p => p.Booking)
        //    //        .HasForeignKey(d => d.TripId)
        //    //        .OnDelete(DeleteBehavior.ClientSetNull)
        //    //        .HasConstraintName("FK_Booking_Trip");
        //    //});

        //    //modelBuilder.Entity<BookingStatus>(entity =>
        //    //{
        //    //    entity.Property(e => e.Id)
        //    //        .HasColumnName("ID")
        //    //        .ValueGeneratedNever();

        //    //    entity.Property(e => e.Note).IsUnicode(false);

        //    //    entity.Property(e => e.StatusTitle)
        //    //        .IsRequired()
        //    //        .HasMaxLength(10)
        //    //        .IsFixedLength();
        //    //});

        //    //modelBuilder.Entity<Class>(entity =>
        //    //{
        //    //    entity.Property(e => e.Id)
        //    //        .HasColumnName("ID")
        //    //        .ValueGeneratedNever();

        //    //    entity.Property(e => e.ClassName)
        //    //        .IsRequired()
        //    //        .HasMaxLength(50)
        //    //        .IsUnicode(false);

        //    //    entity.Property(e => e.ClassSeatPrice).HasColumnType("money");

        //    //    entity.Property(e => e.Deascription).IsUnicode(false);
        //    //});

        //    //modelBuilder.Entity<Passanger>(entity =>
        //    //{
        //    //    entity.Property(e => e.Id)
        //    //        .HasColumnName("ID")
        //    //        .ValueGeneratedNever();

        //    //    entity.Property(e => e.Email)
        //    //        .HasMaxLength(50)
        //    //        .IsUnicode(false);

        //    //    entity.Property(e => e.Image)
        //    //        .HasMaxLength(50)
        //    //        .IsFixedLength();

        //    //    entity.Property(e => e.Name)
        //    //        .IsRequired()
        //    //        .HasMaxLength(50)
        //    //        .IsUnicode(false);
        //    //});

        //    //modelBuilder.Entity<Seat>(entity =>
        //    //{
        //    //    entity.Property(e => e.Id)
        //    //        .HasColumnName("ID")
        //    //        .ValueGeneratedNever();

        //    //    entity.Property(e => e.SeatNumber)
        //    //        .IsRequired()
        //    //        .HasMaxLength(50)
        //    //        .IsUnicode(false);

        //    //    entity.Property(e => e.ShipClassId).HasColumnName("ShipClassID");

        //    //    entity.HasOne(d => d.ShipClass)
        //    //        .WithMany(p => p.Seat)
        //    //        .HasForeignKey(d => d.ShipClassId)
        //    //        .OnDelete(DeleteBehavior.ClientSetNull)
        //    //        .HasConstraintName("FK_Seat_ShipClasses");
        //    //});

        //    //modelBuilder.Entity<SeetBooking>(entity =>
        //    //{
        //    //    entity.Property(e => e.Id).HasColumnName("ID");

        //    //    entity.Property(e => e.BookingId).HasColumnName("BookingID");

        //    //    entity.Property(e => e.SeetId).HasColumnName("SeetID");

        //    //    entity.HasOne(d => d.Booking)
        //    //        .WithMany(p => p.SeetBooking)
        //    //        .HasForeignKey(d => d.BookingId)
        //    //        .OnDelete(DeleteBehavior.SetNull)
        //    //        .HasConstraintName("FK_SeetBooking_Booking");

        //    //    entity.HasOne(d => d.Seet)
        //    //        .WithMany(p => p.SeetBooking)
        //    //        .HasForeignKey(d => d.SeetId)
        //    //        .OnDelete(DeleteBehavior.SetNull)
        //    //        .HasConstraintName("FK_SeetBooking_Seat");
        //    //});

        //    //modelBuilder.Entity<Ship>(entity =>
        //    //{
        //    //    entity.Property(e => e.Id)
        //    //        .HasColumnName("ID")
        //    //        .ValueGeneratedNever();

        //    //    entity.Property(e => e.Details).IsUnicode(false);

        //    //    entity.Property(e => e.Image).HasColumnType("image");

        //    //    entity.Property(e => e.ShipName)
        //    //        .IsRequired()
        //    //        .HasMaxLength(50)
        //    //        .IsUnicode(false);
        //    //});

        //    //modelBuilder.Entity<ShipClasses>(entity =>
        //    //{
        //    //    entity.Property(e => e.Id)
        //    //        .HasColumnName("id")
        //    //        .ValueGeneratedNever();

        //    //    entity.Property(e => e.ClassId).HasColumnName("ClassID");

        //    //    entity.Property(e => e.ShipId).HasColumnName("ShipID");

        //    //    entity.HasOne(d => d.Class)
        //    //        .WithMany(p => p.ShipClasses)
        //    //        .HasForeignKey(d => d.ClassId)
        //    //        .OnDelete(DeleteBehavior.ClientSetNull)
        //    //        .HasConstraintName("FK_ShipClasses_Class");

        //    //    entity.HasOne(d => d.Ship)
        //    //        .WithMany(p => p.ShipClasses)
        //    //        .HasForeignKey(d => d.ShipId)
        //    //        .OnDelete(DeleteBehavior.ClientSetNull)
        //    //        .HasConstraintName("FK_ShipClasses_Ship");
        //    //});

        //    //modelBuilder.Entity<ShipTrip>(entity =>
        //    //{
        //    //    entity.HasNoKey();

        //    //    entity.Property(e => e.Id).HasColumnName("ID");

        //    //    entity.Property(e => e.Note).IsUnicode(false);

        //    //    entity.Property(e => e.ShipId).HasColumnName("ShipID");

        //    //    entity.Property(e => e.TripId).HasColumnName("TripID");

        //    //    entity.HasOne(d => d.Ship)
        //    //        .WithMany()
        //    //        .HasForeignKey(d => d.ShipId)
        //    //        .OnDelete(DeleteBehavior.ClientSetNull)
        //    //        .HasConstraintName("FK_ShipTrip_Ship");

        //    //    entity.HasOne(d => d.Trip)
        //    //        .WithMany()
        //    //        .HasForeignKey(d => d.TripId)
        //    //        .OnDelete(DeleteBehavior.ClientSetNull)
        //    //        .HasConstraintName("FK_ShipTrip_Trip");
        //    //});

        //    //modelBuilder.Entity<Station>(entity =>
        //    //{
        //    //    entity.Property(e => e.Id)
        //    //        .HasColumnName("ID")
        //    //        .ValueGeneratedNever();

        //    //    entity.Property(e => e.Country)
        //    //        .IsRequired()
        //    //        .HasColumnName("country")
        //    //        .HasMaxLength(50)
        //    //        .IsUnicode(false);

        //    //    entity.Property(e => e.Name)
        //    //        .IsRequired()
        //    //        .HasMaxLength(50)
        //    //        .IsUnicode(false);

        //    //    entity.Property(e => e.Note).IsUnicode(false);
        //    //});

        //    //modelBuilder.Entity<Trip>(entity =>
        //    //{
        //    //    entity.Property(e => e.Id)
        //    //        .HasColumnName("ID")
        //    //        .ValueGeneratedNever();

        //    //    entity.Property(e => e.DelayDateTime).HasColumnType("datetime");

        //    //    entity.Property(e => e.Note).IsUnicode(false);

        //    //    entity.Property(e => e.Price).HasColumnType("money");

        //    //    entity.Property(e => e.StatuesId).HasColumnName("StatuesID");

        //    //    entity.Property(e => e.StatusId).HasColumnName("StatusID");

        //    //    entity.Property(e => e.TripDateTime).HasColumnType("datetime");

        //    //    entity.HasOne(d => d.FromNavigation)
        //    //        .WithMany(p => p.TripFromNavigation)
        //    //        .HasForeignKey(d => d.From)
        //    //        .OnDelete(DeleteBehavior.ClientSetNull)
        //    //        .HasConstraintName("FK_Trip_Station");

        //    //    entity.HasOne(d => d.Status)
        //    //        .WithMany(p => p.Trip)
        //    //        .HasForeignKey(d => d.StatusId)
        //    //        .OnDelete(DeleteBehavior.ClientSetNull)
        //    //        .HasConstraintName("FK_Trip_TripStatus");

        //    //    entity.HasOne(d => d.ToNavigation)
        //    //        .WithMany(p => p.TripToNavigation)
        //    //        .HasForeignKey(d => d.To)
        //    //        .OnDelete(DeleteBehavior.ClientSetNull)
        //    //        .HasConstraintName("FK_Trip_Station1");
        //    //});

        //    //modelBuilder.Entity<TripStatus>(entity =>
        //    //{
        //    //    entity.Property(e => e.Id)
        //    //        .HasColumnName("ID")
        //    //        .ValueGeneratedNever();

        //    //    entity.Property(e => e.Note).IsUnicode(false);

        //    //    entity.Property(e => e.StatusTitle)
        //    //        .IsRequired()
        //    //        .HasMaxLength(50)
        //    //        .IsUnicode(false);
        //    //});

        //    OnModelCreatingPartial(modelBuilder);
        //}

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        


    }
}
