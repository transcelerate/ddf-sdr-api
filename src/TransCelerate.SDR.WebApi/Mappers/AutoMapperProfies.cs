using AutoMapper;
using System.Linq;
using TransCelerate.SDR.Core.DTO;
using TransCelerate.SDR.Core.DTO.Study;
using TransCelerate.SDR.Core.Entities.Study;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.WebApi.Mappers
{
    /// <summary>
    /// This class is for creating the mappers between DTOs and Entities
    /// </summary>
    public class AutoMapperProfies : Profile
    {
        public AutoMapperProfies()
        {
            AllowNullCollections = true;
            AllowNullDestinationValues = true;

            //Mappers for search method                      

            CreateMap<GetStudyDTO, SearchResponse>()
               .ForMember(dest => dest.StudyId, opt => opt.MapFrom(src => src.ClinicalStudy.StudyId))
               .ForMember(dest => dest.StudyPhase, opt => opt.MapFrom(src => src.ClinicalStudy.StudyPhase))
               .ForMember(dest => dest.StudyTitle, opt => opt.MapFrom(src => src.ClinicalStudy.StudyTitle))
               .ForMember(dest => dest.StudyType, opt => opt.MapFrom(src => src.ClinicalStudy.StudyType))
               .ForMember(dest => dest.StudyTag, opt => opt.MapFrom(src => src.ClinicalStudy.StudyTag))
               .ForMember(dest => dest.StudyStatus, opt => opt.MapFrom(src => src.ClinicalStudy.StudyStatus))
               .ForMember(dest => dest.StudyIdentifiers, opt => opt.MapFrom(src => src.ClinicalStudy.StudyIdentifiers))
               .ForMember(dest => dest.EntrySystem, opt => opt.MapFrom(src => src.AuditTrail.EntrySystem))
               .ForMember(dest => dest.StudyVersion, opt => opt.MapFrom(src => src.AuditTrail.StudyVersion))
               .ForMember(dest => dest.UsdmVersion, opt => opt.MapFrom(src => src.AuditTrail.UsdmVersion))
               .ForMember(dest => dest.EntryDateTime, opt => opt.MapFrom(src => src.AuditTrail.EntryDateTime)).ReverseMap();

            CreateMap<SearchTitleDTO, SearchTitleEntity>()
               .ForMember(dest => dest.StudyId, opt => opt.MapFrom(src => src.ClinicalStudy.StudyId))
               .ForMember(dest => dest.StudyTitle, opt => opt.MapFrom(src => src.ClinicalStudy.StudyTitle))
               .ForMember(dest => dest.StudyTag, opt => opt.MapFrom(src => src.ClinicalStudy.StudyTag))
               .ForMember(dest => dest.StudyVersion, opt => opt.MapFrom(src => src.AuditTrail.StudyVersion))
               .ForMember(dest => dest.UsdmVersion, opt => opt.MapFrom(src => src.AuditTrail.UsdmVersion))
               .ForMember(dest => dest.EntryDateTime, opt => opt.MapFrom(src => src.AuditTrail.EntryDateTime)).ReverseMap();

            //Mappers for GET Methods
            CreateMap<StudyEntity, GetStudyDTO>();
            CreateMap<ClinicalStudyEntity, GetClinicalStudyDTO>()
                .ForMember(dest => dest.StudyDesigns, opt => opt.MapFrom(src => src.CurrentSections.Select(x => x.StudyDesigns).FirstOrDefault(s => s != null)))
                .ForMember(dest => dest.Objectives, opt => opt.MapFrom(src => src.CurrentSections.Select(x => x.Objectives).FirstOrDefault(s => s != null)))
                .ForMember(dest => dest.StudyIndications, opt => opt.MapFrom(src => src.CurrentSections.Select(x => x.StudyIndications).FirstOrDefault(s => s != null)));
            CreateMap<ClinicalStudyEntity, GetStudySectionsDTO>()
                .ForMember(dest => dest.StudyDesigns, opt => opt.MapFrom(src => src.CurrentSections.Select(x => x.StudyDesigns).FirstOrDefault(s => s != null)))
                .ForMember(dest => dest.Objectives, opt => opt.MapFrom(src => src.CurrentSections.Select(x => x.Objectives).FirstOrDefault(s => s != null)))
                .ForMember(dest => dest.StudyIndications, opt => opt.MapFrom(src => src.CurrentSections.Select(x => x.StudyIndications).FirstOrDefault(s => s != null)));

            CreateMap<StudyDesignEntity, GetStudyDesignsDTO>()
                .ForMember(dest => dest.InvestigationalInterventions, opt => opt.MapFrom(src => src.CurrentSections.Select(x => x.InvestigationalInterventions).FirstOrDefault(s => s != null)))
                .ForMember(dest => dest.PlannedWorkflows, opt => opt.MapFrom(src => src.CurrentSections.Select(x => x.PlannedWorkflows).FirstOrDefault(s => s != null)))
                .ForMember(dest => dest.StudyCells, opt => opt.MapFrom(src => src.CurrentSections.Select(x => x.StudyCells).FirstOrDefault(s => s != null)))
                .ForMember(dest => dest.StudyPopulations, opt => opt.MapFrom(src => src.CurrentSections.Select(x => x.StudyPopulations).FirstOrDefault(s => s != null)));
            CreateMap<StudyEntity, AuditTrailEndpointResponseDTO>()
                .ForMember(dest => dest.EntryDateTime, opt => opt.MapFrom(src => src.AuditTrail.EntryDateTime.ToString(Constants.DateFormats.DateFormatForAuditResponse).ToUpper()))
                .ForMember(dest => dest.EntrySystem, opt => opt.MapFrom(src => src.AuditTrail.EntrySystem))
                .ForMember(dest => dest.StudyVersion, opt => opt.MapFrom(src => src.AuditTrail.StudyVersion))
                .ForMember(dest => dest.UsdmVersion, opt => opt.MapFrom(src => src.AuditTrail.UsdmVersion))
                .ForMember(dest => dest.StudyTag, opt => opt.MapFrom(src => src.ClinicalStudy.StudyTag))
                .ForMember(dest => dest.StudyStatus, opt => opt.MapFrom(src => src.ClinicalStudy.StudyStatus));

            //Mappers for POST methods
            CreateMap<PostStudyDTO, StudyEntity>()
                .ReverseMap();

            //Mappers for both GET and POST
            CreateMap<AuditTrailEntity, AuditTrailDTO>()
                .ForMember(dest => dest.EntryDateTime, opt => opt.MapFrom(src => src.EntryDateTime.ToString(Constants.DateFormats.DateFormatForAuditResponse).ToUpper()))
                .ReverseMap();
            CreateMap<ClinicalStudyEntity, ClinicalStudyDTO>().ReverseMap();
            CreateMap<CodingEntity, CodingDTO>().ReverseMap();
            CreateMap<StudyProtocolEntity, StudyProtocolDTO>().ReverseMap();
            CreateMap<StudyDesignDTO, StudyDesignEntity>().ReverseMap();
            CreateMap<StudyIdentifierEntity, StudyIdentifierDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StudyIdentifierId)).ReverseMap();
            CreateMap<StudyPopulationEntity, StudyPopulationDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StudyPopulationId)).ReverseMap();
            CreateMap<StudyIndicationEntity, StudyIndicationDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StudyIndicationId)).ReverseMap();
            CreateMap<CurrentSectionsDTO, CurrentSectionsEntity>()
                .ForMember(dest => dest.CurrentSectionsId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<InvestigationalInterventionEntity, InvestigationalInterventionDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InvestigationalInterventionId))
                .ReverseMap();
            CreateMap<PlannedWorkflowDTO, PlannedWorkFlowEntity>()
                .ForMember(dest => dest.PlannedWorkFlowId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<PointInTimeEntity, PointInTimeDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PointInTimeId))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.ToString(Constants.DateFormats.DateFormatForAuditResponse).ToUpper()))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.ToString(Constants.DateFormats.DateFormatForAuditResponse).ToUpper()))
                .ReverseMap();
            CreateMap<RuleDTO, RuleEntity>()
                .ForMember(dest => dest.RuleId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<StudyArmDTO, StudyArmEntity>()
                .ForMember(dest => dest.StudyArmId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<StudyCellDTO, StudyCellEntity>()
                .ForMember(dest => dest.StudyCellId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<StudyElementDTO, StudyElementEntity>()
                .ForMember(dest => dest.StudyElementId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<StudyEpochDTO, StudyEpochEntity>()
                .ForMember(dest => dest.StudyEpochId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<StudyObjectiveDTO, StudyObjectiveEntity>()
                .ForMember(dest => dest.ObjectiveId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<EndpointsDTO, EndpointsEntity>()
                .ForMember(dest => dest.EndPointsId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<WorkflowItemMatrixDTO, WorkFlowItemMatrixEntity>()
                .ForMember(dest => dest.WorkFlowItemMatrixId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<MatrixDTO, MatrixEntity>()
                .ForMember(dest => dest.MatrixId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<ItemDTO, ItemEntity>()
                .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<ActivityDTO, ActivityEntity>()
                .ForMember(dest => dest.ActivityId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<DefinedProcedureDTO, DefinedProcedureEntity>().ReverseMap();
            CreateMap<StudyDataCollectionDTO, StudyDataCollectionEntity>()
                .ForMember(dest => dest.StudyDataCollectionId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<EncounterDTO, EncounterEntity>()
                .ForMember(dest => dest.EncounterId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<EpochDTO, EpochEntity>()
                .ForMember(dest => dest.EpochId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

            //Mapper for Search method request body
            CreateMap<SearchParametersDTO, SearchParameters>();
            CreateMap<SearchTitleParametersDTO, SearchTitleParameters>();

        }
    }
}
