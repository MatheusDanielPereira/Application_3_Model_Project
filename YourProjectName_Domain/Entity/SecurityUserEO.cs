using System.Collections.Generic;

namespace YourProjectName_Domain.Entity
{
    public class SecurityUserEO
    {
        public int iduser { get; set; }
        public string user_name { get; set; }
        public string user_email { get; set; }
        public int idarea { get; set; }
        public string plant_id { get; set; }
        public bool multiple_plants { get; set; }
        public List<string> PlantsIDs { get; set; }
        public List<PlantEO> UserPlants { get; set; }
        public List<PlantEO> AvailablePlants { get; set; }
    }
}
