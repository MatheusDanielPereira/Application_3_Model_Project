using AutoMapper;
using YourProjectName_Domain.Entity;
using YourProjectName_ViewModels;
using YourProjectName_Data.Context;

namespace YourProjectName_AutoMapper
{
    public class AutoMapperConfig
    {
        [System.Obsolete]
        public static void RegisterMappings()
        {
            Mapper.Initialize(x =>
            {
                x.CreateMap<ParameterEO, ParameterViewModel>();
                x.CreateMap<SecurityAreaEO, SecurityAreaViewModel>();
                x.CreateMap<SecurityUserEO, SecurityUserViewModel>();
                x.CreateMap<SecurityTaskEO, SecurityTaskViewModel>();
                x.CreateMap<SecurityRoleEO, SecurityRoleViewModel>();
                x.CreateMap<SecurityUserEO, SecurityUserViewModel>();
                x.CreateMap<PlantEO, PlantViewModel>();
                x.AddProfile<DomainToViewModelMappingProfile>();

                x.CreateMap<ParameterViewModel, ParameterEO>();
                x.CreateMap<SecurityAreaViewModel, SecurityAreaEO>();
                x.CreateMap<SecurityUserViewModel, SecurityUserEO>();
                x.CreateMap<SecurityTaskViewModel, SecurityTaskEO>();
                x.CreateMap<SecurityRoleViewModel, SecurityRoleEO>();
                x.CreateMap<SecurityUserViewModel, SecurityUserEO>();
                x.CreateMap<PlantViewModel, PlantEO>();
                x.AddProfile<ViewModelToDomainMappingProfile>();

                x.CreateMap<ParameterEO, parameter>();
                x.CreateMap<SecurityAreaEO, security_area>();
                x.CreateMap<SecurityUserEO, security_user>();
                x.CreateMap<SecurityTaskEO, security_task>();
                x.CreateMap<SecurityRoleEO, security_role>();
                x.CreateMap<SecurityUserEO, security_user>();
                x.CreateMap<PlantEO, plant>();
                x.AddProfile<DomainToDataMappingProfile>();

                x.CreateMap<parameter, ParameterEO>();
                x.CreateMap<security_area, SecurityAreaEO>();
                x.CreateMap<security_user, SecurityUserEO>();
                x.CreateMap<security_task, SecurityTaskEO>();
                x.CreateMap<security_role, SecurityRoleEO>();
                x.CreateMap<security_user, SecurityUserEO>();
                x.CreateMap<plant, PlantEO>();

                //For stored procedures results
                //x.CreateMap<sp_ListsAreaOrders_Result, OrderDetailedEO>();

                x.AddProfile<DataToDomainMappingProfile>();
            });
        }
    }

    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }
    }

    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }
    }

    public class DataToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }
    }

    public class DomainToDataMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }
    }
}