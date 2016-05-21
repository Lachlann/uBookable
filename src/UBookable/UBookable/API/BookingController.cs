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
        [HttpPost]
        public HttpResponseMessage AddBooking (Booking booking)
        {
            HttpContext.Current.Response.ContentType = "application/json";

            Bookings.Save(booking);

            HttpContext.Current.Response.StatusCode = 200;
            HttpContext.Current.Response.Write(new JavaScriptSerializer().Serialize("{Result: Success}"));
            return new HttpResponseMessage();
        }
    }
}