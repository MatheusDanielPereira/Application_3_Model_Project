using System.ComponentModel.DataAnnotations;
using YourProjectName_Resources;

namespace YourProjectName_ViewModels
{
    public class SecurityAreaViewModel
    {
        [Key]
        public int idarea { get; set; }

        [Required(ErrorMessageResourceName = "IsRequired", ErrorMessageResourceType = typeof(Resources))]
        [MaxLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Resources))]
        [MinLength(5, ErrorMessageResourceName = "MinLength", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Name", ResourceType = typeof(Resources))]
        public string area_name { get; set; }
        public string plant_id { get; set; }
    }
}
