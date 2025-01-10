using System.Collections.Generic;
using System.Linq;

using YourProjectName_Data.Context;
using YourProjectName_Domain.Entity;

namespace YourProjectName_Data.Repository
{
    public static class ParameterRepo
    {
        /// <summary>
        /// Inserts or Updates a record
        /// </summary>
        /// <param name="EntityObject"></param>
        /// <returns></returns>
        public static bool Save(ParameterEO EntityObject)
        {
            using (var context = new DTB_ICMS_CREDIT_DEVEntities())
            {
                var original = (from p in context.parameter
                                where p.idparameter == EntityObject.idparameter
                                select p).SingleOrDefault();

                if (original == null)
                {
                    parameter newObj = new parameter();
                    newObj.name = EntityObject.Name;
                    newObj.value = EntityObject.Value;
                    newObj.description = EntityObject.description;
                    newObj.plant_id = EntityObject.plant_id;
                    context.parameter.Add(newObj);
                }
                else
                {
                    original.name = EntityObject.Name;
                    original.value = EntityObject.Value;
                    original.description = EntityObject.description;
                    original.plant_id = EntityObject.plant_id;
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
        public static List<parameter> List(string PlantID)
        {
            using (var context = new DTB_ICMS_CREDIT_DEVEntities())
            {
                var Dados = (from p in context.parameter
                             where p.plant_id == PlantID
                             orderby p.name
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
                    var newObj = new parameter { idparameter = ID };
                    context.parameter.Attach(newObj);
                    context.parameter.Remove(newObj);
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
        public static parameter GetByID(string PlantID, int ID)
        {
            using (var context = new DTB_ICMS_CREDIT_DEVEntities())
            {
                var Record = (from p in context.parameter
                              where p.idparameter == ID
                              && p.plant_id == PlantID
                              select p).SingleOrDefault();

                if (Record == null)
                {
                    Record = new parameter { idparameter = 0, plant_id = PlantID };
                }
                return Record;
            }
        }

        /// <summary>
        /// Returns param Value
        /// </summary>
        /// <param name="PlantID"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static string GetByName(string PlantID, string Name)
        {
            using (var context = new DTB_ICMS_CREDIT_DEVEntities())
            {
                var Record = (from p in context.parameter
                              where p.name == Name && p.plant_id == PlantID
                              select p).SingleOrDefault();

                string Retorno = "";
                if (Record != null)
                {
                    Retorno = Record.value;
                }

                return Retorno;
            }
        }
    }
}
