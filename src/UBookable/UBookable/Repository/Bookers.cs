using UBookable.Models;
using System.Collections.Generic;
using Umbraco.Core.Persistence;


namespace UBookable.Repository
{
    public static class Bookers
    {
        public static IList<Booker> GetAll()
        {
            UmbracoDatabase db = Umbraco.Core.ApplicationContext.Current.DatabaseContext.Database;
            return db.Fetch<Booker>("SELECT * FROM UBBookers ORDER BY Name");
        }

        public static Page<Booker> GetAllPaged(int Page, int RecordsPerPage)
        {
            UmbracoDatabase db = Umbraco.Core.ApplicationContext.Current.DatabaseContext.Database;
            return db.Page<Booker>(Page, RecordsPerPage, "SELECT * FROM UBBookers ORDER BY Name");
        }


        public static Booker GetByBookerD(int BookerID)
        {
            UmbracoDatabase db = Umbraco.Core.ApplicationContext.Current.DatabaseContext.Database;
            List<Booker> Records = db.Fetch<Booker>("SELECT * FROM UBBookers WHERE BookerID = @0", BookerID);

            if (Records.Count > 0)
                return Records[0];
            else
                return null;
        }

        public static Booker Save(Booker item)
        {
            UmbracoDatabase db = Umbraco.Core.ApplicationContext.Current.DatabaseContext.Database;
            db.Save(item);
            return item;
        }

        public static int DeleteByBookerID(int BookerID)
        {
            UmbracoDatabase _database = Umbraco.Core.ApplicationContext.Current.DatabaseContext.Database;
            return _database.Execute("DELETE FROM UBBookers WHERE BookerID = @0", BookerID);
        }
    }
}