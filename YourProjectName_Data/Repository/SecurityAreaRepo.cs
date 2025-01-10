using AutoMapper;
using System.Collections.Generic;
using System.Linq;

using YourProjectName_Data.Context;
using YourProjectName_Domain.Entity;

namespace YourProjectName_Data.Repository
{
    public static class SecurityAreaRepo
    {
        /// <summary>
        /// Inserts or Updates a record
        /// </summary>
        /// <param name="EntityObject"></param>
        /// <returns></returns>
        public static bool Save(SecurityAreaEO EntityObject)
        {
            using (var context = new DTB_ICMS_CREDIT_DEVEntities())
            {
                var original = (from p in context.security_area
                                where p.idarea == EntityObject.idarea
                                select p).SingleOrDefault();

                if (original == null)
                {
                    security_area newObj = Mapper.Map<SecurityAreaEO, security_area>(EntityObject);
                    context.security_area.Add(newObj);
                }
                else
                {
                    Mapper.Map(EntityObject, original);
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
        public static List<security_area> List(string PlantID)
        {
            using (var context = new DTB_ICMS_CREDIT_DEVEntities())
            {
                var Dados = (from p in context.security_area
                             where p.plant_id == PlantID
                             orderby p.area_name
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
                    var newObj = new security_area { idarea = ID };
                    context.security_area.Attach(newObj);
                    context.security_area.Remove(newObj);
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
        /// Loads a specific record
        /// </summary>
        /// <param name="PlantID"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static security_area GetByID(string PlantID, int ID)
        {
            using (var context = new DTB_ICMS_CREDIT_DEVEntities())
            {
                var Record = (from p in context.security_area
                              where p.idarea == ID
                              && p.plant_id == PlantID
                              select p).SingleOrDefault();

                if (Record == null)
                {
                    Record = new security_area { idarea = 0, plant_id = PlantID };
                }
                return Record;
            }
        }
    }
}
