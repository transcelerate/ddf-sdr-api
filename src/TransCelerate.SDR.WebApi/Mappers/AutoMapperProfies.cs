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

namespace TransCelerate.SDR.WebApi.Mappers
{
    public class AutoMapperProfies : Profile
    {
        public AutoMapperProfies()
        {
            AllowNullCollections = true;
            AllowNullDestinationValues = true;

            //Mappers for GET Methods
            CreateMap<StudyEntity, GetStudyDTO>();
            CreateMap<ClinicalStudyEntity, GetClinicalStudyDTO>()
                .ForMember(dest => dest.studyDesigns, opt => opt.MapFrom(src => src.currentSections.Select(x => x.studyDesigns).FirstOrDefault(s => s != null)))
                .ForMember(dest => dest.objectives, opt => opt.MapFrom(src => src.currentSections.Select(x => x.objectives).FirstOrDefault(s => s != null)))
                .ForMember(dest => dest.studyIndications, opt => opt.MapFrom(src => src.currentSections.Select(x => x.studyIndications).FirstOrDefault(s => s != null)));                
            CreateMap<ClinicalStudyEntity, GetStudySectionsDTO>()                
                .ForMember(dest => dest.studyDesigns, opt => opt.MapFrom(src => src.currentSections.Select(x => x.studyDesigns).FirstOrDefault(s => s != null)))
                .ForMember(dest => dest.objectives, opt => opt.MapFrom(src => src.currentSections.Select(x => x.objectives).FirstOrDefault(s => s != null)))                
                .ForMember(dest => dest.studyIndications, opt => opt.MapFrom(src => src.currentSections.Select(x => x.studyIndications).FirstOrDefault(s => s != null)));
                
            CreateMap<StudyDesignEntity, GetStudyDesignsDTO>()
                .ForMember(dest => dest.investigationalInterventions, opt => opt.MapFrom(src => src.currentSections.Select(x => x.investigationalInterventions).FirstOrDefault(s => s != null)))
                .ForMember(dest => dest.plannedWorkflows, opt => opt.MapFrom(src => src.currentSections.Select(x => x.plannedWorkflows).FirstOrDefault(s => s != null)))
                .ForMember(dest => dest.studyCells, opt => opt.MapFrom(src => src.currentSections.Select(x => x.studyCells).FirstOrDefault(s => s != null)))
                .ForMember(dest => dest.studyPopulations, opt => opt.MapFrom(src => src.currentSections.Select(x => x.studyPopulations).FirstOrDefault(s => s != null)));
            CreateMap<StudyEntity, AuditTrailDTO>()
                .ForMember(dest => dest.entryDateTime, opt => opt.MapFrom(src => src.auditTrail.entryDateTime.ToString(Constants.DateFormats.DateFormatForAuditResponse).ToUpper()))
                .ForMember(dest => dest.entrySystem, opt => opt.MapFrom(src => src.auditTrail.entrySystem))
                .ForMember(dest => dest.studyVersion, opt => opt.MapFrom(src => src.auditTrail.studyVersion))
                .ForMember(dest => dest.studyTag, opt => opt.MapFrom(src => src.clinicalStudy.studyTag))
                .ForMember(dest => dest.studyStatus, opt => opt.MapFrom(src => src.clinicalStudy.studyStatus));

            //Mappers for POST methods
            CreateMap<PostStudyDTO, StudyEntity>()
                .ReverseMap();        

            //Mappers for both GET and POST
            CreateMap<AuditTrailEntity, AuditTrailDTO>()
                .ForMember(dest => dest.entryDateTime, opt => opt.MapFrom(src => src.entryDateTime.ToString(Constants.DateFormats.DateFormatForAuditResponse).ToUpper()))
                .ReverseMap();
            CreateMap<ClinicalStudyEntity, ClinicalStudyDTO>().ReverseMap();                          
            CreateMap<CodingEntity, CodingDTO>().ReverseMap();                      
            CreateMap<StudyProtocolEntity, StudyProtocolDTO>().ReverseMap();
            CreateMap<AmendmentEntity, AmendmentDTO>().ReverseMap();
            CreateMap<StudyDesignDTO, StudyDesignEntity>().ReverseMap();            
            CreateMap<StudyIdentifierEntity, StudyIdentifierDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.studyIdentifierId)).ReverseMap();
            CreateMap<StudyPopulationEntity, StudyPopulationDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.studyPopulationId)).ReverseMap();
            CreateMap<StudyIndicationEntity, StudyIndicationDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.studyIndicationId)).ReverseMap();                                             
            CreateMap<CurrentSectionsDTO, CurrentSectionsEntity>()
                .ForMember(dest => dest.currentSectionsId, opt => opt.MapFrom(src => src.id)).ReverseMap();            
            CreateMap<InvestigationalInterventionEntity, InvestigationalInterventionDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.investigationalInterventionId))
                .ReverseMap();
            CreateMap<PlannedWorkflowDTO, PlannedWorkFlowEntity>()
                .ForMember(dest => dest.plannedWorkFlowId, opt => opt.MapFrom(src => src.id)).ReverseMap();
            CreateMap<PointInTimeEntity, PointInTimeDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.pointInTimeId))
                .ForMember(dest=>dest.endDate,opt=>opt.MapFrom(src=>src.endDate.ToString(Constants.DateFormats.DateFormatForAuditResponse).ToUpper()))
                .ForMember(dest=>dest.startDate,opt=>opt.MapFrom(src=>src.startDate.ToString(Constants.DateFormats.DateFormatForAuditResponse).ToUpper()))
                .ReverseMap();
            CreateMap<RuleDTO, RuleEntity>()
                .ForMember(dest => dest.RuleId, opt => opt.MapFrom(src => src.id)).ReverseMap();
            CreateMap<StudyArmDTO, StudyArmEntity>()
                .ForMember(dest => dest.studyArmId, opt => opt.MapFrom(src => src.id)).ReverseMap();
            CreateMap<StudyCellDTO, StudyCellEntity>()
                .ForMember(dest => dest.studyCellId, opt => opt.MapFrom(src => src.id)).ReverseMap();            
            CreateMap<StudyElementDTO, StudyElementEntity>()
                .ForMember(dest => dest.studyElementId, opt => opt.MapFrom(src => src.id)).ReverseMap();            
            CreateMap<StudyEpochDTO, StudyEpochEntity>()
                .ForMember(dest => dest.studyEpochId, opt => opt.MapFrom(src => src.id)).ReverseMap();
            //CreateMap<TransitionCriteriaDTO, TransitionCriteriaEntity>()
            //    .ForMember(dest => dest.transitionCriteriaId, opt => opt.MapFrom(src => src.id)).ReverseMap();
            //CreateMap<TransitionDTO, TransitionEntity>()
            //    .ForMember(dest => dest.transitionId, opt => opt.MapFrom(src => src.id)).ReverseMap();
            //CreateMap<TransitionRuleDTO, TransitionRuleEntity>()
            //    .ForMember(dest => dest.transitionRuleId, opt => opt.MapFrom(src => src.id)).ReverseMap();
            CreateMap<StudyObjectiveDTO, StudyObjectiveEntity>()
                .ForMember(dest => dest.objectiveId, opt => opt.MapFrom(src => src.id)).ReverseMap();
            CreateMap<EndpointsDTO, EndpointsEntity>()
                .ForMember(dest => dest.endPointsId, opt => opt.MapFrom(src => src.id)).ReverseMap();
            CreateMap<WorkflowItemMatrixDTO, WorkFlowItemMatrixEntity>()
                .ForMember(dest => dest.workFlowItemMatrixId, opt => opt.MapFrom(src => src.id)).ReverseMap();
            CreateMap<MatrixDTO, MatrixEntity>()
                .ForMember(dest => dest.matrixId, opt => opt.MapFrom(src => src.id)).ReverseMap();
            CreateMap<ItemDTO, ItemEntity>()
                .ForMember(dest => dest.itemId, opt => opt.MapFrom(src => src.id)).ReverseMap();
            CreateMap<ActivityDTO, ActivityEntity>()
                .ForMember(dest => dest.activityId, opt => opt.MapFrom(src => src.id)).ReverseMap();
            CreateMap<DefinedProcedureDTO, DefinedProcedureEntity>().ReverseMap();
            CreateMap<StudyDataCollectionDTO, StudyDataCollectionEntity>()
                .ForMember(dest => dest.studyDataCollectionId, opt => opt.MapFrom(src => src.id)).ReverseMap();
            CreateMap<EncounterDTO, EncounterEntity>()
                .ForMember(dest => dest.encounterId, opt => opt.MapFrom(src => src.id)).ReverseMap();
            CreateMap<EpochDTO, EpochEntity>()
                .ForMember(dest => dest.epochId, opt => opt.MapFrom(src => src.id)).ReverseMap();

            //Mapper for Search method request body
            CreateMap<SearchParametersDTO, SearchParameters>();         
        }
    }
}
