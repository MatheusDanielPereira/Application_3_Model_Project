using System.Collections.Generic;
using AutoMapper;
using System.Linq;

using YourProjectName_Data.Repository;
using YourProjectName_Data.Context;
using YourProjectName_Domain.Entity;

namespace YourProjectName_Application.Service
{
    public static class PlantService
    {
        /// <summary>
        /// Inserts or Updates a record
        /// </summary>
        /// <param name="EntityObject"></param>
        /// <returns></returns>
        public static bool Save(PlantEO EntityObject)
        {
            return PlantRepo.Save(EntityObject);
        }

        /// <summary>
        /// Loads a list of entity
        /// </summary>
        /// <returns></returns>
        public static List<PlantEO> List()
        {
            List<plant> lista = PlantRepo.List();
            var listaEntity = Mapper.Map<IEnumerable<plant>, IEnumerable<PlantEO>>(lista);
            return listaEntity.ToList();
        }

        /// <summary>
        /// Removes the specified record
        /// </summary>
        /// <param name="PlantID"></param>
        /// <returns></returns>
        public static bool Remove(string PlantID)
        {
            return PlantRepo.Remove(PlantID);
        }


        /// <summary>
        /// Loads a specific record
        /// </summary>
        /// <param name="PlantID"></param>
        /// <returns></returns>
        public static PlantEO GetByID(string PlantID)
        {
            var Data = PlantRepo.GetByID(PlantID);
            var Entity = Mapper.Map<plant, PlantEO>(Data);
            return Entity;
        }

        /// <summary>
        /// Loads the desired picture based on Type parameter. 0-Flag;1-FloorPlant
        /// </summary>
        /// <param name="PlantID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static byte[] GetPicture(string PlantID, int Type)
        {
            return PlantRepo.GetPicture(PlantID, Type);
        }
    }
}
