using Our.Umbraco.Ditto;
using System.Collections.Generic;
using System.ComponentModel;
using UBookable.Models.Calender;
using UBookable.TypeConvertors;
namespace UBookable.ViewModels
{
    public class BookableSettings
    {
        public bool EnabledBooking { get; set; }
        public int MinimumBookingLength { get; set; }
        public string MinimumBookingTimePeriod { get; set; }
        public string DailyStartTime { get; set; }
        public string DailyEndTime { get; set; }
        public int AvailabilityPerSlot { get; set; }
    }
}