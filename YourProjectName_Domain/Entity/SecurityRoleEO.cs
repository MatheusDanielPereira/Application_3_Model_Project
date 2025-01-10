using System.Collections.Generic;

namespace YourProjectName_Domain.Entity
{
    public class SecurityRoleEO
    {
        public int idrole { get; set; }
        public string role_description { get; set; }
        public string plant_id { get; set; }
        public List<int> roleUsersIDs { get; set; }
        public List<string> roleTasksIDs { get; set; }
        public List<SecurityTaskEO> RoleTasks { get; set; }
        public List<SecurityTaskEO> AvailableTasks { get; set; }
        public List<SecurityUserEO> RoleUsers { get; set; }
        public List<SecurityUserEO> AvailableUsers { get; set; }
    }
}
