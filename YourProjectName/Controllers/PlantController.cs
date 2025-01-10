using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using YourProjectName_Domain.Entity;
using YourProjectName_Application.Service;
using YourProjectName_ViewModels;
using System.Web.Helpers;
using System.IO;

namespace YourProjectName.Controllers
{
    public class PlantController : BaseController
    {
        [CheckSessionOut(idPermission = "ADM_CADPLANT")]
        public ActionResult Index()
        {
            List<PlantEO> lista = PlantService.List();

            var listaViewModel = Mapper.Map<IEnumerable<PlantEO>, IEnumerable<PlantViewModel>>(lista);

            return View(listaViewModel);
        }

        [CheckSessionOut(idPermission = "ADM_CADPLANT")]
        public ActionResult EditForm(string PlantID)
        {
            PlantEO Record = PlantService.GetByID(PlantID);

            var RecordViewModel = Mapper.Map<PlantEO, PlantViewModel>(Record);

            return View(RecordViewModel);
        }

        [CheckSessionOut(idPermission = "ADM_CADPLANT")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditForm(PlantViewModel parameter)
        {
            if (ModelState.IsValid)
            {
                var EntityObject = Mapper.Map<PlantViewModel, PlantEO>(parameter);

                if (parameter.plant_flag_load != null && parameter.plant_flag_load.ContentLength > 0)
                {
                    WebImage img = new WebImage(parameter.plant_flag_load.InputStream).Clone().Resize(48, 48, true, true);

                    EntityObject.plant_flag = img.GetBytes();
                }

                if (PlantService.Save(EntityObject))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(parameter);
                }
            }

            return View(parameter);
        }

        //86400 means seconds correspondents to one day. 24 hours.
        //3600 means seconds correspondents to one hours.
        [OutputCache(Duration = 86400)]
        public ActionResult GetImage(string PlantID, int Type)
        {
            byte[] tagimage = PlantService.GetPicture(PlantID, Type);

            string ext = "jpg";
            string exttype = "jpeg";

            switch (Type)
            {
                case 0:
                    ext = "png";
                    exttype = "png";
                    break;
                case 1:
                    ext = "jpg";
                    exttype = "jpeg";
                    break;
            }

            if (tagimage == null)
            {
                //Treatment for loading PictureNotAvailable default image
                string path = Server.MapPath("~/Content/images/PictureNotAvailable.jpg");
                tagimage = System.IO.File.ReadAllBytes(path);
            }

            FileStreamResult result = new FileStreamResult(new MemoryStream(tagimage), "image/" + exttype);
            result.FileDownloadName = /*DateTime.Now.ToString("yyyyMMddHHmmss") +*/ PlantID + Type + "." + ext;
            return result;
        }

        [CheckSessionOut(idPermission = "ADM_CADPLANT")]
        public ActionResult Remove(string PlantID)
        {
            PlantService.Remove(PlantID);
            return RedirectToAction("Index");
        }
    }
}
