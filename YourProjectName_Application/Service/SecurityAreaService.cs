using System.Collections.Generic;
using AutoMapper;
using System.Linq;

using YourProjectName_Data.Repository;
using YourProjectName_Data.Context;
using YourProjectName_Domain.Entity;

namespace YourProjectName_Application.Service
{
    static public class SecurityAreaService
    {
        /// <summary>
        /// Inserts or Updates a record
        /// </summary>
        /// <param name="EntityObject"></param>
        /// <returns></returns>
        public static bool Save(SecurityAreaEO EntityObject)
        {
            return SecurityAreaRepo.Save(EntityObject);
        }

        /// <summary>
        /// Loads a list of entity
        /// </summary>
        /// <param name="PlantID"></param>
        /// <returns></returns>
        public static List<SecurityAreaEO> List(string PlantID)
        {
            List<security_area> lista = SecurityAreaRepo.List(PlantID);
            var listaEntity = Mapper.Map<IEnumerable<security_area>, IEnumerable<SecurityAreaEO>>(lista);
            return listaEntity.ToList();
        }

        /// <summary>
        /// Removes the specified record
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool Remove(int ID)
        {
            return SecurityAreaRepo.Remove(ID);
        }

        /// <summary>
        /// Loads a specific record
        /// </summary>
        /// <param name="PlantID"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static SecurityAreaEO GetByID(string PlantID, int ID)
        {
            var Data = SecurityAreaRepo.GetByID(PlantID, ID);
            var Entity = Mapper.Map<security_area, SecurityAreaEO>(Data);
            return Entity;
        }
    }
}

