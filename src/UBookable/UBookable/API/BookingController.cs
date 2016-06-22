using Umbraco.Web.WebApi;
using System.Net.Http;
using System.Web;
using System.Web.Script.Serialization;
using UBookable.Models;
using UBookable.Repository;
using System.Web.Http;
using System;
using System.Collections.Generic;
using UBookable.Models.Calender;
using UBookable.Helpers;
using Umbraco.Web;
using System.Linq;
using UBookable.ViewModels;
using Our.Umbraco.Ditto;
using Umbraco.Core.Services;
using System.Web.Security;
using Umbraco.Core.Models;
using System.Net;

namespace UBookable.API
{
    public class BookingController : UmbracoApiController
    {

        private UBookableHelper _ubHelper = new UBookableHelper(UmbracoContext.Current);
        private UmbracoHelper _uHelper = new UmbracoHelper(UmbracoContext.Current);
        [HttpPost]
        public HttpResponseMessage AddBooking (Booking booking)
        {
            HttpContext.Current.Response.ContentType = "application/json";
            booking.StartDate = DateTime.Parse(booking.StartDateISO);
            booking.EndDate = DateTime.Parse(booking.EndDateISO);

            //check max bookings not already in palce.
            BookableSettings nodeSettings = _uHelper.TypedContent(booking.NodeID).As<BookableSettings>();
            int maxBookings = nodeSettings.AvailabilityPerSlot;
            int currentBookingsInSpace = Bookings.GetByUnCancelledBookingsByNodeIDAndDate(int.Parse(booking.NodeID), booking.StartDate, booking.EndDate).Count;
            if (currentBookingsInSpace < maxBookings)
            {
                Booking createdBooking = Bookings.Save(booking);
                HttpContext.Current.Response.StatusCode = 200;
                HttpContext.Current.Response.Write(new JavaScriptSerializer().Serialize(createdBooking));
                return new HttpResponseMessage();
            }
            else {
                throw new HttpResponseException(new HttpResponseMessage((HttpStatusCode)422));
            }
            


        }

        [HttpGet]
        public HttpResponseMessage GetTimeSlotsByNodeId(int nodeId, DateTime dayRequest)
        {
            HttpContext.Current.Response.ContentType = "application/json";
            BookableSettings settings = _uHelper.TypedContent(nodeId).As<BookableSettings>();
            DateTime rDate = dayRequest;
            Time dateStartTime = settings.DailyStartTime;
            Time dateEndTime = settings.DailyEndTime; 
            DateTime startTime = new DateTime(rDate.Year, rDate.Month, rDate.Day, dateStartTime.Hours,dateStartTime.Mins, 0);
            DateTime endTime = new DateTime(rDate.Year, rDate.Month, rDate.Day, dateEndTime.Hours, dateEndTime.Mins, 0);

            List<TimeSlot> timeslots = _ubHelper.GetDailyTimeSlots(nodeId, startTime, endTime, settings.MinimumBookingTimePeriod, settings.MinimumBookingLength);
            HttpContext.Current.Response.StatusCode = 200;
            HttpContext.Current.Response.Write(new JavaScriptSerializer().Serialize(timeslots));
            return new HttpResponseMessage();
        }

        [HttpGet]
        public HttpResponseMessage GetAllBookingsByNodeId (int nodeId)
        {
            HttpContext.Current.Response.ContentType = "application/json";

            IMemberService _memberService = UmbracoContext.Application.Services.MemberService;
            MembershipUser currentMember = System.Web.Security.Membership.GetUser();

            List<Booking> bookingsByNode = Bookings.GetByBookingsByNodeId(nodeId);
            IEnumerable<BookingResponse> response = bookingsByNode.Select(x => new BookingResponse
            {
                Approved = x.Approved,
                BookerID = x.BookerID,
                BookingID = x.BookingID,
                Cancelled = x.Cancelled,
                Count = bookingsByNode.Count(),
                EndDate = DateTime.Parse(x.EndDate.ToString("yyyy-MM-dd HH':'mm':'ss")),
                StartDate = DateTime.Parse(x.StartDate.ToString("yyyy-MM-dd HH':'mm':'ss")),
                Name = _memberService.GetById(int.Parse(x.BookerID)).Name
            });

            var groupedresponse = response.GroupBy(x => x.StartDate.ToString("yyyyMMdd"),
                (key, values) => new { Date = key, Count = values.Count(), Bookings = values }
                );


            HttpContext.Current.Response.StatusCode = 200;
            HttpContext.Current.Response.Write(new JavaScriptSerializer().Serialize(groupedresponse));
            return new HttpResponseMessage();
        }


        //[HttpPost]
        //public HttpResponseMessage AddBookingAndBooker(Booking booking)
        //{
        //    HttpContext.Current.Response.ContentType = "application/json";

        //    booking.StartDate = DateTime.Parse(booking.StartDateISO);
        //    booking.EndDate = DateTime.Parse(booking.EndDateISO);

        //    Booking createdBooking = Bookings.Save(booking);

        //    HttpContext.Current.Response.StatusCode = 200;
        //    HttpContext.Current.Response.Write(new JavaScriptSerializer().Serialize(createdBooking));
        //    return new HttpResponseMessage();
        //}


        [AcceptVerbs("GET")]
        public HttpResponseMessage GetDayBookingSlots(long start, long end, int Id, string period, int duration)
        {
            HttpContext.Current.Response.ContentType = "application/json";
            List<TimeSlot> timeSlots = _ubHelper.GetDailyTimeSlots(Id, UnixTimeStampToDateTime(start) , UnixTimeStampToDateTime(end), period, duration);
            HttpContext.Current.Response.StatusCode = 200;
            HttpContext.Current.Response.Write(new JavaScriptSerializer().Serialize(timeSlots));
            return new HttpResponseMessage();
        }

        [AcceptVerbs("PUT")]
        public HttpResponseMessage ApproveBooking(int bookingid)
        {
            HttpContext.Current.Response.ContentType = "application/json";
            Booking updatedBookingId = Bookings.Approve(bookingid);
            HttpContext.Current.Response.StatusCode = 200;
            HttpContext.Current.Response.Write(new JavaScriptSerializer().Serialize(updatedBookingId));
            return new HttpResponseMessage();
        }
        [AcceptVerbs("PUT")]
        public HttpResponseMessage CancelBooking(int bookingid)
        {
            HttpContext.Current.Response.ContentType = "application/json";
            Booking updatedBookingId = Bookings.Cancel(bookingid);
            HttpContext.Current.Response.StatusCode = 200;
            HttpContext.Current.Response.Write(new JavaScriptSerializer().Serialize(updatedBookingId));
            return new HttpResponseMessage();
        }
        private static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime + new TimeSpan(unixTimeStamp * 10000);
            return dtDateTime;
        }

    }
}