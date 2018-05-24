namespace TravelAgency.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Hotel")]
    public partial class Hotel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Hotel()
        {
            Tours = new HashSet<Tour>();
        }

        [Key]
        public int ID_Hotel { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(1, 5)]
        public int Stars { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(100)]
        public string Phone { get; set; }

        public string Descriptions { get; set; }

        public float? Lat { get; set; }

        public float? Lng { get; set; }

        public int ID_Country { get; set; }

        public virtual Country Country { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tour> Tours { get; set; }
    }
}
