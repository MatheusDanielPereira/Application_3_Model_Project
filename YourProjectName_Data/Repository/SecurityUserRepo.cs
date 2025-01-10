using AutoMapper;
using System.Collections.Generic;
using System.Linq;

using YourProjectName_Data.Context;
using YourProjectName_Domain.Entity;

namespace YourProjectName_Data.Repository
{
    public class SecurityUserRepo
    {
        /// <summary>
        /// Inserts or Updates a record
        /// </summary>
        /// <param name="EntityObject"></param>
        /// <returns></returns>
        public static bool Save(SecurityUserEO EntityObject)
        {
            using (var context = new DTB_ICMS_CREDIT_DEVEntities())
            {
                var original = (from p in context.security_user
                                where p.iduser == EntityObject.iduser
                                select p).SingleOrDefault();

                var Users = (from p in context.plant
                             where EntityObject.PlantsIDs.Contains(p.plant_id)
                             select p).ToList();

                if (original == null)
                {
                    security_user newObj = new security_user();
                    newObj.user_name = EntityObject.user_name;
                    newObj.user_email = EntityObject.user_email;
                    newObj.idarea = EntityObject.idarea;
                    newObj.plant_id = EntityObject.plant_id;
                    newObj.plant1 = Users;
                    context.security_user.Add(newObj);
                }
                else
                {
                    original.user_name = EntityObject.user_name;
                    original.user_email = EntityObject.user_email;
                    original.idarea = EntityObject.idarea;
                    original.plant_id = EntityObject.plant_id;
                    original.plant1.Clear();
                    original.plant1 = Users;
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
        public static List<SecurityUserEO> List(string PlantID)
        {
            using (var context = new DTB_ICMS_CREDIT_DEVEntities())
            {
                var Dados = (from p in context.security_user
                             where p.plant_id == PlantID
                             orderby p.user_name
                             select new SecurityUserEO
                             {
                                 iduser = p.iduser,
                                 user_name = p.user_name,
                                 user_email = p.user_email,
                                 idarea = p.idarea,
                                 plant_id = p.plant_id,
                                 multiple_plants = p.plant1.Count > 0
                             }).ToList();

                return Dados;
            }
        }

        /// <summary>
        /// Removes the specified record
        /// </summary>
        /// <param name="ID"></param>
        public static void Remove(int ID)
        {
            using (var context = new DTB_ICMS_CREDIT_DEVEntities())
            {
                context.sp_User_Delete(ID);
            }
        }

        /// <summary>
        /// Loads a specific record. Additional info means Plants lists related or not with the user
        /// </summary>
        /// <param name="PlantID"></param>
        /// <param name="ID"></param>
        /// <param name="AdditionalInfo"></param>
        /// <returns></returns>
        public static SecurityUserEO GetByID(string PlantID, int ID, bool AdditionalInfo)
        {
            using (var context = new DTB_ICMS_CREDIT_DEVEntities())
            {
                var Record = (from p in context.security_user
                              where p.iduser == ID
                              && p.plant_id == PlantID
                              select p).SingleOrDefault();

                SecurityUserEO User;

                if (Record == null)
                {
                    User = new SecurityUserEO { iduser = 0, plant_id = PlantID };
                    User.UserPlants = new List<PlantEO>();
                    var AvailablePlants = (from p in context.plant
                                           select p).ToList();
                    User.AvailablePlants = Mapper.Map<IEnumerable<plant>, IEnumerable<PlantEO>>(AvailablePlants).ToList();
                }
                else
                {
                    User = Mapper.Map<security_user, SecurityUserEO>(Record);

                    if (AdditionalInfo)
                    {
                        //Loads Plants lists
                        User.UserPlants = Mapper.Map<IEnumerable<plant>, IEnumerable<PlantEO>>(Record.plant1.ToList()).ToList();
                        var AllPlants = (from p in context.plant
                                         select p).ToList();
                        var AvailablePlants = (from p in AllPlants
                                               where !Record.plant1.Contains(p)
                                               select p).ToList();
                        User.AvailablePlants = Mapper.Map<IEnumerable<plant>, IEnumerable<PlantEO>>(AvailablePlants).ToList();
                    }
                }

                return User;
            }
        }

    }
}
