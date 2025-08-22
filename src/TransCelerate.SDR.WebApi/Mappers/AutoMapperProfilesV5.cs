using AutoMapper;
using System;
using TransCelerate.SDR.Core.DTO.StudyV5;
using TransCelerate.SDR.Core.Entities.StudyV5;

namespace TransCelerate.SDR.WebApi.Mappers
{
    public class AutoMapperProfilesV5 : Profile
    {
        public AutoMapperProfilesV5()
        {
            AllowNullCollections = true;
            AllowNullDestinationValues = true;


            CreateMap<ActivityDto, ActivityEntity>().ReverseMap();
            CreateMap<AddressDto, AddressEntity>().ReverseMap();
            CreateMap<AdministrableProductIdentifierDto, AdministrableProductIdentifierEntity>().ReverseMap();
            CreateMap<AdministrableProductPropertyDto, AdministrableProductPropertyEntity>().ReverseMap();
            CreateMap<AdministrableProductDto, AdministrableProductEntity>().ReverseMap();
            CreateMap<AdministrationDto, AdministrationEntity>().ReverseMap();
            CreateMap<AliasCodeDto, AliasCodeEntity>().ReverseMap();
            CreateMap<AnalysisPopulationDto, AnalysisPopulationEntity>().ReverseMap();
            CreateMap<AssignedPersonDto, AssignedPersonEntity>().ReverseMap();
            CreateMap<AuditTrailEntity, AuditTrailDto>().ReverseMap();
            CreateMap<BiomedicalConceptCategoryDto, BiomedicalConceptCategoryEntity>().ReverseMap();
            CreateMap<BiomedicalConceptDto, BiomedicalConceptEntity>().ReverseMap();
            CreateMap<BiomedicalConceptPropertyDto, BiomedicalConceptPropertyEntity>().ReverseMap();
            CreateMap<BiomedicalConceptSurrogateDto, BiomedicalConceptSurrogateEntity>().ReverseMap();
            CreateMap<BiospecimenRetentionDto, BiospecimenRetentionEntity>().ReverseMap();
            CreateMap<CharacteristicDto, CharacteristicEntity>().ReverseMap();
            CreateMap<CodeDto, CodeEntity>().ReverseMap();
            CreateMap<ConditionAssignmentDto, ConditionAssignmentEntity>().ReverseMap();
            CreateMap<ConditionDto, ConditionEntity>().ReverseMap();
            CreateMap<DocumentContentReferenceDto, DocumentContentReferenceEntity>().ReverseMap();
            CreateMap<DurationDto, DurationEntity>().ReverseMap();
            CreateMap<EligibilityCriterionDto, EligibilityCriterionEntity>().ReverseMap();
            CreateMap<EligibilityCriterionItemDto, EligibilityCriterionItemEntity>().ReverseMap();
            CreateMap<EncounterDto, EncounterEntity>().ReverseMap();
            CreateMap<EndpointEntity, EndpointDto>().ReverseMap();
            CreateMap<EstimandDto, EstimandEntity>().ReverseMap();
            CreateMap<GeographicScopeDto, GeographicScopeEntity>().ReverseMap();
            CreateMap<GovernanceDateDto, GovernanceDateEntity>().ReverseMap();
            CreateMap<IdentifierDto, IdentifierEntity>().ReverseMap();
            CreateMap<IndicationDto, IndicationEntity>().ReverseMap();
            CreateMap<IngredientDto, IngredientEntity>().ReverseMap();
            CreateMap<IntercurrentEventDto, IntercurrentEventEntity>().ReverseMap();
            CreateMap<InterventionalStudyDesignDto, InterventionalStudyDesignEntity>().ReverseMap();
            CreateMap<MaskingDto, MaskingEntity>().ReverseMap();
            CreateMap<MedicalDeviceDto, MedicalDeviceEntity>().ReverseMap();
            CreateMap<MedicalDeviceIdentifierDto, MedicalDeviceIdentifierEntity>().ReverseMap();
            CreateMap<NarrativeContentDto, NarrativeContentEntity>().ReverseMap();
            CreateMap<ObjectiveDto, ObjectiveEntity>().ReverseMap();
            CreateMap<ObservationalStudyDesignDto, ObservationalStudyDesignEntity>().ReverseMap();
            CreateMap<OrganizationDto, OrganizationEntity>().ReverseMap();
            CreateMap<ParameterMapDto, ParameterMapEntity>().ReverseMap();
            CreateMap<PersonNameDto, PersonNameEntity>().ReverseMap();
            //        CreateMap<PopulationDefinitionDto, PopulationDefinitionEntity>()
            //.ForMember(dest => dest.Criteria, opt => opt.MapFrom(src => src.Criterionids))
            //.Include<StudyDesignPopulationDto, StudyDesignPopulationEntity>()
            //            .Include<StudyCohortDto, StudyCohortEntity>()
            //            .ReverseMap()
            //.ForMember(dest => dest.Criterionids, opt => opt.MapFrom(src => src.Criteria));
            CreateMap<ProcedureDto, ProcedureEntity>().ReverseMap();
            CreateMap<ProductOrganizationRoleDto, ProductOrganizationRoleEntity>().ReverseMap();
            CreateMap<QuantityDto, QuantityEntity>().ReverseMap();
            CreateMap<QuantityRangeDto, QuantityRangeEntity>()
                .Include<QuantityDto, QuantityEntity>()
                .Include<RangeDto, RangeEntity>()
                .ReverseMap();
            CreateMap<RangeDto, RangeEntity>().ReverseMap();
            CreateMap<ReferenceIdentifierDto, ReferenceIdentifierEntity>().ReverseMap();
            CreateMap<ResponseCodeDto, ResponseCodeEntity>().ReverseMap();
            CreateMap<ScheduleTimelineDto, ScheduleTimelineEntity>().ReverseMap();
            CreateMap<ScheduleTimelineExitDto, ScheduleTimelineExitEntity>().ReverseMap();
            CreateMap<ScheduledActivityInstanceDto, ScheduledActivityInstanceEntity>().ReverseMap();
            CreateMap<ScheduledDecisionInstanceDto, ScheduledDecisionInstanceEntity>()
                .ReverseMap();

            CreateMap<ScheduledInstanceDto, ScheduledInstanceEntity>()
                .Include<ScheduledDecisionInstanceDto, ScheduledDecisionInstanceEntity>()
                .Include<ScheduledActivityInstanceDto, ScheduledActivityInstanceEntity>()
                .ReverseMap();
            CreateMap<StrengthDto, StrengthEntity>().ReverseMap();
            CreateMap<StudyArmDto, StudyArmEntity>().ReverseMap();
            CreateMap<StudyAmendmentDto, StudyAmendmentEntity>().ReverseMap();
            CreateMap<StudyAmendmentImpactDto, StudyAmendmentImpactEntity>().ReverseMap();
            CreateMap<StudyAmendmentReasonDto, StudyAmendmentReasonEntity>().ReverseMap();
            CreateMap<StudyCellDto, StudyCellEntity>().ReverseMap();
            CreateMap<StudyChangeDto, StudyChangeEntity>().ReverseMap();
			//CreateMap<StudyCohortDto, StudyCohortEntity>().ReverseMap();
			CreateMap<StudyDefinitionsEntity, StudyDefinitionsDto>()
                .ForMember(dest => dest.Links, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<StudyDesignDto, StudyDesignEntity>().ReverseMap();
            CreateMap<StudyDesignPopulationDto, StudyDesignPopulationEntity>()
            //.ForMember(dest => dest.Criterionids, opt => opt.MapFrom(src => src.Criterionids))
            .ReverseMap();
            //.ForMember(dest => dest.Criterionids, opt => opt.MapFrom(src => src.Criteria));			
            CreateMap<StudyElementDto, StudyElementEntity>().ReverseMap();
            //CreateMap<StudyVersionDto, StudyVersionEntity>().ReverseMap();
            CreateMap<StudyDto, StudyEntity>().ReverseMap();
            CreateMap<StudyVersionDto, StudyVersionEntity>().ReverseMap();
            //.ForMember(dest => dest.DocumentVersionId, opt => opt.MapFrom(src => src.DocumentVersionIds))
            //.ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
            //.ForMember(dest => dest.BusinessTherapeuticAreas, opt => opt.MapFrom(src => src.BusinessTherapeuticAreas))
            //.ForMember(dest => dest.Titles, opt => opt.MapFrom(src => src.Titles))
            //.ForMember(dest => dest.DateValues, opt => opt.MapFrom(src => src.DateValues))
            //.ForMember(dest => dest.StudyIdentifiers, opt => opt.MapFrom(src => src.StudyIdentifiers))
            //.ForMember(dest => dest.Amendments, opt => opt.MapFrom(src => src.Amendments))
            //.ForMember(dest => dest.StudyPhase, opt => opt.MapFrom(src => src.StudyPhase))
            //.ForMember(dest => dest.StudyDesigns, opt => opt.MapFrom(src => src.StudyDesigns))
            //.ForMember(dest => dest.StudyType, opt => opt.MapFrom(src => src.StudyType))
            //.ForMember(dest => dest.InstanceType, opt => opt.MapFrom(src => src.InstanceType))
            // Ignore unmapped properties in the DTO
            //.ForSourceMember(src => src.Criteria, opt => opt.DoNotValidate())
            //      .ForSourceMember(src => src.NarrativeContentItem, opt => opt.DoNotValidate())
            //      .ForSourceMember(src => src.Abbreviation, opt => opt.DoNotValidate()).ReverseMap();
            CreateMap<NarrativeContentItemDto, NarrativeContentItemEntity>().ReverseMap();
            CreateMap<AbbreviationDto, AbbreviationEntity>().ReverseMap();
            CreateMap<PopulationDefinitionDto, PopulationDefinitionEntity>().ReverseMap();
            CreateMap<StudyCohortDto, StudyCohortEntity>()
                .IncludeBase<PopulationDefinitionDto, PopulationDefinitionEntity>()
                .ReverseMap();

            //// Study and StudyVersion Mapping
            //CreateMap<StudyVersionDto, StudyVersionEntity>()
            //	.ForMember(dest => dest.DocumentVersionId, opt => opt.MapFrom(src => src.DocumentVersionIds))
            //	.ReverseMap();

            //CreateMap<StudyVersionDto, StudyVersionEntity>()
            //	.ForMember(dest => dest.DocumentVersionId, opt => opt.MapFrom(src => src.DocumentVersionIds))
            //	.ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
            //	.ForMember(dest => dest.BusinessTherapeuticAreas, opt => opt.MapFrom(src => src.BusinessTherapeuticAreas))
            //	.ForMember(dest => dest.Titles, opt => opt.MapFrom(src => src.Titles))
            //	.ForMember(dest => dest.DateValues, opt => opt.MapFrom(src => src.DateValues))
            //	.ForMember(dest => dest.StudyIdentifiers, opt => opt.MapFrom(src => src.StudyIdentifiers))
            //	.ForMember(dest => dest.Amendments, opt => opt.MapFrom(src => src.Amendments))
            //	.ForMember(dest => dest.StudyPhase, opt => opt.MapFrom(src => src.StudyPhase))
            //	.ForMember(dest => dest.StudyDesigns, opt => opt.MapFrom(src => src.StudyDesigns))
            //	.ForMember(dest => dest.StudyType, opt => opt.MapFrom(src => src.StudyType))
            //	.ForMember(dest => dest.InstanceType, opt => opt.MapFrom(src => src.InstanceType))
            //	.ReverseMap();		

            //CreateMap<StudyDto, StudyEntity>()
            //.ForMember(dest => dest.Versions, opt => opt.MapFrom(src => src.Versions ?? new List<StudyVersionDto>()))
            //.ForMember(dest => dest.Versions, opt => opt.MapFrom(src => src.Versions ?? new List<StudyVersionDto>()))
            //      .ForMember(dest => dest.DocumentedBy, opt => opt.MapFrom(src => src.DocumentedBy ?? new StudyProtocolDocumentDto()))
            //.ReverseMap(); ---
            CreateMap<StudyEpochDto, StudyEpochEntity>().ReverseMap();
            CreateMap<StudyIdentifierDto, StudyIdentifierEntity>().ReverseMap();
            CreateMap<StudyInterventionDto, StudyInterventionEntity>().ReverseMap();
            CreateMap<StudyDefinitionDocumentDto, StudyDefinitionDocumentEntity>().ReverseMap();
            CreateMap<StudyDefinitionDocumentVersionDto, StudyDefinitionDocumentVersionEntity>().ReverseMap();
            CreateMap<StudyRoleDto, StudyRoleEntity>().ReverseMap();
            CreateMap<StudySiteDto, StudySiteEntity>().ReverseMap();
            CreateMap<StudyTitleDto, StudyTitleEntity>().ReverseMap();
            CreateMap<SubstanceDto, SubstanceEntity>().ReverseMap();
            //CreateMap<StudyVersionDto, StudyVersionEntity>().ReverseMap();
            CreateMap<SubjectEnrollmentDto, SubjectEnrollmentEntity>().ReverseMap();
            CreateMap<SyntaxTemplateDictionaryDto, SyntaxTemplateDictionaryEntity>()
                .ForMember(dest => dest.Text, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<SyntaxTemplateDto, SyntaxTemplateEntity>()
                .Include<ObjectiveDto, ObjectiveEntity>()
                .Include<EndpointDto, EndpointEntity>()
                .Include<EligibilityCriterionItemDto, EligibilityCriterionItemEntity>()
                .Include<CharacteristicDto, CharacteristicEntity>()
              .ReverseMap();

            CreateMap<TimingDto, TimingEntity>().ReverseMap();
            CreateMap<TransitionRuleDto, TransitionRuleEntity>().ReverseMap();
            CreateMap<CommentAnnotationDto, CommentAnnotationEntity>().ReverseMap();

            //SoA V5
            CreateMap<ScheduleTimelineEntity, ScheduleTimelines>()
                .ForMember(dest => dest.ScheduleTimelineId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ScheduleTimelineName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ScheduleTimelineDescription, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ScheduleTimelineSoA, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<TimingEntity, TimingSoA>()
                .ForMember(dest => dest.TimingType, opt => opt.MapFrom(src => src.Type != null ? src.Type.Decode : null))
                .ForMember(dest => dest.TimingWindow, opt => opt.MapFrom(src => src.WindowLabel))
                .ForMember(dest => dest.TimingValue, opt => opt.MapFrom(src => !String.IsNullOrWhiteSpace(src.Description) ? $"{src.Value} : {src.Description}" : src.Value))
                .ForMember(dest => dest.Activities, opt => opt.Ignore())
                .ReverseMap();

            //ECPT Mapper
            CreateMap<EndpointDto, TransCelerate.SDR.Core.DTO.eCPT.ObjectiveEndpointDto>()
                 .ForMember(dest => dest.EndpointLevel, opt => opt.MapFrom(src => src.Level != null ? src.Level.Decode : null))
                 .ForMember(dest => dest.EndpointDescription, opt => opt.MapFrom(src => src.Description))
                 .ForMember(dest => dest.EndpointPurposeDescription, opt => opt.MapFrom(src => src.Purpose))
                 .ReverseMap();

            CreateMap<ObjectiveDto, TransCelerate.SDR.Core.DTO.eCPT.ObjectivesDto>()
                  .ForMember(dest => dest.ObjectiveLevel, opt => opt.MapFrom(src => src.Level != null ? src.Level.Decode : null))
                  .ForMember(dest => dest.ObjectiveEndpoints, opt => opt.MapFrom(src => src.Endpoints))
                  .ForMember(dest => dest.ObjectiveDescription, opt => opt.MapFrom(src => src.Description))
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
                .ForMember(dest => dest.RegulatoryAgencyNumber, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.RegulatoryAgencyId, opt => opt.MapFrom(src => src.ScopeId))
                .ReverseMap();
        }
    }
}
