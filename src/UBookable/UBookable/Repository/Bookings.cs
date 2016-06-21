using UBookable.Models;
using System.Collections.Generic;
using Umbraco.Core.Persistence;
using System;

namespace UBookable.Repository
{
    public static class Bookings
    {
        public static IList<Booking> GetAll()
        {
            UmbracoDatabase db = Umbraco.Core.ApplicationContext.Current.DatabaseContext.Database;
            return db.Fetch<Booking>("SELECT * FROM UBBookings ORDER BY StartDate");
        }

        public static Page<Booking> GetAllPaged(int Page, int RecordsPerPage)
        {
            UmbracoDatabase db = Umbraco.Core.ApplicationContext.Current.DatabaseContext.Database;
            return db.Page<Booking>(Page, RecordsPerPage, "SELECT * FROM UBBookings ORDER BY StartDate");
        }

        public static IList<Booking> GetAllByBookerID(int AuthorID)
        {
            UmbracoDatabase db = Umbraco.Core.ApplicationContext.Current.DatabaseContext.Database;
            return db.Fetch<Booking>("SELECT * FROM UBBookings WHERE BookerID = @0 ORDER BY StartDate", AuthorID);
        }

        public static Page<Booking> GetAllPagedByBookerID(int Page, int RecordsPerPage, int AuthorID)
        {
            UmbracoDatabase db = Umbraco.Core.ApplicationContext.Current.DatabaseContext.Database;
            return db.Page<Booking>(Page, RecordsPerPage, "SELECT * FROM UBBookings WHERE BookerID = @0 ORDER BY StartDate", AuthorID);
        }

        public static Booking GetByBookingID(int BookingID)
        {
            UmbracoDatabase db = Umbraco.Core.ApplicationContext.Current.DatabaseContext.Database;
            List<Booking> Records = db.Fetch<Booking>("SELECT * FROM UBBookings WHERE BookingID = @0", BookingID);

            if (Records.Count > 0)
                return Records[0];
            else
                return null;
        }



        public static List<Booking> GetByBookingsByNodeIDAndDate(int BookableID, DateTime start, DateTime end)
        {
            UmbracoDatabase db = Umbraco.Core.ApplicationContext.Current.DatabaseContext.Database;
            return db.Fetch<Booking>("SELECT * FROM UBBookings WHERE NodeID = @0  AND StartDate = @1 AND EndDate = @2", BookableID, start, end);
        }

        public static IEnumerable<dynamic> GetByBookingsByNodeId(int BookableID)
        {
            UmbracoDatabase db = Umbraco.Core.ApplicationContext.Current.DatabaseContext.Database;
            return db.Query<dynamic>("SELECT UBBookings.BookingID, UBBookings.StartDate, UBBookings.EndDate, UBBookings.Approved, UBBookings.Cancelled, UBBookers.BookerID, UBBookers.Name  FROM UBBookings INNER JOIN UBBookers ON UBBookings.BookerID = UBBookers.BookerID  WHERE NodeID = @0", BookableID);
        }

        public static Booking Approve(int BookingId)
        {
            UmbracoDatabase db = Umbraco.Core.ApplicationContext.Current.DatabaseContext.Database;
            Booking updateBooking = GetByBookingID(BookingId);
            updateBooking.Approved = true;
             db.Update(updateBooking);
            return updateBooking;
        }
        public static Booking Cancel(int BookingId)
        {
            UmbracoDatabase db = Umbraco.Core.ApplicationContext.Current.DatabaseContext.Database;
            Booking updateBooking = GetByBookingID(BookingId);
            updateBooking.Cancelled = true;
            db.Update(updateBooking);
            return updateBooking;
        }
        public static Booking Save(Booking item)
        {
            UmbracoDatabase db = Umbraco.Core.ApplicationContext.Current.DatabaseContext.Database;
            db.Save(item);
            return item;
        }

        public static int DeleteByBookingID(int BookingID)
        {
            UmbracoDatabase _database = Umbraco.Core.ApplicationContext.Current.DatabaseContext.Database;
            return _database.Execute("DELETE FROM UBBookings WHERE BookingID = @0", BookingID);
        }
    }
}