using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using YourProjectName_Data.Context;
using YourProjectName_Domain.Entity;

namespace YourProjectName_Data.Repository
{
    public class SecurityRoleRepo
    {
        /// <summary>
        /// Inserts or Updates a record
        /// </summary>
        /// <param name="EntityObject"></param>
        /// <returns></returns>
        public static bool Save(SecurityRoleEO EntityObject)
        {
            using (var context = new DTB_ICMS_CREDIT_DEVEntities())
            {
                var original = (from p in context.security_role
                                where p.idrole == EntityObject.idrole
                                select p).SingleOrDefault();

                var Tasks = (from p in context.security_task
                             where EntityObject.roleTasksIDs.Contains(p.idtask)
                             select p).ToList();
                var Users = (from p in context.security_user
                             where EntityObject.roleUsersIDs.Contains(p.iduser)
                             select p).ToList();

                if (original == null)
                {
                    security_role newObj = new security_role();
                    newObj.role_description = EntityObject.role_description;
                    newObj.plant_id = EntityObject.plant_id;
                    newObj.security_task = Tasks;
                    newObj.security_user = Users;
                    context.security_role.Add(newObj);
                }
                else
                {
                    original.role_description = EntityObject.role_description;
                    original.plant_id = EntityObject.plant_id;
                    original.security_task.Clear();
                    original.security_task = Tasks;
                    original.security_user.Clear();
                    original.security_user = Users;
                }
                context.SaveChanges();

                return true;
            }
        }

        /// <summary>
        /// Loads a list of entity
        /// </summary>
        /// <param name="PlantID"></param>
        /// <returns></returns>
        public static List<security_role> List(string PlantID)
        {
            using (var context = new DTB_ICMS_CREDIT_DEVEntities())
            {
                var Dados = (from p in context.security_role
                             where p.plant_id == PlantID
                             orderby p.role_description
                             select p).ToList();

                return Dados;
            }
        }

        /// <summary>
        /// Removes the specified record
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool Remove(int ID)
        {
            using (var context = new DTB_ICMS_CREDIT_DEVEntities())
            {
                try
                {
                    var Record = (from p in context.security_role
                                  where p.idrole == ID
                                  select p).SingleOrDefault();
                    Record.security_task.Clear();
                    Record.security_user.Clear();
                    context.security_role.Attach(Record);
                    context.security_role.Remove(Record);
                    context.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;

                }
            }
        }

        /// <summary>
        /// Loads a specific record. Additional info means Tasks and Users lists related or not with the role
        /// </summary>
        /// <param name="PlantID"></param>
        /// <param name="ID"></param>
        /// <param name="AdditionalInfo"></param>
        /// <returns></returns>
        public static SecurityRoleEO GetByID(string PlantID, int ID, bool AdditionalInfo)
        {
            using (var context = new DTB_ICMS_CREDIT_DEVEntities())
            {
                var Record = (from p in context.security_role
                              where p.idrole == ID
                              && p.plant_id == PlantID
                              select p).SingleOrDefault();

                SecurityRoleEO Role;

                if (Record == null)
                {
                    Role = new SecurityRoleEO { idrole = 0, plant_id = PlantID };
                    Role.RoleTasks = new List<SecurityTaskEO>();
                    var AvailableTasks = (from p in context.security_task
                                          select p).ToList();
                    Role.AvailableTasks = Mapper.Map<IEnumerable<security_task>, IEnumerable<SecurityTaskEO>>(AvailableTasks).ToList();
                    Role.RoleUsers = new List<SecurityUserEO>();
                    var AvailableUsers = (from p in context.security_user
                                          orderby p.user_name
                                          select p).ToList();
                    Role.AvailableUsers = Mapper.Map<IEnumerable<security_user>, IEnumerable<SecurityUserEO>>(AvailableUsers).ToList();
                }
                else
                {
                    Role = Mapper.Map<security_role, SecurityRoleEO>(Record);

                    if (AdditionalInfo)
                    {
                        //Loads Tasks lists
                        Role.RoleTasks = Mapper.Map<IEnumerable<security_task>, IEnumerable<SecurityTaskEO>>(Record.security_task.ToList()).ToList();
                        var AllTasks = (from p in context.security_task
                                        select p).ToList();
                        var AvailableTasks = (from p in AllTasks
                                              where !Record.security_task.Contains(p)
                                              select p).ToList();
                        Role.AvailableTasks = Mapper.Map<IEnumerable<security_task>, IEnumerable<SecurityTaskEO>>(AvailableTasks).ToList();

                        //Loads Users lists
                        Role.RoleUsers = Mapper.Map<IEnumerable<security_user>, IEnumerable<SecurityUserEO>>(Record.security_user.OrderBy(o => o.user_name).ToList()).ToList();
                        var AllUsers = (from p in context.security_user
                                        orderby p.user_name
                                        select p).ToList();
                        var AvailableUsers = (from p in AllUsers
                                              where !Record.security_user.Contains(p)
                                              orderby p.user_name
                                              select p).ToList();
                        Role.AvailableUsers = Mapper.Map<IEnumerable<security_user>, IEnumerable<SecurityUserEO>>(AvailableUsers).ToList();
                    }
                }

                return Role;
            }
        }


        /// <summary>
        /// Get a list of all user permissions
        /// </summary>
        /// <param name="useremail"></param>
        /// <param name="plant_id"></param>
        /// <param name="multiple_plants"></param>
        /// <param name="Plants"></param>
        /// <returns></returns>
        public static List<string> GetUserPermissions(string useremail, out string plant_id, out bool multiple_plants, out List<PlantEO> Plants)
        {
            plant_id = "";
            multiple_plants = false;
            Plants = new List<PlantEO>();

            using (var context = new DTB_ICMS_CREDIT_DEVEntities())
            {
                var User = (from p in context.security_user
                            where p.user_email == useremail
                            select p).SingleOrDefault();

                List<string> Permissions = new List<string>();

                if (User != null)
                {
                    plant_id = User.plant_id;
                    multiple_plants = User.plant1.Count > 0;

                    var Data = SecurityUserRepo.GetByID(plant_id, User.iduser, true);
                    Plants = Data.UserPlants;

                    foreach (security_role role in User.security_role)
                    {
                        foreach (security_task task in role.security_task)
                        {
                            Permissions.Add(task.idtask);
                        }
                    }
                }
                return Permissions;
            }
        }

    }
}
