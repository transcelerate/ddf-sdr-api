﻿using AutoMapper;
using TransCelerate.SDR.Core.DTO.StudyV4;
using TransCelerate.SDR.Core.Entities.StudyV4;

namespace TransCelerate.SDR.WebApi.Mappers
{
    public class AutoMapperProfilesV4 : Profile
    {
        public AutoMapperProfilesV4()
        {
            AllowNullCollections = true;
            AllowNullDestinationValues = true;

            CreateMap<StudyDefinitionsEntity, StudyDefinitionsDto>().ReverseMap();
            CreateMap<ActivityDto, ActivityEntity>().ReverseMap();
            CreateMap<AnalysisPopulationDto, AnalysisPopulationEntity>().ReverseMap();
            CreateMap<AuditTrailEntity, AuditTrailDto>()
                //.ForMember(dest => dest.EntryDateTime, opt => opt.MapFrom(src => src.EntryDateTime.ToString(Constants.DateFormats.DateFormatForAuditResponse).ToUpper()))
                .ReverseMap();
            CreateMap<StudyDto, StudyEntity>().ReverseMap();
            CreateMap<CodeDto, CodeEntity>().ReverseMap();
            CreateMap<ProcedureDto, ProcedureEntity>().ReverseMap();
            CreateMap<EncounterDto, EncounterEntity>().ReverseMap();
            CreateMap<EndpointEntity, EndpointDto>().ReverseMap();
            CreateMap<EstimandDto, EstimandEntity>().ReverseMap();
            CreateMap<IndicationDto, IndicationEntity>().ReverseMap();
            CreateMap<InterCurrentEventDto, InterCurrentEventEntity>().ReverseMap();
            CreateMap<StudyInterventionDto, StudyInterventionEntity>().ReverseMap();
            CreateMap<ObjectiveDto, ObjectiveEntity>().ReverseMap();
            CreateMap<StudyArmDto, StudyArmEntity>().ReverseMap();
            CreateMap<StudyCellDto, StudyCellEntity>().ReverseMap();
            CreateMap<StudyDesignDto, StudyDesignEntity>().ReverseMap();
            CreateMap<StudyDesignPopulationDto, StudyDesignPopulationEntity>().ReverseMap();
            CreateMap<StudyElementDto, StudyElementEntity>().ReverseMap();
            CreateMap<StudyEpochDto, StudyEpochEntity>().ReverseMap();
            CreateMap<StudyIdentifierDto, StudyIdentifierEntity>().ReverseMap();
            CreateMap<OrganisationDto, OrganisationEntity>().ReverseMap();
            CreateMap<StudyProtocolVersionDto, StudyProtocolVersionEntity>().ReverseMap();
            CreateMap<TransitionRuleDto, TransitionRuleEntity>().ReverseMap();
            CreateMap<AliasCodeDto, AliasCodeEntity>().ReverseMap();
            CreateMap<AddressDto, AddressEntity>().ReverseMap();
            CreateMap<BiomedicalConceptCategoryDto, BiomedicalConceptCategoryEntity>().ReverseMap();
            CreateMap<BiomedicalConceptDto, BiomedicalConceptEntity>().ReverseMap();
            CreateMap<BiomedicalConceptPropertyDto, BiomedicalConceptPropertyEntity>().ReverseMap();
            CreateMap<BiomedicalConceptSurrogateDto, BiomedicalConceptSurrogateEntity>().ReverseMap();
            CreateMap<ResponseCodeDto, ResponseCodeEntity>().ReverseMap();
            CreateMap<ScheduleTimelineExitDto, ScheduleTimelineExitEntity>().ReverseMap();
            CreateMap<ScheduleTimelineDto, ScheduleTimelineEntity>().ReverseMap();
            CreateMap<ScheduledInstanceDto, ScheduledInstanceEntity>()
                //.ForMember(dest => dest.ScheduleInstanceType, opt => opt.MapFrom(src => src.ScheduleInstanceType.ToString()))
                .Include<ScheduledDecisionInstanceDto, ScheduledDecisionInstanceEntity>()
                .Include<ScheduledActivityInstanceDto, ScheduledActivityInstanceEntity>()
                .ReverseMap();
            CreateMap<ScheduledDecisionInstanceDto, ScheduledDecisionInstanceEntity>().ReverseMap();
            CreateMap<ScheduledActivityInstanceDto, ScheduledActivityInstanceEntity>().ReverseMap();
            CreateMap<TimingDto, TimingEntity>().ReverseMap();

            //SoA V3
            CreateMap<ScheduleTimelineEntity, ScheduleTimelines>()
                .ForMember(dest => dest.ScheduleTimelineId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
            CreateMap<TimingEntity, TimingSoA>()
                .ForMember(dest => dest.TimingType, opt => opt.MapFrom(src => src.TimingType != null ? src.TimingType.Decode : null))
                .ReverseMap();

            //ECPT Mapper
            CreateMap<EndpointDto, TransCelerate.SDR.Core.DTO.eCPT.ObjectiveEndpointDto>()
                 .ForMember(dest => dest.EndpointLevel, opt => opt.MapFrom(src => src.Level != null ? src.Level.Decode : null))
                 .ReverseMap();

            CreateMap<ObjectiveDto, TransCelerate.SDR.Core.DTO.eCPT.ObjectivesDto>()
                  .ForMember(dest => dest.ObjectiveLevel, opt => opt.MapFrom(src => src.Level != null ? src.Level.Decode : null))
                  .ReverseMap();

            CreateMap<StudyArmDto, TransCelerate.SDR.Core.DTO.eCPT.StudyArmDto>()
                    .ForMember(dest => dest.ArmName, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.ArmDescription, opt => opt.MapFrom(src => src.Description))
                    .ForMember(dest => dest.ArmType, opt => opt.MapFrom(src => src.Type != null ? src.Type.Decode : null))
                    .ReverseMap();

            CreateMap<StudyInterventionDto, TransCelerate.SDR.Core.DTO.eCPT.StudyInterventionsAdministeredDto>()
                .ForMember(dest => dest.InterventionDescription, opt => opt.MapFrom(src => src.Description))
                .ReverseMap();

            CreateMap<StudyIdentifierDto, TransCelerate.SDR.Core.DTO.eCPT.RegulatoryAgencyIdentifierNumberDto>()
                .ForMember(dest => dest.RegulatoryAgencyNumber, opt => opt.MapFrom(src => src.StudyIdentifier))
                .ForMember(dest => dest.RegulatoryAgencyId, opt => opt.MapFrom(src => src.StudyIdentifierScope != null ? src.StudyIdentifierScope.OrganisationIdentifierScheme : null))
                .ReverseMap();
        }
    }
}
