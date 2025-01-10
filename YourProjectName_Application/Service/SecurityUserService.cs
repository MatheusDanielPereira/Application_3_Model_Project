using System.Collections.Generic;
using AutoMapper;
using System.Linq;

using YourProjectName_Data.Repository;
using YourProjectName_Data.Context;
using YourProjectName_Domain.Entity;

namespace YourProjectName_Application.Service
{
    static public class SecurityUserService
    {
        /// <summary>
        /// Inserts or Updates a record
        /// </summary>
        /// <param name="EntityObject"></param>
        /// <returns></returns>
        public static bool Save(SecurityUserEO EntityObject)
        {
            return SecurityUserRepo.Save(EntityObject);
        }

        /// <summary>
        /// Loads a list of entity
        /// </summary>
        /// <param name="PlantID"></param>
        /// <returns></returns>
        public static List<SecurityUserEO> List(string PlantID)
        {
            return SecurityUserRepo.List(PlantID);
        }

        /// <summary>
        /// Removes the specified record
        /// </summary>
        /// <param name="ID"></param>
        public static void Remove(int ID)
        {
            SecurityUserRepo.Remove(ID);
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
            return SecurityUserRepo.GetByID(PlantID, ID, AdditionalInfo);
        }
    }
}
