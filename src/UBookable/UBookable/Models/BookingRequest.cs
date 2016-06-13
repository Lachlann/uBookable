using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UBookable.Models
{
    public class BookingRequest
    {
        public Booking booking { get; set; }
        public Booker booker { get; set; }
    }
}