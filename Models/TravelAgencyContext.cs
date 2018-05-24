namespace TravelAgency.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TravelAgencyContext : DbContext
    {
        public TravelAgencyContext()
            : base("name=TravelAgencyContext")
        {
        }

        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<Resort> Resorts { get; set; }
        public virtual DbSet<Tour> Tours { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.Descriptions)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Hotels)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Resorts)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Hotel>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Hotel>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Hotel>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Hotel>()
                .Property(e => e.Descriptions)
                .IsUnicode(false);

            modelBuilder.Entity<Hotel>()
                .HasMany(e => e.Tours)
                .WithRequired(e => e.Hotel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Resort>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Resort>()
                .Property(e => e.Descriptions)
                .IsUnicode(false);

            modelBuilder.Entity<Resort>()
                .HasMany(e => e.Tours)
                .WithRequired(e => e.Resort)
                .WillCascadeOnDelete(false);
        }
    }
}
