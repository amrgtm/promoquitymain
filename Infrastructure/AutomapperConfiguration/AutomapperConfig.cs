using Application.DTOs.ApplicationBlogDTO;
using Application.DTOs.ApplicationCompanyProfileDTO;
using Application.DTOs.ApplicationDownloadDTO;
using Application.DTOs.ApplicationFaqDTO;
using Application.DTOs.ApplicationFinancialKPIDTO;
using Application.DTOs.ApplicationHomeContentDTO;
using Application.DTOs.ApplicationHomeContentMidDTO;
using Application.DTOs.ApplicationImageMasterDTO;
using Application.DTOs.ApplicationMarketKPIDTO;
using Application.DTOs.ApplicationNewsAnnounceDTO;
using Application.DTOs.ApplicationOperationalKPIDTO;
using Application.DTOs.ApplicationPromoConfigDTO;
using Application.DTOs.ApplicationRoleDTO;
using Application.DTOs.ApplicationUserDTO;
using Application.DTOs.ApplicationUserRoleDTO;
using Application.DTOs.ApplicationValuationKPIDTO;
using AutoMapper;
using Domain.Entities;
using System.Reflection.Metadata;

namespace Infrastructure.AutomapperConfiguration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            this.CreateEntityMappings<ApplicationUser, UserDTO, CreateUserDTO, UpdateUserDTO>();
            this.CreateEntityMappings<ApplicationRole, RoleDTO, CreateRoleDTO, UpdateRoleDTO>();
            this.CreateEntityMappings<ApplicationUserRole, UserRoleDTO, CreateUserRoleDTO, UpdateUserRoleDTO>();
            
            //Source and Destination property names are different so we need to map them manually
            this.CreateEntityMappings<ApplicationImageMaster, ImageMasterDTO, CreateImageMasterDTO, UpdateImageMasterDTO>();
            this.CreateMap<CreateImageMasterDTO, ApplicationImageMaster>().ForMember(dest => dest.TableName, opt => opt.MapFrom(src => src.Source)).ReverseMap();
            this.CreateMap<UpdateImageMasterDTO, ApplicationImageMaster>().ForMember(dest => dest.TableName, opt => opt.MapFrom(src => src.Source)).ReverseMap();
            this.CreateMap<ApplicationImageMaster, ImageMasterDTO>().ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.TableName)).ReverseMap();

             this.CreateEntityMappings<ApplicationCompanyProfile, CompanyProfileDTO, CreateCompanyProfileDTO, UpdateCompanyProfileDTO>();
            this.CreateEntityMappings<ApplicationHomeContent, HomeContentDTO, CreateHomeContentDTO, UpdateHomeContentDTO>();
            this.CreateEntityMappings<ApplicationHomeContentMid, HomeContentMidDTO, CreateHomeContentMidDTO, UpdateHomeContentMidDTO>();


            this.CreateEntityMappings<ApplicationNewsAnnounce, NewsAnnounceDTO, CreateNewsAnnounceDTO, UpdateNewsAnnounceDTO>();
            this.CreateEntityMappings<ApplicationOperationalKPI, OperationalKPIDTO, CreateOperationalKPIDTO, UpdateOperationalKPIDTO>();
            this.CreateEntityMappings<ApplicationMarketKPI, MarketKPIDTO, CreateMarketKPIDTO, UpdateMarketKPIDTO>();
            this.CreateEntityMappings<ApplicationFinancialKPI, FinancialKPIDTO, CreateFinancialKPIDTO, UpdateFinancialKPIDTO>();
            this.CreateEntityMappings<ApplicationValuationKPI, ValuationKPIDTO, CreateValuationKPIDTO, UpdateValuationKPIDTO>();
            this.CreateEntityMappings<ApplicationDownload, DownloadDTO, CreateDownloadDTO, UpdateDownloadDTO>();
            this.CreateEntityMappings<ApplicationBlog,BlogDTO, CreateBlogDTO, UpdateBlogDTO>();

            this.CreateEntityMappings<ApplicationFaq, FaqDTO, CreateFaqDTO, UpdateFaqDTO>();
            this.CreateEntityMappings<ApplicationPromoConfig, PromoConfigDTO, CreatePromoConfigDTO, UpdatePromoConfigDTO>();

        }
    }
}
