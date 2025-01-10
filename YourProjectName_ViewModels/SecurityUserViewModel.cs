using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using YourProjectName_Resources;

namespace YourProjectName_ViewModels
{
    public class SecurityUserViewModel
    {
        [Key]
        public int iduser { get; set; }

        [Required(ErrorMessageResourceName = "IsRequired", ErrorMessageResourceType = typeof(Resources))]
        [MaxLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Resources))]
        [MinLength(5, ErrorMessageResourceName = "MinLength", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Name", ResourceType = typeof(Resources))]
        public string user_name { get; set; }

        [Required(ErrorMessageResourceName = "IsRequired", ErrorMessageResourceType = typeof(Resources))]
        [MaxLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Resources))]
        [MinLength(10, ErrorMessageResourceName = "MinLength", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Email", ResourceType = typeof(Resources))]
        public string user_email { get; set; }

        [Required(ErrorMessageResourceName = "IsRequired", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Area", ResourceType = typeof(Resources))]
        public int idarea { get; set; }
        public string plant_id { get; set; }

        [Display(Name = "MultiplePlants", ResourceType = typeof(Resources))]
        public bool multiple_plants { get; set; }

        public string area_name { get; set; }

        public List<string> PlantsIDs { get; set; }
    }
}
