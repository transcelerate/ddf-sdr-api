using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using TransCelerate.SDR.Core.DTO.Common;
using TransCelerate.SDR.Core.DTO.UserGroups;
using TransCelerate.SDR.Core.Entities.Common;
using TransCelerate.SDR.Core.Entities.UserGroups;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using static TransCelerate.SDR.Core.Utilities.Common.Constants;

namespace TransCelerate.SDR.WebApi.Mappers
{
    public class SharedAutoMapperProfiles : Profile
    {
        public SharedAutoMapperProfiles()
        {
            AllowNullCollections = true;
            AllowNullDestinationValues = true;

            //Mapper for User Group Mapping
            CreateMap<SDRGroupsEntity, SDRGroupsDTO>()
                .ForMember(dest => dest.GroupCreatedOn, opt => opt.MapFrom(src => src.GroupCreatedOn.ToString(Constants.DateFormats.DateFormatForAuditResponse).ToUpper()))
                .ForMember(dest => dest.GroupModifiedOn, opt => opt.MapFrom(src => src.GroupModifiedOn.ToString(Constants.DateFormats.DateFormatForAuditResponse).ToUpper()))
                .AfterMap((src, dest) =>
                {
                    if (src.Users != null && src.Users.Count > 0)
                    {
                        dest.Users.ForEach(x => x.GroupName = src.GroupName);
                        dest.Users.ForEach(x => x.GroupId = src.GroupId);
                        dest.Users.ForEach(x => x.GroupModifiedOn = src.GroupModifiedOn);
                    }
                })
                .ReverseMap();
            CreateMap<UsersEntity, UsersDTO>().ReverseMap();
            CreateMap<UserGroupMappingEntity, UserGroupMappingDTO>().ReverseMap();
            CreateMap<GroupFilterEntity, GroupFilterDTO>().ReverseMap();
            CreateMap<GroupDetailsEntity, GroupDetailsDTO>()
                .ForMember(dest => dest.GroupCreatedOn, opt => opt.MapFrom(src => src.GroupCreatedOn.ToString(Constants.DateFormats.DateFormatForAuditResponse).ToUpper()))
                .ForMember(dest => dest.GroupModifiedOn, opt => opt.MapFrom(src => src.GroupModifiedOn.ToString(Constants.DateFormats.DateFormatForAuditResponse).ToUpper()))
                .ReverseMap();
            CreateMap<GroupListEntity, GroupListDTO>().ReverseMap();
            CreateMap<GroupFilterValuesEntity, GroupFilterValuesDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.GroupFilterValueId))
                .ReverseMap();

            //Mapper for AuditTrail
            CreateMap<AuditTrailDto, AuditTrailResponseEntity>().ReverseMap();
            CreateMap<AuditTrailResponseEntity, AuditTrailResponseWithLinksDto>()
                .ForMember(dest => dest.Links, opt => opt.MapFrom(src => LinksHelper.GetLinksForUi(src.StudyId,
                           src.UsdmVersion == Constants.USDMVersions.MVP ? 
                                        src.StudyDesignIdsMVP != null ? 
                                        src.StudyDesignIdsMVP.Where(x => x != null && x.Any()).SelectMany(x => x).ToList() 
                                        : null 
                                        : src.UsdmVersion == Constants.USDMVersions.V3 ?
                                        src.StudyDesignIdsV4 != null ?
                                        src.StudyDesignIdsV4.Where(x => x != null && x.Any()).SelectMany(x => x).ToList()
                                        : null
                                        : src.StudyDesignIds != null ? 
                                        src.StudyDesignIds.ToList() 
                                        : null,
                           src.UsdmVersion, src.SDRUploadVersion)))
                .ReverseMap();

            //Mapper for Search Titke
            CreateMap<SearchTitleParametersDto, SearchTitleParametersEntity>();
            CreateMap<SearchTitleResponseDto, SearchTitleResponseEntity>()
               .ForMember(dest => dest.StudyId, opt => opt.MapFrom(src => src.Study.StudyId))
               .ForMember(dest => dest.StudyTitle, opt => opt.MapFrom(src => src.Study.StudyTitle))
               .ForMember(dest => dest.SDRUploadVersion, opt => opt.MapFrom(src => src.AuditTrail.SDRUploadVersion))
               .ForMember(dest => dest.UsdmVersion, opt => opt.MapFrom(src => src.AuditTrail.UsdmVersion))
               .ForMember(dest => dest.EntryDateTime, opt => opt.MapFrom(src => src.AuditTrail.EntryDateTime))
               .ReverseMap();

            //Mapper for Study History
            CreateMap<StudyHistoryResponseEntity, UploadVersionDto>()
                 .ForMember(dest => dest.StudyTitle, opt => opt.MapFrom(src => src.StudyTitle.V4StudyTitleToObjectConvertor(src.UsdmVersion)))
                 .ForMember(dest => dest.UploadVersion, opt => opt.MapFrom(src => src.SDRUploadVersion))
                 .ForMember(dest => dest.ProtocolVersions, opt => opt.MapFrom(src => src.ProtocolVersions))
                 .ForMember(dest => dest.Links, opt => opt.MapFrom(src => LinksHelper.GetLinks(src.StudyId,
                           src.UsdmVersion == Constants.USDMVersions.MVP ? src.StudyDesignIdsMVP != null ? 
                                            src.StudyDesignIdsMVP.Where(x => x != null && x.Any()).SelectMany(x => x).ToList() : 
                                            null :
                                            src.UsdmVersion == Constants.USDMVersions.V3 ? src.StudyDesignIdsV4 != null ? 
                                            src.StudyDesignIdsV4.Where(x => x != null && x.Any()).SelectMany(x => x).ToList() : 
                                            null :
                                            src.StudyDesignIds,
                           src.UsdmVersion, src.SDRUploadVersion)))
                 .ReverseMap();

            //Mapper for Search
            CreateMap<SearchParametersDto, SearchParametersEntity>();

            //Mapper for Common Search
            CreateMap<SearchResponseDto, SearchResponseEntity>()
                .ForMember(dest => dest.StudyId, opt => opt.MapFrom(src => src.Study.StudyId))
                .ForMember(dest => dest.StudyTitle, opt => opt.MapFrom(src => src.Study.StudyTitle))
                .ForMember(dest => dest.EntryDateTime, opt => opt.MapFrom(src => src.AuditTrail.EntryDateTime))
                .ForMember(dest => dest.SDRUploadVersion, opt => opt.MapFrom(src => src.AuditTrail.SDRUploadVersion))
                .ForMember(dest => dest.UsdmVersion, opt => opt.MapFrom(src => src.AuditTrail.UsdmVersion))
                .ReverseMap();                                    

            //Mapper for Search V2
            CreateMap<SearchResponseDto, Core.Entities.StudyV2.SearchResponseEntity>()
                .ForMember(dest => dest.StudyId, opt => opt.MapFrom(src => src.Study.StudyId))
                .ForMember(dest => dest.StudyTitle, opt => opt.MapFrom(src => src.Study.StudyTitle))
                .ForMember(dest => dest.StudyType, opt => opt.MapFrom(src => src.Study.StudyType))                
                .ForMember(dest => dest.EntryDateTime, opt => opt.MapFrom(src => src.AuditTrail.EntryDateTime))
                .ForMember(dest => dest.SDRUploadVersion, opt => opt.MapFrom(src => src.AuditTrail.SDRUploadVersion))
                .ForMember(dest => dest.UsdmVersion, opt => opt.MapFrom(src => src.AuditTrail.UsdmVersion))
                .ReverseMap();

            CreateMap<CommonCodeDto, Core.Entities.StudyV2.CodeEntity>()
                .ReverseMap();
            CreateMap<CommonStudyIdentifiersDto, Core.Entities.StudyV2.StudyIdentifierEntity>()
                .ReverseMap();
            CreateMap<CommonOrganisationDto, Core.Entities.StudyV2.OrganisationEntity>()
                .ReverseMap();
            CreateMap<Core.DTO.Common.CommonStudyIndication, Core.Entities.StudyV2.IndicationEntity>()
                .ForMember(dest => dest.IndicationDescription, opt => opt.MapFrom(src => src.IndicationDescription))
                .ReverseMap();

            //Mapper for Search V3
            CreateMap<SearchResponseDto, Core.Entities.StudyV3.SearchResponseEntity>()
                .ForMember(dest => dest.StudyId, opt => opt.MapFrom(src => src.Study.StudyId))
                .ForMember(dest => dest.StudyTitle, opt => opt.MapFrom(src => src.Study.StudyTitle))
                .ForMember(dest => dest.StudyType, opt => opt.MapFrom(src => src.Study.StudyType))
                .ForMember(dest => dest.EntryDateTime, opt => opt.MapFrom(src => src.AuditTrail.EntryDateTime))
                .ForMember(dest => dest.SDRUploadVersion, opt => opt.MapFrom(src => src.AuditTrail.SDRUploadVersion))
                .ForMember(dest => dest.UsdmVersion, opt => opt.MapFrom(src => src.AuditTrail.UsdmVersion))
                .ReverseMap();

            CreateMap<CommonCodeDto, Core.Entities.StudyV3.CodeEntity>()
                .ReverseMap();
            CreateMap<CommonStudyIdentifiersDto, Core.Entities.StudyV3.StudyIdentifierEntity>()
                .ReverseMap();
            CreateMap<CommonOrganisationDto, Core.Entities.StudyV3.OrganisationEntity>()
                .ReverseMap();
            CreateMap<Core.DTO.Common.CommonStudyIndication, Core.Entities.StudyV3.IndicationEntity>()
                .ForMember(dest => dest.IndicationDescription, opt => opt.MapFrom(src => src.IndicationDescription))
                .ReverseMap();

            //Mapper for Search V4
            CreateMap<SearchResponseDto, Core.Entities.StudyV4.SearchResponseEntity>()
                .ForMember(dest => dest.StudyId, opt => opt.MapFrom(src => src.Study.StudyId))
                .ForMember(dest => dest.StudyTitle, opt => opt.MapFrom(src => src.Study.StudyTitle))
                .ForMember(dest => dest.StudyType, opt => opt.MapFrom(src => src.Study.StudyType))
                .ForMember(dest => dest.EntryDateTime, opt => opt.MapFrom(src => src.AuditTrail.EntryDateTime))
                .ForMember(dest => dest.SDRUploadVersion, opt => opt.MapFrom(src => src.AuditTrail.SDRUploadVersion))
                .ForMember(dest => dest.UsdmVersion, opt => opt.MapFrom(src => src.AuditTrail.UsdmVersion))
                .ReverseMap();

            CreateMap<CommonStudyIdentifiersDto, Core.DTO.StudyV4.StudyIdentifierDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StudyIdentifier, opt => opt.MapFrom(src => src.StudyIdentifier))
                .ForMember(dest => dest.StudyIdentifierScope, opt => opt.MapFrom(src => src.StudyIdentifierScope))
                .ReverseMap();
            CreateMap<CommonOrganisationDto, Core.DTO.StudyV4.OrganizationDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.OrganizationType, opt => opt.MapFrom(src => src.OrganisationType))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.OrganisationName))
                .ForMember(dest => dest.IdentifierScheme, opt => opt.MapFrom(src => src.OrganisationIdentifierScheme))
                .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.OrganisationIdentifier))
                .ReverseMap();

            CreateMap<CommonCodeDto, Core.Entities.StudyV4.CodeEntity>()
                .ReverseMap();
            CreateMap<CommonCodeDto, Core.DTO.StudyV4.CodeDto>()
                .ReverseMap();
            CreateMap<CommonStudyIdentifiersDto, Core.Entities.StudyV4.StudyIdentifierEntity>()
                .ReverseMap();
            CreateMap<CommonOrganisationDto, Core.Entities.StudyV4.OrganizationEntity>()
                .ReverseMap();
            CreateMap<Core.DTO.Common.CommonStudyIndication, Core.Entities.StudyV4.IndicationEntity>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.IndicationDescription))
                .ReverseMap();

            //ChangeAudit
            CreateMap<ChangeAuditStudyDto, ChangeAuditStudyEntity>().ReverseMap();
            CreateMap<ChangeAuditDto, ChangeAuditEntity>().ReverseMap();
            CreateMap<ChangesDto, ChangesEntity>().ReverseMap();
        }
    }
}
