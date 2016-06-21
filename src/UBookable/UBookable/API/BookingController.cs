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
            Booking createdBooking  = Bookings.Save(booking);

            HttpContext.Current.Response.StatusCode = 200;
            HttpContext.Current.Response.Write(new JavaScriptSerializer().Serialize(createdBooking));
            return new HttpResponseMessage();
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

            IEnumerable<dynamic> bookingsByNode = Bookings.GetByBookingsByNodeId(nodeId);
            IEnumerable<BookingResponse> allBookings = bookingsByNode.
                Select(x => new BookingResponse
                {
                    EndDate =  DateTime.Parse(((IDictionary<string, dynamic>)x)["EndDate"].ToString("yyyy-MM-dd HH':'mm':'ss")),
                    StartDate = DateTime.Parse(((IDictionary<string, dynamic>)x)["StartDate"].ToString("yyyy-MM-dd HH':'mm':'ss")),
                    Name = ((IDictionary<string, dynamic>)x)["Name"],
                    Approved = ((IDictionary<string, dynamic>)x)["Approved"],
                    Cancelled = ((IDictionary<string, dynamic>)x)["Cancelled"],
                    BookingID = ((IDictionary<string, dynamic>)x)["BookingID"]
                });

            var groupedresponse = allBookings.GroupBy(x => x.StartDate.ToString("yyyyMMdd"),
                (key, values) => new { Date = key, Count = values.Count(), Bookings = values }
                );

            HttpContext.Current.Response.StatusCode = 200;
            HttpContext.Current.Response.Write(new JavaScriptSerializer().Serialize(groupedresponse));
            return new HttpResponseMessage();
        }


        [HttpPost]
        public HttpResponseMessage AddBookingAndBooker(BookingRequest bookingRequest)
        {
            HttpContext.Current.Response.ContentType = "application/json";

            Booker createdBooker = Bookers.Save(bookingRequest.booker);

            bookingRequest.booking.BookerID = createdBooker.BookerID;

            bookingRequest.booking.StartDate = DateTime.Parse(bookingRequest.booking.StartDateISO);
            bookingRequest.booking.EndDate = DateTime.Parse(bookingRequest.booking.EndDateISO);

            Booking createdBooking = Bookings.Save(bookingRequest.booking);

            HttpContext.Current.Response.StatusCode = 200;
            HttpContext.Current.Response.Write(new JavaScriptSerializer().Serialize(createdBooking));
            return new HttpResponseMessage();
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage AddBooker(Booker booker)
        {
            HttpContext.Current.Response.ContentType = "application/json";

            Booker createdBooker = Bookers.Save(booker);

            HttpContext.Current.Response.StatusCode = 200;
            HttpContext.Current.Response.Write(new JavaScriptSerializer().Serialize(createdBooker));
            return new HttpResponseMessage();
        }


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