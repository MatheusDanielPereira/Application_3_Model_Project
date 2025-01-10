using System;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using YourProjectName_Domain.Helpers;

namespace YourProjectName.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;

            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ?
                        Request.UserLanguages[0] :  // obtain it from HTTP header AcceptLanguages
                        null;
            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe

            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
        }

        protected string CurrentDateFormat
        {
            get
            {
                HttpCookie datepickerformat = Request.Cookies["_datepickerformat"];
                return datepickerformat.Value.Replace("yy", "yyyy").Replace("mm", "MM");
            }
        }

        protected string CurrentPlant
        {
            get
            {
                return "BR03";//Session["UserPlant"].ToString();
            }
        }

        protected DateTime CurrentPlantDateTime
        {
            get
            {
                DateTime utcTime = DateTime.UtcNow;
                string PlantTimeZone = "E. South America Standard Time"; //(GMT-03:00) Brasilia
                //When this is information is not set on Plant database, considers BR03-Sorocaba time
                if (Session["UserPlantTimeZone"] != null)
                {
                    PlantTimeZone = Session["UserPlantTimeZone"].ToString();
                    //TimeZone Ids are available at https://support.microsoft.com/en-us/help/973627/microsoft-time-zone-index-values
                }
                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById(PlantTimeZone);
                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local

                return localTime;
            }
        }
    }
}