using AutoMapper;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.Entities.StudyV2;

namespace TransCelerate.SDR.WebApi.Mappers
{
    public class AutoMapperProfilesV2 : Profile
    {
        public AutoMapperProfilesV2()
        {
            AllowNullCollections = true;
            AllowNullDestinationValues = true;

            CreateMap<StudyEntity, StudyDto>().ReverseMap();
            CreateMap<ActivityDto, ActivityEntity>().ReverseMap();
            CreateMap<AnalysisPopulationDto, AnalysisPopulationEntity>().ReverseMap();
            CreateMap<AuditTrailEntity, AuditTrailDto>()
                //.ForMember(dest => dest.EntryDateTime, opt => opt.MapFrom(src => src.EntryDateTime.ToString(Constants.DateFormats.DateFormatForAuditResponse).ToUpper()))
                .ReverseMap();
            CreateMap<ClinicalStudyDto, ClinicalStudyEntity>().ReverseMap();
            CreateMap<CodeDto, CodeEntity>().ReverseMap();
            CreateMap<ProcedureDto, ProcedureEntity>().ReverseMap();
            CreateMap<EncounterDto, EncounterEntity>().ReverseMap();
            CreateMap<EndpointEntity, EndpointDto>().ReverseMap();
            CreateMap<EstimandDto, EstimandEntity>().ReverseMap();
            CreateMap<IndicationDto, IndicationEntity>().ReverseMap();
            CreateMap<InterCurrentEventDto, InterCurrentEventEntity>().ReverseMap();
            CreateMap<InvestigationalInterventionDto, InvestigationalInterventionEntity>().ReverseMap();
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

            //Mapper for Study History
            CreateMap<StudyHistoryResponseEntity, UploadVersionDto>()
                 .ForMember(dest => dest.UploadVersion, opt => opt.MapFrom(src => src.SDRUploadVersion))
                 .ForMember(dest => dest.ProtocolVersions, opt => opt.MapFrom(src => src.ProtocolVersions))
                .ReverseMap();

            //Mapper for AuditTrail
            CreateMap<AuditTrailDto, AuditTrailResponseEntity>().ReverseMap();

            //ChangeAudit
            CreateMap<ChangeAuditStudyDto, ChangeAuditStudyEntity>().ReverseMap();
            CreateMap<ChangeAuditDto, ChangeAuditEntity>().ReverseMap();
            CreateMap<ChangesDto, ChangesEntity>().ReverseMap();
        }
    }
}
