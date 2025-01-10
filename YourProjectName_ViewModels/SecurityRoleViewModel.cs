using System.ComponentModel.DataAnnotations;
using YourProjectName_Resources;

namespace YourProjectName_ViewModels
{
    public class SecurityRoleViewModel
    {
        [Key]
        public int idrole { get; set; }

        [Required(ErrorMessageResourceName = "IsRequired", ErrorMessageResourceType = typeof(Resources))]
        [MaxLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Resources))]
        [MinLength(5, ErrorMessageResourceName = "MinLength", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Description", ResourceType = typeof(Resources))]
        public string role_description { get; set; }
        public string plant_id { get; set; }
    }
}
