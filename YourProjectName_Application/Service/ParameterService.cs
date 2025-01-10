using System.Collections.Generic;
using AutoMapper;
using System.Linq;

using YourProjectName_Data.Repository;
using YourProjectName_Data.Context;
using YourProjectName_Domain.Entity;

namespace YourProjectName_Application.Service
{
    static public class ParameterService
    {
        /// <summary>
        /// Inserts or Updates a record
        /// </summary>
        /// <param name="EntityObject"></param>
        /// <returns></returns>
        public static bool Save(ParameterEO EntityObject)
        {
            return ParameterRepo.Save(EntityObject);
        }

        /// <summary>
        /// Loads a list of entity
        /// </summary>
        /// <param name="PlantID"></param>
        /// <returns></returns>
        public static List<ParameterEO> List(string PlantID)
        {
            List<parameter> lista = ParameterRepo.List(PlantID);
            var listaEntity = Mapper.Map<IEnumerable<parameter>, IEnumerable<ParameterEO>>(lista);
            return listaEntity.ToList();
        }

        /// <summary>
        /// Removes the specified record
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool Remove(int ID)
        {
            return ParameterRepo.Remove(ID);
        }

        /// <summary>
        /// Loads a specific record
        /// </summary>
        /// <param name="PlantID"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static ParameterEO GetByID(string PlantID, int ID)
        {
            var Data = ParameterRepo.GetByID(PlantID, ID);
            var Entity = Mapper.Map<parameter, ParameterEO>(Data);
            return Entity;
        }

        /// <summary>
        /// Returns param Value
        /// </summary>
        /// <param name="PlantID"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static string GetByName(string PlantID, string Name)
        {
            return ParameterRepo.GetByName(PlantID, Name);
        }
    }
}
