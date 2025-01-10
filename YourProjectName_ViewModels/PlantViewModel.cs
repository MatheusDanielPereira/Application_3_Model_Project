using System.ComponentModel.DataAnnotations;
using YourProjectName_Resources;
using System.Web;

namespace YourProjectName_ViewModels
{
    public class PlantViewModel
    {
        [Required(ErrorMessageResourceName = "IsRequired", ErrorMessageResourceType = typeof(Resources))]
        [MaxLength(10, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Plant", ResourceType = typeof(Resources))]
        public string plant_id { get; set; }

        [MaxLength(150, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Description", ResourceType = typeof(Resources))] 
        public string plant_description { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Flag", ResourceType = typeof(Resources))]
        public HttpPostedFileBase plant_flag_load { get; set; }

        [Display(Name = "TimeZone", ResourceType = typeof(Resources))]
        public string plant_time_zone { get; set; }
    }
}
