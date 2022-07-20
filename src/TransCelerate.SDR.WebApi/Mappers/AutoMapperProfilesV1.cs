using AutoMapper;
using TransCelerate.SDR.Core.DTO.StudyV1;
using TransCelerate.SDR.Core.Entities.StudyV1;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.WebApi.Mappers
{
    public class AutoMapperProfilesV1 : Profile
    {
        public AutoMapperProfilesV1()
        {
            AllowNullCollections = true;
            AllowNullDestinationValues = true;

            CreateMap<StudyEntity, StudyDto>().ReverseMap();
            CreateMap<ActivityDto, ActivityEntity>().ReverseMap();
            CreateMap<AnalysisPopulationDto, AnalysisPopulationEntity>().ReverseMap();
            CreateMap<AuditTrailEntity, AuditTrailDto>()
                .ForMember(dest => dest.SDRUploadVersion, opt => opt.MapFrom(src => src.SDRUploadVersion))
                .ForMember(dest => dest.EntryDateTime, opt => opt.MapFrom(src => src.EntryDateTime.ToString(Constants.DateFormats.DateFormatForAuditResponse).ToUpper()))
                .ReverseMap();
            CreateMap<ClinicalStudyDto, ClinicalStudyEntity>().ReverseMap();
            CreateMap<CodeDto, CodeEntity>().ReverseMap();
            CreateMap<DefinedProcedureDto, DefinedProcedureEntity>().ReverseMap();
            CreateMap<EncounterDto, EncounterEntity>().ReverseMap();
            CreateMap<EndpointEntity, EndpointDto>().ReverseMap();
            CreateMap<EstimandDto, EstimandEntity>().ReverseMap();
            CreateMap<IndicationDto, IndicationEntity>().ReverseMap();
            CreateMap<InterCurrentEventDto, InterCurrentEventEntity>().ReverseMap();
            CreateMap<InvestigationalInterventionDto, InvestigationalInterventionEntity>().ReverseMap();
            CreateMap<ObjectiveDto, ObjectiveEntity>().ReverseMap();
            CreateMap<StudyArmDto, StudyArmEntity>().ReverseMap();
            CreateMap<StudyCellDto, StudyCellEntity>().ReverseMap();
            CreateMap<StudyDataCollectionDto, StudyDataCollectionEntity>().ReverseMap();
            CreateMap<StudyDesignDto, StudyDesignEntity>().ReverseMap();
            CreateMap<StudyDesignPopulationDto, StudyDesignPopulationEntity>().ReverseMap();
            CreateMap<StudyElementDto, StudyElementEntity>().ReverseMap();
            CreateMap<StudyEpochDto, StudyEpochEntity>().ReverseMap();
            CreateMap<StudyIdentifierDto, StudyIdentifierEntity>().ReverseMap();
            CreateMap<StudyIdentifiersScopeDto, StudyIdentifierScopeEntity>().ReverseMap();                                               
            CreateMap<StudyProtocolVersionDto, StudyProtocolVersionEntity>().ReverseMap();
            CreateMap<TransitionRuleDto, TransitionRuleEntity>().ReverseMap();
            CreateMap<WorkflowDto, WorkflowEntity>().ReverseMap();
            CreateMap<WorkflowItemDto, WorkFlowItemEntity>().ReverseMap();

            //Mapper for Search method request body
            CreateMap<SearchParametersDto, SearchParameters>();


            //Mapper for Search Response
            CreateMap<StudyDto, SearchResponseEntity>()
                .ForMember(dest => dest.Uuid, opt => opt.MapFrom(src => src.ClinicalStudy.Uuid))
                .ForMember(dest => dest.StudyTitle, opt => opt.MapFrom(src => src.ClinicalStudy.StudyTitle))
                .ForMember(dest => dest.StudyIdentifiers, opt => opt.MapFrom(src => src.ClinicalStudy.StudyIdentifiers))                
                .ForMember(dest => dest.StudyType, opt => opt.MapFrom(src => src.ClinicalStudy.StudyType))
                .ForMember(dest => dest.StudyPhase, opt => opt.MapFrom(src => src.ClinicalStudy.StudyPhase))
                .ForMember(dest => dest.EntryDateTime, opt => opt.MapFrom(src => src.AuditTrail.EntryDateTime))
                .ForMember(dest => dest.SDRUploadVersion, opt => opt.MapFrom(src => src.AuditTrail.SDRUploadVersion))
                .ReverseMap();



        }
    }
}
