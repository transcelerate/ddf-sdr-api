using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TransCelerate.SDR.Core.DTO;
using TransCelerate.SDR.Core.DTO.Study;

namespace TransCelerate.SDR.RuleEngine
{
    public static class ValidationDependencies
    {
        public static IServiceCollection AddValidationDependencies(this IServiceCollection services)
        {
            //Study Level Validators
            services.AddTransient<IValidator<PostStudyDTO>, ClinicalStudyValidator>();
            services.AddTransient<IValidator<StudyIdentifierDTO>, StudyIdentifiersValidator>();
            services.AddTransient<IValidator<StudyProtocolDTO>, StudyProtocolValidator>();
            //Section Level Validators
            ///Investigational Intervention Section
            services.AddTransient<IValidator<InvestigationalInterventionDTO>, InvestigationalInterventionValidatior>();
            ///Study Objectives Section
            services.AddTransient<IValidator<StudyObjectiveDTO>, StudyObjectivesValidator>();            
            ///Study Indications Section
            services.AddTransient<IValidator<StudyIndicationDTO>, StudyIndicationValidator>();
            ///Study Design Section
            ////Study Population Section
            services.AddTransient<IValidator<StudyPopulationDTO>, StudyPopulationValidator>();
            ////Study Cell Section
            services.AddTransient<IValidator<StudyCellDTO>, StudyCellsValidator>();
            services.AddTransient<IValidator<StudyElementDTO>, StudyElementsValidator>();            
            services.AddTransient<IValidator<StudyArmDTO>, StudyArmValidator>();
            services.AddTransient<IValidator<StudyEpochDTO>, StudyEpochValidator>();
            ////Planned WorkFlow Section
            services.AddTransient<IValidator<PlannedWorkflowDTO>, PlannedWorkFlowValidator>();            
            services.AddTransient<IValidator<WorkflowItemMatrixDTO>, WorkFlowItemMatrixValidator>();
            services.AddTransient<IValidator<MatrixDTO>, MatrixValidator>();
            services.AddTransient<IValidator<ItemDTO>, ItemValidator>();
            services.AddTransient<IValidator<ActivityDTO>, ActivityValidator>();
            services.AddTransient<IValidator<DefinedProcedureDTO>, DefinedProcedureValidator>();
            services.AddTransient<IValidator<StudyDataCollectionDTO>, StudyDataCollectionValidator>();
            services.AddTransient<IValidator<EncounterDTO>, EncounterValidator>();            

            //Common Section
            services.AddTransient<IValidator<CodingDTO>, CodingValidator>();
            services.AddTransient<IValidator<RuleDTO>, RuleValidator>();
            services.AddTransient<IValidator<EndpointsDTO>, EndpointsValidator>();
            services.AddTransient<IValidator<PointInTimeDTO>, PointInTimeValidator>();
            services.AddTransient<IValidator<EpochDTO>, EpochValidator>();

            return services;
        }
    }
}
