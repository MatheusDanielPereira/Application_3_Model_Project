using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using YourProjectName_Domain.Entity;
using YourProjectName_Application.Service;
using YourProjectName_ViewModels;

namespace YourProjectName.Controllers
{
    public class ParameterController : BaseController
    {
        [CheckSessionOut(idPermission = "ADM_CADPARAM")]
        public ActionResult Index()
        {
            List<ParameterEO> lista = ParameterService.List(CurrentPlant);

            var listaViewModel = Mapper.Map<IEnumerable<ParameterEO>, IEnumerable<ParameterViewModel>>(lista);

            return View(listaViewModel);
        }

        [CheckSessionOut(idPermission = "ADM_CADPARAM")]
        public ActionResult EditForm(int ID)
        {
            ParameterEO Record = ParameterService.GetByID(CurrentPlant, ID);

            var RecordViewModel = Mapper.Map<ParameterEO, ParameterViewModel>(Record);

            return View(RecordViewModel);
        }

        [CheckSessionOut(idPermission = "ADM_CADPARAM")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditForm(ParameterViewModel parameter)
        {
            if (ModelState.IsValid)
            {
                var EntityObject = Mapper.Map<ParameterViewModel, ParameterEO>(parameter);
                if (ParameterService.Save(EntityObject))
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

        [CheckSessionOut(idPermission = "ADM_CADPARAM")]
        public ActionResult Remove(int ID)
        {
            ParameterService.Remove(ID);
            return RedirectToAction("Index");
        }
    }
}
