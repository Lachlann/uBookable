using Umbraco.Core;
using Umbraco.Core.Persistence;
using UBookable.Models;
namespace UBookable.ApplicationEvents
{
    public class CreateTables : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            var ctx = applicationContext.DatabaseContext;
            var db = new DatabaseSchemaHelper(ctx.Database, applicationContext.ProfilingLogger.Logger, ctx.SqlSyntax);

            //if (!db.TableExist("UBBookers"))
            //{
            //    db.CreateTable<Booker>(false);
            //}

            if (!db.TableExist("UBBookings"))
            {
                db.CreateTable<Booking>(false);
            }


        }
    }
}