using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Entities.StudyV1;

namespace TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1
{
    /// <summary>
    /// This class is used as a helper for different funtionalities
    /// </summary>
    public class HelperV1 : IHelperV1
    {
        /// <summary>
        /// Get Audit Trail fields for the POST Api
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public AuditTrailEntity GetAuditTrail(string user)
        {
            return new AuditTrailEntity
            {
                EntryDateTime = DateTime.UtcNow,
                CreatedBy = user,
            };
        }

        #region Generate Id for each sections
        /// <summary>
        /// Generate uuid for Each section of study
        /// </summary>
        /// <param name="study">Study Entity</param>
        /// <returns></returns>
        public StudyDefinitionsEntity GeneratedSectionId(StudyDefinitionsEntity study)
        {
            study.Study.Uuid = IdGenerator.GenerateId();

            if (study.Study.StudyType is not null)
                study.Study.StudyType.Uuid = IdGenerator.GenerateId();

            study.Study.StudyIdentifiers = GenerateIdForStudyIdentifier(study.Study.StudyIdentifiers);

            if (study.Study.StudyPhase is not null)
                study.Study.StudyPhase.Uuid = IdGenerator.GenerateId();

            study.Study.StudyProtocolVersions = GenerateIdForStudyProtocol(study.Study.StudyProtocolVersions);

            study.Study.StudyDesigns = GenerateIdForStudyDesign(study.Study.StudyDesigns);

            return study;
        }
        /// <summary>
        /// Generate uuid for Study Identifiers
        /// </summary>
        /// <param name="studyIdentifiers"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Generate uuid for Study Protocol Versions
        /// </summary>
        /// <param name="studyProtocolVersions"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Generate uuid for Study StudyDesigns
        /// </summary>
        /// <param name="studyDesigns"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Generate uuid for Study Indications
        /// </summary>
        /// <param name="indications"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Generate uuid for Study Investigational Interventions
        /// </summary>
        /// <param name="investigationalInterventions"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Generate uuid for Study Objectives
        /// </summary>
        /// <param name="objectives"></param>
        /// <returns></returns>
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
                            if (y.EndpointLevel is not null && y.EndpointLevel.Any())
                                y.EndpointLevel.ForEach(z => z.Uuid = IdGenerator.GenerateId());
                        });
                    }
                });
            }
            return objectives;
        }
        /// <summary>
        /// Generate uuid for Study Cells
        /// </summary>
        /// <param name="studyCells"></param>
        /// <returns></returns>
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

                        if (x.StudyEpoch.Encounters is not null && x.StudyEpoch.Encounters.Any())
                        {
                            x.StudyEpoch.Encounters.ForEach(y =>
                            {
                                y.Uuid = IdGenerator.GenerateId();
                                if (y.EncounterContactMode is not null && y.EncounterContactMode.Any())
                                {
                                    y.EncounterContactMode.ForEach(procedure => procedure.Uuid = IdGenerator.GenerateId());
                                }
                                if (y.EncounterEnvironmentalSetting is not null && y.EncounterEnvironmentalSetting.Any())
                                {
                                    y.EncounterEnvironmentalSetting.ForEach(procedure => procedure.Uuid = IdGenerator.GenerateId());
                                }
                                if (y.EncounterType is not null && y.EncounterType.Any())
                                {
                                    y.EncounterType.ForEach(procedure => procedure.Uuid = IdGenerator.GenerateId());
                                }
                                if (y.TransitionStartRule is not null)
                                    y.TransitionStartRule.Uuid = IdGenerator.GenerateId();
                                if (y.TransitionEndRule is not null)
                                    y.TransitionEndRule.Uuid = IdGenerator.GenerateId();
                            });
                        }
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

        /// <summary>
        /// Generate uuid for Study Workflows
        /// </summary>
        /// <param name="workflows"></param>
        /// <returns></returns>
        public List<WorkflowEntity> GenerateIdForStudyWorkflow(List<WorkflowEntity> workflows)
        {
            if (workflows is not null && workflows.Any())
            {
                workflows.ForEach(x =>
                {
                    x.Uuid = IdGenerator.GenerateId();

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
                                        procedure.ProcedureCode?.ForEach(y => y.Uuid = IdGenerator.GenerateId());
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
                                if (y.WorkflowItemEncounter.TransitionStartRule is not null)
                                    y.WorkflowItemEncounter.TransitionStartRule.Uuid = IdGenerator.GenerateId();
                                if (y.WorkflowItemEncounter.TransitionEndRule is not null)
                                    y.WorkflowItemEncounter.TransitionEndRule.Uuid = IdGenerator.GenerateId();
                            }

                        });
                    }
                });
            }
            return workflows;
        }
        /// <summary>
        /// Generate uuid for Study Estimands
        /// </summary>
        /// <param name="estimands"></param>
        /// <returns></returns>
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
                        if (x.Treatment.Codes is not null && x.Treatment.Codes.Any())
                            x.Treatment.Codes.ForEach(y => y.Uuid = IdGenerator.GenerateId());
                    }

                    if (x.AnalysisPopulation is not null)
                        x.AnalysisPopulation.Uuid = IdGenerator.GenerateId();

                    if (x.VariableOfInterest is not null)
                    {
                        x.VariableOfInterest.Uuid = IdGenerator.GenerateId();
                        if (x.VariableOfInterest.EndpointLevel is not null && x.VariableOfInterest.EndpointLevel.Any())
                            x.VariableOfInterest.EndpointLevel.ForEach(z => z.Uuid = IdGenerator.GenerateId());
                    }

                    if (x.IntercurrentEvents is not null && x.IntercurrentEvents.Any())
                    {
                        x.IntercurrentEvents.ForEach(y =>
                        {
                            y.Uuid = IdGenerator.GenerateId();
                        });
                    }
                });
            }
            return estimands;
        }
        #endregion

        #region Remove Id for Each section
        /// <summary>
        /// Remode uuid for Study
        /// </summary>
        /// <param name="study"></param>
        /// <returns></returns>
        public StudyDefinitionsEntity RemovedSectionId(StudyDefinitionsEntity study)
        {
            study.Study.Uuid = null;

            if (study.Study.StudyType is not null)
                study.Study.StudyType.Uuid = null;

            study.Study.StudyIdentifiers = RemoveIdForStudyIdentifier(study.Study.StudyIdentifiers);

            if (study.Study.StudyPhase is not null)
                study.Study.StudyPhase.Uuid = null;

            study.Study.StudyProtocolVersions = RemoveIdForStudyProtocol(study.Study.StudyProtocolVersions);

            study.Study.StudyDesigns = RemoveIdForStudyDesign(study.Study.StudyDesigns);

            return study;
        }
        /// <summary>
        /// Remove uuid for Study Identifier
        /// </summary>
        /// <param name="studyIdentifiers"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Remove uuid for Study Protocol Versions
        /// </summary>
        /// <param name="studyProtocolVersions"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Remove uuid for Study Designs
        /// </summary>
        /// <param name="studyDesigns"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Remove uuid for Study Indications
        /// </summary>
        /// <param name="indications"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Remove uuid for Study Investigational Interventions
        /// </summary>
        /// <param name="investigationalInterventions"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Remove uuid for Study Objectives
        /// </summary>
        /// <param name="objectives"></param>
        /// <returns></returns>
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
                            if (y.EndpointLevel is not null && y.EndpointLevel.Any())
                                y.EndpointLevel.ForEach(z => z.Uuid = null);
                        });
                    }
                });
            }
            return objectives;
        }
        /// <summary>
        /// Remove uuid for Study Cells
        /// </summary>
        /// <param name="studyCells"></param>
        /// <returns></returns>
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
                            x.StudyArm.StudyArmDataOriginType.ForEach(y => y.Uuid = null);
                        if (x.StudyArm.StudyArmType is not null && x.StudyArm.StudyArmType.Any())
                            x.StudyArm.StudyArmType.ForEach(y => y.Uuid = null);
                    }
                    if (x.StudyEpoch is not null)
                    {
                        x.StudyEpoch.Uuid = null;
                        if (x.StudyEpoch.StudyEpochType is not null && x.StudyEpoch.StudyEpochType.Any())
                            x.StudyEpoch.StudyEpochType.ForEach(y => y.Uuid = null);

                        if (x.StudyEpoch.Encounters is not null && x.StudyEpoch.Encounters.Any())
                        {
                            x.StudyEpoch.Encounters.ForEach(y =>
                            {
                                y.Uuid = null;
                                if (y.EncounterContactMode is not null && y.EncounterContactMode.Any())
                                {
                                    y.EncounterContactMode.ForEach(procedure => procedure.Uuid = null);
                                }
                                if (y.EncounterEnvironmentalSetting is not null && y.EncounterEnvironmentalSetting.Any())
                                {
                                    y.EncounterEnvironmentalSetting.ForEach(procedure => procedure.Uuid = null);
                                }
                                if (y.EncounterType is not null && y.EncounterType.Any())
                                {
                                    y.EncounterType.ForEach(procedure => procedure.Uuid = null);
                                }
                                if (y.TransitionStartRule is not null)
                                    y.TransitionStartRule.Uuid = null;
                                if (y.TransitionEndRule is not null)
                                    y.TransitionEndRule.Uuid = null;
                            });
                        }
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
        /// <summary>
        /// Remove uuid for Study Workflows
        /// </summary>
        /// <param name="workflows"></param>
        /// <returns></returns>
        public List<WorkflowEntity> RemoveIdForStudyWorkflow(List<WorkflowEntity> workflows)
        {
            if (workflows is not null && workflows.Any())
            {
                workflows.ForEach(x =>
                {
                    x.Uuid = null;

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
                                    y.WorkflowItemActivity.DefinedProcedures.ForEach(procedure =>
                                    {

                                        procedure.Uuid = null;
                                        procedure.ProcedureCode?.ForEach(y => y.Uuid = null);
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
                                if (y.WorkflowItemEncounter.TransitionStartRule is not null)
                                    y.WorkflowItemEncounter.TransitionStartRule.Uuid = null;
                                if (y.WorkflowItemEncounter.TransitionEndRule is not null)
                                    y.WorkflowItemEncounter.TransitionEndRule.Uuid = null;
                            }

                        });
                    }
                });
            }
            return workflows;
        }
        /// <summary>
        /// Remove uuid for Study Estimands
        /// </summary>
        /// <param name="estimands"></param>
        /// <returns></returns>
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
                        if (x.VariableOfInterest.EndpointLevel is not null && x.VariableOfInterest.EndpointLevel.Any())
                            x.VariableOfInterest.EndpointLevel.ForEach(z => z.Uuid = null);
                    }

                    if (x.IntercurrentEvents is not null && x.IntercurrentEvents.Any())
                    {
                        x.IntercurrentEvents.ForEach(y =>
                        {
                            y.Uuid = null;
                        });
                    }
                });
            }
            return estimands;
        }
        #endregion

        #region Check whole study
        /// <summary>
        /// Compare Full Study
        /// </summary>
        /// <param name="incoming"></param>
        /// <param name="existing"></param>
        /// <returns></returns>
        public bool IsSameStudy(StudyDefinitionsEntity incoming, StudyDefinitionsEntity existing)
        {
            try
            {
                var duplicateExistingStudy = JsonConvert.DeserializeObject<StudyDefinitionsEntity>(JsonConvert.SerializeObject(existing)); // Creating duplicates for existing entity
                var duplicateIncomingStudy = JsonConvert.DeserializeObject<StudyDefinitionsEntity>(JsonConvert.SerializeObject(incoming)); // Creating duplicates for incoming entity

                duplicateIncomingStudy.AuditTrail = duplicateExistingStudy.AuditTrail = null;
                duplicateIncomingStudy.Id = duplicateExistingStudy.Id = null;

                return JsonObjectCheck(RemovedSectionId(duplicateIncomingStudy), RemovedSectionId(duplicateExistingStudy));
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Deep compare of existing and incoming study
        /// </summary>
        /// <param name="incoming"></param>
        /// <param name="existing"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Comparison between existing and incoming study
        /// </summary>
        /// <param name="incoming"></param>
        /// <param name="existing"></param>
        /// <returns></returns>
        public StudyDefinitionsEntity CheckForSections(StudyDefinitionsEntity incoming, StudyDefinitionsEntity existing)
        {
            //For StudyType
            if (existing.Study.StudyType is null && incoming.Study.StudyType is not null)
                incoming.Study.StudyType.Uuid = IdGenerator.GenerateId();
            else if (existing.Study.StudyType is not null && incoming.Study.StudyType is not null)
                incoming.Study.StudyType.Uuid = String.IsNullOrWhiteSpace(incoming.Study.StudyType.Uuid) ? IdGenerator.GenerateId() : incoming.Study.StudyType.Uuid;

            //For StudyPhase
            if (existing.Study.StudyPhase is null && incoming.Study.StudyPhase is not null)
                incoming.Study.StudyPhase.Uuid = IdGenerator.GenerateId();
            else if (existing.Study.StudyPhase is not null && incoming.Study.StudyPhase is not null)
                incoming.Study.StudyPhase.Uuid = String.IsNullOrWhiteSpace(incoming.Study.StudyPhase.Uuid) ? IdGenerator.GenerateId() : incoming.Study.StudyPhase.Uuid;

            incoming.Study.StudyIdentifiers = CheckForStudyIdentifierSection(incoming.Study.StudyIdentifiers,
                                                                                     existing.Study.StudyIdentifiers);

            incoming.Study.StudyProtocolVersions = CheckForStudyProtocolSection(incoming.Study.StudyProtocolVersions,
                                                                                     existing.Study.StudyProtocolVersions);
            incoming.Study.StudyDesigns = CheckForStudyDesignSection(incoming.Study.StudyDesigns, existing.Study.StudyDesigns);

            return incoming;
        }
        /// <summary>
        /// Comparison between existing and incoming Study Identifiers
        /// </summary>
        /// <param name="incomingStudyIdentifiers"></param>
        /// <param name="existingStudyIdentifiers"></param>
        /// <returns></returns>
        public List<StudyIdentifierEntity> CheckForStudyIdentifierSection(List<StudyIdentifierEntity> incomingStudyIdentifiers, List<StudyIdentifierEntity> existingStudyIdentifiers)
        {
            if (incomingStudyIdentifiers is not null && existingStudyIdentifiers is not null)
            {
                List<StudyIdentifierEntity> studyIdentifiers = new();
                incomingStudyIdentifiers.ForEach(x =>
                {
                    if (existingStudyIdentifiers.Any(y => y.Uuid == x.Uuid))
                    {
                        if (x.StudyIdentifierScope is not null)
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
        /// <summary>
        /// Comparison between existing and incoming Study ProtocolVersions
        /// </summary>
        /// <param name="incomingStudyProtocolVersions"></param>
        /// <param name="existingStudyProtocolVersions"></param>
        /// <returns></returns>
        public List<StudyProtocolVersionEntity> CheckForStudyProtocolSection(List<StudyProtocolVersionEntity> incomingStudyProtocolVersions, List<StudyProtocolVersionEntity> existingStudyProtocolVersions)
        {
            if (incomingStudyProtocolVersions is not null && existingStudyProtocolVersions is not null)
            {
                List<StudyProtocolVersionEntity> studyProtocols = new();
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
        /// <summary>
        /// Comparison between existing and incoming Code
        /// </summary>
        /// <param name="incomingCodes"></param>
        /// <param name="existingCodes"></param>
        /// <returns></returns>
        public List<CodeEntity> CheckForCodeSection(List<CodeEntity> incomingCodes, List<CodeEntity> existingCodes)
        {
            if (incomingCodes is not null && existingCodes is not null)
            {
                List<CodeEntity> codes = new();
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
        /// <summary>
        /// Comparison between existing and incoming Study Designs
        /// </summary>
        /// <param name="incomingStudyDesigns"></param>
        /// <param name="existingStudyDesigns"></param>
        /// <returns></returns>
        public List<StudyDesignEntity> CheckForStudyDesignSection(List<StudyDesignEntity> incomingStudyDesigns, List<StudyDesignEntity> existingStudyDesigns)
        {
            if (incomingStudyDesigns is not null && existingStudyDesigns is not null)
            {
                List<StudyDesignEntity> studyDesigns = new();
                incomingStudyDesigns.ForEach(x =>
                {
                    if (existingStudyDesigns.Any(y => y.Uuid == x.Uuid))
                    {
                        ParallelOptions parallelOptions = new()
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
        /// <summary>
        /// Comparison between existing and incoming Study Indications
        /// </summary>
        /// <param name="incomingIndications"></param>
        /// <param name="exisitingIndications"></param>
        /// <returns></returns>
        public List<IndicationEntity> CheckForStudyIndicationsSection(List<IndicationEntity> incomingIndications, List<IndicationEntity> exisitingIndications)
        {
            if (incomingIndications is not null && exisitingIndications is not null)
            {
                List<IndicationEntity> indications = new();
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
        /// <summary>
        /// Comparison between existing and incoming Study Investigational Interventions
        /// </summary>
        /// <param name="incomingInvestigationalInterventions"></param>
        /// <param name="existingInvestigationalInterventions"></param>
        /// <returns></returns>
        public List<InvestigationalInterventionEntity> CheckForInvestigationalInterventionsSection(List<InvestigationalInterventionEntity> incomingInvestigationalInterventions, List<InvestigationalInterventionEntity> existingInvestigationalInterventions)
        {
            if (incomingInvestigationalInterventions is not null && existingInvestigationalInterventions is not null)
            {
                List<InvestigationalInterventionEntity> investigationalInterventions = new();
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
        /// <summary>
        /// Comparison between existing and incoming Study Design Population
        /// </summary>
        /// <param name="incomingStudyDesignPopulations"></param>
        /// <param name="existingStudyDesignPopulations"></param>
        /// <returns></returns>
        public List<StudyDesignPopulationEntity> CheckForStudyDesignPopulationsSection(List<StudyDesignPopulationEntity> incomingStudyDesignPopulations, List<StudyDesignPopulationEntity> existingStudyDesignPopulations)
        {
            if (incomingStudyDesignPopulations is not null && existingStudyDesignPopulations is not null)
            {
                List<StudyDesignPopulationEntity> studyDesignPopulations = new();
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
        /// <summary>
        /// Comparison between existing and incoming Study Objectives
        /// </summary>
        /// <param name="incomingObjectives"></param>
        /// <param name="existingObjectives"></param>
        /// <returns></returns>
        public List<ObjectiveEntity> CheckForStudyObjectivesSection(List<ObjectiveEntity> incomingObjectives, List<ObjectiveEntity> existingObjectives)
        {
            if (incomingObjectives is not null && existingObjectives is not null)
            {
                List<ObjectiveEntity> studyObjectives = new();
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
        /// <summary>
        /// Comparison between existing and incoming Study Objective Endpoints
        /// </summary>
        /// <param name="incomingEndpoints"></param>
        /// <param name="existingEndpoints"></param>
        /// <returns></returns>
        public List<EndpointEntity> CheckForStudyObjectivesEndpointsSection(List<EndpointEntity> incomingEndpoints, List<EndpointEntity> existingEndpoints)
        {
            if (incomingEndpoints is not null && existingEndpoints is not null)
            {
                List<EndpointEntity> studyEndpoints = new();
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
                    if (x.EndpointLevel is not null && x.EndpointLevel.Any())
                        x.EndpointLevel.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                });
            }
            return incomingEndpoints;
        }
        /// <summary>
        /// Comparison between existing and incoming Study Cells
        /// </summary>
        /// <param name="incomingStudyCells"></param>
        /// <param name="existingStudyCells"></param>
        /// <returns></returns>
        public List<StudyCellEntity> CheckForStudyCellsSection(List<StudyCellEntity> incomingStudyCells, List<StudyCellEntity> existingStudyCells)
        {
            if (incomingStudyCells is not null && existingStudyCells is not null)
            {
                List<StudyCellEntity> studyCells = new();
                incomingStudyCells.ForEach(x =>
                {
                    if (existingStudyCells.Any(y => y.Uuid == x.Uuid))
                    {
                        if (x.StudyArm is not null && existingStudyCells.Find(y => y.Uuid == x.Uuid).StudyArm is null)
                        {
                            x.StudyArm.Uuid = IdGenerator.GenerateId();
                            if (x.StudyArm.StudyArmDataOriginType is not null && x.StudyArm.StudyArmDataOriginType.Any())
                                x.StudyArm.StudyArmDataOriginType.ForEach(y => y.Uuid = IdGenerator.GenerateId());
                            if (x.StudyArm.StudyArmType is not null && x.StudyArm.StudyArmType.Any())
                                x.StudyArm.StudyArmType.ForEach(y => y.Uuid = IdGenerator.GenerateId());
                        }
                        else if (x.StudyArm is not null && existingStudyCells.Find(y => y.Uuid == x.Uuid).StudyArm is not null)
                        {
                            if (String.IsNullOrWhiteSpace(x.StudyArm.Uuid))
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
                            if (x.StudyEpoch.StudyEpochType is not null && x.StudyEpoch.StudyEpochType.Any())
                                x.StudyEpoch.StudyEpochType.ForEach(y => y.Uuid = IdGenerator.GenerateId());
                            if (x.StudyEpoch.Encounters is not null && x.StudyEpoch.Encounters.Any())
                            {
                                x.StudyEpoch.Encounters.ForEach(y =>
                                {
                                    y.Uuid = IdGenerator.GenerateId();
                                    if (y.EncounterContactMode is not null && y.EncounterContactMode.Any())
                                    {
                                        y.EncounterContactMode.ForEach(procedure => procedure.Uuid = IdGenerator.GenerateId());
                                    }
                                    if (y.EncounterEnvironmentalSetting is not null && y.EncounterEnvironmentalSetting.Any())
                                    {
                                        y.EncounterEnvironmentalSetting.ForEach(procedure => procedure.Uuid = IdGenerator.GenerateId());
                                    }
                                    if (y.EncounterType is not null && y.EncounterType.Any())
                                    {
                                        y.EncounterType.ForEach(procedure => procedure.Uuid = IdGenerator.GenerateId());
                                    }
                                    if (y.TransitionStartRule is not null)
                                        y.TransitionStartRule.Uuid = IdGenerator.GenerateId();
                                    if (y.TransitionEndRule is not null)
                                        y.TransitionEndRule.Uuid = IdGenerator.GenerateId();
                                });
                            }

                        }
                        else if (x.StudyEpoch is not null && existingStudyCells.Find(y => y.Uuid == x.Uuid).StudyEpoch is not null)
                        {
                            if (String.IsNullOrWhiteSpace(x.StudyEpoch.Uuid))
                            {
                                x.StudyEpoch.Uuid = IdGenerator.GenerateId();
                                x.StudyEpoch.StudyEpochType?.ForEach(y => y.Uuid = IdGenerator.GenerateId());
                                if (x.StudyEpoch.Encounters is not null && x.StudyEpoch.Encounters.Any())
                                {
                                    x.StudyEpoch.Encounters.ForEach(y =>
                                    {
                                        y.Uuid = IdGenerator.GenerateId();
                                        if (y.EncounterContactMode is not null && y.EncounterContactMode.Any())
                                        {
                                            y.EncounterContactMode.ForEach(procedure => procedure.Uuid = IdGenerator.GenerateId());
                                        }
                                        if (y.EncounterEnvironmentalSetting is not null && y.EncounterEnvironmentalSetting.Any())
                                        {
                                            y.EncounterEnvironmentalSetting.ForEach(procedure => procedure.Uuid = IdGenerator.GenerateId());
                                        }
                                        if (y.EncounterType is not null && y.EncounterType.Any())
                                        {
                                            y.EncounterType.ForEach(procedure => procedure.Uuid = IdGenerator.GenerateId());
                                        }
                                        if (y.TransitionStartRule is not null)
                                            y.TransitionStartRule.Uuid = IdGenerator.GenerateId();
                                        if (y.TransitionEndRule is not null)
                                            y.TransitionEndRule.Uuid = IdGenerator.GenerateId();
                                    });
                                }
                            }
                            else
                            {
                                x.StudyEpoch.StudyEpochType = CheckForCodeSection(x.StudyEpoch.StudyEpochType, existingStudyCells.Find(y => y.Uuid == x.Uuid).StudyEpoch.StudyEpochType);
                                x.StudyEpoch.Encounters = CheckForEncounterListSection(x.StudyEpoch.Encounters, existingStudyCells.Find(y => y.Uuid == x.Uuid).StudyEpoch.Encounters);
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
        /// <summary>
        /// Comparison between existing and incoming Encounters
        /// </summary>
        /// <param name="incomingEncounters"></param>
        /// <param name="existingEncounters"></param>
        /// <returns></returns>
        public List<EncounterEntity> CheckForEncounterListSection(List<EncounterEntity> incomingEncounters, List<EncounterEntity> existingEncounters)
        {
            if (incomingEncounters is not null && existingEncounters is not null)
            {
                List<EncounterEntity> encounters = new();
                incomingEncounters.ForEach(x =>
                {
                    if (existingEncounters.Any(y => y.Uuid == x.Uuid))
                    {
                        x.EncounterContactMode = CheckForCodeSection(x.EncounterContactMode, existingEncounters.Find(y => y.Uuid == x.Uuid).EncounterContactMode);
                        x.EncounterEnvironmentalSetting = CheckForCodeSection(x.EncounterEnvironmentalSetting, existingEncounters.Find(y => y.Uuid == x.Uuid).EncounterEnvironmentalSetting);
                        x.EncounterType = CheckForCodeSection(x.EncounterType, existingEncounters.Find(y => y.Uuid == x.Uuid).EncounterType);
                        if (x.TransitionStartRule is not null)
                            x.TransitionStartRule.Uuid = String.IsNullOrWhiteSpace(x.TransitionStartRule.Uuid) ? IdGenerator.GenerateId() : x.TransitionStartRule.Uuid;
                        if (x.TransitionEndRule is not null)
                            x.TransitionEndRule.Uuid = String.IsNullOrWhiteSpace(x.TransitionEndRule.Uuid) ? IdGenerator.GenerateId() : x.TransitionEndRule.Uuid;

                        encounters.Add(x);
                        existingEncounters.RemoveAll(y => y.Uuid == x.Uuid);
                    }
                    else
                    {
                        x.Uuid = IdGenerator.GenerateId();
                        x.EncounterContactMode?.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                        x.EncounterEnvironmentalSetting?.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                        x.EncounterType?.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                        if (x.TransitionStartRule is not null)
                            x.TransitionStartRule.Uuid = IdGenerator.GenerateId();
                        if (x.TransitionEndRule is not null)
                            x.TransitionEndRule.Uuid = IdGenerator.GenerateId();
                        encounters.Add(x);
                    }
                });
                incomingEncounters = encounters;
            }
            else if (incomingEncounters is not null && existingEncounters is null)
            {
                incomingEncounters.ForEach(x =>
                {
                    x.Uuid = IdGenerator.GenerateId();
                    x.EncounterContactMode?.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                    x.EncounterEnvironmentalSetting?.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                    x.EncounterType?.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                    if (x.TransitionStartRule is not null)
                        x.TransitionStartRule.Uuid = IdGenerator.GenerateId();
                    if (x.TransitionEndRule is not null)
                        x.TransitionEndRule.Uuid = IdGenerator.GenerateId();
                });
            }
            return incomingEncounters;
        }

        /// <summary>
        /// Comparison between existing and incoming Study Elements
        /// </summary>
        /// <param name="incomingStudyElements"></param>
        /// <param name="existingStudyElements"></param>
        /// <returns></returns>
        public List<StudyElementEntity> CheckForStudyElementsSection(List<StudyElementEntity> incomingStudyElements, List<StudyElementEntity> existingStudyElements)
        {
            if (incomingStudyElements is not null && existingStudyElements is not null)
            {
                List<StudyElementEntity> studyElements = new();
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
        /// <summary>
        /// Comparison between existing and incoming Study Data Collections
        /// </summary>
        /// <param name="incomingStudyDataCollections"></param>
        /// <param name="existingStudyDataCollections"></param>
        /// <returns></returns>
        public List<StudyDataCollectionEntity> CheckForStudyDataCollectionSection(List<StudyDataCollectionEntity> incomingStudyDataCollections, List<StudyDataCollectionEntity> existingStudyDataCollections)
        {
            if (incomingStudyDataCollections is not null && existingStudyDataCollections is not null)
            {
                List<StudyDataCollectionEntity> studyDataCollections = new();
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
        /// <summary>
        /// Comparison between existing and incoming Study WorkFlows
        /// </summary>
        /// <param name="incomingWorkflows"></param>
        /// <param name="existingWorkflows"></param>
        /// <returns></returns>
        public List<WorkflowEntity> CheckForStudyWorkflowSection(List<WorkflowEntity> incomingWorkflows, List<WorkflowEntity> existingWorkflows)
        {
            if (incomingWorkflows is not null && existingWorkflows is not null)
            {
                List<WorkflowEntity> workflows = new();
                incomingWorkflows.ForEach(x =>
                {
                    if (existingWorkflows.Any(y => y.Uuid == x.Uuid))
                    {
                        x.WorkflowItems = CheckForStudyWorkflowItemsSection(x.WorkflowItems, existingWorkflows.Find(y => y.Uuid == x.Uuid).WorkflowItems);
                        workflows.Add(x);
                        existingWorkflows.RemoveAll(y => y.Uuid == x.Uuid);
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
        /// <summary>
        /// Comparison between existing and incoming Study WorkFlow Items
        /// </summary>
        /// <param name="incomingWorkflowItems"></param>
        /// <param name="existingWorkflowItems"></param>
        /// <returns></returns>
        public List<WorkFlowItemEntity> CheckForStudyWorkflowItemsSection(List<WorkFlowItemEntity> incomingWorkflowItems, List<WorkFlowItemEntity> existingWorkflowItems)
        {
            if (incomingWorkflowItems is not null && existingWorkflowItems is not null)
            {
                List<WorkFlowItemEntity> workFlowItems = new();
                incomingWorkflowItems.ForEach(x =>
                {
                    if (existingWorkflowItems.Any(y => y.Uuid == x.Uuid))
                    {
                        if (x.WorkflowItemEncounter is not null && existingWorkflowItems.Find(y => y.Uuid == x.Uuid).WorkflowItemEncounter is not null)
                        {
                            if (String.IsNullOrWhiteSpace(x.WorkflowItemEncounter.Uuid))
                            {
                                x.WorkflowItemEncounter.Uuid = IdGenerator.GenerateId();
                                x.WorkflowItemEncounter.EncounterContactMode?.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                                x.WorkflowItemEncounter.EncounterEnvironmentalSetting?.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                                x.WorkflowItemEncounter.EncounterType?.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                                if (x.WorkflowItemEncounter.TransitionStartRule is not null)
                                    x.WorkflowItemEncounter.TransitionStartRule.Uuid = IdGenerator.GenerateId();
                                if (x.WorkflowItemEncounter.TransitionEndRule is not null)
                                    x.WorkflowItemEncounter.TransitionEndRule.Uuid = IdGenerator.GenerateId();
                            }
                            else
                            {
                                x.WorkflowItemEncounter.EncounterContactMode = CheckForCodeSection(x.WorkflowItemEncounter.EncounterContactMode, existingWorkflowItems.Find(y => y.Uuid == x.Uuid).WorkflowItemEncounter.EncounterContactMode);
                                x.WorkflowItemEncounter.EncounterEnvironmentalSetting = CheckForCodeSection(x.WorkflowItemEncounter.EncounterEnvironmentalSetting, existingWorkflowItems.Find(y => y.Uuid == x.Uuid).WorkflowItemEncounter.EncounterEnvironmentalSetting);
                                x.WorkflowItemEncounter.EncounterType = CheckForCodeSection(x.WorkflowItemEncounter.EncounterType, existingWorkflowItems.Find(y => y.Uuid == x.Uuid).WorkflowItemEncounter.EncounterType);
                                if (x.WorkflowItemEncounter.TransitionStartRule is not null)
                                    x.WorkflowItemEncounter.TransitionStartRule.Uuid = String.IsNullOrWhiteSpace(x.WorkflowItemEncounter.TransitionStartRule.Uuid) ? IdGenerator.GenerateId() : x.WorkflowItemEncounter.TransitionStartRule.Uuid;
                                if (x.WorkflowItemEncounter.TransitionEndRule is not null)
                                    x.WorkflowItemEncounter.TransitionEndRule.Uuid = String.IsNullOrWhiteSpace(x.WorkflowItemEncounter.TransitionEndRule.Uuid) ? IdGenerator.GenerateId() : x.WorkflowItemEncounter.TransitionEndRule.Uuid;
                            }
                        }
                        else if (x.WorkflowItemEncounter is not null && existingWorkflowItems.Find(y => y.Uuid == x.Uuid).WorkflowItemEncounter is null)
                        {
                            x.WorkflowItemEncounter.Uuid = IdGenerator.GenerateId();
                            x.WorkflowItemEncounter.EncounterContactMode?.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                            x.WorkflowItemEncounter.EncounterEnvironmentalSetting?.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                            x.WorkflowItemEncounter.EncounterType?.ForEach(x => x.Uuid = IdGenerator.GenerateId());
                            if (x.WorkflowItemEncounter.TransitionStartRule is not null)
                                x.WorkflowItemEncounter.TransitionStartRule.Uuid = IdGenerator.GenerateId();
                            if (x.WorkflowItemEncounter.TransitionEndRule is not null)
                                x.WorkflowItemEncounter.TransitionEndRule.Uuid = IdGenerator.GenerateId();
                        }
                        if (x.WorkflowItemActivity is not null && existingWorkflowItems.Find(y => y.Uuid == x.Uuid).WorkflowItemActivity is not null)
                        {
                            if (String.IsNullOrWhiteSpace(x.WorkflowItemActivity.Uuid))
                            {
                                x.WorkflowItemActivity.Uuid = IdGenerator.GenerateId();
                                if (x.WorkflowItemActivity.DefinedProcedures is not null && x.WorkflowItemActivity.DefinedProcedures.Any())
                                {
                                    x.WorkflowItemActivity.DefinedProcedures.ForEach(y =>
                                    {
                                        y.Uuid = IdGenerator.GenerateId();
                                        y.ProcedureCode?.ForEach(z => z.Uuid = IdGenerator.GenerateId());
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
                                    y.ProcedureCode?.ForEach(z => z.Uuid = IdGenerator.GenerateId());
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
                            if (x.WorkflowItemEncounter.TransitionStartRule is not null)
                                x.WorkflowItemEncounter.TransitionStartRule.Uuid = IdGenerator.GenerateId();
                            if (x.WorkflowItemEncounter.TransitionEndRule is not null)
                                x.WorkflowItemEncounter.TransitionEndRule.Uuid = IdGenerator.GenerateId();
                        }
                        if (x.WorkflowItemActivity is not null)
                        {
                            x.WorkflowItemActivity.Uuid = IdGenerator.GenerateId();
                            if (x.WorkflowItemActivity.DefinedProcedures is not null && x.WorkflowItemActivity.DefinedProcedures.Any())
                            {
                                x.WorkflowItemActivity.DefinedProcedures.ForEach(y =>
                                {
                                    y.Uuid = IdGenerator.GenerateId();
                                    y.ProcedureCode?.ForEach(z => z.Uuid = IdGenerator.GenerateId());
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
                        if (x.WorkflowItemEncounter.TransitionStartRule is not null)
                            x.WorkflowItemEncounter.TransitionStartRule.Uuid = IdGenerator.GenerateId();
                        if (x.WorkflowItemEncounter.TransitionEndRule is not null)
                            x.WorkflowItemEncounter.TransitionEndRule.Uuid = IdGenerator.GenerateId();
                    }
                    if (x.WorkflowItemActivity is not null)
                    {
                        x.WorkflowItemActivity.Uuid = IdGenerator.GenerateId();
                        if (x.WorkflowItemActivity.DefinedProcedures is not null && x.WorkflowItemActivity.DefinedProcedures.Any())
                        {
                            x.WorkflowItemActivity.DefinedProcedures.ForEach(y =>
                            {
                                y.Uuid = IdGenerator.GenerateId();
                                y.ProcedureCode?.ForEach(z => z.Uuid = IdGenerator.GenerateId());
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
        /// <summary>
        /// Comparison between existing and incoming Defined Procedures
        /// </summary>
        /// <param name="incomingDefinedProcedures"></param>
        /// <param name="exisitingDefinedProcedures"></param>
        /// <returns></returns>
        public List<DefinedProcedureEntity> CheckForDefinedProceduresSection(List<DefinedProcedureEntity> incomingDefinedProcedures, List<DefinedProcedureEntity> exisitingDefinedProcedures)
        {
            if (incomingDefinedProcedures is not null && exisitingDefinedProcedures is not null)
            {
                List<DefinedProcedureEntity> definedProcedures = new();
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
        /// <summary>
        /// Comparison between existing and incoming Study Estimands
        /// </summary>
        /// <param name="incomingEstimands"></param>
        /// <param name="existingEstimands"></param>
        /// <returns></returns>
        public List<EstimandEntity> CheckForStudyEstimandSection(List<EstimandEntity> incomingEstimands, List<EstimandEntity> existingEstimands)
        {
            if (incomingEstimands is not null && existingEstimands is not null)
            {
                List<EstimandEntity> estimands = new();
                incomingEstimands.ForEach(x =>
                {
                    if (existingEstimands.Any(y => y.Uuid == x.Uuid))
                    {
                        if (x.Treatment is not null && existingEstimands.Find(y => y.Uuid == x.Uuid).Treatment is not null)
                        {
                            if (String.IsNullOrWhiteSpace(x.Treatment.Uuid))
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
                            if (String.IsNullOrWhiteSpace(x.VariableOfInterest.Uuid))
                            {
                                x.VariableOfInterest.Uuid = IdGenerator.GenerateId();
                                if (x.VariableOfInterest.EndpointLevel is not null && x.VariableOfInterest.EndpointLevel.Any())
                                    x.VariableOfInterest.EndpointLevel.ForEach(z => z.Uuid = IdGenerator.GenerateId());
                            }
                            else
                            {
                                if (x.VariableOfInterest.EndpointLevel is not null && x.VariableOfInterest.EndpointLevel.Any())
                                    x.VariableOfInterest.EndpointLevel.ForEach(z => z.Uuid = String.IsNullOrWhiteSpace(z.Uuid) ? IdGenerator.GenerateId() : z.Uuid);
                            }
                        }
                        else if (x.VariableOfInterest is not null && existingEstimands.Find(y => y.Uuid == x.Uuid).VariableOfInterest is null)
                        {
                            x.VariableOfInterest.Uuid = IdGenerator.GenerateId();
                            if (x.VariableOfInterest.EndpointLevel is not null && x.VariableOfInterest.EndpointLevel.Any())
                                x.VariableOfInterest.EndpointLevel.ForEach(z => z.Uuid = IdGenerator.GenerateId());
                        }
                        x.IntercurrentEvents = CheckForIntercurrentEventsSection(x.IntercurrentEvents, existingEstimands.Find(y => y.Uuid == x.Uuid).IntercurrentEvents);
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
        /// <summary>
        /// Comparison between existing and incoming Intercurrent events
        /// </summary>
        /// <param name="incomingInterCurrentEvents"></param>
        /// <param name="exisitingInterCurrentEvents"></param>
        /// <returns></returns>
        public List<InterCurrentEventEntity> CheckForIntercurrentEventsSection(List<InterCurrentEventEntity> incomingInterCurrentEvents, List<InterCurrentEventEntity> exisitingInterCurrentEvents)
        {
            if (incomingInterCurrentEvents is not null && exisitingInterCurrentEvents is not null)
            {
                List<InterCurrentEventEntity> interCurrentEvents = new();
                incomingInterCurrentEvents.ForEach(x =>
                {
                    if (exisitingInterCurrentEvents.Any(y => y.Uuid == x.Uuid))
                    {
                        interCurrentEvents.Add(x);
                        exisitingInterCurrentEvents.RemoveAll(y => y.Uuid == x.Uuid);
                    }
                    else
                    {
                        x.Uuid = IdGenerator.GenerateId();
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
                });
            }
            return incomingInterCurrentEvents;
        }

        #endregion

    }

}
