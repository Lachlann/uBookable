
using System;

namespace UBookable.Models.Calender
{
    public class TimeSlot
    {
        public DateTime StartTime { get; set;}
        public DateTime EndTime { get; set; }
        public int AvailableSpaces { get; set; }
        public int TotalSpaces { get; set; }
    }
}