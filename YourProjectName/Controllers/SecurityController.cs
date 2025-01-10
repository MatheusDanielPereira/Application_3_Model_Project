using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using YourProjectName_Domain.Entity;
using YourProjectName_Application.Service;
using YourProjectName_ViewModels;
using System.Linq;

namespace YourProjectName.Controllers
{
    public class SecurityController : BaseController
    {
        #region Area
        [CheckSessionOut(idPermission = "ADM_SEG_CADAREAUSUARIO")]
        public ActionResult AreaIndex()
        {
            List<SecurityAreaEO> lista = SecurityAreaService.List(CurrentPlant);

            var listaViewModel = Mapper.Map<IEnumerable<SecurityAreaEO>, IEnumerable<SecurityAreaViewModel>>(lista);

            return View(listaViewModel);
        }

        [CheckSessionOut(idPermission = "ADM_SEG_CADAREAUSUARIO")]
        public ActionResult AreaEditForm(int ID)
        {
            SecurityAreaEO Record = SecurityAreaService.GetByID(CurrentPlant, ID);

            var RecordViewModel = Mapper.Map<SecurityAreaEO, SecurityAreaViewModel>(Record);

            return View(RecordViewModel);
        }

        [CheckSessionOut(idPermission = "ADM_SEG_CADAREAUSUARIO")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AreaEditForm(SecurityAreaViewModel parameter)
        {
            if (ModelState.IsValid)
            {
                var EntityObject = Mapper.Map<SecurityAreaViewModel, SecurityAreaEO>(parameter);
                if (SecurityAreaService.Save(EntityObject))
                {
                    return RedirectToAction("AreaIndex");
                }
                else
                {
                    return View(parameter);
                }
            }

            return View(parameter);
        }

        [CheckSessionOut(idPermission = "ADM_SEG_CADAREAUSUARIO")]
        public ActionResult AreaRemove(int ID)
        {
            SecurityAreaService.Remove(ID);
            return RedirectToAction("AreaIndex");
        }

        public void AreasForCombo()
        {
            List<SecurityAreaEO> lista = SecurityAreaService.List(CurrentPlant);
            List<SelectListItem> listaCombo = lista.Select(i => new SelectListItem
            {
                Text = i.area_name,
                Value = i.idarea.ToString()
            }).ToList();
            ViewBag.UserAreas = listaCombo;
        }

        #endregion

        #region User
        [CheckSessionOut(idPermission = "ADM_SEG_CADUSUARIO")]
        public ActionResult UserIndex()
        {
            List<SecurityUserEO> lista = SecurityUserService.List(CurrentPlant);

            var listaViewModel = Mapper.Map<IEnumerable<SecurityUserEO>, IEnumerable<SecurityUserViewModel>>(lista);

            foreach (SecurityUserViewModel item in listaViewModel)
            {
                item.area_name = SecurityAreaService.GetByID(CurrentPlant, item.idarea).area_name;
            }

            return View(listaViewModel);
        }

        [CheckSessionOut(idPermission = "ADM_SEG_CADUSUARIO")]
        public ActionResult UserEditForm(int ID)
        {
            AreasForCombo();

            SecurityUserEO Record = SecurityUserService.GetByID(CurrentPlant, ID, true);
            var RecordViewModel = Mapper.Map<SecurityUserEO, SecurityUserViewModel>(Record);

            //Plants
            ViewBag.UsersPlants = PlantsForList(Record.UserPlants);
            ViewBag.AvailablePlants = PlantsForList(Record.AvailablePlants);

            return View(RecordViewModel);
        }

        [CheckSessionOut(idPermission = "ADM_SEG_CADUSUARIO")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserEditForm(SecurityUserViewModel parameter)
        {
            SecurityUserEO Record = SecurityUserService.GetByID(parameter.plant_id, parameter.iduser, true);

            if (ModelState.IsValid)
            {
                var EntityObject = Mapper.Map<SecurityUserViewModel, SecurityUserEO>(parameter);

                string UsersPlantsIDs = Request.Form["UsersPlantsIDs"].ToString();

                EntityObject.PlantsIDs = new List<string>();
                if ((UsersPlantsIDs.Length > 0) && (UsersPlantsIDs[0] == ','))
                {
                    UsersPlantsIDs = UsersPlantsIDs.Remove(0, 1);
                    EntityObject.PlantsIDs.AddRange(UsersPlantsIDs.Split(',').ToList());
                }

                if (SecurityUserService.Save(EntityObject))
                {
                    return RedirectToAction("UserIndex");
                }
                else
                {
                    //Plants
                    ViewBag.UsersPlants = PlantsForList(Record.UserPlants);
                    ViewBag.AvailablePlants = PlantsForList(Record.AvailablePlants);

                    return View(parameter);
                }
            }

            //Plants
            ViewBag.UsersPlants = PlantsForList(Record.UserPlants);
            ViewBag.AvailablePlants = PlantsForList(Record.AvailablePlants);

            return View(parameter);
        }

        [CheckSessionOut(idPermission = "ADM_SEG_CADUSUARIO")]
        public ActionResult UserRemove(int ID)
        {
            SecurityUserService.Remove(ID);
            return RedirectToAction("UserIndex");
        }

        public void UsersForCombo()
        {
            List<SecurityUserEO> lista = SecurityUserService.List(CurrentPlant);
            List<SelectListItem> listaCombo = lista.Select(i => new SelectListItem
            {
                Text = i.user_name,
                Value = i.iduser.ToString()
            }).ToList();
            ViewBag.Users = listaCombo;
        }

        public List<SelectListItem> UsersForList(List<SecurityUserEO> lista)
        {
            List<SelectListItem> listaCombo = lista.Select(i => new SelectListItem
            {
                Text = i.user_name +" ("+ i.iduser.ToString() + ")",
                Value = i.iduser.ToString()
            }).ToList();

            return listaCombo;
        }

        public List<SelectListItem> PlantsForList(List<PlantEO> lista)
        {
            List<SelectListItem> listaCombo = lista.Select(i => new SelectListItem
            {
                Text = i.plant_id + " - " + i.plant_description,
                Value = i.plant_id.ToString()
            }).ToList();

            return listaCombo;
        }

        #endregion

        #region Task
        public List<SelectListItem> TasksForList(List<SecurityTaskEO> lista)
        {
            List<SelectListItem> listaCombo = lista.Select(i => new SelectListItem
            {
                Text = i.idtask + " - " + i.task_description,
                Value = i.idtask
            }).ToList();

            return listaCombo;
        }

        #endregion

        #region Role
        [CheckSessionOut(idPermission = "ADM_SEG_CADPERFIL")]
        public ActionResult RoleIndex()
        {
            List<SecurityRoleEO> lista = SecurityRoleService.List(CurrentPlant);

            var listaViewModel = Mapper.Map<IEnumerable<SecurityRoleEO>, IEnumerable<SecurityRoleViewModel>>(lista);

            return View(listaViewModel);
        }

        [CheckSessionOut(idPermission = "ADM_SEG_CADPERFIL")]
        public ActionResult RoleEditForm(int ID)
        {
            SecurityRoleEO Record = SecurityRoleService.GetByID(CurrentPlant, ID, true);
            var RecordViewModel = Mapper.Map<SecurityRoleEO, SecurityRoleViewModel>(Record);

            //Tasks
            ViewBag.RoleTasks = TasksForList(Record.RoleTasks);
            ViewBag.RoleAvailableTasks = TasksForList(Record.AvailableTasks);

            //Users
            ViewBag.RoleUsers = UsersForList(Record.RoleUsers);
            ViewBag.RoleAvailableUsers = UsersForList(Record.AvailableUsers);

            return View(RecordViewModel);
        }

        [CheckSessionOut(idPermission = "ADM_SEG_CADPERFIL")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleEditForm(SecurityRoleViewModel parameter)
        {
            SecurityRoleEO Record = SecurityRoleService.GetByID(parameter.plant_id, parameter.idrole, true);

            if (ModelState.IsValid)
            {
                var EntityObject = Mapper.Map<SecurityRoleViewModel, SecurityRoleEO>(parameter);

                string RoleTasksIDs = Request.Form["RoleTasksIDs"].ToString();
                string RoleUsersIDs = Request.Form["RoleUsersIDs"].ToString();

                EntityObject.roleTasksIDs = new List<string>();
                if ((RoleTasksIDs.Length > 0) && (RoleTasksIDs[0] == ','))
                {
                    RoleTasksIDs = RoleTasksIDs.Remove(0, 1);
                    EntityObject.roleTasksIDs.AddRange(RoleTasksIDs.Split(',').ToList());
                }

                EntityObject.roleUsersIDs = new List<int>();
                if ((RoleUsersIDs.Length > 0) && (RoleUsersIDs[0] == ','))
                {
                    RoleUsersIDs = RoleUsersIDs.Remove(0, 1);
                    EntityObject.roleUsersIDs.AddRange(RoleUsersIDs.Split(',').ToList().Select(int.Parse));
                }

                if (SecurityRoleService.Save(EntityObject))
                {
                    return RedirectToAction("RoleIndex");
                }
                else
                {
                    //Tasks
                    ViewBag.RoleTasks = TasksForList(Record.RoleTasks);
                    ViewBag.RoleAvailableTasks = TasksForList(Record.AvailableTasks);

                    //Users
                    ViewBag.RoleUsers = UsersForList(Record.RoleUsers);
                    ViewBag.RoleAvailableUsers = UsersForList(Record.AvailableUsers);
                    return View(parameter);
                }
            }

            //Tasks
            ViewBag.RoleTasks = TasksForList(Record.RoleTasks);
            ViewBag.RoleAvailableTasks = TasksForList(Record.AvailableTasks);

            //Users
            ViewBag.RoleUsers = UsersForList(Record.RoleUsers);
            ViewBag.RoleAvailableUsers = UsersForList(Record.AvailableUsers);

            return View(parameter);
        }

        [CheckSessionOut(idPermission = "ADM_SEG_CADPERFIL")]
        public ActionResult RoleRemove(int ID)
        {
            SecurityRoleService.Remove(ID);
            return RedirectToAction("RoleIndex");
        }

        public void RolesForCombo()
        {
            List<SecurityRoleEO> lista = SecurityRoleService.List(CurrentPlant);
            List<SelectListItem> listaCombo = lista.Select(i => new SelectListItem
            {
                Text = i.role_description,
                Value = i.idrole.ToString()
            }).ToList();
            ViewBag.Roles = listaCombo;
        }

        #endregion

    }
}