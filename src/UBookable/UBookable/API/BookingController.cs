using Umbraco.Web.WebApi;
using System.Net.Http;
using System.Web;
using System.Web.Script.Serialization;
using UBookable.Models;
using UBookable.Repository;
using System.Web.Http;

namespace UBookable.API
{
    public class BookingController : UmbracoApiController
    {
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

        [AcceptVerbs("POST")]
        public HttpResponseMessage AddBooker(Booker booker)
        {
            HttpContext.Current.Response.ContentType = "application/json";

            Booker createdBooker = Bookers.Save(booker);

            HttpContext.Current.Response.StatusCode = 200;
            HttpContext.Current.Response.Write(new JavaScriptSerializer().Serialize(createdBooker));
            return new HttpResponseMessage();
        }

    }
}