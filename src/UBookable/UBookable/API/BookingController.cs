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
namespace UBookable.API
{
    public class BookingController : UmbracoApiController
    {

        private UBookableHelper _ubHelper = new UBookableHelper(UmbracoContext.Current);

        [AcceptVerbs("POST")]
        [HttpPost]
        public HttpResponseMessage AddBooking (Booking booking)
        {
            HttpContext.Current.Response.ContentType = "application/json";

            Booking createdBooking  = Bookings.Save(booking);

            HttpContext.Current.Response.StatusCode = 200;
            HttpContext.Current.Response.Write(new JavaScriptSerializer().Serialize(createdBooking));
            return new HttpResponseMessage();
        }

        [HttpGet]
        public HttpResponseMessage GetAllBookingsByNodeId (int nodeId)
        {
            HttpContext.Current.Response.ContentType = "application/json";
            IEnumerable<BookingResponse> allBookings = Bookings.GetByBookingsByNodeId(nodeId).Select(x => new BookingResponse
            {
                EndDate = ((IDictionary<string, dynamic>)x)["EndDate"],
                StartDate = ((IDictionary<string, dynamic>)x)["StartDate"],
                Name = ((IDictionary<string, dynamic>)x)["Name"]
            });
            HttpContext.Current.Response.StatusCode = 200;
            HttpContext.Current.Response.Write(new JavaScriptSerializer().Serialize(allBookings));
            return new HttpResponseMessage();
        }


        [HttpPost]
        public HttpResponseMessage AddBookingAndBooker(BookingRequest bookingRequest)
        {
            HttpContext.Current.Response.ContentType = "application/json";

            Booker createdBooker = Bookers.Save(bookingRequest.booker);

            bookingRequest.booking.BookerID = createdBooker.BookerID;

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

        private static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime + new TimeSpan(unixTimeStamp * 10000);
            return dtDateTime;
        }

    }
}