using System.Collections.Generic;
using AutoMapper;
using System.Linq;

using YourProjectName_Data.Repository;
using YourProjectName_Data.Context;
using YourProjectName_Domain.Entity;

namespace YourProjectName_Application.Service
{
    static public class SecurityRoleService
    {
        /// <summary>
        /// Inserts or Updates a record
        /// </summary>
        /// <param name="EntityObject"></param>
        /// <returns></returns>
        public static bool Save(SecurityRoleEO EntityObject)
        {
            return SecurityRoleRepo.Save(EntityObject);
        }

        /// <summary>
        /// Loads a list of entity
        /// </summary>
        /// <param name="PlantID"></param>
        /// <returns></returns>
        public static List<SecurityRoleEO> List(string PlantID)
        {
            List<security_role> lista = SecurityRoleRepo.List(PlantID);
            var listaEntity = Mapper.Map<IEnumerable<security_role>, IEnumerable<SecurityRoleEO>>(lista);
            return listaEntity.ToList();
        }

        /// <summary>
        /// Removes the specified record
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool Remove(int ID)
        {
            return SecurityRoleRepo.Remove(ID);
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
            return SecurityRoleRepo.GetByID(PlantID, ID, AdditionalInfo);
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
            return SecurityRoleRepo.GetUserPermissions(useremail, out plant_id, out multiple_plants, out Plants);
        }
    }
}
