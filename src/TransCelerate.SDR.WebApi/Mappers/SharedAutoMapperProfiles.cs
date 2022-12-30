using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Entities.Study;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.DTO.Study;
using System.Globalization;
using TransCelerate.SDR.Core.DTO;
using TransCelerate.SDR.Core.DTO.UserGroups;
using TransCelerate.SDR.Core.Entities.UserGroups;
using TransCelerate.SDR.Core.DTO.Common;
using TransCelerate.SDR.Core.Entities.Common;

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
                .ForMember(dest => dest.groupCreatedOn, opt => opt.MapFrom(src => src.groupCreatedOn.ToString(Constants.DateFormats.DateFormatForAuditResponse).ToUpper()))
                .ForMember(dest => dest.groupModifiedOn, opt => opt.MapFrom(src => src.groupModifiedOn.ToString(Constants.DateFormats.DateFormatForAuditResponse).ToUpper()))
                .AfterMap((src, dest) =>
                {
                    if (src.users != null && src.users.Count > 0)
                    {
                        dest.users.ForEach(x => x.groupName = src.groupName);
                        dest.users.ForEach(x => x.groupId = src.groupId);
                        dest.users.ForEach(x => x.groupModifiedOn = src.groupModifiedOn);
                    }
                })
                .ReverseMap();
            CreateMap<UsersEntity, UsersDTO>().ReverseMap();
            CreateMap<UserGroupMappingEntity, UserGroupMappingDTO>().ReverseMap();
            CreateMap<GroupFilterEntity, GroupFilterDTO>().ReverseMap();
            CreateMap<GroupDetailsEntity, GroupDetailsDTO>()
                .ForMember(dest => dest.groupCreatedOn, opt => opt.MapFrom(src => src.groupCreatedOn.ToString(Constants.DateFormats.DateFormatForAuditResponse).ToUpper()))
                .ForMember(dest => dest.groupModifiedOn, opt => opt.MapFrom(src => src.groupModifiedOn.ToString(Constants.DateFormats.DateFormatForAuditResponse).ToUpper()))
                .ReverseMap();
            CreateMap<GroupListEntity, GroupListDTO>().ReverseMap();
            CreateMap<GroupFilterValuesEntity, GroupFilterValuesDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.groupFilterValueId))
                .ReverseMap();
            //Mapper for AuditTrail
            CreateMap<AuditTrailDto, AuditTrailResponseEntity>().ReverseMap();
        }
    }
}
