using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YourProjectName_ViewModels;
using YourProjectName_Application.Service;
using YourProjectName_Domain.Entity;
using System.Threading.Tasks;
using MetsoFramework.Utils;
using System.Web.Security;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using System.Globalization;
using AutoMapper;
using YourProjectName_Domain.Helpers;
using YourProjectName_Resources;

namespace YourProjectName.Controllers
{
    public class HomeController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Menu()
        {
            string Idioma = "pt-br";

            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
            {
                Idioma = cookie.Value;
            }
            else
            {
                Idioma = CultureInfo.CurrentCulture.IetfLanguageTag;
            }
            WriteCultureCookies(Request, Idioma);
            switch (Idioma)
            {
                case "pt-br": Idioma = "Português (" + Idioma.ToUpper() + ")"; break;
                case "en": Idioma = "English (" + Idioma.ToUpper() + ")"; break;
                //case "zh-CN": Idioma = "中文 (" + Idioma.ToUpper() + ")"; break;
                //case "cs-CZ": Idioma = "Česky (" + Idioma.ToUpper() + ")"; break;
            }
            ViewBag.Language = Idioma;

            return PartialView();
        }

        private void WriteCultureCookies(HttpRequestBase Request, string culture)
        {
            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
            {
                if (culture.Trim().Length > 0)
                {
                    cookie.Value = culture; //update cookie value
                }
                else
                {
                    if (cookie.Value.Trim().Length == 0)
                    {
                        cookie.Value = CultureHelper.GetImplementedCulture(culture); //update cookie value
                    }
                }
            }
            else
            {
                if (culture.Trim().Length == 0)
                {
                    culture = CultureHelper.GetImplementedCulture(culture);
                }
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }

            Response.Cookies.Add(cookie);

            // Save datepickerformat in a cookie
            HttpCookie datepickerformat = Request.Cookies["_datepickerformat"];
            if (datepickerformat != null)
            {
                if (culture.ToLower() == "en")
                {
                    datepickerformat.Value = "mm/dd/yy";   // update cookie value
                }
                else
                {
                    datepickerformat.Value = "dd/mm/yy";   // update cookie value
                }
            }
            else
            {
                datepickerformat = new HttpCookie("_datepickerformat");
                if (culture.ToLower() == "en")
                {
                    datepickerformat.Value = "mm/dd/yy";   // update cookie value
                }
                else
                {
                    datepickerformat.Value = "dd/mm/yy";   // update cookie value
                }
                datepickerformat.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(datepickerformat);

            // Save thousandseparator in a cookie
            HttpCookie thousandseparator = Request.Cookies["_thousandseparator"];
            if (thousandseparator != null)
            {
                if (culture.ToLower() == "en")
                {
                    thousandseparator.Value = ",";   // update cookie value
                }
                else
                {
                    thousandseparator.Value = ".";   // update cookie value
                }
            }
            else
            {
                thousandseparator = new HttpCookie("_thousandseparator");
                if (culture.ToLower() == "en")
                {
                    thousandseparator.Value = ",";   // update cookie value
                }
                else
                {
                    thousandseparator.Value = ".";   // update cookie value
                }
                thousandseparator.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(thousandseparator);

        }

        [AllowAnonymous]
        //[HttpPost]
        public ActionResult SetCulture(string culture)
        {
            WriteCultureCookies(Request, culture);

            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

        public ActionResult SetPlant(string ID)
        {
            SetPlantSettings(ID);

            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

        private void SetPlantSettings(string PlantID)
        {
            Session["UserPlant"] = PlantID;
            PlantEO Plant = PlantService.GetByID(PlantID);
            Session["UserPlantTimeZone"] = Plant.plant_time_zone;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string ReturnUrl)
        {
            try
            {
                string LogonName = "";
                string[] InfoUsers;
                int MaximumLoginAttempts = 3;

                if (ModelState.IsValid)
                {
                    LoginEO login = (LoginEO)Session["LoginAttempts"];
                    if (login == null)
                    {
                        login = new LoginEO();
                    }

                    if ((login.Attempts == MaximumLoginAttempts) && ((DateTime.Now - login.LastBlock).TotalMinutes < 5))
                    {
                        Session["LoginError"] = Resources.ExceededLoginAttempts;
                    }
                    else
                    {
                        if ((login.Attempts == MaximumLoginAttempts) && ((DateTime.Now - login.LastBlock).TotalMinutes > 5))
                        {
                            login.Attempts = 0;
                        }

                        if (Uteis.ValidateUser(model.UserName, model.Password, out LogonName, out InfoUsers))
                        {
                            string plant_id = "";
                            bool multiple_plants = false;
                            List<PlantEO> Plants;
                            List<string> Permissions = SecurityRoleService.GetUserPermissions(model.UserName, out plant_id, out multiple_plants, out Plants);

                            if (Permissions.Count > 0)
                            {
                                Session["LogonName"] = LogonName;
                                Session["UserName"] = InfoUsers[0];
                                Session["Permissions"] = Permissions;

                                SetPlantSettings(plant_id);

                                Session["UserMultiplePlant"] = multiple_plants;
                                Session["UserAvailablePlants"] = Mapper.Map<IEnumerable<PlantEO>, IEnumerable<PlantViewModel>>(Plants).ToList();

                                FormsAuthentication.SetAuthCookie(model.UserName, false);
                                Autenticar(model.UserName);

                                String UrlToGO = HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["ReturnUrl"];

                                String decodeURL = String.Empty;

                                if (!String.IsNullOrEmpty(UrlToGO))
                                {
                                    decodeURL = Server.UrlDecode(UrlToGO);

                                    if (Url.IsLocalUrl(decodeURL))
                                    {
                                        return Redirect(decodeURL);
                                    }
                                    else
                                    {
                                        return RedirectToAction("MainPage", "Home");
                                    }
                                }
                                else
                                {
                                    WriteCultureCookies(Request, "");
                                    return RedirectToAction("MainPage", "Home");
                                }
                            }
                            else
                            {
                                ViewBag.ErrorMessage = Resources.UserHasNoPermission;
                            }
                        }
                        else
                        {
                            login.Attempts = login.Attempts + 1;
                            Session["LoginError"] = Resources.InvalidLogin;

                            ViewBag.ErrorMessage = Resources.InvalidLogin;
                        }

                        if (login.Attempts == MaximumLoginAttempts)
                        {
                            login.LastBlock = DateTime.Now;
                            Session["LoginError"] = Resources.ExceededLoginAttempts;
                        }
                    }

                    //Saves current status of Login Attempts
                    Session["LoginAttempts"] = login;

                    return View("Index", model);
                }
                else
                {
                    return View("Index", model);
                }
            }
            catch (Exception ex)
            {
                string ErrorMessage = Resources.InvalidLogin + " - " + ErrorHelper.GetAllErrorMessages(ex, false);
                ViewBag.ErrorMessage = ErrorMessage.Replace("'", "").Replace("\r\n", string.Empty);
                return View("Index", model);
            }
        }

        private void Autenticar(string user_login)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user_login));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user_login));

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            Request.GetOwinContext().Authentication.SignIn(identity);
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            Request.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            Session.Clear();
            Session.Abandon();

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            Session["UserPlant"] = "";
            Session["UserPlantTimeZone"] = "";
            Session["UserMultiplePlant"] = false;

            return RedirectToAction("Index", "Home");
        }

        [CheckSessionOut(idPermission = "MAIN_SCREEN")]
        public ActionResult MainPage()
        {
            //Random data. It can came from a stored procedure for example.
            #region Loads chart data
            List<ChartDataEO> lista = new List<ChartDataEO>();
            Random a = new Random();
            lista.Add(new ChartDataEO
            {
                ChartType = "TYPE1",
                Label = "Place 1",
                Value = a.Next()
            });
            lista.Add(new ChartDataEO
            {
                ChartType = "TYPE1",
                Label = "Place 2",
                Value = a.Next()
            });
            lista.Add(new ChartDataEO
            {
                ChartType = "TYPE1",
                Label = "Place 3",
                Value = a.Next()
            });
            lista.Add(new ChartDataEO
            {
                ChartType = "TYPE2",
                Label = "Place 1",
                Value = a.Next()
            });
            lista.Add(new ChartDataEO
            {
                ChartType = "TYPE2",
                Label = "Place 2",
                Value = a.Next()
            });
            lista.Add(new ChartDataEO
            {
                ChartType = "TYPE2",
                Label = "Place 3",
                Value = a.Next()
            });
            lista.Add(new ChartDataEO
            {
                ChartType = "TYPE3",
                Label = "Place 1",
                Value = a.Next()
            });
            lista.Add(new ChartDataEO
            {
                ChartType = "TYPE3",
                Label = "Place 2",
                Value = a.Next()
            });
            lista.Add(new ChartDataEO
            {
                ChartType = "TYPE3",
                Label = "Place 3",
                Value = a.Next()
            });
            lista.Add(new ChartDataEO
            {
                ChartType = "TYPE4",
                Label = "Place 1",
                Value = a.Next()
            });
            lista.Add(new ChartDataEO
            {
                ChartType = "TYPE4",
                Label = "Place 2",
                Value = a.Next()
            });
            lista.Add(new ChartDataEO
            {
                ChartType = "TYPE4",
                Label = "Place 3",
                Value = a.Next()
            });
            #endregion

            #region Type1 Chart
            List<string> Type1ChartLabels = new List<string>();
            List<double> Type1ChartValues = new List<double>();
            List<string> Type1ChartColors = new List<string>();

            List<ChartDataEO> Moldedlista = lista.Where(w => w.ChartType == "TYPE1").ToList();
            Type1ChartLabels.AddRange(Moldedlista.Select(s => s.Label));
            Type1ChartValues.AddRange(Moldedlista.Select(s => (double)s.Value));
            Type1ChartColors.AddRange(Moldedlista.Select(s => "rgba(0, 102, 255, 0.5)"));//blue

            ViewBag.Type1ChartLabels = Type1ChartLabels;
            ViewBag.Type1ChartValues = Type1ChartValues;
            ViewBag.Type1ChartColors = Type1ChartColors;
            #endregion

            #region Type2 Chart
            List<string> Type2ChartLabels = new List<string>();
            List<double> Type2ChartValues = new List<double>();
            List<string> Type2ChartColors = new List<string>();

            List<ChartDataEO> Type2lista = lista.Where(w => w.ChartType == "TYPE2").ToList();
            Type2ChartLabels.AddRange(Type2lista.Select(s => s.Label));
            Type2ChartValues.AddRange(Type2lista.Select(s => (double)s.Value));
            Type2ChartColors.AddRange(Type2lista.Select(s => "rgba(255, 0, 0, 0.5)"));//red

            ViewBag.Type2ChartLabels = Type2ChartLabels;
            ViewBag.Type2ChartValues = Type2ChartValues;
            ViewBag.Type2ChartColors = Type2ChartColors;
            #endregion

            #region Type3 Chart
            List<string> Type3ChartLabels = new List<string>();
            List<double> Type3ChartValues = new List<double>();
            List<string> Type3ChartColors = new List<string>();

            List<ChartDataEO> Type3lista = lista.Where(w => w.ChartType == "TYPE3").ToList();
            Type3ChartLabels.AddRange(Type3lista.Select(s => s.Label));
            Type3ChartValues.AddRange(Type3lista.Select(s => (double)s.Value));
            Type3ChartColors.AddRange(Type3lista.Select(s => "rgba(0, 153, 0, 0.5)"));//green

            ViewBag.Type3ChartLabels = Type3ChartLabels;
            ViewBag.Type3ChartValues = Type3ChartValues;
            ViewBag.Type3ChartColors = Type3ChartColors;
            #endregion

            #region Type4 Chart
            List<string> Type4ChartLabels = new List<string>();
            List<double> Type4ChartValues = new List<double>();
            List<string> Type4ChartColors = new List<string>();

            List<ChartDataEO> Type4lista = lista.Where(w => w.ChartType == "TYPE4").ToList();
            Type4ChartLabels.AddRange(Type4lista.Select(s => s.Label));
            Type4ChartValues.AddRange(Type4lista.Select(s => (double)s.Value));
            Type4ChartColors.Add("rgba(255, 0, 0, 0.5)");//red
            Type4ChartColors.Add("rgba(255, 153, 0, 0.5)");//orange
            Type4ChartColors.Add("rgba(0, 153, 0, 0.5)");//green

            ViewBag.Type4ChartLabels = Type4ChartLabels;
            ViewBag.Type4ChartValues = Type4ChartValues;
            ViewBag.Type4ChartColors = Type4ChartColors;
            #endregion

            string DashboardRefreshInterval = ParameterService.GetByName(CurrentPlant, "DashboardRefreshInterval");
            if (DashboardRefreshInterval.Equals(""))
            {
                DashboardRefreshInterval = "60";
            }

            Response.AddHeader("Refresh", DashboardRefreshInterval);//Refreshs page in specified seconds

            return View();
        }

        public ActionResult NoAccess()
        {
            return View();
        }
    }
}