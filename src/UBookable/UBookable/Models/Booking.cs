using System;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace UBookable.Models
{
    [TableName("UBBookings")]
    [PrimaryKey("BookingID", autoIncrement = true)]
    [ExplicitColumns]
    public class Booking
    {
        [Column("BookingID")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int BookingID { get; set; }

        [Column("NodeID")]
        public string NodeID { get; set; }

        [Column("BookerID")]
        public string BookerID { get; set; }

        [Column("StartDate")]
        public DateTime StartDate { get; set; }

        [Column("EndDate")]
        public DateTime EndDate { get; set; }

        [Column("Approved")]
        public bool Approved { get; set; }

        [Column("Cancelled")]
        public bool Cancelled { get; set; }

        [Ignore]
        public string StartDateISO { get; set; }
        [Ignore]
        public string EndDateISO { get; set; }
    }
}