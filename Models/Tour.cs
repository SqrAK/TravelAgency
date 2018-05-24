namespace TravelAgency.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tour")]
    public partial class Tour
    {
        [Key]
        public int ID_Tour { get; set; }

        public int Price { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}")]
        public DateTime? DateStart { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}")]
        public DateTime? DateEnd { get; set; }

        public int ID_Hotel { get; set; }

        public int ID_Resort { get; set; }

        public virtual Hotel Hotel { get; set; }

        public virtual Resort Resort { get; set; }
    }
}
