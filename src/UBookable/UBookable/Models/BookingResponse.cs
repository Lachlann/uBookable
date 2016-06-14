

using System;

namespace UBookable.Models
{
    public class BookingResponse
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Name { get; set; }
    }
}