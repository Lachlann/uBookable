using Our.Umbraco.Ditto;
using System;
using System.Collections.Generic;
using UBookable.Models.Calender;
using UBookable.Repository;
using UBookable.ViewModels;
using Umbraco.Web;

namespace UBookable.Helpers
{
    public class UBookableHelper
    {
        private UmbracoHelper _helper;
        public UBookableHelper(UmbracoContext umbracoContext) {
            _helper = new UmbracoHelper(umbracoContext);
        }


        public List<TimeSlot> GetDailyTimeSlots(int nodeId, DateTime start, DateTime end, string period, int duration)
        {
            List<TimeSlot> timeSlots = new List<TimeSlot>();
            BookableSettings ubSettings = _helper.TypedContent(nodeId).As<BookableSettings>();
            DateTime time = start;
            while (time < end)
            {
                DateTime slotEnd = _addSplotDuration(time, duration , period);
                int availableSpaces = ubSettings.AvailabilityPerSlot - Bookings.GetByBookingsByNodeIDAndDate(nodeId, time, slotEnd).Count;
                timeSlots.Add(_getTimeSlot(time, slotEnd, ubSettings.AvailabilityPerSlot, availableSpaces));
                time = slotEnd;
            }
            
            return timeSlots;
        }
        private DateTime _addSplotDuration(DateTime start, int duration, string period)
        {
            return (period == "Hours") ? start.AddHours(duration) : start.AddMinutes(duration);
        }


        private TimeSlot _getTimeSlot(DateTime start, DateTime end, int totalSpaces, int availableSpaces) {
            TimeSlot timeSlot = new TimeSlot
            {
                StartTime = start,
                EndTime = end,
                AvailableSpaces = availableSpaces,
                TotalSpaces = totalSpaces
            };

            return timeSlot;
        }

    }
}