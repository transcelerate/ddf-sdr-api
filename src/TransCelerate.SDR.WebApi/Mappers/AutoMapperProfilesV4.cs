using AutoMapper;
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


            CreateMap<ActivityDto, ActivityEntity>().ReverseMap();
            CreateMap<AddressDto, AddressEntity>().ReverseMap();
            CreateMap<AdministrationDurationDto, AdministrationDurationEntity>().ReverseMap();
            CreateMap<AgentAdministrationDto, AgentAdministrationEntity>().ReverseMap();
            CreateMap<AliasCodeDto, AliasCodeEntity>().ReverseMap();
            CreateMap<AnalysisPopulationDto, AnalysisPopulationEntity>().ReverseMap();
            CreateMap<AuditTrailEntity, AuditTrailDto>().ReverseMap();
            CreateMap<BiomedicalConceptCategoryDto, BiomedicalConceptCategoryEntity>().ReverseMap();
            CreateMap<BiomedicalConceptDto, BiomedicalConceptEntity>().ReverseMap();
            CreateMap<BiomedicalConceptPropertyDto, BiomedicalConceptPropertyEntity>().ReverseMap();
            CreateMap<BiomedicalConceptSurrogateDto, BiomedicalConceptSurrogateEntity>().ReverseMap();
            CreateMap<CharacteristicDto, CharacteristicEntity>().ReverseMap();
            CreateMap<CodeDto, CodeEntity>().ReverseMap();
            CreateMap<EligibilityCriteriaDto, EligibilityCriteriaEntity>().ReverseMap();
            CreateMap<EncounterDto, EncounterEntity>().ReverseMap();
            CreateMap<EndpointEntity, EndpointDto>().ReverseMap();
            CreateMap<EstimandDto, EstimandEntity>().ReverseMap();
            CreateMap<GeographicScopeDto, GeographicScopeEntity>().ReverseMap();
            CreateMap<GovernanceDateDto, GovernanceDateEntity>().ReverseMap();
            CreateMap<IndicationDto, IndicationEntity>().ReverseMap();            
            CreateMap<InterCurrentEventDto, InterCurrentEventEntity>().ReverseMap();
            CreateMap<NarrativeContentDto, NarrativeContentEntity>().ReverseMap();
            CreateMap<ObjectiveDto, ObjectiveEntity>().ReverseMap();
            CreateMap<OrganisationDto, OrganisationEntity>().ReverseMap();
            CreateMap<PopulationDefinitionDto, PopulationDefinitionEntity>()
                .Include<StudyDesignPopulationDto, StudyDesignPopulationEntity>()
                .Include<StudyCohortDto, StudyCohortEntity>()
                .ReverseMap();
            CreateMap<ProcedureDto, ProcedureEntity>().ReverseMap();
            CreateMap<QuantityDto, QuantityEntity>().ReverseMap();
            CreateMap<RangeDto, RangeEntity>().ReverseMap();
            CreateMap<ResponseCodeDto, ResponseCodeEntity>().ReverseMap();
            CreateMap<ScheduleTimelineDto, ScheduleTimelineEntity>().ReverseMap();
            CreateMap<ScheduleTimelineExitDto, ScheduleTimelineExitEntity>().ReverseMap();
            CreateMap<ScheduledActivityInstanceDto, ScheduledActivityInstanceEntity>().ReverseMap();
            CreateMap<ScheduledDecisionInstanceDto, ScheduledDecisionInstanceEntity>().ReverseMap();
            CreateMap<ScheduledInstanceDto, ScheduledInstanceEntity>()
                .Include<ScheduledDecisionInstanceDto, ScheduledDecisionInstanceEntity>()
                .Include<ScheduledActivityInstanceDto, ScheduledActivityInstanceEntity>()
                .ReverseMap();
            CreateMap<StudyArmDto, StudyArmEntity>().ReverseMap();
            CreateMap<StudyAmendmentDto, StudyAmendmentEntity>().ReverseMap();
            CreateMap<StudyAmendmentReasonDto, StudyAmendmentReasonEntity>().ReverseMap();
            CreateMap<StudyCellDto, StudyCellEntity>().ReverseMap();
            CreateMap<StudyCohortDto, StudyCohortEntity>().ReverseMap();
            CreateMap<StudyDefinitionsEntity, StudyDefinitionsDto>().ReverseMap();
            CreateMap<StudyDesignDto, StudyDesignEntity>().ReverseMap();
            CreateMap<StudyDesignPopulationDto, StudyDesignPopulationEntity>().ReverseMap();
            CreateMap<StudyElementDto, StudyElementEntity>().ReverseMap();
            CreateMap<StudyDto, StudyEntity>().ReverseMap();
            CreateMap<StudyEpochDto, StudyEpochEntity>().ReverseMap();
            CreateMap<StudyIdentifierDto, StudyIdentifierEntity>().ReverseMap();
            CreateMap<StudyInterventionDto, StudyInterventionEntity>().ReverseMap();
            CreateMap<StudyProtocolDocumentDto, StudyProtocolDocumentEntity>().ReverseMap();
            CreateMap<StudyProtocolDocumentVersionDto, StudyProtocolDocumentVersionEntity>().ReverseMap();
            CreateMap<StudyVersionDto, StudyVersionEntity>().ReverseMap();
            CreateMap<SubjectEnrollmentDto, SubjectEnrollmentEntity>().ReverseMap();
            CreateMap<SyntaxTemplateDictionaryDto, SyntaxTemplateDictionaryEntity>().ReverseMap();
            CreateMap<SyntaxTemplateDto, SyntaxTemplateEntity>()
                .Include<ObjectiveDto, ObjectiveEntity>()
                .Include<EndpointDto, EndpointEntity>()
                .Include<EligibilityCriteriaDto, EligibilityCriteriaEntity>()
                .Include<CharacteristicDto, CharacteristicEntity>()
                .ReverseMap();
            CreateMap<TimingDto, TimingEntity>().ReverseMap();
            CreateMap<TransitionRuleDto, TransitionRuleEntity>().ReverseMap();


            //SoA V3
            CreateMap<ScheduleTimelineEntity, ScheduleTimelines>()
                .ForMember(dest => dest.ScheduleTimelineId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ScheduleTimelineName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ScheduleTimelineDescription, opt => opt.MapFrom(src => src.Description))
                .ReverseMap();
            CreateMap<TimingEntity, TimingSoA>()
                .ForMember(dest => dest.TimingType, opt => opt.MapFrom(src => src.Type != null ? src.Type.Decode : null))
                .ForMember(dest => dest.TimingWindow, opt => opt.MapFrom(src => src.Window))
                .ForMember(dest => dest.TimingValue, opt => opt.MapFrom(src => src.Value))
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
