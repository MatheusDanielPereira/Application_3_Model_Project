using System.Collections.Generic;
using System.Linq;

using YourProjectName_Data.Context;

namespace YourProjectName_Data.Repository
{
    public class SecurityTaskRepo
    {
        /// <summary>
        /// Loads a list of entity
        /// </summary>
        /// <returns></returns>
        public static List<security_task> List()
        {
            using (var context = new DTB_ICMS_CREDIT_DEVEntities())
            {
                var Dados = (from p in context.security_task
                             orderby p.task_description
                             select p).ToList();

                return Dados;
            }
        }

        /// <summary>
        /// Loads a specific record
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static security_task GetByID(string ID)
        {
            using (var context = new DTB_ICMS_CREDIT_DEVEntities())
            {
                var Record = (from p in context.security_task
                              where p.idtask == ID
                              select p).SingleOrDefault();

                if (Record == null)
                {
                    Record = new security_task { idtask = "" };
                }
                return Record;
            }
        }

    }
}
