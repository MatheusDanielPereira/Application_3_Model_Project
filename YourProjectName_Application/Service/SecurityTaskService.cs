using System.Collections.Generic;
using AutoMapper;
using System.Linq;

using YourProjectName_Data.Repository;
using YourProjectName_Data.Context;
using YourProjectName_Domain.Entity;

namespace YourProjectName_Application.Service
{
    public class SecurityTaskService
    {
        /// <summary>
        /// Loads a list of entity
        /// </summary>
        /// <returns></returns>
        public static List<SecurityTaskEO> List()
        {
            List<security_task> lista = SecurityTaskRepo.List();
            var listaEntity = Mapper.Map<IEnumerable<security_task>, IEnumerable<SecurityTaskEO>>(lista);
            return listaEntity.ToList();
        }

        /// <summary>
        /// Loads a specific record
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static SecurityTaskEO GetByID(string ID)
        {
            var Data = SecurityTaskRepo.GetByID(ID);
            var Entity = Mapper.Map<security_task, SecurityTaskEO>(Data);
            return Entity;
        }
    }
}
