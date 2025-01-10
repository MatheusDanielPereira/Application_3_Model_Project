using System.Collections.Generic;
using System.Linq;

using YourProjectName_Data.Context;
using YourProjectName_Domain.Entity;

namespace YourProjectName_Data.Repository
{
    public static class PlantRepo
    {
        /// <summary>
        /// Inserts or Updates a record
        /// </summary>
        /// <param name="EntityObject"></param>
        /// <returns></returns>
        public static bool Save(PlantEO EntityObject)
        {
            using (var context = new DTB_ICMS_CREDIT_DEVEntities())
            {
                var original = (from p in context.plant
                                where p.plant_id == EntityObject.plant_id
                                select p).SingleOrDefault();

                if (original == null)
                {
                    plant newObj = new plant();
                    newObj.plant_id = EntityObject.plant_id;
                    newObj.plant_description = EntityObject.plant_description;
                    newObj.plant_flag = EntityObject.plant_flag;
                    newObj.plant_time_zone = EntityObject.plant_time_zone;
                    context.plant.Add(newObj);
                }
                else
                {
                    original.plant_description = EntityObject.plant_description;
                    if (EntityObject.plant_flag != null)
                    {
                        original.plant_flag = EntityObject.plant_flag;
                    }
                    original.plant_time_zone = EntityObject.plant_time_zone;
                }
                context.SaveChanges();

                return true;
            }
        }

        /// <summary>
        /// Loads a list of entity
        /// </summary>
        /// <returns></returns>
        public static List<plant> List()
        {
            using (var context = new DTB_ICMS_CREDIT_DEVEntities())
            {
                var Dados = (from p in context.plant
                             orderby p.plant_description
                             select p).ToList();

                return Dados;
            }
        }

        /// <summary>
        /// Removes the specified record
        /// </summary>
        /// <param name="PlantID"></param>
        /// <returns></returns>
        public static bool Remove(string PlantID)
        {
            using (var context = new DTB_ICMS_CREDIT_DEVEntities())
            {
                try
                {
                    var newObj = new plant { plant_id = PlantID };
                    context.plant.Attach(newObj);
                    context.plant.Remove(newObj);
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
        /// <returns></returns>
        public static plant GetByID(string PlantID)
        {
            using (var context = new DTB_ICMS_CREDIT_DEVEntities())
            {
                var Record = (from p in context.plant
                              where p.plant_id == PlantID
                              select p).SingleOrDefault();

                if (Record == null)
                {
                    Record = new plant { plant_id = "" };
                }
                return Record;
            }
        }

        /// <summary>
        /// Loads the desired picture based on Type parameter. 0-Flag;1-FloorPlant
        /// </summary>
        /// <param name="PlantID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static byte[] GetPicture(string PlantID, int Type)
        {
            byte[] fileBytes = null;

            using (var context = new DTB_ICMS_CREDIT_DEVEntities())
            {
                var Record = (from p in context.plant
                              where p.plant_id == PlantID
                              select p).FirstOrDefault();

                if (Record != null)
                {
                    switch (Type)
                    {
                        case 0:
                            fileBytes = Record.plant_flag;
                            break;
                        //case 1:
                        //    fileBytes = Record.plant_floor_plant;
                        //    break;
                    }

                }

                return fileBytes;
            }
        }
    }
}
