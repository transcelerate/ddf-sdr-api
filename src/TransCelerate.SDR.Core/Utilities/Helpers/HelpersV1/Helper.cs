using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Entities.StudyV1;

namespace TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1
{
    public class Helper : IHelper
    {
        public AuditTrailEntity GetAuditTrail(string user)
        {
            return new AuditTrailEntity
            {
                EntryDateTime = DateTime.UtcNow,
                CreatedBy = user,
            };
        }

        #region Generate Id for each sections
        public StudyEntity GeneratedSectionId(StudyEntity study)
        {
            study.ClinicalStudy.Uuid = IdGenerator.GenerateId();

            if (study.ClinicalStudy.StudyType is not null)
                study.ClinicalStudy.StudyType.Uuid = IdGenerator.GenerateId();

            study.ClinicalStudy.StudyIdentifiers = GenerateIdForStudyIdentifier(study.ClinicalStudy.StudyIdentifiers);

            if (study.ClinicalStudy.StudyPhase is not null)
                study.ClinicalStudy.StudyPhase.Uuid = IdGenerator.GenerateId();

            study.ClinicalStudy.StudyProtocolVersions = GenerateIdForStudyProtocol(study.ClinicalStudy.StudyProtocolVersions);

            study.ClinicalStudy.StudyDesigns = GenerateIdForStudyDesign(study.ClinicalStudy.StudyDesigns);

            return study;
        }

        public List<StudyIdentifierEntity> GenerateIdForStudyIdentifier(List<StudyIdentifierEntity> studyIdentifiers)
        {
            if (studyIdentifiers is not null && studyIdentifiers.Any())
            {
                studyIdentifiers.ForEach(x =>
                {
                    x.Uuid = IdGenerator.GenerateId();
                    if (x.StudyIdentifierScope is not null)
                    {
                        x.StudyIdentifierScope.Uuid = IdGenerator.GenerateId();
                        if (x.StudyIdentifierScope.OrganisationType is not null)
                            x.StudyIdentifierScope.OrganisationType.Uuid = IdGenerator.GenerateId();
                    }
                });
            }
            return studyIdentifiers;
        }

        public List<StudyProtocolVersionEntity> GenerateIdForStudyProtocol(List<StudyProtocolVersionEntity> studyProtocolVersions)
        {
            if (studyProtocolVersions is not null && studyProtocolVersions.Any())
            {
                studyProtocolVersions.ForEach(x =>
                {
                    x.Uuid = IdGenerator.GenerateId();
                    if (x.ProtocolStatus is not null && x.ProtocolStatus.Any())
                    {
                        x.ProtocolStatus.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                    }
                });
            }
            return studyProtocolVersions;
        }

        public List<StudyDesignEntity> GenerateIdForStudyDesign(List<StudyDesignEntity> studyDesigns)
        {
            if (studyDesigns is not null && studyDesigns.Any())
            {
                studyDesigns.ForEach(x =>
                {
                    x.Uuid = IdGenerator.GenerateId();
                    if (x.InterventionModel is not null && x.InterventionModel.Any())
                        x.InterventionModel.ForEach(x => x.Uuid = IdGenerator.GenerateId());

                    if (x.TrialIntentType is not null && x.TrialIntentType.Any())
                        x.TrialIntentType.ForEach(x => x.Uuid = IdGenerator.GenerateId());

                    if (x.TrialType is not null && x.TrialType.Any())
                        x.TrialType.ForEach(x => x.Uuid = IdGenerator.GenerateId());

                    if (x.StudyPopulations is not null && x.StudyPopulations.Any())
                        x.StudyPopulations.ForEach(x => x.Uuid = IdGenerator.GenerateId());

                    if (x.StudyIndications is not null && x.StudyIndications.Any())
                        x.StudyIndications = GenerateIdForStudyIndications(x.StudyIndications);

                    if (x.StudyInvestigationalInterventions is not null && x.StudyInvestigationalInterventions.Any())
                        x.StudyInvestigationalInterventions = GenerateIdForInvestigationalInterventions(x.StudyInvestigationalInterventions);

                    if (x.StudyObjectives is not null && x.StudyObjectives.Any())
                        x.StudyObjectives = GenerateIdForStudyObjectives(x.StudyObjectives);

                    if (x.StudyCells is not null && x.StudyCells.Any())
                        x.StudyCells = GenerateIdForStudyCells(x.StudyCells);

                    if (x.StudyWorkflows is not null && x.StudyWorkflows.Any())
                        x.StudyWorkflows = GenerateIdForStudyWorkflow(x.StudyWorkflows);

                    if (x.StudyEstimands is not null && x.StudyEstimands.Any())
                        x.StudyEstimands = GenerateIdForStudyEstimand(x.StudyEstimands);
                });
            }
            return studyDesigns;
        }

        public List<IndicationEntity> GenerateIdForStudyIndications(List<IndicationEntity> indications)
        {
            if (indications is not null && indications.Any())
            {
                indications.ForEach(x =>
                {
                    x.Uuid = IdGenerator.GenerateId();
                    if (x.Codes is not null && x.Codes.Any())
                    {
                        x.Codes.ForEach(y => y.Uuid = IdGenerator.GenerateId());
                    }
                });
            }
            return indications;
        }
        public List<InvestigationalInterventionEntity> GenerateIdForInvestigationalInterventions(List<InvestigationalInterventionEntity> investigationalInterventions)
        {
            if (investigationalInterventions is not null && investigationalInterventions.Any())
            {
                investigationalInterventions.ForEach(x =>
                {
                    x.Uuid = IdGenerator.GenerateId();
                    if (x.Codes is not null && x.Codes.Any())
                    {
                        x.Codes.ForEach(y => y.Uuid = IdGenerator.GenerateId());
                    }
                });
            }
            return investigationalInterventions;
        }

        public List<ObjectiveEntity> GenerateIdForStudyObjectives(List<ObjectiveEntity> objectives)
        {
            if (objectives is not null && objectives.Any())
            {
                objectives.ForEach(x =>
                {
                    x.Uuid = IdGenerator.GenerateId();
                    if (x.ObjectiveLevel is not null && x.ObjectiveLevel.Any())
                    {
                        x.ObjectiveLevel.ForEach(y => y.Uuid = IdGenerator.GenerateId());
                    }
                    if (x.ObjectiveEndpoints is not null && x.ObjectiveEndpoints.Any())
                    {
                        x.ObjectiveEndpoints.ForEach(y =>
                        {
                            y.Uuid = IdGenerator.GenerateId();
                            if(y.EndpointLevel is not null && y.EndpointLevel.Any())
                                y.EndpointLevel.ForEach(z => z.Uuid = IdGenerator.GenerateId());
                        });
                    }
                });
            }
            return objectives;
        }

        public List<StudyCellEntity> GenerateIdForStudyCells(List<StudyCellEntity> studyCells)
        {
            if (studyCells is not null && studyCells.Any())
            {
                studyCells.ForEach(x =>
                {
                    x.Uuid = IdGenerator.GenerateId();
                    if (x.StudyArm is not null)
                    {
                        x.StudyArm.Uuid = IdGenerator.GenerateId();
                        if (x.StudyArm.StudyArmDataOriginType is not null && x.StudyArm.StudyArmDataOriginType.Any())
                            x.StudyArm.StudyArmDataOriginType.ForEach(y => y.Uuid = IdGenerator.GenerateId());
                        if (x.StudyArm.StudyArmType is not null && x.StudyArm.StudyArmType.Any())
                            x.StudyArm.StudyArmType.ForEach(y => y.Uuid = IdGenerator.GenerateId());
                    }
                    if (x.StudyEpoch is not null)
                    {
                        x.StudyEpoch.Uuid = IdGenerator.GenerateId();
                        if (x.StudyEpoch.StudyEpochType is not null && x.StudyEpoch.StudyEpochType.Any())
                            x.StudyEpoch.StudyEpochType.ForEach(y => y.Uuid = IdGenerator.GenerateId());
                    }
                    if (x.StudyElements is not null && x.StudyElements.Any())
                    {
                        x.StudyElements.ForEach(y =>
                        {
                            y.Uuid = IdGenerator.GenerateId();
                            if (y.TransitionEndRule is not null)
                                y.TransitionEndRule.Uuid = IdGenerator.GenerateId();
                            if (y.TransitionStartRule is not null)
                                y.TransitionStartRule.Uuid = IdGenerator.GenerateId();

                        });
                    }
                });
            }
            return studyCells;
        }

        public List<WorkflowEntity> GenerateIdForStudyWorkflow(List<WorkflowEntity> workflows)
        {
            if (workflows is not null && workflows.Any())
            {
                workflows.ForEach(x =>
                {
                    x.WorkflowId = IdGenerator.GenerateId();

                    if (x.WorkflowItems is not null && x.WorkflowItems.Any())
                    {
                        x.WorkflowItems.ForEach(y =>
                        {
                            y.Uuid = IdGenerator.GenerateId();
                            if (y.WorkflowItemActivity is not null)
                            {
                                y.WorkflowItemActivity.Uuid = IdGenerator.GenerateId();
                                if (y.WorkflowItemActivity.DefinedProcedures is not null && y.WorkflowItemActivity.DefinedProcedures.Any())
                                {
                                    y.WorkflowItemActivity.DefinedProcedures.ForEach(procedure =>
                                    {
                                        procedure.Uuid = IdGenerator.GenerateId();
                                        if(procedure.ProcedureCode is not null)
                                            procedure.ProcedureCode.ForEach(y=>y.Uuid=IdGenerator.GenerateId());
                                    });
                                }
                                if (y.WorkflowItemActivity.StudyDataCollection is not null && y.WorkflowItemActivity.StudyDataCollection.Any())
                                {
                                    y.WorkflowItemActivity.StudyDataCollection.ForEach(procedure => procedure.Uuid = IdGenerator.GenerateId());
                                }
                            }
                            if (y.WorkflowItemEncounter is not null)
                            {
                                y.WorkflowItemEncounter.Uuid = IdGenerator.GenerateId();
                                if (y.WorkflowItemEncounter.EncounterContactMode is not null && y.WorkflowItemEncounter.EncounterContactMode.Any())
                                {
                                    y.WorkflowItemEncounter.EncounterContactMode.ForEach(procedure => procedure.Uuid = IdGenerator.GenerateId());
                                }
                                if (y.WorkflowItemEncounter.EncounterEnvironmentalSetting is not null && y.WorkflowItemEncounter.EncounterEnvironmentalSetting.Any())
                                {
                                    y.WorkflowItemEncounter.EncounterEnvironmentalSetting.ForEach(procedure => procedure.Uuid = IdGenerator.GenerateId());
                                }
                                if (y.WorkflowItemEncounter.EncounterType is not null && y.WorkflowItemEncounter.EncounterType.Any())
                                {
                                    y.WorkflowItemEncounter.EncounterType.ForEach(procedure => procedure.Uuid = IdGenerator.GenerateId());
                                }
                                if (y.WorkflowItemEncounter.StartRule is not null)
                                    y.WorkflowItemEncounter.StartRule.Uuid = IdGenerator.GenerateId();
                                if (y.WorkflowItemEncounter.EndRule is not null)
                                    y.WorkflowItemEncounter.EndRule.Uuid = IdGenerator.GenerateId();
                            }

                        });
                    }
                });
            }
            return workflows;
        }
        public List<EstimandEntity> GenerateIdForStudyEstimand(List<EstimandEntity> estimands)
        {
            if (estimands is not null && estimands.Any())
            {
                estimands.ForEach(x =>
                {
                    x.Uuid = IdGenerator.GenerateId();
                    if (x.Treatment is not null)
                    {
                        x.Treatment.Uuid = IdGenerator.GenerateId();
                        if(x.Treatment.Codes is not null && x.Treatment.Codes.Any())
                            x.Treatment.Codes.ForEach(y => y.Uuid = IdGenerator.GenerateId());
                    }                       

                    if (x.AnalysisPopulation is not null)
                        x.AnalysisPopulation.Uuid = IdGenerator.GenerateId();

                    if (x.VariableOfInterest is not null)
                    {
                        x.VariableOfInterest.Uuid = IdGenerator.GenerateId();
                        if (x.VariableOfInterest.EncounterContactMode is not null && x.VariableOfInterest.EncounterContactMode.Any())
                        {
                            x.VariableOfInterest.EncounterContactMode.ForEach(procedure => procedure.Uuid = IdGenerator.GenerateId());
                        }
                        if (x.VariableOfInterest.EncounterEnvironmentalSetting is not null && x.VariableOfInterest.EncounterEnvironmentalSetting.Any())
                        {
                            x.VariableOfInterest.EncounterEnvironmentalSetting.ForEach(procedure => procedure.Uuid = IdGenerator.GenerateId());
                        }
                        if (x.VariableOfInterest.EncounterType is not null && x.VariableOfInterest.EncounterType.Any())
                        {
                            x.VariableOfInterest.EncounterType.ForEach(procedure => procedure.Uuid = IdGenerator.GenerateId());
                        }
                        if (x.VariableOfInterest.StartRule is not null)
                            x.VariableOfInterest.StartRule.Uuid = IdGenerator.GenerateId();
                        if (x.VariableOfInterest.EndRule is not null)
                            x.VariableOfInterest.EndRule.Uuid = IdGenerator.GenerateId();
                    }

                    if (x.InterCurrentEvents is not null && x.InterCurrentEvents.Any())
                    {
                        x.InterCurrentEvents.ForEach(y =>
                        {
                            y.Uuid = IdGenerator.GenerateId();
                            if (y.Strategy is not null && y.Strategy.Any())
                                y.Strategy.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                        });
                    }
                });
            }
            return estimands;
        }
        #endregion

        #region Remove Id for Each section
        public StudyEntity RemovedSectionId(StudyEntity study)
        {
            study.ClinicalStudy.Uuid = null;

            if (study.ClinicalStudy.StudyType is not null)
                study.ClinicalStudy.StudyType.Uuid = null;

            study.ClinicalStudy.StudyIdentifiers = RemoveIdForStudyIdentifier(study.ClinicalStudy.StudyIdentifiers);

            if (study.ClinicalStudy.StudyPhase is not null)
                study.ClinicalStudy.StudyPhase.Uuid = null;

            study.ClinicalStudy.StudyProtocolVersions = RemoveIdForStudyProtocol(study.ClinicalStudy.StudyProtocolVersions);

            study.ClinicalStudy.StudyDesigns = RemoveIdForStudyDesign(study.ClinicalStudy.StudyDesigns);

            return study;
        }

        public List<StudyIdentifierEntity> RemoveIdForStudyIdentifier(List<StudyIdentifierEntity> studyIdentifiers)
        {
            if (studyIdentifiers is not null && studyIdentifiers.Any())
            {
                studyIdentifiers.ForEach(x =>
                {
                    x.Uuid = null;
                    if (x.StudyIdentifierScope is not null)
                    {
                        x.StudyIdentifierScope.Uuid = null;
                        if (x.StudyIdentifierScope.OrganisationType is not null)
                            x.StudyIdentifierScope.OrganisationType.Uuid = null;
                    }
                });
            }
            return studyIdentifiers;
        }

        public List<StudyProtocolVersionEntity> RemoveIdForStudyProtocol(List<StudyProtocolVersionEntity> studyProtocolVersions)
        {
            if (studyProtocolVersions is not null && studyProtocolVersions.Any())
            {
                studyProtocolVersions.ForEach(x =>
                {
                    x.Uuid = null;
                    if (x.ProtocolStatus is not null && x.ProtocolStatus.Any())
                    {
                        x.ProtocolStatus.ForEach(x => x.Uuid = null);
                    }
                });
            }
            return studyProtocolVersions;
        }

        public List<StudyDesignEntity> RemoveIdForStudyDesign(List<StudyDesignEntity> studyDesigns)
        {
            if (studyDesigns is not null && studyDesigns.Any())
            {
                studyDesigns.ForEach(x =>
                {
                    x.Uuid = null;
                    if (x.InterventionModel is not null && x.InterventionModel.Any())
                        x.InterventionModel.ForEach(x => x.Uuid = null);

                    if (x.TrialIntentType is not null && x.TrialIntentType.Any())
                        x.TrialIntentType.ForEach(x => x.Uuid = null);

                    if (x.TrialType is not null && x.TrialType.Any())
                        x.TrialType.ForEach(x => x.Uuid = null);

                    if (x.StudyPopulations is not null && x.StudyPopulations.Any())
                        x.StudyPopulations.ForEach(x => x.Uuid = null);

                    if (x.StudyIndications is not null && x.StudyIndications.Any())
                        x.StudyIndications = RemoveIdForStudyIndications(x.StudyIndications);

                    if (x.StudyInvestigationalInterventions is not null && x.StudyInvestigationalInterventions.Any())
                        x.StudyInvestigationalInterventions = RemoveIdForInvestigationalInterventions(x.StudyInvestigationalInterventions);

                    if (x.StudyObjectives is not null && x.StudyObjectives.Any())
                        x.StudyObjectives = RemoveIdForStudyObjectives(x.StudyObjectives);

                    if (x.StudyCells is not null && x.StudyCells.Any())
                        x.StudyCells = RemoveIdForStudyCells(x.StudyCells);

                    if (x.StudyWorkflows is not null && x.StudyWorkflows.Any())
                        x.StudyWorkflows = RemoveIdForStudyWorkflow(x.StudyWorkflows);

                    if (x.StudyEstimands is not null && x.StudyEstimands.Any())
                        x.StudyEstimands = RemoveIdForStudyEstimand(x.StudyEstimands);
                });
            }
            return studyDesigns;
        }

        public List<IndicationEntity> RemoveIdForStudyIndications(List<IndicationEntity> indications)
        {
            if (indications is not null && indications.Any())
            {
                indications.ForEach(x =>
                {
                    x.Uuid = null;
                    if (x.Codes is not null && x.Codes.Any())
                    {
                        x.Codes.ForEach(y => y.Uuid = null);
                    }
                });
            }
            return indications;
        }
        public List<InvestigationalInterventionEntity> RemoveIdForInvestigationalInterventions(List<InvestigationalInterventionEntity> investigationalInterventions)
        {
            if (investigationalInterventions is not null && investigationalInterventions.Any())
            {
                investigationalInterventions.ForEach(x =>
                {
                    x.Uuid = null;
                    if (x.Codes is not null && x.Codes.Any())
                    {
                        x.Codes.ForEach(y => y.Uuid = null);
                    }
                });
            }
            return investigationalInterventions;
        }

        public List<ObjectiveEntity> RemoveIdForStudyObjectives(List<ObjectiveEntity> objectives)
        {
            if (objectives is not null && objectives.Any())
            {
                objectives.ForEach(x =>
                {
                    x.Uuid = null;
                    if (x.ObjectiveLevel is not null && x.ObjectiveLevel.Any())
                    {
                        x.ObjectiveLevel.ForEach(y => y.Uuid = null);
                    }
                    if (x.ObjectiveEndpoints is not null && x.ObjectiveEndpoints.Any())
                    {
                        x.ObjectiveEndpoints.ForEach(y =>
                        {
                            y.Uuid = null;
                            if(y.EndpointLevel is not null && y.EndpointLevel.Any())
                                y.EndpointLevel.ForEach(z => z.Uuid = null);
                        });
                    }
                });
            }
            return objectives;
        }

        public List<StudyCellEntity> RemoveIdForStudyCells(List<StudyCellEntity> studyCells)
        {
            if (studyCells is not null && studyCells.Any())
            {
                studyCells.ForEach(x =>
                {
                    x.Uuid = null;
                    if (x.StudyArm is not null)
                    {
                        x.StudyArm.Uuid = null;
                        if (x.StudyArm.StudyArmDataOriginType is not null && x.StudyArm.StudyArmDataOriginType.Any())
                            x.StudyArm.StudyArmDataOriginType.ForEach(y => y.Uuid =null);
                        if (x.StudyArm.StudyArmType is not null && x.StudyArm.StudyArmType.Any())
                            x.StudyArm.StudyArmType.ForEach(y => y.Uuid = null);
                    }
                    if (x.StudyEpoch is not null)
                    {
                        x.StudyEpoch.Uuid = null;
                        if (x.StudyEpoch.StudyEpochType is not null && x.StudyEpoch.StudyEpochType.Any())
                            x.StudyEpoch.StudyEpochType.ForEach(y => y.Uuid = null);
                    }
                    if (x.StudyElements is not null && x.StudyElements.Any())
                    {
                        x.StudyElements.ForEach(y =>
                        {
                            y.Uuid = null;
                            if (y.TransitionEndRule is not null)
                                y.TransitionEndRule.Uuid = null;
                            if (y.TransitionStartRule is not null)
                                y.TransitionStartRule.Uuid = null;

                        });
                    }
                });
            }
            return studyCells;
        }

        public List<WorkflowEntity> RemoveIdForStudyWorkflow(List<WorkflowEntity> workflows)
        {
            if (workflows is not null && workflows.Any())
            {
                workflows.ForEach(x =>
                {
                    x.WorkflowId = null;

                    if (x.WorkflowItems is not null && x.WorkflowItems.Any())
                    {
                        x.WorkflowItems.ForEach(y =>
                        {
                            y.Uuid = null;
                            if (y.WorkflowItemActivity is not null)
                            {
                                y.WorkflowItemActivity.Uuid = null;
                                if (y.WorkflowItemActivity.DefinedProcedures is not null && y.WorkflowItemActivity.DefinedProcedures.Any())
                                {
                                    y.WorkflowItemActivity.DefinedProcedures.ForEach(procedure => { 
                                        
                                        procedure.Uuid = null;
                                        if (procedure.ProcedureCode is not null)
                                            procedure.ProcedureCode.ForEach(y => y.Uuid =null);
                                    });
                                }
                                if (y.WorkflowItemActivity.StudyDataCollection is not null && y.WorkflowItemActivity.StudyDataCollection.Any())
                                {
                                    y.WorkflowItemActivity.StudyDataCollection.ForEach(procedure => procedure.Uuid = null);
                                }
                            }
                            if (y.WorkflowItemEncounter is not null)
                            {
                                y.WorkflowItemEncounter.Uuid = null;
                                if (y.WorkflowItemEncounter.EncounterContactMode is not null && y.WorkflowItemEncounter.EncounterContactMode.Any())
                                {
                                    y.WorkflowItemEncounter.EncounterContactMode.ForEach(procedure => procedure.Uuid = null);
                                }
                                if (y.WorkflowItemEncounter.EncounterEnvironmentalSetting is not null && y.WorkflowItemEncounter.EncounterEnvironmentalSetting.Any())
                                {
                                    y.WorkflowItemEncounter.EncounterEnvironmentalSetting.ForEach(procedure => procedure.Uuid = null);
                                }
                                if (y.WorkflowItemEncounter.EncounterType is not null && y.WorkflowItemEncounter.EncounterType.Any())
                                {
                                    y.WorkflowItemEncounter.EncounterType.ForEach(procedure => procedure.Uuid = null);
                                }
                                if (y.WorkflowItemEncounter.StartRule is not null)
                                    y.WorkflowItemEncounter.StartRule.Uuid = null;
                                if (y.WorkflowItemEncounter.EndRule is not null)
                                    y.WorkflowItemEncounter.EndRule.Uuid = null;
                            }

                        });
                    }
                });
            }
            return workflows;
        }
        public List<EstimandEntity> RemoveIdForStudyEstimand(List<EstimandEntity> estimands)
        {
            if (estimands is not null && estimands.Any())
            {
                estimands.ForEach(x =>
                {
                    x.Uuid = null;
                    if (x.Treatment is not null)
                    {
                        x.Treatment.Uuid = null;
                        if (x.Treatment.Codes is not null && x.Treatment.Codes.Any())
                            x.Treatment.Codes.ForEach(y => y.Uuid = null);
                    }    

                    if (x.AnalysisPopulation is not null)
                        x.AnalysisPopulation.Uuid = null;

                    if (x.VariableOfInterest is not null)
                    {
                        x.VariableOfInterest.Uuid = null;
                        if (x.VariableOfInterest.EncounterContactMode is not null && x.VariableOfInterest.EncounterContactMode.Any())
                        {
                            x.VariableOfInterest.EncounterContactMode.ForEach(procedure => procedure.Uuid = null);
                        }
                        if (x.VariableOfInterest.EncounterEnvironmentalSetting is not null && x.VariableOfInterest.EncounterEnvironmentalSetting.Any())
                        {
                            x.VariableOfInterest.EncounterEnvironmentalSetting.ForEach(procedure => procedure.Uuid = null);
                        }
                        if (x.VariableOfInterest.EncounterType is not null && x.VariableOfInterest.EncounterType.Any())
                        {
                            x.VariableOfInterest.EncounterType.ForEach(procedure => procedure.Uuid = null);
                        }
                        if (x.VariableOfInterest.StartRule is not null)
                            x.VariableOfInterest.StartRule.Uuid = null;
                        if (x.VariableOfInterest.EndRule is not null)
                            x.VariableOfInterest.EndRule.Uuid = null;
                    }

                    if (x.InterCurrentEvents is not null && x.InterCurrentEvents.Any())
                    {
                        x.InterCurrentEvents.ForEach(y =>
                        {
                            y.Uuid = null;
                            if (y.Strategy is not null && y.Strategy.Any())
                                y.Strategy.ForEach(x => x.Uuid = null);
                        });
                    }
                });
            }
            return estimands;
        }
        #endregion

        #region Check whole study
        public bool IsSameStudy(StudyEntity incoming, StudyEntity existing)
        {
            try
            {
                var duplicateExistingStudy = JsonConvert.DeserializeObject<StudyEntity>(JsonConvert.SerializeObject(existing)); // Creating duplicates for existing entity
                var duplicateIncomingStudy = JsonConvert.DeserializeObject<StudyEntity>(JsonConvert.SerializeObject(incoming)); // Creating duplicates for incoming entity

                duplicateIncomingStudy.AuditTrail = duplicateExistingStudy.AuditTrail = null;
                duplicateIncomingStudy._id = duplicateExistingStudy._id = null;

                return JsonObjectCheck(RemovedSectionId(duplicateIncomingStudy), RemovedSectionId(duplicateExistingStudy));
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool JsonObjectCheck(object incoming, object existing)
        {
            try
            {
                return JToken.DeepEquals(JObject.Parse(JsonConvert.SerializeObject(incoming)),
                               JObject.Parse(JsonConvert.SerializeObject(existing)));
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Check for each sections
        public StudyEntity CheckForSections(StudyEntity incoming, StudyEntity existing)
        {
            //For StudyType
            if (existing.ClinicalStudy.StudyType is null && incoming.ClinicalStudy.StudyType is not null)
                incoming.ClinicalStudy.StudyType.Uuid = IdGenerator.GenerateId();
            else if (existing.ClinicalStudy.StudyType is not null && incoming.ClinicalStudy.StudyType is not null)
                incoming.ClinicalStudy.StudyType.Uuid = String.IsNullOrWhiteSpace(incoming.ClinicalStudy.StudyType.Uuid) ? IdGenerator.GenerateId() : incoming.ClinicalStudy.StudyType.Uuid;

            //For StudyPhase
            if (existing.ClinicalStudy.StudyPhase is null && incoming.ClinicalStudy.StudyPhase is not null)
                incoming.ClinicalStudy.StudyPhase.Uuid = IdGenerator.GenerateId();
            else if (existing.ClinicalStudy.StudyPhase is not null && incoming.ClinicalStudy.StudyPhase is not null)
                incoming.ClinicalStudy.StudyPhase.Uuid = String.IsNullOrWhiteSpace(incoming.ClinicalStudy.StudyPhase.Uuid) ? IdGenerator.GenerateId() : incoming.ClinicalStudy.StudyPhase.Uuid;
            
            incoming.ClinicalStudy.StudyIdentifiers = CheckForStudyIdentifierSection(incoming.ClinicalStudy.StudyIdentifiers,
                                                                                     existing.ClinicalStudy.StudyIdentifiers);

            incoming.ClinicalStudy.StudyProtocolVersions = CheckForStudyProtocolSection(incoming.ClinicalStudy.StudyProtocolVersions,
                                                                                     existing.ClinicalStudy.StudyProtocolVersions);
            incoming.ClinicalStudy.StudyDesigns = CheckForStudyDesignSection(incoming.ClinicalStudy.StudyDesigns, existing.ClinicalStudy.StudyDesigns);

            return incoming;
        }
        public List<StudyIdentifierEntity> CheckForStudyIdentifierSection(List<StudyIdentifierEntity> incomingStudyIdentifiers, List<StudyIdentifierEntity> existingStudyIdentifiers)
        {
            if (incomingStudyIdentifiers is not null && existingStudyIdentifiers is not null)
            {
                List<StudyIdentifierEntity> studyIdentifiers = new List<StudyIdentifierEntity>();
                incomingStudyIdentifiers.ForEach(x =>
                {
                    if (existingStudyIdentifiers.Any(y => y.Uuid == x.Uuid))
                    {
                        if(x.StudyIdentifierScope is not null)
                        {
                            x.StudyIdentifierScope.Uuid = String.IsNullOrEmpty(x.StudyIdentifierScope.Uuid) ? IdGenerator.GenerateId() : x.StudyIdentifierScope.Uuid;
                            if (x.StudyIdentifierScope.OrganisationType is not null)
                                x.StudyIdentifierScope.OrganisationType.Uuid = String.IsNullOrWhiteSpace(x.StudyIdentifierScope.OrganisationType.Uuid) ? IdGenerator.GenerateId() : x.StudyIdentifierScope.OrganisationType.Uuid;
                        }
                        studyIdentifiers.Add(x);
                        existingStudyIdentifiers.RemoveAll(y => y.Uuid == x.Uuid);
                    }
                    else
                    {
                        studyIdentifiers.AddRange(GenerateIdForStudyIdentifier(new List<StudyIdentifierEntity> { x }));
                    }
                });
                incomingStudyIdentifiers = studyIdentifiers;
            }
            else if (incomingStudyIdentifiers is not null && existingStudyIdentifiers is null)
            {
                incomingStudyIdentifiers = GenerateIdForStudyIdentifier(incomingStudyIdentifiers);
            }
            return incomingStudyIdentifiers;
        }

        public List<StudyProtocolVersionEntity> CheckForStudyProtocolSection(List<StudyProtocolVersionEntity> incomingStudyProtocolVersions, List<StudyProtocolVersionEntity> existingStudyProtocolVersions)
        {
            if (incomingStudyProtocolVersions is not null && existingStudyProtocolVersions is not null)
            {
                List<StudyProtocolVersionEntity> studyProtocols = new List<StudyProtocolVersionEntity>();
                incomingStudyProtocolVersions.ForEach(x =>
                {
                    if (existingStudyProtocolVersions.Any(y => y.Uuid == x.Uuid))
                    {
                        x.ProtocolStatus = CheckForCodeSection(x.ProtocolStatus, existingStudyProtocolVersions.Find(y => y.Uuid == x.Uuid).ProtocolStatus);
                        studyProtocols.Add(x);
                        existingStudyProtocolVersions.RemoveAll(y => y.Uuid == x.Uuid);
                    }
                    else
                    {
                        studyProtocols.AddRange(GenerateIdForStudyProtocol(new List<StudyProtocolVersionEntity> { x }));
                    }
                });
                incomingStudyProtocolVersions = studyProtocols;
            }
            else if (incomingStudyProtocolVersions is not null && existingStudyProtocolVersions is null)
            {
                incomingStudyProtocolVersions = GenerateIdForStudyProtocol(incomingStudyProtocolVersions);
            }
            return incomingStudyProtocolVersions;
        }

        public List<CodeEntity> CheckForCodeSection(List<CodeEntity> incomingCodes, List<CodeEntity> existingCodes)
        {
            if (incomingCodes is not null && existingCodes is not null)
            {
                List<CodeEntity> codes = new List<CodeEntity>();
                incomingCodes.ForEach(x =>
                {
                    if (existingCodes.Any(y => y.Uuid == x.Uuid))
                    {
                        codes.Add(x);
                        existingCodes.RemoveAll(y => y.Uuid == x.Uuid);
                    }
                    else
                    {
                        x.Uuid = IdGenerator.GenerateId();
                        codes.Add(x);
                    }
                });
                incomingCodes = codes;
            }
            else if (incomingCodes is not null && existingCodes is null)
            {
                incomingCodes.ForEach(x => x.Uuid = IdGenerator.GenerateId());
            }
            return incomingCodes;
        }
        public List<StudyDesignEntity> CheckForStudyDesignSection(List<StudyDesignEntity> incomingStudyDesigns, List<StudyDesignEntity> existingStudyDesigns)
        {
            if (incomingStudyDesigns is not null && existingStudyDesigns is not null)
            {
                List<StudyDesignEntity> studyDesigns = new List<StudyDesignEntity>();
                incomingStudyDesigns.ForEach(x =>
                {
                    if (existingStudyDesigns.Any(y => y.Uuid == x.Uuid))
                    {
                        ParallelOptions parallelOptions = new ParallelOptions
                        {
                            MaxDegreeOfParallelism = 4
                        };
                        Parallel.Invoke(parallelOptions,
                                        () => x.InterventionModel = CheckForCodeSection(x.InterventionModel, existingStudyDesigns.Find(y => y.Uuid == x.Uuid).InterventionModel),
                                        () => x.TrialIntentType = CheckForCodeSection(x.TrialIntentType, existingStudyDesigns.Find(y => y.Uuid == x.Uuid).TrialIntentType),
                                        () => x.TrialType = CheckForCodeSection(x.TrialType, existingStudyDesigns.Find(y => y.Uuid == x.Uuid).TrialType),
                                        () => x.StudyIndications = CheckForStudyIndicationsSection(x.StudyIndications, existingStudyDesigns.Find(y => y.Uuid == x.Uuid).StudyIndications),
                                        () => x.StudyInvestigationalInterventions = CheckForInvestigationalInterventionsSection(x.StudyInvestigationalInterventions, existingStudyDesigns.Find(y => y.Uuid == x.Uuid).StudyInvestigationalInterventions),
                                        () => x.StudyObjectives = CheckForStudyObjectivesSection(x.StudyObjectives, existingStudyDesigns.Find(y => y.Uuid == x.Uuid).StudyObjectives),
                                        () => x.StudyPopulations = CheckForStudyDesignPopulationsSection(x.StudyPopulations, existingStudyDesigns.Find(y => y.Uuid == x.Uuid).StudyPopulations),
                                        () => x.StudyCells = CheckForStudyCellsSection(x.StudyCells, existingStudyDesigns.Find(y => y.Uuid == x.Uuid).StudyCells),
                                        () => x.StudyWorkflows = CheckForStudyWorkflowSection(x.StudyWorkflows, existingStudyDesigns.Find(y => y.Uuid == x.Uuid).StudyWorkflows),
                                        () => x.StudyEstimands = CheckForStudyEstimandSection(x.StudyEstimands, existingStudyDesigns.Find(y => y.Uuid == x.Uuid).StudyEstimands)
                                        );
                        studyDesigns.Add(x);
                        existingStudyDesigns.RemoveAll(y => y.Uuid == x.Uuid);
                    }
                    else
                    {
                        studyDesigns.AddRange(GenerateIdForStudyDesign(new List<StudyDesignEntity> { x }));
                    }
                });
                incomingStudyDesigns = studyDesigns;
            }
            else if (incomingStudyDesigns is not null && existingStudyDesigns is null)
            {
                incomingStudyDesigns = GenerateIdForStudyDesign(incomingStudyDesigns);
            }
            return incomingStudyDesigns;
        }

        public List<IndicationEntity> CheckForStudyIndicationsSection(List<IndicationEntity> incomingIndications, List<IndicationEntity> exisitingIndications)
        {
            if (incomingIndications is not null && exisitingIndications is not null)
            {
                List<IndicationEntity> indications = new List<IndicationEntity>();
                incomingIndications.ForEach(x =>
                {
                    if (exisitingIndications.Any(y => y.Uuid == x.Uuid))
                    {
                        x.Codes = CheckForCodeSection(x.Codes, exisitingIndications.Find(y => y.Uuid == x.Uuid).Codes);
                        indications.Add(x);
                        exisitingIndications.RemoveAll(y => y.Uuid == x.Uuid);
                    }
                    else
                    {
                        indications.AddRange(GenerateIdForStudyIndications(new List<IndicationEntity> { x }));
                    }
                });
                incomingIndications = indications;
            }
            else if (incomingIndications is not null && exisitingIndications is null)
            {
                incomingIndications = GenerateIdForStudyIndications(incomingIndications);
            }
            return incomingIndications;
        }
        public List<InvestigationalInterventionEntity> CheckForInvestigationalInterventionsSection(List<InvestigationalInterventionEntity> incomingInvestigationalInterventions, List<InvestigationalInterventionEntity> existingInvestigationalInterventions)
        {
            if (incomingInvestigationalInterventions is not null && existingInvestigationalInterventions is not null)
            {
                List<InvestigationalInterventionEntity> investigationalInterventions = new List<InvestigationalInterventionEntity>();
                incomingInvestigationalInterventions.ForEach(x =>
                {
                    if (existingInvestigationalInterventions.Any(y => y.Uuid == x.Uuid))
                    {
                        x.Codes = CheckForCodeSection(x.Codes, existingInvestigationalInterventions.Find(y => y.Uuid == x.Uuid).Codes);
                        investigationalInterventions.Add(x);
                        existingInvestigationalInterventions.RemoveAll(y => y.Uuid == x.Uuid);
                    }
                    else
                    {
                        investigationalInterventions.AddRange(GenerateIdForInvestigationalInterventions(new List<InvestigationalInterventionEntity> { x }));
                    }
                });
                incomingInvestigationalInterventions = investigationalInterventions;
            }
            else if (incomingInvestigationalInterventions is not null && existingInvestigationalInterventions is null)
            {
                incomingInvestigationalInterventions = GenerateIdForInvestigationalInterventions(incomingInvestigationalInterventions);
            }
            return incomingInvestigationalInterventions;
        }
        public List<StudyDesignPopulationEntity> CheckForStudyDesignPopulationsSection(List<StudyDesignPopulationEntity> incomingStudyDesignPopulations, List<StudyDesignPopulationEntity> existingStudyDesignPopulations)
        {
            if (incomingStudyDesignPopulations is not null && existingStudyDesignPopulations is not null)
            {
                List<StudyDesignPopulationEntity> studyDesignPopulations = new List<StudyDesignPopulationEntity>();
                incomingStudyDesignPopulations.ForEach(x =>
                {
                    if (existingStudyDesignPopulations.Any(y => y.Uuid == x.Uuid))
                    {
                        studyDesignPopulations.Add(x);
                        existingStudyDesignPopulations.RemoveAll(y => y.Uuid == x.Uuid);
                    }
                    else
                    {
                        x.Uuid = IdGenerator.GenerateId();
                        studyDesignPopulations.Add(x);
                    }
                });
                incomingStudyDesignPopulations = studyDesignPopulations;
            }
            else if (incomingStudyDesignPopulations is not null && existingStudyDesignPopulations is null)
            {
                incomingStudyDesignPopulations.ForEach(x => x.Uuid = IdGenerator.GenerateId());
            }
            return incomingStudyDesignPopulations;
        }

        public List<ObjectiveEntity> CheckForStudyObjectivesSection(List<ObjectiveEntity> incomingObjectives, List<ObjectiveEntity> existingObjectives)
        {
            if (incomingObjectives is not null && existingObjectives is not null)
            {
                List<ObjectiveEntity> studyObjectives = new List<ObjectiveEntity>();
                incomingObjectives.ForEach(x =>
                {
                    if (existingObjectives.Any(y => y.Uuid == x.Uuid))
                    {
                        x.ObjectiveLevel = CheckForCodeSection(x.ObjectiveLevel, existingObjectives.Find(y => y.Uuid == x.Uuid).ObjectiveLevel);
                        x.ObjectiveEndpoints = CheckForStudyObjectivesEndpointsSection(x.ObjectiveEndpoints, existingObjectives.Find(y => y.Uuid == x.Uuid).ObjectiveEndpoints);
                        studyObjectives.Add(x);
                        existingObjectives.RemoveAll(y => y.Uuid == x.Uuid);
                    }
                    else
                    {
                        studyObjectives.AddRange(GenerateIdForStudyObjectives(new List<ObjectiveEntity> { x }));
                    }
                });
                incomingObjectives = studyObjectives;
            }
            else if (incomingObjectives is not null && existingObjectives is null)
            {
                incomingObjectives = GenerateIdForStudyObjectives(incomingObjectives);
            }
            return incomingObjectives;
        }

        public List<EndpointEntity> CheckForStudyObjectivesEndpointsSection(List<EndpointEntity> incomingEndpoints, List<EndpointEntity> existingEndpoints)
        {
            if (incomingEndpoints is not null && existingEndpoints is not null)
            {
                List<EndpointEntity> studyEndpoints = new List<EndpointEntity>();
                incomingEndpoints.ForEach(x =>
                {
                    if (existingEndpoints.Any(y => y.Uuid == x.Uuid))
                    {
                        x.EndpointLevel = CheckForCodeSection(x.EndpointLevel, existingEndpoints.Find(y => y.Uuid == x.Uuid).EndpointLevel);
                        studyEndpoints.Add(x);
                        existingEndpoints.RemoveAll(y => y.Uuid == x.Uuid);
                    }
                    else
                    {
                        x.Uuid = IdGenerator.GenerateId();
                        if (x.EndpointLevel is not null && x.EndpointLevel.Any())
                            x.EndpointLevel.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                        studyEndpoints.Add(x);
                    }
                });
                incomingEndpoints = studyEndpoints;
            }
            else if (incomingEndpoints is not null && existingEndpoints is null)
            {
                incomingEndpoints.ForEach(x =>
                {
                    x.Uuid = IdGenerator.GenerateId();
                    if(x.EndpointLevel is not null && x.EndpointLevel.Any())
                        x.EndpointLevel.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                });
            }
            return incomingEndpoints;
        }

        public List<StudyCellEntity> CheckForStudyCellsSection(List<StudyCellEntity> incomingStudyCells, List<StudyCellEntity> existingStudyCells)
        {
            if (incomingStudyCells is not null && existingStudyCells is not null)
            {
                List<StudyCellEntity> studyCells = new List<StudyCellEntity>();
                incomingStudyCells.ForEach(x =>
                {
                    if (existingStudyCells.Any(y => y.Uuid == x.Uuid))
                    {
                        if (x.StudyArm is not null && existingStudyCells.Find(y => y.Uuid == x.Uuid).StudyArm is null)
                        {
                            x.StudyArm.Uuid = IdGenerator.GenerateId();
                            if(x.StudyArm.StudyArmDataOriginType is not null && x.StudyArm.StudyArmDataOriginType.Any())
                                x.StudyArm.StudyArmDataOriginType.ForEach(y => y.Uuid = IdGenerator.GenerateId());
                            if(x.StudyArm.StudyArmType is not null && x.StudyArm.StudyArmType.Any())
                                x.StudyArm.StudyArmType.ForEach(y => y.Uuid = IdGenerator.GenerateId());
                        }
                        else if (x.StudyArm is not null && existingStudyCells.Find(y => y.Uuid == x.Uuid).StudyArm is not null)
                        {
                            if(String.IsNullOrWhiteSpace(x.StudyArm.Uuid))
                            {
                                x.StudyArm.Uuid = IdGenerator.GenerateId();
                                x.StudyArm.StudyArmDataOriginType?.ForEach(y => y.Uuid = IdGenerator.GenerateId());
                                x.StudyArm.StudyArmType?.ForEach(y => y.Uuid = IdGenerator.GenerateId());
                            }
                            else
                            {                                
                                x.StudyArm.StudyArmDataOriginType = CheckForCodeSection(x.StudyArm.StudyArmDataOriginType, existingStudyCells.Find(y => y.Uuid == x.Uuid).StudyArm.StudyArmDataOriginType);
                                x.StudyArm.StudyArmType = CheckForCodeSection(x.StudyArm.StudyArmType, existingStudyCells.Find(y => y.Uuid == x.Uuid).StudyArm.StudyArmType);
                            }
                        }
                        if (x.StudyEpoch is not null && existingStudyCells.Find(y => y.Uuid == x.Uuid).StudyEpoch is null)
                        {
                            x.StudyEpoch.Uuid = IdGenerator.GenerateId();
                            if(x.StudyEpoch.StudyEpochType is not null && x.StudyEpoch.StudyEpochType.Any())
                                x.StudyEpoch.StudyEpochType.ForEach(y => y.Uuid = IdGenerator.GenerateId());
                        }
                        else if (x.StudyEpoch is not null && existingStudyCells.Find(y => y.Uuid == x.Uuid).StudyEpoch is not null)
                        {
                            if(String.IsNullOrWhiteSpace(x.StudyEpoch.Uuid))
                            {
                                x.StudyEpoch.Uuid = IdGenerator.GenerateId();
                                x.StudyEpoch.StudyEpochType?.ForEach(y => y.Uuid = IdGenerator.GenerateId()); 
                            }
                            else
                            {
                                x.StudyEpoch.StudyEpochType = CheckForCodeSection(x.StudyEpoch.StudyEpochType, existingStudyCells.Find(y => y.Uuid == x.Uuid).StudyEpoch.StudyEpochType);
                            }
                            
                        }
                        x.StudyElements = CheckForStudyElementsSection(x.StudyElements, existingStudyCells.Find(y => y.Uuid == x.Uuid).StudyElements);
                        studyCells.Add(x);
                        existingStudyCells.RemoveAll(y => y.Uuid == x.Uuid);
                    }
                    else
                    {
                        studyCells.AddRange(GenerateIdForStudyCells(new List<StudyCellEntity> { x }));
                    }
                });
                incomingStudyCells = studyCells;
            }
            else if (incomingStudyCells is not null && existingStudyCells is null)
            {
                incomingStudyCells = GenerateIdForStudyCells(incomingStudyCells);
            }
            return incomingStudyCells;
        }        
        public List<StudyElementEntity> CheckForStudyElementsSection(List<StudyElementEntity> incomingStudyElements, List<StudyElementEntity> existingStudyElements)
        {
            if (incomingStudyElements is not null && existingStudyElements is not null)
            {
                List<StudyElementEntity> studyElements = new List<StudyElementEntity>();
                incomingStudyElements.ForEach(x =>
                {
                    if (existingStudyElements.Any(y => y.Uuid == x.Uuid))
                    {
                        if (x.TransitionEndRule is not null)
                            x.TransitionEndRule.Uuid = String.IsNullOrWhiteSpace(x.TransitionEndRule.Uuid) ? IdGenerator.GenerateId() : x.TransitionEndRule.Uuid;

                        if (x.TransitionStartRule is not null)
                            x.TransitionStartRule.Uuid = String.IsNullOrWhiteSpace(x.TransitionStartRule.Uuid) ? IdGenerator.GenerateId() : x.TransitionStartRule.Uuid;                        

                        studyElements.Add(x);
                        existingStudyElements.RemoveAll(y => y.Uuid == x.Uuid);
                    }
                    else
                    {
                        x.Uuid = IdGenerator.GenerateId();
                        if (x.TransitionEndRule is not null)
                            x.TransitionEndRule.Uuid = IdGenerator.GenerateId();
                        if (x.TransitionStartRule is not null)
                            x.TransitionStartRule.Uuid = IdGenerator.GenerateId();
                        studyElements.Add(x);
                    }
                });
                incomingStudyElements = studyElements;
            }
            else if (incomingStudyElements is not null && existingStudyElements is null)
            {
                incomingStudyElements.ForEach(x => 
                {
                    x.Uuid = IdGenerator.GenerateId();
                    if (x.TransitionEndRule is not null)
                        x.TransitionEndRule.Uuid = IdGenerator.GenerateId();
                    if (x.TransitionStartRule is not null)
                        x.TransitionStartRule.Uuid = IdGenerator.GenerateId();
                });
            }
            return incomingStudyElements;
        }
        public List<StudyDataCollectionEntity> CheckForStudyDataCollectionSection(List<StudyDataCollectionEntity> incomingStudyDataCollections, List<StudyDataCollectionEntity> existingStudyDataCollections)
        {
            if (incomingStudyDataCollections is not null && existingStudyDataCollections is not null)
            {
                List<StudyDataCollectionEntity> studyDataCollections = new List<StudyDataCollectionEntity>();
                incomingStudyDataCollections.ForEach(x =>
                {
                    if (existingStudyDataCollections.Any(y => y.Uuid == x.Uuid))
                    {
                        studyDataCollections.Add(x);
                        existingStudyDataCollections.RemoveAll(y => y.Uuid == x.Uuid);
                    }
                    else
                    {
                        x.Uuid = IdGenerator.GenerateId();
                        studyDataCollections.Add(x);
                    }
                });
                incomingStudyDataCollections = studyDataCollections;
            }
            else if (incomingStudyDataCollections is not null && existingStudyDataCollections is null)
            {
                incomingStudyDataCollections.ForEach(x => x.Uuid = IdGenerator.GenerateId());
            }
            return incomingStudyDataCollections;
        }

        public List<WorkflowEntity> CheckForStudyWorkflowSection(List<WorkflowEntity> incomingWorkflows, List<WorkflowEntity> existingWorkflows)
        {
            if (incomingWorkflows is not null && existingWorkflows is not null)
            {
                List<WorkflowEntity> workflows = new List<WorkflowEntity>();
                incomingWorkflows.ForEach(x =>
                {
                    if (existingWorkflows.Any(y => y.WorkflowId == x.WorkflowId))
                    {
                        x.WorkflowItems = CheckForStudyWorkflowItemsSection(x.WorkflowItems, existingWorkflows.Find(y => y.WorkflowId == x.WorkflowId).WorkflowItems);
                        workflows.Add(x);
                        existingWorkflows.RemoveAll(y => y.WorkflowId == x.WorkflowId);
                    }
                    else
                    {
                        workflows.AddRange(GenerateIdForStudyWorkflow(new List<WorkflowEntity> { x }));
                    }
                });
                incomingWorkflows = workflows;
            }
            else if (incomingWorkflows is not null && existingWorkflows is null)
            {
                incomingWorkflows = GenerateIdForStudyWorkflow(incomingWorkflows);
            }

            return incomingWorkflows;
        }
        public List<WorkFlowItemEntity> CheckForStudyWorkflowItemsSection(List<WorkFlowItemEntity> incomingWorkflowItems, List<WorkFlowItemEntity> existingWorkflowItems)
        {
            if (incomingWorkflowItems is not null && existingWorkflowItems is not null)
            {
                List<WorkFlowItemEntity> workFlowItems = new List<WorkFlowItemEntity>();
                incomingWorkflowItems.ForEach(x =>
                {
                    if (existingWorkflowItems.Any(y => y.Uuid == x.Uuid))
                    {
                        if (x.WorkflowItemEncounter is not null && existingWorkflowItems.Find(y => y.Uuid == x.Uuid).WorkflowItemEncounter is not null)
                        {
                            if(String.IsNullOrWhiteSpace(x.WorkflowItemEncounter.Uuid))
                            {
                                x.WorkflowItemEncounter.Uuid = IdGenerator.GenerateId();
                                x.WorkflowItemEncounter.EncounterContactMode?.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                                x.WorkflowItemEncounter.EncounterEnvironmentalSetting?.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                                x.WorkflowItemEncounter.EncounterType?.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                                if (x.WorkflowItemEncounter.StartRule is not null)
                                    x.WorkflowItemEncounter.StartRule.Uuid = IdGenerator.GenerateId();
                                if (x.WorkflowItemEncounter.EndRule is not null)
                                    x.WorkflowItemEncounter.EndRule.Uuid = IdGenerator.GenerateId();
                            }
                            else
                            {
                                x.WorkflowItemEncounter.EncounterContactMode = CheckForCodeSection(x.WorkflowItemEncounter.EncounterContactMode, existingWorkflowItems.Find(y => y.Uuid == x.Uuid).WorkflowItemEncounter.EncounterContactMode);
                                x.WorkflowItemEncounter.EncounterEnvironmentalSetting = CheckForCodeSection(x.WorkflowItemEncounter.EncounterEnvironmentalSetting, existingWorkflowItems.Find(y => y.Uuid == x.Uuid).WorkflowItemEncounter.EncounterEnvironmentalSetting);
                                x.WorkflowItemEncounter.EncounterType = CheckForCodeSection(x.WorkflowItemEncounter.EncounterType, existingWorkflowItems.Find(y => y.Uuid == x.Uuid).WorkflowItemEncounter.EncounterType);
                                if (x.WorkflowItemEncounter.StartRule is not null)
                                    x.WorkflowItemEncounter.StartRule.Uuid = String.IsNullOrWhiteSpace(x.WorkflowItemEncounter.StartRule.Uuid) ? IdGenerator.GenerateId(): x.WorkflowItemEncounter.StartRule.Uuid;
                                if (x.WorkflowItemEncounter.EndRule is not null)
                                    x.WorkflowItemEncounter.EndRule.Uuid = String.IsNullOrWhiteSpace(x.WorkflowItemEncounter.EndRule.Uuid) ? IdGenerator.GenerateId() : x.WorkflowItemEncounter.EndRule.Uuid;
                            }
                        }
                        else if (x.WorkflowItemEncounter is not null && existingWorkflowItems.Find(y => y.Uuid == x.Uuid).WorkflowItemEncounter is null)
                        {
                            x.WorkflowItemEncounter.Uuid = IdGenerator.GenerateId();
                            x.WorkflowItemEncounter.EncounterContactMode?.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                            x.WorkflowItemEncounter.EncounterEnvironmentalSetting?.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                            x.WorkflowItemEncounter.EncounterType?.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                            if (x.WorkflowItemEncounter.StartRule is not null)
                                x.WorkflowItemEncounter.StartRule.Uuid = IdGenerator.GenerateId();
                            if (x.WorkflowItemEncounter.EndRule is not null)
                                x.WorkflowItemEncounter.EndRule.Uuid = IdGenerator.GenerateId();
                        }
                        if (x.WorkflowItemActivity is not null && existingWorkflowItems.Find(y => y.Uuid == x.Uuid).WorkflowItemActivity is not null)
                        {
                            if(String.IsNullOrWhiteSpace(x.WorkflowItemActivity.Uuid))
                            {
                                x.WorkflowItemActivity.Uuid = IdGenerator.GenerateId();
                                if (x.WorkflowItemActivity.DefinedProcedures is not null && x.WorkflowItemActivity.DefinedProcedures.Any())
                                {
                                    x.WorkflowItemActivity.DefinedProcedures.ForEach(y =>
                                    {
                                        y.Uuid = IdGenerator.GenerateId();
                                        if (y.ProcedureCode is not null)
                                            y.ProcedureCode.ForEach(z => z.Uuid = IdGenerator.GenerateId());
                                    });
                                }
                                if (x.WorkflowItemActivity.StudyDataCollection is not null && x.WorkflowItemActivity.StudyDataCollection.Any())
                                {
                                    x.WorkflowItemActivity.StudyDataCollection.ForEach(z => z.Uuid = IdGenerator.GenerateId());
                                }
                            }
                            else
                            {
                                
                                x.WorkflowItemActivity.DefinedProcedures = CheckForDefinedProceduresSection(x.WorkflowItemActivity.DefinedProcedures, existingWorkflowItems.Find(y => y.Uuid == x.Uuid).WorkflowItemActivity.DefinedProcedures);
                                x.WorkflowItemActivity.StudyDataCollection = CheckForStudyDataCollectionSection(x.WorkflowItemActivity.StudyDataCollection, existingWorkflowItems.Find(y => y.Uuid == x.Uuid).WorkflowItemActivity.StudyDataCollection);
                            }
                        }
                        else if (x.WorkflowItemActivity is not null && existingWorkflowItems.Find(y => y.Uuid == x.Uuid).WorkflowItemActivity is null)
                        {
                            x.WorkflowItemActivity.Uuid = IdGenerator.GenerateId();
                            if (x.WorkflowItemActivity.DefinedProcedures is not null && x.WorkflowItemActivity.DefinedProcedures.Any())
                            {
                                x.WorkflowItemActivity.DefinedProcedures.ForEach(y =>
                                {
                                    y.Uuid = IdGenerator.GenerateId();
                                    if (y.ProcedureCode is not null)
                                        y.ProcedureCode.ForEach(z => z.Uuid = IdGenerator.GenerateId());
                                });
                            }
                            if (x.WorkflowItemActivity.StudyDataCollection is not null && x.WorkflowItemActivity.StudyDataCollection.Any())
                            {
                                x.WorkflowItemActivity.StudyDataCollection.ForEach(z => z.Uuid = IdGenerator.GenerateId());
                            }
                        }
                        workFlowItems.Add(x);
                        existingWorkflowItems.RemoveAll(y => y.Uuid == x.Uuid);
                    }
                    else
                    {
                        x.Uuid = IdGenerator.GenerateId();
                        if (x.WorkflowItemEncounter is not null)
                        {
                            x.WorkflowItemEncounter.Uuid = IdGenerator.GenerateId();
                            x.WorkflowItemEncounter.EncounterContactMode?.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                            x.WorkflowItemEncounter.EncounterEnvironmentalSetting?.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                            x.WorkflowItemEncounter.EncounterType?.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                            if (x.WorkflowItemEncounter.StartRule is not null)
                                x.WorkflowItemEncounter.StartRule.Uuid = IdGenerator.GenerateId();
                            if (x.WorkflowItemEncounter.EndRule is not null)
                                x.WorkflowItemEncounter.EndRule.Uuid = IdGenerator.GenerateId();
                        }
                        if (x.WorkflowItemActivity is not null)
                        {
                            x.WorkflowItemActivity.Uuid = IdGenerator.GenerateId();
                            if (x.WorkflowItemActivity.DefinedProcedures is not null && x.WorkflowItemActivity.DefinedProcedures.Any())
                            {
                                x.WorkflowItemActivity.DefinedProcedures.ForEach(y =>
                                {
                                    y.Uuid = IdGenerator.GenerateId();
                                    if (y.ProcedureCode is not null)
                                        y.ProcedureCode.ForEach(z => z.Uuid = IdGenerator.GenerateId());
                                });
                            }
                            if (x.WorkflowItemActivity.StudyDataCollection is not null && x.WorkflowItemActivity.StudyDataCollection.Any())
                            {
                                x.WorkflowItemActivity.StudyDataCollection.ForEach(z => z.Uuid = IdGenerator.GenerateId());
                            }
                        }
                        workFlowItems.Add(x);
                    }
                });
                incomingWorkflowItems = workFlowItems;
            }
            else if (incomingWorkflowItems is not null && existingWorkflowItems is null)
            {
                incomingWorkflowItems.ForEach(x =>
                {
                    x.Uuid = IdGenerator.GenerateId();
                    if (x.WorkflowItemEncounter is not null)
                    {
                        x.WorkflowItemEncounter.Uuid = IdGenerator.GenerateId();
                        x.WorkflowItemEncounter.EncounterContactMode?.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                        x.WorkflowItemEncounter.EncounterEnvironmentalSetting?.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                        x.WorkflowItemEncounter.EncounterType?.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                        if (x.WorkflowItemEncounter.StartRule is not null)
                            x.WorkflowItemEncounter.StartRule.Uuid = IdGenerator.GenerateId();
                        if (x.WorkflowItemEncounter.EndRule is not null)
                            x.WorkflowItemEncounter.EndRule.Uuid = IdGenerator.GenerateId();
                    }
                    if (x.WorkflowItemActivity is not null)
                    {
                        x.WorkflowItemActivity.Uuid = IdGenerator.GenerateId();
                        if (x.WorkflowItemActivity.DefinedProcedures is not null && x.WorkflowItemActivity.DefinedProcedures.Any())
                        {
                            x.WorkflowItemActivity.DefinedProcedures.ForEach(y =>
                            {
                                y.Uuid = IdGenerator.GenerateId();
                                if (y.ProcedureCode is not null)
                                    y.ProcedureCode.ForEach(z => z.Uuid = IdGenerator.GenerateId());
                            });
                        }
                        if (x.WorkflowItemActivity.StudyDataCollection is not null && x.WorkflowItemActivity.StudyDataCollection.Any())
                        {
                            x.WorkflowItemActivity.StudyDataCollection.ForEach(z => z.Uuid = IdGenerator.GenerateId());
                        }
                    }
                });
            }

            return incomingWorkflowItems;
        }
        public List<DefinedProcedureEntity> CheckForDefinedProceduresSection(List<DefinedProcedureEntity> incomingDefinedProcedures, List<DefinedProcedureEntity> exisitingDefinedProcedures)
        {
            if (incomingDefinedProcedures is not null && exisitingDefinedProcedures is not null)
            {
                List<DefinedProcedureEntity> definedProcedures = new List<DefinedProcedureEntity>();
                incomingDefinedProcedures.ForEach(x =>
                {
                    if (exisitingDefinedProcedures.Any(y => y.Uuid == x.Uuid))
                    {
                        x.ProcedureCode = CheckForCodeSection(x.ProcedureCode, exisitingDefinedProcedures.Find(y => y.Uuid == x.Uuid).ProcedureCode);
                        definedProcedures.Add(x);
                        exisitingDefinedProcedures.RemoveAll(y => y.Uuid == x.Uuid);
                    }
                    else
                    {
                        x.Uuid = IdGenerator.GenerateId();
                        x.ProcedureCode?.ForEach(y => y.Uuid = IdGenerator.GenerateId());
                        definedProcedures.Add(x);
                    }
                });
                incomingDefinedProcedures = definedProcedures;
            }
            else if (incomingDefinedProcedures is not null && exisitingDefinedProcedures is null)
            {
                incomingDefinedProcedures.ForEach(x =>
                {
                    x.Uuid = IdGenerator.GenerateId();
                    x.ProcedureCode?.ForEach(y => y.Uuid = IdGenerator.GenerateId());
                });
            }
            return incomingDefinedProcedures;
        }

        public List<EstimandEntity> CheckForStudyEstimandSection(List<EstimandEntity> incomingEstimands, List<EstimandEntity> existingEstimands)
        {
            if (incomingEstimands is not null && existingEstimands is not null)
            {
                List<EstimandEntity> estimands = new List<EstimandEntity>();
                incomingEstimands.ForEach(x =>
                {
                    if (existingEstimands.Any(y => y.Uuid == x.Uuid))
                    {
                        if (x.Treatment is not null && existingEstimands.Find(y => y.Uuid == x.Uuid).Treatment is not null)
                        {
                            if(String.IsNullOrWhiteSpace(x.Treatment.Uuid))
                            {
                                x.Treatment.Uuid = IdGenerator.GenerateId();
                                x.Treatment.Codes?.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                            }
                            else
                            {                                
                                x.Treatment.Codes = CheckForCodeSection(x.Treatment.Codes, existingEstimands.Find(y => y.Uuid == x.Uuid).Treatment.Codes);
                            }
                        }
                        else if (x.Treatment is not null && existingEstimands.Find(y => y.Uuid == x.Uuid).Treatment is null)
                        {
                            x.Treatment.Uuid = IdGenerator.GenerateId();
                            x.Treatment.Codes?.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                        }
                        if (x.AnalysisPopulation is not null)
                        {
                            x.AnalysisPopulation.Uuid = String.IsNullOrWhiteSpace(x.AnalysisPopulation.Uuid) ? IdGenerator.GenerateId() : x.AnalysisPopulation.Uuid; 
                        }
                        if (x.VariableOfInterest is not null && existingEstimands.Find(y => y.Uuid == x.Uuid).VariableOfInterest is not null)
                        {                           
                            if(String.IsNullOrWhiteSpace(x.VariableOfInterest.Uuid))
                            {
                                x.VariableOfInterest.Uuid =  IdGenerator.GenerateId();
                                x.VariableOfInterest.EncounterContactMode?.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                                x.VariableOfInterest.EncounterEnvironmentalSetting?.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                                x.VariableOfInterest.EncounterType?.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                                if (x.VariableOfInterest.StartRule is not null)
                                    x.VariableOfInterest.StartRule.Uuid = IdGenerator.GenerateId();
                                if (x.VariableOfInterest.EndRule is not null)
                                    x.VariableOfInterest.EndRule.Uuid = IdGenerator.GenerateId();
                            }
                            else
                            {
                                x.VariableOfInterest.EncounterContactMode = CheckForCodeSection(x.VariableOfInterest.EncounterContactMode, existingEstimands.Find(y => y.Uuid == x.Uuid).VariableOfInterest.EncounterContactMode);
                                x.VariableOfInterest.EncounterEnvironmentalSetting = CheckForCodeSection(x.VariableOfInterest.EncounterEnvironmentalSetting, existingEstimands.Find(y => y.Uuid == x.Uuid).VariableOfInterest.EncounterEnvironmentalSetting);
                                x.VariableOfInterest.EncounterType = CheckForCodeSection(x.VariableOfInterest.EncounterType, existingEstimands.Find(y => y.Uuid == x.Uuid).VariableOfInterest.EncounterType);
                                if (x.VariableOfInterest.StartRule is not null)
                                    x.VariableOfInterest.StartRule.Uuid = String.IsNullOrWhiteSpace(x.VariableOfInterest.StartRule.Uuid) ? IdGenerator.GenerateId() : x.VariableOfInterest.StartRule.Uuid;
                                if (x.VariableOfInterest.EndRule is not null)
                                    x.VariableOfInterest.EndRule.Uuid = String.IsNullOrWhiteSpace(x.VariableOfInterest.EndRule.Uuid) ? IdGenerator.GenerateId() : x.VariableOfInterest.EndRule.Uuid;
                            }
                        }
                        else if (x.VariableOfInterest is not null && existingEstimands.Find(y => y.Uuid == x.Uuid).VariableOfInterest is null)
                        {
                            x.VariableOfInterest.Uuid = IdGenerator.GenerateId();
                            x.VariableOfInterest.EncounterContactMode?.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                            x.VariableOfInterest.EncounterEnvironmentalSetting?.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                            x.VariableOfInterest.EncounterType?.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                            if (x.VariableOfInterest.StartRule is not null)
                                x.VariableOfInterest.StartRule.Uuid = IdGenerator.GenerateId();
                            if (x.VariableOfInterest.EndRule is not null)
                                x.VariableOfInterest.EndRule.Uuid = IdGenerator.GenerateId();
                        }
                        x.InterCurrentEvents = CheckForIntercurrentEventsSection(x.InterCurrentEvents, existingEstimands.Find(y => y.Uuid == x.Uuid).InterCurrentEvents);
                        estimands.Add(x);
                        existingEstimands.RemoveAll(y => y.Uuid == x.Uuid);
                    }
                    else
                    {
                        estimands.AddRange(GenerateIdForStudyEstimand(new List<EstimandEntity> { x }));
                    }
                });
                incomingEstimands = estimands;
            }
            else if (incomingEstimands is not null && existingEstimands is null)
            {
                incomingEstimands = GenerateIdForStudyEstimand(incomingEstimands);
            }
            return incomingEstimands;
        }
        public List<InterCurrentEventEntity> CheckForIntercurrentEventsSection(List<InterCurrentEventEntity> incomingInterCurrentEvents, List<InterCurrentEventEntity> exisitingInterCurrentEvents)
        {
            if (incomingInterCurrentEvents is not null && exisitingInterCurrentEvents is not null)
            {
                List<InterCurrentEventEntity> interCurrentEvents = new List<InterCurrentEventEntity>();
                incomingInterCurrentEvents.ForEach(x =>
                {
                    if (exisitingInterCurrentEvents.Any(y => y.Uuid == x.Uuid))
                    {
                        x.Strategy = CheckForCodeSection(x.Strategy, exisitingInterCurrentEvents.Find(y => y.Uuid == x.Uuid).Strategy);
                        interCurrentEvents.Add(x);
                        exisitingInterCurrentEvents.RemoveAll(y => y.Uuid == x.Uuid);
                    }
                    else
                    {
                        x.Uuid = IdGenerator.GenerateId();
                        if (x.Strategy is not null && x.Strategy.Any())
                            x.Strategy.ForEach(y => y.Uuid = IdGenerator.GenerateId());
                        interCurrentEvents.Add(x);
                    }
                });
                incomingInterCurrentEvents = interCurrentEvents;
            }
            else if (incomingInterCurrentEvents is not null && exisitingInterCurrentEvents is null)
            {
                incomingInterCurrentEvents.ForEach(x =>
                {
                    x.Uuid = IdGenerator.GenerateId();
                    if(x.Strategy is not null && x.Strategy.Any())
                      x.Strategy.ForEach(y => y.Uuid = IdGenerator.GenerateId());
                });
            }
            return incomingInterCurrentEvents;
        }

        #endregion
    }

}
