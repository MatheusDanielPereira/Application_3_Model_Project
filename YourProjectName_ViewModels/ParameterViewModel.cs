using System.ComponentModel.DataAnnotations;
using YourProjectName_Resources;

namespace YourProjectName_ViewModels
{
    public class ParameterViewModel
    {
        [Key]
        public int idparameter { get; set; }

        [Required(ErrorMessageResourceName = "IsRequired", ErrorMessageResourceType = typeof(Resources))]
        [MaxLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Resources))]
        [MinLength(5, ErrorMessageResourceName = "MinLength", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Name", ResourceType = typeof(Resources))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "IsRequired", ErrorMessageResourceType = typeof(Resources))]
        [MaxLength(1000, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Value", ResourceType = typeof(Resources))]
        [DataType(DataType.MultilineText)]
        public string Value { get; set; }

        [Required(ErrorMessageResourceName = "IsRequired", ErrorMessageResourceType = typeof(Resources))]
        [MaxLength(300, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Description", ResourceType = typeof(Resources))]
        [DataType(DataType.MultilineText)]
        public string description { get; set; }
        public string plant_id { get; set; }
    }
}