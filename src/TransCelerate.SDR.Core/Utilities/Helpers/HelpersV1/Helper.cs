using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.StudyV1;
using TransCelerate.SDR.Core.Entities.StudyV1;
using TransCelerate.SDR.Core.Utilities.Common;
using ObjectsComparer;
using TransCelerate.SDR.Core.Utilities.Helpers;

namespace TransCelerate.SDR.Core.Utilities.Helpers.HelpersV1
{
    /// <summary>
    /// This class is used as a helper for different funtionalities
    /// </summary>
    public class Helper : IHelper
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

        public JsonSerializerSettings GetSerializerSettingsForCamelCasing()
        {
            return new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                Formatting = Formatting.Indented

            };
        }

        #region Partial StudyElements
        /// <summary>
        /// Check whether the the input list of study elements are valid or not
        /// </summary>
        /// <param name="listofelements"></param>
        /// <returns></returns>
        public bool AreValidStudyElements(string listofelements)
        {
            bool isValid = true;
            if (listofelements is not null)
            {
                string[] listofElementsArray = listofelements?.Split(Constants.Roles.Seperator);

                if (listofElementsArray is not null)
                {
                    foreach (string element in listofElementsArray)
                    {
                        if (!Constants.ClinicalStudyElements.Select(x=>x.ToLower()).Contains(element.ToLower()))
                        {
                            isValid = false;
                            break;
                        }
                    }
                }
            }
            return isValid;
        }
        /// <summary>
        /// Check whether the the input list of study design elements are valid or not
        /// </summary>
        /// <param name="listofelements"></param>
        /// <returns></returns>
        public bool AreValidStudyDesignElements(string listofelements)
        {
            bool isValid = true;
            if (listofelements is not null)
            {
                string[] listofElementsArray = listofelements?.Split(Constants.Roles.Seperator);

                if (listofElementsArray is not null)
                {
                    foreach (string element in listofElementsArray)
                    {
                        if (!Constants.StudyDesignElements.Select(x=>x.ToLower()).Contains(element.ToLower()))
                        {
                            isValid = false;
                            break;
                        }
                    }
                }
            }
            return isValid;
        }

        /// <summary>
        /// Remove the study elemets which are not requested
        /// </summary>
        /// <param name="sections"></param>
        /// <param name="studyDTO"></param>
        /// <returns></returns>
        public object RemoveStudyElements(string[] sections, StudyDto studyDTO)
        {
            var serializer = GetSerializerSettingsForCamelCasing();
            var jsonObject = JObject.Parse(JsonConvert.SerializeObject(studyDTO, serializer));
            jsonObject.Property((nameof(StudyEntity.AuditTrail).Substring(0, 1).ToLower() + (nameof(StudyEntity.AuditTrail).Substring(1)))).Remove();
            foreach (var item in Constants.ClinicalStudyElements.Select(x => x.ToLower()))
            {
                sections = sections.Select(t => t.Trim().ToLower()).ToArray();
                if (!sections.Contains(item))
                {
                    if (item == nameof(ClinicalStudyDto.StudyTitle).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (nameof(ClinicalStudyDto.StudyTitle).Substring(0, 1).ToLower() + (nameof(ClinicalStudyDto.StudyTitle).Substring(1)))).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(ClinicalStudyDto.StudyPhase).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (nameof(ClinicalStudyDto.StudyPhase).Substring(0, 1).ToLower() + (nameof(ClinicalStudyDto.StudyPhase).Substring(1)))).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(ClinicalStudyDto.StudyType).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (nameof(ClinicalStudyDto.StudyType).Substring(0, 1).ToLower() + (nameof(ClinicalStudyDto.StudyType).Substring(1)))).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(ClinicalStudyDto.StudyIdentifiers).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (nameof(ClinicalStudyDto.StudyIdentifiers).Substring(0, 1).ToLower() + (nameof(ClinicalStudyDto.StudyIdentifiers).Substring(1)))).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(ClinicalStudyDto.StudyDesigns).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (nameof(ClinicalStudyDto.StudyDesigns).Substring(0, 1).ToLower() + (nameof(ClinicalStudyDto.StudyDesigns).Substring(1)))).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(ClinicalStudyDto.StudyProtocolVersions).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (nameof(ClinicalStudyDto.StudyProtocolVersions).Substring(0, 1).ToLower() + (nameof(ClinicalStudyDto.StudyProtocolVersions).Substring(1)))).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(ClinicalStudyDto.StudyVersion).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (nameof(ClinicalStudyDto.StudyVersion).Substring(0, 1).ToLower() + (nameof(ClinicalStudyDto.StudyVersion).Substring(1)))).ToList().ForEach(x => x.Remove());
                }
            }

            return jsonObject;
        }
        /// <summary>
        /// Remove the study design elemets which are not requested
        /// </summary>
        /// <param name="sections"></param>
        /// <param name="studyDesigns"></param>
        /// <param name="study_uuid"></param>
        /// <returns></returns>
        public object RemoveStudyDesignElements(string[] sections, List<StudyDesignDto> studyDesigns, string study_uuid)
        {
            var serializer = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                Formatting = Formatting.Indented

            };
            var studyDesingsJArray = new JArray();


            foreach (var studyDesign in studyDesigns)
            {
                var jsonObject = JObject.Parse(JsonConvert.SerializeObject(studyDesign, serializer));
                foreach (var item in Constants.StudyDesignElements.Select(x => x.ToLower()))
                {
                    if (sections != null && sections.Any())
                    {
                        sections = sections.Select(t => t.Trim().ToLower()).ToArray();
                        if (!sections.Contains(item))
                        {
                            if (item == nameof(StudyDesignDto.InterventionModel).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (nameof(StudyDesignDto.InterventionModel).Substring(0, 1).ToLower() + (nameof(StudyDesignDto.InterventionModel).Substring(1)))).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.TrialIntentType).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (nameof(StudyDesignDto.TrialIntentType).Substring(0, 1).ToLower() + (nameof(StudyDesignDto.TrialIntentType).Substring(1)))).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.TrialType).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (nameof(StudyDesignDto.TrialType).Substring(0, 1).ToLower() + (nameof(StudyDesignDto.TrialType).Substring(1)))).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.StudyInvestigationalInterventions).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (nameof(StudyDesignDto.StudyInvestigationalInterventions).Substring(0, 1).ToLower() + (nameof(StudyDesignDto.StudyInvestigationalInterventions).Substring(1)))).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.StudyIndications).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (nameof(StudyDesignDto.StudyIndications).Substring(0, 1).ToLower() + (nameof(StudyDesignDto.StudyIndications).Substring(1)))).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.StudyPopulations).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (nameof(StudyDesignDto.StudyPopulations).Substring(0, 1).ToLower() + (nameof(StudyDesignDto.StudyPopulations).Substring(1)))).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.StudyObjectives).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (nameof(StudyDesignDto.StudyObjectives).Substring(0, 1).ToLower() + (nameof(StudyDesignDto.StudyObjectives).Substring(1)))).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.StudyCells).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (nameof(StudyDesignDto.StudyCells).Substring(0, 1).ToLower() + (nameof(StudyDesignDto.StudyCells).Substring(1)))).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.StudyWorkflows).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (nameof(StudyDesignDto.StudyWorkflows).Substring(0, 1).ToLower() + (nameof(StudyDesignDto.StudyWorkflows).Substring(1)))).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.StudyEstimands).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (nameof(StudyDesignDto.StudyEstimands).Substring(0, 1).ToLower() + (nameof(StudyDesignDto.StudyEstimands).Substring(1)))).ToList().ForEach(x => x.Remove());
                        }
                    }
                }
                studyDesingsJArray.Add(jsonObject);
            }

            //JObject studyJobject = new JObject();
            //JObject clinicalStudyJobject = new JObject();
            //clinicalStudyJobject.Add(nameof(ClinicalStudyDto.Uuid).Substring(0, 1).ToLower() + nameof(ClinicalStudyDto.Uuid).Substring(1), study_uuid);
            //clinicalStudyJobject.Add(nameof(ClinicalStudyDto.StudyDesigns).Substring(0, 1).ToLower() + nameof(ClinicalStudyDto.StudyDesigns).Substring(1), studyDesingsJArray);

            //studyJobject.Add(nameof(StudyDto.ClinicalStudy).Substring(0, 1).ToLower() + nameof(StudyDto.ClinicalStudy).Substring(1), clinicalStudyJobject);

            //return studyJobject;
            return studyDesingsJArray;
        }

        #endregion

        #region Generate Id for each sections
        /// <summary>
        /// Generate uuid for Each section of study
        /// </summary>
        /// <param name="study">Study Entity</param>
        /// <returns></returns>
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
                            if(y.EndpointLevel is not null && y.EndpointLevel.Any())
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
                        if(x.Treatment.Codes is not null && x.Treatment.Codes.Any())
                            x.Treatment.Codes.ForEach(y => y.Uuid = IdGenerator.GenerateId());
                    }                       

                    if (x.AnalysisPopulation is not null)
                        x.AnalysisPopulation.Uuid = IdGenerator.GenerateId();

                    if(x.VariableOfInterest is not null)
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
                            if(y.EndpointLevel is not null && y.EndpointLevel.Any())
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
                            x.StudyArm.StudyArmDataOriginType.ForEach(y => y.Uuid =null);
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
                            if(String.IsNullOrWhiteSpace(x.StudyEpoch.Uuid))
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
                List<EncounterEntity> encounters = new List<EncounterEntity>();
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
                List<WorkflowEntity> workflows = new List<WorkflowEntity>();
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
                                    x.WorkflowItemEncounter.TransitionStartRule.Uuid = String.IsNullOrWhiteSpace(x.WorkflowItemEncounter.TransitionStartRule.Uuid) ? IdGenerator.GenerateId(): x.WorkflowItemEncounter.TransitionStartRule.Uuid;
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
                List<InterCurrentEventEntity> interCurrentEvents = new List<InterCurrentEventEntity>();
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

        #region GetDifference For Each Section
        /// <summary>
        /// Get the differences between two studies
        /// </summary>
        /// <param name="currentStudyVersion">Current study version</param>
        /// <param name="previousStudyVersion">Previous study version</param>
        /// <returns></returns>
        public List<string> GetChangedValues(StudyEntity currentStudyVersion, StudyEntity previousStudyVersion)
        {
            var comparer = new ObjectsComparer.Comparer<ClinicalStudyEntity>();
            bool isEqual = comparer.Compare(currentStudyVersion.ClinicalStudy, previousStudyVersion.ClinicalStudy, out var differences);
            List<string> changedValues = new List<string>();

            if (currentStudyVersion.ClinicalStudy.StudyTitle != previousStudyVersion.ClinicalStudy.StudyTitle)
                changedValues.Add($"{nameof(StudyEntity.ClinicalStudy)}.{nameof(ClinicalStudyEntity.StudyTitle)}");
            if (currentStudyVersion.ClinicalStudy.StudyVersion != previousStudyVersion.ClinicalStudy.StudyVersion)
                changedValues.Add($"{nameof(StudyEntity.ClinicalStudy)}.{nameof(ClinicalStudyEntity.StudyVersion)}");
            if (GetDifferences<CodeEntity>(currentStudyVersion.ClinicalStudy.StudyType, previousStudyVersion.ClinicalStudy.StudyType).Any())
                changedValues.Add($"{nameof(StudyEntity.ClinicalStudy)}.{nameof(ClinicalStudyEntity.StudyType)}");
            if (GetDifferences<CodeEntity>(currentStudyVersion.ClinicalStudy.StudyPhase, previousStudyVersion.ClinicalStudy.StudyPhase).Any())
                changedValues.Add($"{nameof(StudyEntity.ClinicalStudy)}.{nameof(ClinicalStudyEntity.StudyPhase)}");

            //StudyIdentifiers
            changedValues.AddRange(GetDifferenceForStudyIdentifiers(currentStudyVersion.ClinicalStudy.StudyIdentifiers, previousStudyVersion.ClinicalStudy.StudyIdentifiers));
            //StudyProtocolVersion
            changedValues.AddRange(GetDifferenceForStudyProtocolVersions(currentStudyVersion.ClinicalStudy.StudyProtocolVersions, previousStudyVersion.ClinicalStudy.StudyProtocolVersions));
            //Study Designs
            changedValues.AddRange(GetDifferenceForStudyDesigns(currentStudyVersion.ClinicalStudy.StudyDesigns, previousStudyVersion.ClinicalStudy.StudyDesigns));


            return changedValues;
        }

        public List<string> GetDifferences<T>(T currentVersion, T previousVersion)
        {
            var comparer = new ObjectsComparer.Comparer<T>();
            bool isEqual = comparer.Compare(currentVersion, previousVersion, out var differences);
            return differences.Select(x => x.MemberPath).ToList();
        }

        public List<string> GetDifferenceForAList<T>(List<T> currentVersion, List<T> previousVersion) where T : class, IUuid
        {
            List<string> changedValues = new List<string>();
            if (currentVersion != null && currentVersion.Any())
            {
                currentVersion.ForEach(currentItem =>
                {                    
                    if (previousVersion != null && previousVersion.Any(x => x.Uuid == currentItem.Uuid))
                    {
                        changedValues.AddRange(GetDifferences<T>(currentItem, previousVersion.Find(x => x.Uuid == currentItem.Uuid)));
                        if(currentVersion.IndexOf(currentItem) != previousVersion.IndexOf(previousVersion.Find(x => x.Uuid == currentItem.Uuid)))
                            changedValues.Add(nameof(T));
                    }
                    else if(previousVersion != null && currentVersion?.Count == previousVersion?.Count && !previousVersion.Any(x => x.Uuid == currentItem.Uuid))
                    {
                        changedValues.Add(nameof(T));
                    }                    
                });
            }
            else if ((currentVersion is null && previousVersion is not null) || (currentVersion is not null && previousVersion is null))
                changedValues.Add(nameof(T));
            if(currentVersion?.Count != previousVersion?.Count)
                changedValues.Add(nameof(T));
            return changedValues;
        }
        public List<string> CheckForNumberOfElementsMismatch<T>(List<T> currentVersion, List<T> previousVersion) where T : class, IUuid
        {
            var differences = CheckDifferences<List<T>>(currentVersion, previousVersion);
            if (differences.Any(x => x.DifferenceType == DifferenceTypes.NumberOfElementsMismatch))
                return new List<string>();
            else
                return differences.Select(x => x.MemberPath).ToList();
        }
        public List<Difference> CheckDifferences<T>(T currentVersion, T previousVersion)
        {
            var comparer = new ObjectsComparer.Comparer<T>();
            bool isEqual = comparer.Compare(currentVersion, previousVersion, out var differences);
            return differences.ToList();
        }

        public List<string> GetDifferenceForStudyIdentifiers(List<StudyIdentifierEntity> currentVersion, List<StudyIdentifierEntity> previousVersion)
        {            
            var tempList = new List<string>();
            if ((currentVersion is null && previousVersion is not null) || (currentVersion is not null && previousVersion is null))
                tempList.Add($"{nameof(StudyEntity.ClinicalStudy)}.{nameof(ClinicalStudyEntity.StudyIdentifiers)}");
            if (currentVersion?.Count != previousVersion?.Count)
                if (currentVersion?.Count != previousVersion?.Count)
                tempList.Add($"{nameof(StudyEntity.ClinicalStudy)}.{nameof(ClinicalStudyEntity.StudyIdentifiers)}");
            GetDifferenceForAList<StudyIdentifierEntity>(currentVersion, previousVersion).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyEntity.ClinicalStudy)}.{nameof(ClinicalStudyEntity.StudyIdentifiers)}.{x}");
            });
            return tempList;
        }

        public List<string> GetDifferenceForStudyProtocolVersions(List<StudyProtocolVersionEntity> currentVersion, List<StudyProtocolVersionEntity> previousVersion)
        {
            var tempList = new List<string>();
            if((currentVersion is null && previousVersion is not null) || (currentVersion is not null && previousVersion is null))
                tempList.Add($"{nameof(StudyEntity.ClinicalStudy)}.{nameof(ClinicalStudyEntity.StudyProtocolVersions)}");
            if (currentVersion?.Count != previousVersion?.Count)
                tempList.Add($"{nameof(StudyEntity.ClinicalStudy)}.{nameof(ClinicalStudyEntity.StudyProtocolVersions)}");
            GetDifferenceForAList<StudyProtocolVersionEntity>(currentVersion, previousVersion).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyEntity.ClinicalStudy)}.{nameof(ClinicalStudyEntity.StudyProtocolVersions)}.{x}");
            });
            return tempList;
        }

        public List<string> GetDifferenceForStudyDesigns(List<StudyDesignEntity> currentVersion, List<StudyDesignEntity> previousVersion)
        {
            List<string> changedValues = new List<string>();
            List<string> formattedChangedValues = new List<string>();

            if ((currentVersion is null && previousVersion is not null) || (currentVersion is not null && previousVersion is null))
                formattedChangedValues.Add($"{nameof(StudyEntity.ClinicalStudy)}.{nameof(ClinicalStudyEntity.StudyDesigns)}");
            if (currentVersion?.Count != previousVersion?.Count)
                formattedChangedValues.Add($"{nameof(StudyEntity.ClinicalStudy)}.{nameof(ClinicalStudyEntity.StudyDesigns)}");
            changedValues.AddRange(GetDifferenceForEachStudyDesigns(currentVersion, previousVersion));

            if (changedValues.Any())
            {
                changedValues.ForEach(x =>
                {
                    string addRootPath = $"{nameof(StudyEntity.ClinicalStudy)}.{nameof(ClinicalStudyEntity.StudyDesigns)}.{x}";
                    formattedChangedValues.Add(addRootPath);
                });
            }
            return formattedChangedValues;
        }

        public List<string> GetDifferenceForEachStudyDesigns(List<StudyDesignEntity> currentVersion, List<StudyDesignEntity> previousVersion)
        {
            List<string> changedValues = new List<string>();

            if (currentVersion != null && currentVersion.Any())
            {
                currentVersion.ForEach(currentStudyDesign =>
                {
                    if (previousVersion != null && previousVersion.Any(x => x.Uuid == currentStudyDesign.Uuid))
                    {
                        var previousStudyDesign = previousVersion.Find(x => x.Uuid == currentStudyDesign.Uuid);

                        //Intervention Model
                        if (GetDifferences<List<CodeEntity>>(currentStudyDesign.InterventionModel, previousStudyDesign.InterventionModel).Any())
                            changedValues.Add($"{nameof(StudyDesignEntity.InterventionModel)}");

                        //Trial Type
                        if (GetDifferences<List<CodeEntity>>(currentStudyDesign.TrialType, previousStudyDesign.TrialType).Any())
                            changedValues.Add($"{nameof(StudyDesignEntity.TrialType)}");

                        //Trial Intent Type
                        if (GetDifferences<List<CodeEntity>>(currentStudyDesign.TrialIntentType, previousStudyDesign.TrialIntentType).Any())
                            changedValues.Add($"{nameof(StudyDesignEntity.TrialIntentType)}");

                        //StudyIndications                                                
                        changedValues.AddRange(GetDifferenceForStudyIndications(currentStudyDesign, previousStudyDesign));

                        //Investigational Intervention                        
                        changedValues.AddRange(GetDifferenceForStudyInvestigationalIntervention(currentStudyDesign, previousStudyDesign));

                        //Study Populations                        
                        changedValues.AddRange(GetDifferenceForStudyPopulations(currentStudyDesign, previousStudyDesign));
                        //Study Objectives                        
                        changedValues.AddRange(GetDifferenceForStudyObjectives(currentStudyDesign, previousStudyDesign));

                        //Estimands                        
                        changedValues.AddRange(GetDifferenceForStudyEstimands(currentStudyDesign, previousStudyDesign));

                        //Study Cells                        
                        changedValues.AddRange(GetDifferenceForStudyCells(currentStudyDesign, previousStudyDesign));

                        //Workflows                        
                        changedValues.AddRange(GetDifferenceForStudyWorkFlows(currentStudyDesign, previousStudyDesign));
                    }

                    else if (currentVersion?.Count == previousVersion?.Count && previousVersion != null && !previousVersion.Any(x => x.Uuid == currentStudyDesign.Uuid))
                    {
                        changedValues.Add("T");
                    }
                });
            }

            return changedValues;
        }

        public List<string> GetDifferenceForStudyIndications(StudyDesignEntity currentStudyDesign, StudyDesignEntity previousStudyDesign)
        {
            var tempList = new List<string>();
            if (currentStudyDesign.StudyIndications?.Count != previousStudyDesign.StudyIndications?.Count)
                tempList.Add($"{nameof(StudyDesignEntity.StudyIndications)}");
            GetDifferenceForAList<IndicationEntity>(currentStudyDesign.StudyIndications, previousStudyDesign.StudyIndications).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDesignEntity.StudyIndications)}.{x}");
            });
            return tempList;
        }

        public List<string> GetDifferenceForStudyObjectives(StudyDesignEntity currentStudyDesign, StudyDesignEntity previousStudyDesign)
        {
            var tempList = new List<string>();
            if (currentStudyDesign.StudyObjectives?.Count != previousStudyDesign.StudyObjectives?.Count)
                tempList.Add($"{nameof(StudyDesignEntity.StudyObjectives)}");
            GetDifferenceForAList<ObjectiveEntity>(currentStudyDesign.StudyObjectives, previousStudyDesign.StudyObjectives).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDesignEntity.StudyObjectives)}.{x}");
            });
            tempList.RemoveAll(x => x.Contains($"{nameof(ObjectiveEntity.ObjectiveEndpoints)}"));
            currentStudyDesign.StudyObjectives?.ForEach(currentObjective =>
            {
                if (previousStudyDesign.StudyObjectives != null && previousStudyDesign.StudyObjectives.Any(x => x.Uuid == currentObjective.Uuid))
                {
                    var previousObjective = previousStudyDesign.StudyObjectives.Find(x => x.Uuid == currentObjective.Uuid);

                    GetDifferenceForAList<EndpointEntity>(currentObjective.ObjectiveEndpoints, previousObjective.ObjectiveEndpoints).ForEach(x =>
                    {
                        tempList.Add($"{nameof(StudyDesignEntity.StudyObjectives)}.{nameof(ObjectiveEntity.ObjectiveEndpoints)}.{x}");
                    });
                }
            });
            return tempList;
        }

        public List<string> GetDifferenceForStudyInvestigationalIntervention(StudyDesignEntity currentStudyDesign, StudyDesignEntity previousStudyDesign)
        {
            var tempList = new List<string>();
            if (currentStudyDesign.StudyInvestigationalInterventions?.Count != previousStudyDesign.StudyInvestigationalInterventions?.Count)
                tempList.Add($"{nameof(StudyDesignEntity.StudyInvestigationalInterventions)}");
            GetDifferenceForAList<InvestigationalInterventionEntity>(currentStudyDesign.StudyInvestigationalInterventions, previousStudyDesign.StudyInvestigationalInterventions).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDesignEntity.StudyInvestigationalInterventions)}.{x}");
            });
            return tempList;
        }

        public List<string> GetDifferenceForStudyPopulations(StudyDesignEntity currentStudyDesign, StudyDesignEntity previousStudyDesign)
        {
            var tempList = new List<string>();
            if (currentStudyDesign.StudyPopulations?.Count != previousStudyDesign.StudyPopulations?.Count)
                tempList.Add($"{nameof(StudyDesignEntity.StudyPopulations)}");
            GetDifferenceForAList<StudyDesignPopulationEntity>(currentStudyDesign.StudyPopulations, previousStudyDesign.StudyPopulations).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDesignEntity.StudyPopulations)}.{x}");
            });
            return tempList;
        }

        public List<string> GetDifferenceForStudyEstimands(StudyDesignEntity currentStudyDesign, StudyDesignEntity previousStudyDesign)
        {
            var tempList = new List<string>();
            if (currentStudyDesign.StudyEstimands?.Count != previousStudyDesign.StudyEstimands?.Count)
                tempList.Add($"{nameof(StudyDesignEntity.StudyEstimands)}");
            GetDifferenceForAList<EstimandEntity>(currentStudyDesign.StudyEstimands, previousStudyDesign.StudyEstimands).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDesignEntity.StudyEstimands)}.{x}");
            });
            tempList.RemoveAll(x => x.Contains($"{nameof(EstimandEntity.IntercurrentEvents)}"));
            currentStudyDesign.StudyEstimands?.ForEach(currentEstimand =>
            {
                if (previousStudyDesign.StudyEstimands != null && previousStudyDesign.StudyEstimands.Any(x => x.Uuid == currentEstimand.Uuid))
                {
                    var previousEstimand = previousStudyDesign.StudyEstimands.Find(x => x.Uuid == currentEstimand.Uuid);
                    GetDifferenceForAList<InterCurrentEventEntity>(currentEstimand.IntercurrentEvents, previousEstimand.IntercurrentEvents).ForEach(x =>
                    {
                        tempList.Add($"{nameof(StudyDesignEntity.StudyEstimands)}.{nameof(EstimandEntity.IntercurrentEvents)}.{x}");
                    });
                }
            });
            return tempList;
        }

        public List<string> GetDifferenceForStudyCells(StudyDesignEntity currentStudyDesign, StudyDesignEntity previousStudyDesign)
        {
            var tempList = new List<string>();
            if (currentStudyDesign.StudyCells?.Count != previousStudyDesign.StudyCells?.Count)
                tempList.Add($"{nameof(StudyDesignEntity.StudyCells)}");
            GetDifferenceForAList<StudyCellEntity>(currentStudyDesign.StudyCells, previousStudyDesign.StudyCells).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDesignEntity.StudyCells)}.{x}");
            });
            tempList.RemoveAll(x => x.Contains($"{nameof(StudyCellEntity.StudyElements)}"));
            tempList.RemoveAll(x => x.Contains($"{nameof(StudyEpochEntity.Encounters)}"));
            currentStudyDesign.StudyCells?.ForEach(currentStudyCell =>
            {
                if (previousStudyDesign.StudyCells != null && previousStudyDesign.StudyCells.Any(x => x.Uuid == currentStudyCell.Uuid))
                {
                    var previousStudyCell = previousStudyDesign.StudyCells.Find(x => x.Uuid == currentStudyCell.Uuid);
                    
                    GetDifferenceForAList<StudyElementEntity>(currentStudyCell.StudyElements, previousStudyCell.StudyElements).ForEach(x =>
                    {
                        tempList.Add($"{nameof(StudyDesignEntity.StudyCells)}.{nameof(StudyCellEntity.StudyElements)}.{x}");
                    });

                    if (currentStudyCell.StudyEpoch is not null && previousStudyCell.StudyEpoch is not null)
                    {
                        GetDifferenceForAList<EncounterEntity>(currentStudyCell.StudyEpoch?.Encounters, previousStudyCell.StudyEpoch?.Encounters).ForEach(x =>
                        {
                            tempList.Add($"{nameof(StudyDesignEntity.StudyCells)}.{nameof(StudyCellEntity.StudyEpoch)}.{nameof(StudyEpochEntity.Encounters)}.{x}");
                        });
                    }

                }
            });
            return tempList;
        }

        public List<string> GetDifferenceForStudyWorkFlows(StudyDesignEntity currentStudyDesign, StudyDesignEntity previousStudyDesign)
        {
            var tempList = new List<string>();
            if (currentStudyDesign.StudyWorkflows?.Count != previousStudyDesign.StudyWorkflows?.Count)
                tempList.Add($"{nameof(StudyDesignEntity.StudyWorkflows)}");
            GetDifferenceForAList<WorkflowEntity>(currentStudyDesign.StudyWorkflows, previousStudyDesign.StudyWorkflows).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDesignEntity.StudyWorkflows)}.{x}");
            });
            tempList.RemoveAll(x => x.Contains($"{nameof(ActivityEntity.DefinedProcedures)}"));
            tempList.RemoveAll(x => x.Contains($"{nameof(ActivityEntity.StudyDataCollection)}"));
            tempList.RemoveAll(x => x.Contains($"{nameof(WorkflowEntity.WorkflowItems)}"));
            currentStudyDesign.StudyWorkflows?.ForEach(currentStudyWorkflows =>
            {
                var workFlowItemChangeList = new List<string>();
                if (previousStudyDesign.StudyWorkflows != null && previousStudyDesign.StudyWorkflows.Any(x => x.Uuid == currentStudyWorkflows.Uuid))
                {
                    var previousStudyWorkflows = previousStudyDesign.StudyWorkflows.Find(x => x.Uuid == currentStudyWorkflows.Uuid);
                    GetDifferenceForAList<WorkFlowItemEntity>(currentStudyWorkflows.WorkflowItems, previousStudyWorkflows.WorkflowItems).ForEach(x =>
                    {
                        workFlowItemChangeList.Add($"{nameof(StudyDesignEntity.StudyWorkflows)}.{nameof(WorkflowEntity.WorkflowItems)}.{x}");
                    });
                    if (currentStudyWorkflows.WorkflowItems != null && previousStudyWorkflows.WorkflowItems != null)
                    {                        
                        workFlowItemChangeList.RemoveAll(x => x.Contains($"{nameof(ActivityEntity.DefinedProcedures)}"));
                        workFlowItemChangeList.RemoveAll(x => x.Contains($"{nameof(ActivityEntity.StudyDataCollection)}"));
                        currentStudyWorkflows?.WorkflowItems?.ForEach(currentWorkflowItem =>
                        {
                            if (previousStudyWorkflows.WorkflowItems != null && previousStudyWorkflows.WorkflowItems.Any(x => x.Uuid == currentWorkflowItem.Uuid))
                            {
                                var previousWorkflowItem = previousStudyWorkflows.WorkflowItems.Find(x => x.Uuid == currentWorkflowItem.Uuid);

                                if(currentWorkflowItem.WorkflowItemActivity is not null && previousWorkflowItem.WorkflowItemActivity is not null)
                                {
                                    GetDifferenceForAList<DefinedProcedureEntity>(currentWorkflowItem.WorkflowItemActivity?.DefinedProcedures, previousWorkflowItem.WorkflowItemActivity?.DefinedProcedures).ForEach(x =>
                                    {
                                        workFlowItemChangeList.Add($"{nameof(StudyDesignEntity.StudyWorkflows)}.{nameof(WorkflowEntity.WorkflowItems)}.{nameof(WorkFlowItemEntity.WorkflowItemActivity)}.{nameof(ActivityEntity.DefinedProcedures)}.{x}");
                                    });
                                    GetDifferenceForAList<StudyDataCollectionEntity>(currentWorkflowItem.WorkflowItemActivity?.StudyDataCollection, previousWorkflowItem.WorkflowItemActivity?.StudyDataCollection).ForEach(x =>
                                    {
                                        workFlowItemChangeList.Add($"{nameof(StudyDesignEntity.StudyWorkflows)}.{nameof(WorkflowEntity.WorkflowItems)}.{nameof(WorkFlowItemEntity.WorkflowItemActivity)}.{nameof(ActivityEntity.StudyDataCollection)}.{x}");
                                    });
                                }
                            }
                        });
                    }
                }
                tempList.AddRange(workFlowItemChangeList);
            });
            return tempList;
        }
        #endregion
    }

}
