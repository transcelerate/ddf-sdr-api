using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using ObjectsComparer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.Entities.StudyV2;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2
{
    /// <summary>
    /// This class is used as a helper for different funtionalities
    /// </summary>
    public class HelperV2 : IHelperV2
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
                UsdmVersion = Constants.USDMVersions.V1_9
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
        /// <param name="listofElementsArray"></param>
        /// <returns></returns>
        public bool AreValidStudyElements(string listofelements, out string[] listofElementsArray)
        {
            bool isValid = true;
            listofElementsArray = listofelements?.Split(Constants.Roles.Seperator);
            if (listofelements is not null)
            {
                if (listofElementsArray is not null)
                {
                    foreach (string element in listofElementsArray)
                    {
                        if (!Constants.StudyElementsV2.Select(x => x.ToLower()).Contains(element.ToLower()))
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
        /// <param name="listofElementsArray"></param>
        /// <returns></returns>
        public bool AreValidStudyDesignElements(string listofelements, out string[] listofElementsArray)
        {
            bool isValid = true;
            listofElementsArray = listofelements?.Split(Constants.Roles.Seperator);
            if (listofelements is not null)
            {
                if (listofElementsArray is not null)
                {
                    foreach (string element in listofElementsArray)
                    {
                        if (!Constants.StudyDesignElementsV2.Select(x => x.ToLower()).Contains(element.ToLower()))
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
        public object RemoveStudyElements(string[] sections, StudyDefinitionsDto studyDTO)
        {
            var serializer = GetSerializerSettingsForCamelCasing();
            var jsonObject = JObject.Parse(JsonConvert.SerializeObject(studyDTO, serializer));
            jsonObject.Property(string.Concat(nameof(StudyDefinitionsEntity.AuditTrail)[..1].ToLower(), nameof(StudyDefinitionsEntity.AuditTrail).AsSpan(1))).Remove();
            jsonObject.Property("links").Remove();
            foreach (var item in Constants.StudyElementsV2.Select(x => x.ToLower()))
            {
                sections = sections.Select(t => t.Trim().ToLower()).ToArray();
                if (!sections.Contains(item))
                {
                    if (item == nameof(StudyDto.StudyTitle).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (string.Concat(nameof(StudyDto.StudyTitle)[..1].ToLower(), nameof(StudyDto.StudyTitle).AsSpan(1)))).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyDto.StudyPhase).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (string.Concat(nameof(StudyDto.StudyPhase)[..1].ToLower(), nameof(StudyDto.StudyPhase).AsSpan(1)))).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyDto.StudyType).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (string.Concat(nameof(StudyDto.StudyType)[..1].ToLower(), nameof(StudyDto.StudyType).AsSpan(1)))).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyDto.StudyIdentifiers).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (string.Concat(nameof(StudyDto.StudyIdentifiers)[..1].ToLower(), nameof(StudyDto.StudyIdentifiers).AsSpan(1)))).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyDto.StudyDesigns).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (string.Concat(nameof(StudyDto.StudyDesigns)[..1].ToLower(), nameof(StudyDto.StudyDesigns).AsSpan(1)))).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyDto.StudyProtocolVersions).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (string.Concat(nameof(StudyDto.StudyProtocolVersions)[..1].ToLower(), nameof(StudyDto.StudyProtocolVersions).AsSpan(1)))).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyDto.StudyVersion).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (string.Concat(nameof(StudyDto.StudyVersion)[..1].ToLower(), nameof(StudyDto.StudyVersion).AsSpan(1)))).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyDto.BusinessTherapeuticAreas).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (string.Concat(nameof(StudyDto.BusinessTherapeuticAreas)[..1].ToLower(), nameof(StudyDto.BusinessTherapeuticAreas).AsSpan(1)))).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyDto.StudyAcronym).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (string.Concat(nameof(StudyDto.StudyAcronym)[..1].ToLower(), nameof(StudyDto.StudyAcronym).AsSpan(1)))).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyDto.StudyRationale).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (string.Concat(nameof(StudyDto.StudyRationale)[..1].ToLower(), nameof(StudyDto.StudyRationale).AsSpan(1)))).ToList().ForEach(x => x.Remove());
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
            var serializer = GetSerializerSettingsForCamelCasing();
            var studyDesingsJArray = new JArray();


            foreach (var studyDesign in studyDesigns)
            {
                var jsonObject = JObject.Parse(JsonConvert.SerializeObject(studyDesign, serializer));
                foreach (var item in Constants.StudyDesignElementsV2.Select(x => x.ToLower()))
                {
                    if (sections != null && sections.Any())
                    {
                        sections = sections.Select(t => t.Trim().ToLower()).ToArray();
                        if (!sections.Contains(item))
                        {
                            if (item == nameof(StudyDesignDto.InterventionModel).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (string.Concat(nameof(StudyDesignDto.InterventionModel)[..1].ToLower(), nameof(StudyDesignDto.InterventionModel).AsSpan(1)))).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.TrialIntentType).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (string.Concat(nameof(StudyDesignDto.TrialIntentType)[..1].ToLower(), nameof(StudyDesignDto.TrialIntentType).AsSpan(1)))).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.TrialType).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (string.Concat(nameof(StudyDesignDto.TrialType)[..1].ToLower(), nameof(StudyDesignDto.TrialType).AsSpan(1)))).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.StudyInvestigationalInterventions).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (string.Concat(nameof(StudyDesignDto.StudyInvestigationalInterventions)[..1].ToLower(), nameof(StudyDesignDto.StudyInvestigationalInterventions).AsSpan(1)))).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.StudyIndications).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (string.Concat(nameof(StudyDesignDto.StudyIndications)[..1].ToLower(), nameof(StudyDesignDto.StudyIndications).AsSpan(1)))).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.StudyPopulations).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (string.Concat(nameof(StudyDesignDto.StudyPopulations)[..1].ToLower(), nameof(StudyDesignDto.StudyPopulations).AsSpan(1)))).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.StudyObjectives).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (string.Concat(nameof(StudyDesignDto.StudyObjectives)[..1].ToLower(), nameof(StudyDesignDto.StudyObjectives).AsSpan(1)))).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.StudyCells).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (string.Concat(nameof(StudyDesignDto.StudyCells)[..1].ToLower(), nameof(StudyDesignDto.StudyCells).AsSpan(1)))).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.StudyScheduleTimelines).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (string.Concat(nameof(StudyDesignDto.StudyScheduleTimelines)[..1].ToLower(), nameof(StudyDesignDto.StudyScheduleTimelines).AsSpan(1)))).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.StudyEstimands).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (string.Concat(nameof(StudyDesignDto.StudyEstimands)[..1].ToLower(), nameof(StudyDesignDto.StudyEstimands).AsSpan(1)))).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.StudyDesignName).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (string.Concat(nameof(StudyDesignDto.StudyDesignName)[..1].ToLower(), nameof(StudyDesignDto.StudyDesignName).AsSpan(1)))).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.StudyDesignDescription).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (string.Concat(nameof(StudyDesignDto.StudyDesignDescription)[..1].ToLower(), nameof(StudyDesignDto.StudyDesignDescription).AsSpan(1)))).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.TherapeuticAreas).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (string.Concat(nameof(StudyDesignDto.TherapeuticAreas)[..1].ToLower(), nameof(StudyDesignDto.TherapeuticAreas).AsSpan(1)))).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.Activities).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (string.Concat(nameof(StudyDesignDto.Activities)[..1].ToLower(), nameof(StudyDesignDto.Activities).AsSpan(1)))).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.Encounters).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (string.Concat(nameof(StudyDesignDto.Encounters)[..1].ToLower(), nameof(StudyDesignDto.Encounters).AsSpan(1)))).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.StudyDesignRationale).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (string.Concat(nameof(StudyDesignDto.StudyDesignRationale)[..1].ToLower(), nameof(StudyDesignDto.StudyDesignRationale).AsSpan(1)))).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.StudyDesignBlindingScheme).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (string.Concat(nameof(StudyDesignDto.StudyDesignBlindingScheme)[..1].ToLower(), nameof(StudyDesignDto.StudyDesignBlindingScheme).AsSpan(1)))).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.BiomedicalConcepts).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (string.Concat(nameof(StudyDesignDto.BiomedicalConcepts)[..1].ToLower(), nameof(StudyDesignDto.BiomedicalConcepts).AsSpan(1)))).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.BcCategories).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (string.Concat(nameof(StudyDesignDto.BcCategories)[..1].ToLower(), nameof(StudyDesignDto.BcCategories).AsSpan(1)))).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.BcSurrogates).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == (string.Concat(nameof(StudyDesignDto.BcSurrogates)[..1].ToLower(), nameof(StudyDesignDto.BcSurrogates).AsSpan(1)))).ToList().ForEach(x => x.Remove());
                        }
                    }
                }
                studyDesingsJArray.Add(jsonObject);
            }

            return studyDesingsJArray;
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
            study.Study.StudyId = null;

            if (study.Study.StudyType is not null)
                study.Study.StudyType.Id = null;

            study.Study.StudyIdentifiers = RemoveIdForStudyIdentifier(study.Study.StudyIdentifiers);

            study.Study.StudyPhase = RemoveIdForAliasCode(study.Study.StudyPhase);


            if (study.Study.BusinessTherapeuticAreas is not null && study.Study.BusinessTherapeuticAreas.Any())
            {
                study.Study.BusinessTherapeuticAreas.ForEach(x => x.Id = null);
            }

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
                    x.Id = null;
                    if (x.StudyIdentifierScope is not null)
                    {
                        x.StudyIdentifierScope.Id = null;
                        if (x.StudyIdentifierScope.OrganisationType is not null)
                            x.StudyIdentifierScope.OrganisationType.Id = null;
                        if (x.StudyIdentifierScope.OrganizationLegalAddress is not null && x.StudyIdentifierScope.OrganizationLegalAddress.Country is not null)
                            x.StudyIdentifierScope.OrganizationLegalAddress.Country.Id = null;
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
                    x.Id = null;
                    if (x.ProtocolStatus is not null)
                    {
                        x.ProtocolStatus.Id = null;
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
                    x.Id = null;
                    if (x.InterventionModel is not null)
                        x.InterventionModel.Id = null;

                    if (x.TrialIntentType is not null && x.TrialIntentType.Any())
                        x.TrialIntentType.ForEach(x => x.Id = null);

                    if (x.TrialType is not null && x.TrialType.Any())
                        x.TrialType.ForEach(x => x.Id = null);

                    if (x.StudyPopulations is not null && x.StudyPopulations.Any())
                    {
                        x.StudyPopulations.ForEach(x =>
                        {
                            x.Id = null;
                            if (x.PlannedSexOfParticipants is not null && x.PlannedSexOfParticipants.Any())
                                x.PlannedSexOfParticipants.ForEach(y => y.Id = null);
                        });
                    }

                    if (x.StudyIndications is not null && x.StudyIndications.Any())
                        x.StudyIndications = RemoveIdForStudyIndications(x.StudyIndications);

                    if (x.StudyInvestigationalInterventions is not null && x.StudyInvestigationalInterventions.Any())
                        x.StudyInvestigationalInterventions = RemoveIdForInvestigationalInterventions(x.StudyInvestigationalInterventions);

                    if (x.StudyObjectives is not null && x.StudyObjectives.Any())
                        x.StudyObjectives = RemoveIdForStudyObjectives(x.StudyObjectives);

                    if (x.StudyCells is not null && x.StudyCells.Any())
                        x.StudyCells = RemoveIdForStudyCells(x.StudyCells);

                    //if (x.StudyWorkflows is not null && x.StudyWorkflows.Any())
                    //    x.StudyWorkflows = RemoveIdForStudyWorkflow(x.StudyWorkflows);
                    if (x.StudyScheduleTimelines is not null && x.StudyScheduleTimelines.Any())
                        x.StudyScheduleTimelines = RemoveIdForScheduleTimelines(x.StudyScheduleTimelines);

                    if (x.StudyEstimands is not null && x.StudyEstimands.Any())
                        x.StudyEstimands = RemoveIdForStudyEstimand(x.StudyEstimands);

                    if (x.Encounters is not null && x.Encounters.Any())
                        x.Encounters = RemoveIdForEncounters(x.Encounters);

                    if (x.Activities is not null && x.Activities.Any())
                        x.Activities = RemoveIdForActivities(x.Activities);

                    if (x.TherapeuticAreas is not null && x.TherapeuticAreas.Any())
                        x.TherapeuticAreas.ForEach(x => x.Id = null);

                    if (x.BiomedicalConcepts is not null && x.BiomedicalConcepts.Any())
                        x.BiomedicalConcepts = RemoveIdForBioMedicalConcepts(x.BiomedicalConcepts);

                    if (x.BcCategories is not null && x.BcCategories.Any())
                        x.BcCategories.ForEach(y => y.Id = null);

                    if (x.BcSurrogates is not null && x.BcSurrogates.Any())
                        x.BcSurrogates.ForEach(y => y.Id = null);

                    x.StudyDesignBlindingScheme = RemoveIdForAliasCode(x.StudyDesignBlindingScheme);
                });
            }
            return studyDesigns;
        }
        /// <summary>
        /// Remove uuid for Biomedical Concepts
        /// </summary>
        /// <param name="biomedicalConcepts"></param>
        /// <returns></returns>
        public static List<BiomedicalConceptEntity> RemoveIdForBioMedicalConcepts(List<BiomedicalConceptEntity> biomedicalConcepts)
        {
            if (biomedicalConcepts is not null && biomedicalConcepts.Any())
            {
                biomedicalConcepts.ForEach(x =>
                {
                    x.Id = null;
                    if (x.BcConceptCode is not null)
                    {
                        x.BcConceptCode.Id = null;
                        if (x.BcConceptCode.StandardCode is not null)
                            x.BcConceptCode.StandardCode.Id = null;
                        x.BcConceptCode.StandardCodeAliases?.ForEach(y => y.Id = null);
                    }
                    if (x.BcProperties is not null && x.BcProperties.Any())
                    {
                        x.BcProperties.ForEach(y =>
                        {
                            y.Id = null;
                            y.BcPropertyConceptCode = RemoveIdForAliasCode(y.BcPropertyConceptCode);

                            if (y.BcPropertyResponseCodes is not null && y.BcPropertyResponseCodes.Any())
                            {
                                y.BcPropertyResponseCodes.ForEach(z =>
                                {
                                    z.Id = null;
                                    if (z.Code is not null)
                                        z.Code.Id = null;
                                });
                            }
                        });
                    }
                });
            }
            return biomedicalConcepts;
        }
        /// <summary>
        /// Remove uuid for AliasCode
        /// </summary>
        /// <param name="aliasCode"></param>
        /// <returns></returns>
        public static AliasCodeEntity RemoveIdForAliasCode(AliasCodeEntity aliasCode)
        {
            if (aliasCode is not null)
            {
                aliasCode.Id = null;
                if (aliasCode.StandardCode is not null)
                    aliasCode.StandardCode.Id = null;
                aliasCode.StandardCodeAliases?.ForEach(z => z.Id = null);
            }
            return aliasCode;
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
                    x.Id = null;
                    if (x.Codes is not null && x.Codes.Any())
                    {
                        x.Codes.ForEach(y => y.Id = null);
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
                    x.Id = null;
                    if (x.Codes is not null && x.Codes.Any())
                    {
                        x.Codes.ForEach(y => y.Id = null);
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
                    x.Id = null;
                    if (x.ObjectiveLevel is not null)
                    {
                        x.ObjectiveLevel.Id = null;
                    }
                    x.ObjectiveEndpoints?.ForEach(y => 
                    { 
                        y.Id = null; 
                        if (y.EndpointLevel is not null) 
                        { 
                            y.EndpointLevel.Id = null; 
                        } 
                    });
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
                    x.Id = null;
                    if (x.StudyArm is not null)
                    {
                        x.StudyArm.Id = null;
                        if (x.StudyArm.StudyArmDataOriginType is not null)
                            x.StudyArm.StudyArmDataOriginType.Id = null;
                        if (x.StudyArm.StudyArmType is not null)
                            x.StudyArm.StudyArmType.Id = null;
                    }
                    if (x.StudyEpoch is not null)
                    {
                        x.StudyEpoch.Id = null;
                        if (x.StudyEpoch.StudyEpochType is not null)
                            x.StudyEpoch.StudyEpochType.Id = null;
                    }
                    if (x.StudyElements is not null && x.StudyElements.Any())
                    {
                        x.StudyElements.ForEach(y =>
                        {
                            y.Id = null;
                            if (y.TransitionEndRule is not null)
                                y.TransitionEndRule.Id = null;
                            if (y.TransitionStartRule is not null)
                                y.TransitionStartRule.Id = null;

                        });
                    }
                });
            }
            return studyCells;
        }

        /// <summary>
        /// Remove uuid for Schedule Timelines
        /// </summary>
        /// <param name="scheduleTimelines"></param>
        /// <returns></returns>
        public static List<ScheduleTimelineEntity> RemoveIdForScheduleTimelines(List<ScheduleTimelineEntity> scheduleTimelines)
        {
            if (scheduleTimelines is not null && scheduleTimelines.Any())
            {
                scheduleTimelines.ForEach(x =>
                {
                    x.Id = null;

                    if (x.ScheduleTimelineExits is not null && x.ScheduleTimelineExits.Any())
                        x.ScheduleTimelineExits.ForEach(y => y.Id = null);

                    if (x.ScheduleTimelineInstances is not null && x.ScheduleTimelineInstances.Any())
                    {
                        x.ScheduleTimelineInstances.ForEach(y =>
                        {
                            y.Id = null;

                            if (y.ScheduledInstanceTimings is not null && y.ScheduledInstanceTimings.Any())
                            {
                                y.ScheduledInstanceTimings.ForEach(z =>
                                {
                                    z.Id = null;

                                    if (z.TimingType is not null)
                                        z.TimingType.Id = null;

                                    if (z.TimingRelativeToFrom is not null)
                                        z.TimingRelativeToFrom.Id = null;
                                });
                            }

                        });
                    }
                });
            }
            return scheduleTimelines;
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
                    x.Id = null;

                    if (x.AnalysisPopulation is not null)
                        x.AnalysisPopulation.Id = null;

                    if (x.IntercurrentEvents is not null && x.IntercurrentEvents.Any())
                    {
                        x.IntercurrentEvents.ForEach(y =>
                        {
                            y.Id = null;
                        });
                    }
                });
            }
            return estimands;
        }

        /// <summary>
        /// Remove uuid for Encounters
        /// </summary>
        /// <param name="encounters"></param>
        /// <returns></returns>
        public static List<EncounterEntity> RemoveIdForEncounters(List<EncounterEntity> encounters)
        {
            if (encounters is not null && encounters.Any())
            {
                encounters.ForEach(y =>
                {
                    y.Id = null;
                    if (y.EncounterContactModes is not null && y.EncounterContactModes.Any())
                    {
                        y.EncounterContactModes.ForEach(procedure => procedure.Id = null);
                    }
                    if (y.EncounterEnvironmentalSetting is not null)
                    {
                        y.EncounterEnvironmentalSetting.Id = null;
                    }
                    if (y.EncounterType is not null)
                    {
                        y.EncounterType.Id = null;
                    }
                    if (y.TransitionStartRule is not null)
                        y.TransitionStartRule.Id = null;
                    if (y.TransitionEndRule is not null)
                        y.TransitionEndRule.Id = null;
                });
            }
            return encounters;
        }

        /// <summary>
        /// Remove uuid for Activities
        /// </summary>
        /// <param name="activities"></param>
        /// <returns></returns>
        public static List<ActivityEntity> RemoveIdForActivities(List<ActivityEntity> activities)
        {
            if (activities is not null && activities.Any())
            {
                activities.ForEach(y =>
                {
                    y.Id = null;

                    if (y.DefinedProcedures is not null && y.DefinedProcedures.Any())
                    {
                        y.DefinedProcedures.ForEach(procedure =>
                        {
                            procedure.Id = null;
                            if (procedure.ProcedureCode is not null)
                                procedure.ProcedureCode.Id = null;
                        });
                    }
                });
            }
            return activities;
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

        #region GetDifference For Each Section
        /// <summary>
        /// Get the differences between two studies
        /// </summary>
        /// <param name="currentStudyVersion">Current study version</param>
        /// <param name="previousStudyVersion">Previous study version</param>
        /// <returns></returns>
        public List<string> GetChangedValues(StudyDefinitionsEntity currentStudyVersion, StudyDefinitionsEntity previousStudyVersion)
        {
            List<string> changedValues = new();

            if (currentStudyVersion.Study.StudyTitle != previousStudyVersion.Study.StudyTitle)
                changedValues.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}.{nameof(StudyEntity.StudyTitle)}");
            if (currentStudyVersion.Study.StudyVersion != previousStudyVersion.Study.StudyVersion)
                changedValues.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}.{nameof(StudyEntity.StudyVersion)}");
            if (currentStudyVersion.Study.StudyRationale != previousStudyVersion.Study.StudyRationale)
                changedValues.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}.{nameof(StudyEntity.StudyRationale)}");
            if (currentStudyVersion.Study.StudyAcronym != previousStudyVersion.Study.StudyAcronym)
                changedValues.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}.{nameof(StudyEntity.StudyAcronym)}");
            if (GetDifferences<CodeEntity>(currentStudyVersion.Study.StudyType, previousStudyVersion.Study.StudyType).Any())
                changedValues.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}.{nameof(StudyEntity.StudyType)}");

            //StudyPhase
            GetDifferenceForAliasCode(currentStudyVersion.Study.StudyPhase, previousStudyVersion.Study.StudyPhase).ForEach(x =>
            {
                changedValues.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}.{nameof(StudyEntity.StudyPhase)}.{x}");
            });

            //BusinessTherapeuticAreas
            if (GetDifferences<List<CodeEntity>>(currentStudyVersion.Study.BusinessTherapeuticAreas, previousStudyVersion.Study.BusinessTherapeuticAreas).Any())
                changedValues.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}.{nameof(StudyEntity.BusinessTherapeuticAreas)}");

            //StudyIdentifiers
            changedValues.AddRange(GetDifferenceForStudyIdentifiers(currentStudyVersion.Study.StudyIdentifiers, previousStudyVersion.Study.StudyIdentifiers));
            //StudyProtocolVersion
            changedValues.AddRange(GetDifferenceForStudyProtocolVersions(currentStudyVersion.Study.StudyProtocolVersions, previousStudyVersion.Study.StudyProtocolVersions));
            //Study Designs
            changedValues.AddRange(GetDifferenceForStudyDesigns(currentStudyVersion.Study.StudyDesigns, previousStudyVersion.Study.StudyDesigns));


            return changedValues;
        }

        public static List<string> GetDifferences<T>(T currentVersion, T previousVersion)
        {
            var comparer = new ObjectsComparer.Comparer<T>();
            bool isEqual = comparer.Compare(currentVersion, previousVersion, out var differences);
            return differences.Select(x => x.MemberPath).ToList();
        }

        public List<string> GetDifferenceForAList<T>(List<T> currentVersion, List<T> previousVersion) where T : class, Entities.StudyV2.IId
        {
            List<string> changedValues = new();
            if (currentVersion != null && currentVersion.Any())
            {
                currentVersion.ForEach(currentItem =>
                {
                    if (previousVersion != null && previousVersion.Any(x => x.Id == currentItem.Id))
                    {
                        changedValues.AddRange(GetDifferences<T>(currentItem, previousVersion.Find(x => x.Id == currentItem.Id)));
                        if (currentVersion.IndexOf(currentItem) != previousVersion.IndexOf(previousVersion.Find(x => x.Id == currentItem.Id)))
                            changedValues.Add(nameof(T));
                    }
                    else if (previousVersion != null && currentVersion?.Count == previousVersion?.Count && !previousVersion.Any(x => x.Id == currentItem.Id))
                    {
                        changedValues.Add(nameof(T));
                    }
                });
            }
            else if ((currentVersion is null && previousVersion is not null) || (currentVersion is not null && previousVersion is null))
                changedValues.Add(nameof(T));
            if (currentVersion?.Count != previousVersion?.Count)
                changedValues.Add(nameof(T));
            return changedValues;
        }
        public static List<string> CheckForNumberOfElementsMismatch<T>(List<T> currentVersion, List<T> previousVersion) where T : class, Entities.StudyV2.IId
        {
            var differences = CheckDifferences<List<T>>(currentVersion, previousVersion);
            if (differences.Any(x => x.DifferenceType == DifferenceTypes.NumberOfElementsMismatch))
                return new List<string>();
            else
                return differences.Select(x => x.MemberPath).ToList();
        }
        public static List<Difference> CheckDifferences<T>(T currentVersion, T previousVersion)
        {
            var comparer = new ObjectsComparer.Comparer<T>();
            _ = comparer.Compare(currentVersion, previousVersion, out var differences);
            return differences.ToList();
        }

        public List<string> GetDifferenceForStudyIdentifiers(List<StudyIdentifierEntity> currentVersion, List<StudyIdentifierEntity> previousVersion)
        {
            var tempList = new List<string>();
            if ((currentVersion is null && previousVersion is not null) || (currentVersion is not null && previousVersion is null))
                tempList.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}.{nameof(StudyEntity.StudyIdentifiers)}");
            if (currentVersion?.Count != previousVersion?.Count)
                if (currentVersion?.Count != previousVersion?.Count)
                    tempList.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}.{nameof(StudyEntity.StudyIdentifiers)}");
            GetDifferenceForAList<StudyIdentifierEntity>(currentVersion, previousVersion).ForEach(x =>
            {
                tempList.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}.{nameof(StudyEntity.StudyIdentifiers)}.{x}");
            });
            return tempList;
        }

        public static List<string> GetDifferenceForAliasCode(AliasCodeEntity currentVersion, AliasCodeEntity previousVersion)
        {
            var tempList = new List<string>();
            if ((currentVersion is null && previousVersion is not null) || (currentVersion is not null && previousVersion is null))
            {
                tempList.Add("T");
                return tempList;
            }
            if (currentVersion?.Id != previousVersion?.Id)
            {
                tempList.Add("T");
                return tempList;
            }
            //Changed below two to ignore Id change
            //GetDifferences<CodeEntity>(currentVersion?.StandardCode, previousVersion?.StandardCode).ForEach(x =>
            //{
            //    tempList.Add($"{nameof(AliasCodeEntity.StandardCode)}.{x}");
            //});
            //GetDifferenceForAList<CodeEntity>(currentVersion?.StandardCodeAliases, previousVersion?.StandardCodeAliases).ForEach(x =>
            //{
            //    tempList.Add($"{nameof(AliasCodeEntity.StandardCodeAliases)}.{x}");
            //});

            GetDifferences<AliasCodeEntity>(currentVersion, previousVersion).ForEach(x =>
            {
                tempList.Add($"{x}");
            });
            return tempList;
        }

        public List<string> GetDifferenceForStudyProtocolVersions(List<StudyProtocolVersionEntity> currentVersion, List<StudyProtocolVersionEntity> previousVersion)
        {
            var tempList = new List<string>();
            if ((currentVersion is null && previousVersion is not null) || (currentVersion is not null && previousVersion is null))
                tempList.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}.{nameof(StudyEntity.StudyProtocolVersions)}");
            if (currentVersion?.Count != previousVersion?.Count)
                tempList.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}.{nameof(StudyEntity.StudyProtocolVersions)}");
            GetDifferenceForAList<StudyProtocolVersionEntity>(currentVersion, previousVersion).ForEach(x =>
            {
                tempList.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}.{nameof(StudyEntity.StudyProtocolVersions)}.{x}");
            });
            return tempList;
        }

        public List<string> GetDifferenceForStudyDesigns(List<StudyDesignEntity> currentVersion, List<StudyDesignEntity> previousVersion)
        {
            List<string> changedValues = new();
            List<string> formattedChangedValues = new();

            if ((currentVersion is null && previousVersion is not null) || (currentVersion is not null && previousVersion is null))
                formattedChangedValues.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}.{nameof(StudyEntity.StudyDesigns)}");
            if (currentVersion?.Count != previousVersion?.Count)
                formattedChangedValues.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}.{nameof(StudyEntity.StudyDesigns)}");
            changedValues.AddRange(GetDifferenceForEachStudyDesigns(currentVersion, previousVersion));

            if (changedValues.Any())
            {
                changedValues.ForEach(x =>
                {
                    string addRootPath = $"{IdFieldPropertyName.ParentElement.ClinicalStudy}.{nameof(StudyEntity.StudyDesigns)}.{x}";
                    formattedChangedValues.Add(addRootPath);
                });
            }
            return formattedChangedValues;
        }

        public List<string> GetDifferenceForEachStudyDesigns(List<StudyDesignEntity> currentVersion, List<StudyDesignEntity> previousVersion)
        {
            List<string> changedValues = new();

            if (currentVersion != null && currentVersion.Any())
            {
                currentVersion.ForEach(currentStudyDesign =>
                {
                    if (previousVersion != null && previousVersion.Any(x => x.Id == currentStudyDesign.Id))
                    {
                        var previousStudyDesign = previousVersion.Find(x => x.Id == currentStudyDesign.Id);

                        if (currentStudyDesign.StudyDesignName != previousStudyDesign.StudyDesignName)
                            changedValues.Add($"{nameof(StudyDesignEntity.StudyDesignName)}");

                        if (currentStudyDesign.StudyDesignDescription != previousStudyDesign.StudyDesignDescription)
                            changedValues.Add($"{nameof(StudyDesignEntity.StudyDesignDescription)}");

                        //StudyRationale
                        if (currentStudyDesign.StudyDesignRationale != previousStudyDesign.StudyDesignRationale)
                            changedValues.Add($"{nameof(StudyDesignEntity.StudyDesignRationale)}");

                        //Intervention Model
                        if (GetDifferences<CodeEntity>(currentStudyDesign.InterventionModel, previousStudyDesign.InterventionModel).Any())
                            changedValues.Add($"{nameof(StudyDesignEntity.InterventionModel)}");

                        //Trial Type
                        if (GetDifferences<List<CodeEntity>>(currentStudyDesign.TrialType, previousStudyDesign.TrialType).Any())
                            changedValues.Add($"{nameof(StudyDesignEntity.TrialType)}");

                        //Trial Intent Type
                        if (GetDifferences<List<CodeEntity>>(currentStudyDesign.TrialIntentType, previousStudyDesign.TrialIntentType).Any())
                            changedValues.Add($"{nameof(StudyDesignEntity.TrialIntentType)}");

                        //TherapeuticAreas
                        if (GetDifferences<List<CodeEntity>>(currentStudyDesign.TherapeuticAreas, previousStudyDesign.TherapeuticAreas).Any())
                            changedValues.Add($"{nameof(StudyDesignEntity.TherapeuticAreas)}");

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
                        changedValues.AddRange(GetDifferenceForStudyScheduleTimelines(currentStudyDesign, previousStudyDesign));

                        //DesignBlindingScheme
                        GetDifferenceForAliasCode(currentStudyDesign.StudyDesignBlindingScheme, previousStudyDesign.StudyDesignBlindingScheme).ForEach(x =>
                        {
                            changedValues.Add($"{nameof(StudyDesignEntity.StudyDesignBlindingScheme)}.{x}");
                        });


                        //Encounters
                        GetDifferenceForAList<EncounterEntity>(currentStudyDesign.Encounters, previousStudyDesign.Encounters).ForEach(x =>
                        {
                            changedValues.Add($"{nameof(StudyDesignEntity.Encounters)}.{x}");
                        });

                        //Activities
                        changedValues.AddRange(GetDifferenceForActivities(currentStudyDesign, previousStudyDesign));

                        //Biomedical Concept Category
                        GetDifferenceForAList<BiomedicalConceptCategoryEntity>(currentStudyDesign.BcCategories, previousStudyDesign.BcCategories).ForEach(x =>
                        {
                            changedValues.Add($"{nameof(StudyDesignEntity.BcCategories)}.{x}");
                        });

                        //Biomedical Concept Surrogate
                        GetDifferenceForAList<BiomedicalConceptSurrogateEntity>(currentStudyDesign.BcSurrogates, previousStudyDesign.BcSurrogates).ForEach(x =>
                        {
                            changedValues.Add($"{nameof(StudyDesignEntity.BcSurrogates)}.{x}");
                        });

                        //Biomedical Concepts
                        changedValues.AddRange(GetDifferenceForBiomedicalConcepts(currentStudyDesign, previousStudyDesign));
                    }

                    else if (currentVersion?.Count == previousVersion?.Count && previousVersion != null && !previousVersion.Any(x => x.Id == currentStudyDesign.Id))
                    {
                        changedValues.Add("T");
                    }
                });
            }

            return changedValues;
        }
        public List<string> GetDifferenceForBiomedicalConcepts(StudyDesignEntity currentStudyDesign, StudyDesignEntity previousStudyDesign)
        {
            var tempList = new List<string>();
            if (currentStudyDesign.BiomedicalConcepts?.Count != previousStudyDesign.BiomedicalConcepts?.Count)
                tempList.Add($"{nameof(StudyDesignEntity.BiomedicalConcepts)}");
            GetDifferenceForAList<BiomedicalConceptEntity>(currentStudyDesign.BiomedicalConcepts, previousStudyDesign.BiomedicalConcepts).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDesignEntity.BiomedicalConcepts)}.{x}");
            });
            tempList.RemoveAll(x => x.Contains($"{nameof(BiomedicalConceptEntity.BcProperties)}"));
            tempList.RemoveAll(x => x.Contains($"{nameof(BiomedicalConceptEntity.BcConceptCode)}"));
            currentStudyDesign.BiomedicalConcepts?.ForEach(currentBc =>
            {
                var currentBcChangedValues = new List<string>();
                if (previousStudyDesign.BiomedicalConcepts != null && previousStudyDesign.BiomedicalConcepts.Any(x => x.Id == currentBc.Id))
                {
                    var previousBc = previousStudyDesign.BiomedicalConcepts.Find(x => x.Id == currentBc.Id);
                    GetDifferenceForAList<BiomedicalConceptPropertyEntity>(currentBc.BcProperties, previousBc.BcProperties).ForEach(x =>
                    {
                        currentBcChangedValues.Add($"{nameof(StudyDesignEntity.BiomedicalConcepts)}.{nameof(BiomedicalConceptEntity.BcProperties)}.{x}");
                    });
                    currentBcChangedValues.RemoveAll(x => x.Contains($"{nameof(BiomedicalConceptPropertyEntity.BcPropertyResponseCodes)}"));
                    currentBcChangedValues.RemoveAll(x => x.Contains($"{nameof(BiomedicalConceptPropertyEntity.BcPropertyConceptCode)}"));
                    currentBc.BcProperties?.ForEach(currentBcProp =>
                    {
                        if (previousBc.BcProperties != null && previousBc.BcProperties.Any(x => x.Id == currentBcProp.Id))
                        {
                            var previousBcProp = previousBc.BcProperties.Find(x => x.Id == currentBcProp.Id);

                            GetDifferenceForAList<ResponseCodeEntity>(currentBcProp.BcPropertyResponseCodes, previousBcProp.BcPropertyResponseCodes).ForEach(x =>
                            {
                                currentBcChangedValues.Add($"{nameof(StudyDesignEntity.BiomedicalConcepts)}.{nameof(BiomedicalConceptEntity.BcProperties)}.{nameof(BiomedicalConceptPropertyEntity.BcPropertyResponseCodes)}.{x}");
                            });

                            GetDifferenceForAliasCode(currentBcProp.BcPropertyConceptCode, previousBcProp.BcPropertyConceptCode).ForEach(x =>
                            {
                                currentBcChangedValues.Add($"{nameof(StudyDesignEntity.BiomedicalConcepts)}.{nameof(BiomedicalConceptEntity.BcProperties)}.{nameof(BiomedicalConceptPropertyEntity.BcPropertyConceptCode)}.{x}");
                            });
                        }
                    });
                    GetDifferenceForAliasCode(currentBc.BcConceptCode, previousBc.BcConceptCode).ForEach(x =>
                    {
                        currentBcChangedValues.Add($"{nameof(StudyDesignEntity.BiomedicalConcepts)}.{nameof(BiomedicalConceptEntity.BcConceptCode)}.{x}");
                    });
                }
                tempList.AddRange(currentBcChangedValues);
            });
            return tempList;
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
                if (previousStudyDesign.StudyObjectives != null && previousStudyDesign.StudyObjectives.Any(x => x.Id == currentObjective.Id))
                {
                    var previousObjective = previousStudyDesign.StudyObjectives.Find(x => x.Id == currentObjective.Id);

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
                if (previousStudyDesign.StudyEstimands != null && previousStudyDesign.StudyEstimands.Any(x => x.Id == currentEstimand.Id))
                {
                    var previousEstimand = previousStudyDesign.StudyEstimands.Find(x => x.Id == currentEstimand.Id);
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
            currentStudyDesign.StudyCells?.ForEach(currentStudyCell =>
            {
                if (previousStudyDesign.StudyCells != null && previousStudyDesign.StudyCells.Any(x => x.Id == currentStudyCell.Id))
                {
                    var previousStudyCell = previousStudyDesign.StudyCells.Find(x => x.Id == currentStudyCell.Id);

                    GetDifferenceForAList<StudyElementEntity>(currentStudyCell.StudyElements, previousStudyCell.StudyElements).ForEach(x =>
                    {
                        tempList.Add($"{nameof(StudyDesignEntity.StudyCells)}.{nameof(StudyCellEntity.StudyElements)}.{x}");
                    });
                }
            });
            return tempList;
        }

        public List<string> GetDifferenceForStudyScheduleTimelines(StudyDesignEntity currentStudyDesign, StudyDesignEntity previousStudyDesign)
        {
            var tempList = new List<string>();
            if (currentStudyDesign.StudyScheduleTimelines?.Count != previousStudyDesign.StudyScheduleTimelines?.Count)
                tempList.Add($"{nameof(StudyDesignEntity.StudyScheduleTimelines)}");
            GetDifferenceForAList<ScheduleTimelineEntity>(currentStudyDesign.StudyScheduleTimelines, previousStudyDesign.StudyScheduleTimelines).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDesignEntity.StudyScheduleTimelines)}.{x}");
            });
            tempList.RemoveAll(x => x.Contains($"{nameof(ScheduleTimelineEntity.ScheduleTimelineInstances)}"));
            tempList.RemoveAll(x => x.Contains($"{nameof(ScheduleTimelineEntity.ScheduleTimelineExits)}"));
            currentStudyDesign.StudyScheduleTimelines?.ForEach(currentTimeline =>
            {
                var scheduleTimelineChangeList = new List<string>();
                if (previousStudyDesign.StudyScheduleTimelines != null && previousStudyDesign.StudyScheduleTimelines.Any(x => x.Id == currentTimeline.Id))
                {
                    var previousTimeline = previousStudyDesign.StudyScheduleTimelines.Find(x => x.Id == currentTimeline.Id);
                    GetDifferenceForAList<ScheduleTimelineExitEntity>(currentTimeline.ScheduleTimelineExits, previousTimeline.ScheduleTimelineExits).ForEach(x =>
                    {
                        scheduleTimelineChangeList.Add($"{nameof(StudyDesignEntity.StudyScheduleTimelines)}.{nameof(ScheduleTimelineEntity.ScheduleTimelineExits)}.{x}");
                    });
                    GetDifferenceForScheduledInstances(currentTimeline.ScheduleTimelineInstances, previousTimeline.ScheduleTimelineInstances).ForEach(x =>
                    {
                        scheduleTimelineChangeList.Add($"{nameof(StudyDesignEntity.StudyScheduleTimelines)}.{nameof(ScheduleTimelineEntity.ScheduleTimelineInstances)}.{x}");
                    });
                    scheduleTimelineChangeList.RemoveAll(x => x.Contains($"{nameof(ScheduledInstanceEntity.ScheduledInstanceTimings)}"));
                    currentTimeline.ScheduleTimelineInstances?.ForEach(currentInstance =>
                    {
                        var scheduleTimelineTimingChangeList = new List<string>();
                        if (previousTimeline.ScheduleTimelineInstances != null && previousTimeline.ScheduleTimelineInstances.Any(x => x.Id == currentInstance.Id))
                        {
                            var previousInstance = previousTimeline.ScheduleTimelineInstances.Find(x => x.Id == currentInstance.Id);

                            GetDifferenceForAList<TimingEntity>(currentInstance.ScheduledInstanceTimings, previousInstance.ScheduledInstanceTimings).ForEach(x =>
                            {
                                scheduleTimelineTimingChangeList.Add($"{nameof(StudyDesignEntity.StudyScheduleTimelines)}.{nameof(ScheduleTimelineEntity.ScheduleTimelineInstances)}.{nameof(ScheduledInstanceEntity.ScheduledInstanceTimings)}.{x}");
                            });
                        }
                        scheduleTimelineChangeList.AddRange(scheduleTimelineTimingChangeList);
                    });
                }
                tempList.AddRange(scheduleTimelineChangeList);
            });
            return tempList;
        }

        public List<string> GetDifferenceForScheduledInstances<T>(List<T> currentVersion, List<T> previousVersion) where T : Entities.StudyV2.ScheduledInstanceEntity
        {
            List<string> changedValues = new();
            if (currentVersion != null && currentVersion.Any())
            {
                currentVersion.ForEach(currentItem =>
                {
                    if (previousVersion != null && previousVersion.Any(x => x.Id == currentItem.Id))
                    {
                        var previousItem = previousVersion.Find(x => x.Id == currentItem.Id);
                        if (previousItem.GetType() != currentItem.GetType())
                            changedValues.Add(nameof(T));

                        if (previousItem.GetType() == currentItem.GetType())
                        {
                            if (currentItem.GetType() == typeof(ScheduledActivityInstanceEntity))
                                changedValues.AddRange(GetDifferences(currentItem as ScheduledActivityInstanceEntity, previousItem as ScheduledActivityInstanceEntity));
                            if (currentItem.GetType() == typeof(ScheduledDecisionInstanceEntity))
                            {
                                var differences = GetDifferences(currentItem as ScheduledDecisionInstanceEntity, previousItem as ScheduledDecisionInstanceEntity);
                                if (differences.Any())
                                {
                                    differences.ForEach(difference => changedValues.Add(difference.Replace(Constants.StringToBeRemovedForChangeAudit.ConditionAssignmentsValue, "")));
                                }
                            }
                        }
                        if (currentVersion.IndexOf(currentItem) != previousVersion.IndexOf(previousItem))
                            changedValues.Add(nameof(T));
                    }
                    else if (previousVersion != null && currentVersion?.Count == previousVersion?.Count && !previousVersion.Any(x => x.Id == currentItem.Id))
                    {
                        changedValues.Add(nameof(T));
                    }
                });
            }
            else if ((currentVersion is null && previousVersion is not null) || (currentVersion is not null && previousVersion is null))
                changedValues.Add(nameof(T));
            if (currentVersion?.Count != previousVersion?.Count)
                changedValues.Add(nameof(T));
            return changedValues;
        }
        public List<string> GetDifferenceForActivities(StudyDesignEntity currentStudyDesign, StudyDesignEntity previousStudyDesign)
        {
            var tempList = new List<string>();
            if (currentStudyDesign.Activities?.Count != previousStudyDesign.Activities?.Count)
                tempList.Add($"{nameof(StudyDesignEntity.Activities)}");
            GetDifferenceForAList<ActivityEntity>(currentStudyDesign.Activities, previousStudyDesign.Activities).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDesignEntity.Activities)}.{x}");
            });
            tempList.RemoveAll(x => x.Contains($"{nameof(ActivityEntity.DefinedProcedures)}"));
            currentStudyDesign.Activities?.ForEach(currentActivitiy =>
            {
                if (previousStudyDesign.Activities != null && previousStudyDesign.Activities.Any(x => x.Id == currentActivitiy.Id))
                {
                    var previousActivity = previousStudyDesign.Activities.Find(x => x.Id == currentActivitiy.Id);

                    GetDifferenceForAList<ProcedureEntity>(currentActivitiy.DefinedProcedures, previousActivity.DefinedProcedures).ForEach(x =>
                    {
                        tempList.Add($"{nameof(StudyDesignEntity.Activities)}.{nameof(ActivityEntity.DefinedProcedures)}.{x}");
                    });
                }
            });
            return tempList;
        }
        #endregion

        #region ReferenceIntegrity
        public bool ReferenceIntegrityValidation(StudyDefinitionsDto study, out object referenceErrors)
        {
            List<string> errors = new();
            if (study.Study.StudyDesigns != null && study.Study.StudyDesigns.Any())
            {
                study.Study.StudyDesigns.ForEach(design =>
                {
                    Parallel.Invoke(
                        //Study Epoch & Encounters
                        () => errors.AddRange(ReferenceIntegrityValidationForStudyCells(design, study.Study.StudyDesigns.IndexOf(design))),

                        //StudyScheduleTimelines
                        () => errors.AddRange(ReferenceIntegrityValidationForStudyScheduleTimelines(design, study.Study.StudyDesigns.IndexOf(design))),

                        //Activities 
                        () => errors.AddRange(ReferenceIntegrityValidationForActivities(design, study.Study.StudyDesigns.IndexOf(design))),

                        //Encounters
                        () => errors.AddRange(ReferenceIntegrityValidationForEncounters(design, study.Study.StudyDesigns.IndexOf(design))),

                        //Endpoints & InvestigationalIntervention
                        () => errors.AddRange(ReferenceIntegrityValidationForStudyEstimands(design, study.Study.StudyDesigns.IndexOf(design))),

                        //BcCategories
                        () => errors.AddRange(ReferenceIntegrityValidationForBcCategories(design, study.Study.StudyDesigns.IndexOf(design)))
                     );

                });
            }
            List<string> formattedErrors = new();
            errors.ForEach(element =>
            {
                formattedErrors.Add(string.Join(".", element?.Split(".").Select(key => $"{key?[..1]?.ToLower()}{key?[1..]}")));
            });
            referenceErrors = GetErrors(formattedErrors);
            return errors.Any();
        }

        public static List<string> ReferenceIntegrityValidationForStudyCells(StudyDesignDto design, int indexOfDesign)
        {
            List<String> errors = new();

            if (design.StudyCells != null && design.StudyCells.Any())
            {
                List<string> studyEpochUUIDs = design.StudyCells.Select(cell => cell?.StudyEpoch?.Id).ToList();
                studyEpochUUIDs.RemoveAll(x => String.IsNullOrWhiteSpace(x));
                design.StudyCells.ForEach(cell =>
                {
                    if (cell.StudyEpoch != null)
                    {
                        List<string> tempStudyEpochUUIDs = studyEpochUUIDs.ToList();
                        tempStudyEpochUUIDs.RemoveAll(x => x == cell.StudyEpoch.Id);
                        if (!String.IsNullOrWhiteSpace(cell.StudyEpoch.PreviousStudyEpochId) && !tempStudyEpochUUIDs.Contains(cell.StudyEpoch.PreviousStudyEpochId))
                            errors.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}." +
                                $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                                $"{nameof(StudyDesignDto.StudyCells)}[{design.StudyCells.IndexOf(cell)}]." +
                                $"{nameof(StudyCellDto.StudyEpoch)}.{nameof(StudyEpochDto.PreviousStudyEpochId)}");

                        if (!String.IsNullOrWhiteSpace(cell.StudyEpoch.NextStudyEpochId) && !tempStudyEpochUUIDs.Contains(cell.StudyEpoch.NextStudyEpochId))
                            errors.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}." +
                                $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                                $"{nameof(StudyDesignDto.StudyCells)}[{design.StudyCells.IndexOf(cell)}].{nameof(StudyCellDto.StudyEpoch)}." +
                                $"{nameof(StudyEpochDto.NextStudyEpochId)}");

                        if (cell.StudyEpoch.Encounters != null && cell.StudyEpoch.Encounters.Any())
                        {
                            cell.StudyEpoch.Encounters.ForEach(encounterId =>
                            {
                                List<string> encounterIds = design.Encounters is null ? new List<string>() : design.Encounters.Select(x => x.Id).ToList();
                                if (!String.IsNullOrWhiteSpace(encounterId) && !encounterIds.Contains(encounterId))
                                    errors.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}." +
                                               $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                                               $"{nameof(StudyDesignDto.StudyCells)}[{design.StudyCells.IndexOf(cell)}].{nameof(StudyCellDto.StudyEpoch)}." +
                                               $"{nameof(StudyEpochDto.Encounters)}[{cell.StudyEpoch.Encounters.IndexOf(encounterId)}]");
                            });
                        }
                    }
                });
            }

            return errors;
        }

        public static List<string> ReferenceIntegrityValidationForStudyScheduleTimelines(StudyDesignDto design, int indexOfDesign)
        {
            List<String> errors = new();

            if (design.StudyScheduleTimelines != null && design.StudyScheduleTimelines.Any())
            {
                List<string> scheduledTimelineIds = design.StudyScheduleTimelines.Select(x => x.Id).ToList();
                List<string> encounterIds = design.Encounters is null ? new List<string>() : design.Encounters.Select(x => x.Id).ToList();
                List<string> allScheduleInstanceIds = design.StudyScheduleTimelines.Where(x => x.ScheduleTimelineInstances != null && x.ScheduleTimelineInstances.Any()).SelectMany(x => x.ScheduleTimelineInstances).Select(x => x.Id).ToList();
                List<string> allActivityIds = design.Activities is null ? new List<string>() : design.Activities.Select(x => x.Id).ToList();
                design.StudyScheduleTimelines.ForEach(scheduleTimeline =>
                {
                    List<string> scheduledTimelineInstanceIds = scheduleTimeline.ScheduleTimelineInstances is not null && scheduleTimeline.ScheduleTimelineInstances.Any() ? scheduleTimeline.ScheduleTimelineInstances.Select(x => x.Id).ToList() : new List<string>();
                    List<string> scheduleTimelineExitIds = scheduleTimeline.ScheduleTimelineExits is null ? new List<string>() : scheduleTimeline.ScheduleTimelineExits.Select(x => x.Id).ToList();

                    if (!String.IsNullOrWhiteSpace(scheduleTimeline.ScheduleTimelineEntryId) && !scheduledTimelineInstanceIds.Contains(scheduleTimeline.ScheduleTimelineEntryId))
                        errors.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}." +
                            $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                            $"{nameof(StudyDesignDto.StudyScheduleTimelines)}[{design.StudyScheduleTimelines.IndexOf(scheduleTimeline)}].{nameof(ScheduleTimelineDto.ScheduleTimelineEntryId)}");

                    if (scheduleTimeline.ScheduleTimelineInstances != null && scheduleTimeline.ScheduleTimelineInstances.Any())
                    {
                        scheduleTimeline.ScheduleTimelineInstances.ForEach(timelineInstance =>
                        {
                            if (!String.IsNullOrWhiteSpace(timelineInstance.ScheduleTimelineExitId) && !scheduleTimelineExitIds.Contains(timelineInstance.ScheduleTimelineExitId))
                                errors.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}." +
                                    $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                                    $"{nameof(StudyDesignDto.StudyScheduleTimelines)}[{design.StudyScheduleTimelines.IndexOf(scheduleTimeline)}]." +
                                    $"{nameof(ScheduleTimelineDto.ScheduleTimelineInstances)}[{scheduleTimeline.ScheduleTimelineInstances.IndexOf(timelineInstance)}]." +
                                    $"{nameof(ScheduledInstanceDto.ScheduleTimelineExitId)}");

                            if (!String.IsNullOrWhiteSpace(timelineInstance.ScheduledInstanceEncounterId) && !encounterIds.Contains(timelineInstance.ScheduledInstanceEncounterId))
                                errors.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}." +
                                    $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                                    $"{nameof(StudyDesignDto.StudyScheduleTimelines)}[{design.StudyScheduleTimelines.IndexOf(scheduleTimeline)}]." +
                                    $"{nameof(ScheduleTimelineDto.ScheduleTimelineInstances)}[{scheduleTimeline.ScheduleTimelineInstances.IndexOf(timelineInstance)}]." +
                                    $"{nameof(ScheduledInstanceDto.ScheduledInstanceEncounterId)}");

                            if (!String.IsNullOrWhiteSpace(timelineInstance.ScheduledInstanceTimelineId) && !scheduledTimelineIds.Contains(timelineInstance.ScheduledInstanceTimelineId))
                                errors.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}." +
                                    $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                                    $"{nameof(StudyDesignDto.StudyScheduleTimelines)}[{design.StudyScheduleTimelines.IndexOf(scheduleTimeline)}]." +
                                    $"{nameof(ScheduleTimelineDto.ScheduleTimelineInstances)}[{scheduleTimeline.ScheduleTimelineInstances.IndexOf(timelineInstance)}]." +
                                    $"{nameof(ScheduledInstanceDto.ScheduledInstanceTimelineId)}");

                            if (timelineInstance.GetType() == typeof(ScheduledActivityInstanceDto))
                            {
                                var activityIds = (timelineInstance as ScheduledActivityInstanceDto).ActivityIds;
                                if (activityIds is not null && activityIds.Any())
                                {
                                    activityIds.ForEach(id =>
                                    {
                                        if (!String.IsNullOrWhiteSpace(id) && !allActivityIds.Contains(id))
                                            errors.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}." +
                                                $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                                                $"{nameof(StudyDesignDto.StudyScheduleTimelines)}[{design.StudyScheduleTimelines.IndexOf(scheduleTimeline)}]." +
                                                $"{nameof(ScheduleTimelineDto.ScheduleTimelineInstances)}[{scheduleTimeline.ScheduleTimelineInstances.IndexOf(timelineInstance)}]." +
                                                $"{nameof(ScheduledActivityInstanceDto.ActivityIds)}[{activityIds.IndexOf(id)}]");
                                    });
                                }
                            }

                            if (timelineInstance.ScheduledInstanceTimings is not null && timelineInstance.ScheduledInstanceTimings.Any())
                            {
                                timelineInstance.ScheduledInstanceTimings.ForEach(timing =>
                                {
                                    if (!String.IsNullOrWhiteSpace(timing.RelativeFromScheduledInstanceId) && !allScheduleInstanceIds.Contains(timing.RelativeFromScheduledInstanceId))
                                        errors.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}." +
                                            $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                                            $"{nameof(StudyDesignDto.StudyScheduleTimelines)}[{design.StudyScheduleTimelines.IndexOf(scheduleTimeline)}]." +
                                            $"{nameof(ScheduleTimelineDto.ScheduleTimelineInstances)}[{scheduleTimeline.ScheduleTimelineInstances.IndexOf(timelineInstance)}]." +
                                            $"{nameof(ScheduledInstanceDto.ScheduledInstanceTimings)}[{timelineInstance.ScheduledInstanceTimings.IndexOf(timing)}]." +
                                            $"{nameof(TimingDto.RelativeFromScheduledInstanceId)}");

                                    if (!String.IsNullOrWhiteSpace(timing.RelativeToScheduledInstanceId) && !allScheduleInstanceIds.Contains(timing.RelativeToScheduledInstanceId))
                                        errors.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}." +
                                            $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                                            $"{nameof(StudyDesignDto.StudyScheduleTimelines)}[{design.StudyScheduleTimelines.IndexOf(scheduleTimeline)}]." +
                                            $"{nameof(ScheduleTimelineDto.ScheduleTimelineInstances)}[{scheduleTimeline.ScheduleTimelineInstances.IndexOf(timelineInstance)}]." +
                                            $"{nameof(ScheduledInstanceDto.ScheduledInstanceTimings)}[{timelineInstance.ScheduledInstanceTimings.IndexOf(timing)}]." +
                                            $"{nameof(TimingDto.RelativeToScheduledInstanceId)}");
                                });
                            }
                        });
                    }
                });
            }

            return errors;
        }

        public static List<string> ReferenceIntegrityValidationForActivities(StudyDesignDto design, int indexOfDesign)
        {
            List<String> errors = new();

            if (design.Activities != null && design.Activities.Any())
            {
                List<string> biomedicalConceptIds = design.BiomedicalConcepts is null ? new List<string>() : design.BiomedicalConcepts.Select(x => x.Id).ToList();
                List<string> bcCategoryIds = design.BcCategories is null ? new List<string>() : design.BcCategories.Select(x => x.Id).ToList();
                List<string> bcSurrogateIds = design.BcSurrogates is null ? new List<string>() : design.BcSurrogates.Select(x => x.Id).ToList();
                List<string> activitiesIds = design.Activities.Select(act => act?.Id).ToList();
                activitiesIds.RemoveAll(x => String.IsNullOrWhiteSpace(x));
                List<string> scheduleTimelineIds = design.StudyScheduleTimelines is not null && design.StudyScheduleTimelines.Any() ? design.StudyScheduleTimelines.Select(x => x.Id).ToList() : new List<string>();
                design.Activities.ForEach(act =>
                {
                    List<string> tempActIDs = activitiesIds.ToList();
                    tempActIDs.RemoveAll(x => x == act.Id);
                    if (!String.IsNullOrWhiteSpace(act.PreviousActivityId) && !tempActIDs.Contains(act.PreviousActivityId))
                        errors.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}." +
                            $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                            $"{nameof(StudyDesignDto.Activities)}[{design.Activities.IndexOf(act)}]." +
                            $"{nameof(ActivityDto.PreviousActivityId)}");

                    if (!String.IsNullOrWhiteSpace(act.NextActivityId) && !tempActIDs.Contains(act.NextActivityId))
                        errors.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}." +
                          $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                          $"{nameof(StudyDesignDto.Activities)}[{design.Activities.IndexOf(act)}]." +
                          $"{nameof(ActivityDto.NextActivityId)}");

                    if (!String.IsNullOrWhiteSpace(act.ActivityTimelineId) && !scheduleTimelineIds.Contains(act.ActivityTimelineId))
                        errors.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}." +
                          $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                          $"{nameof(StudyDesignDto.Activities)}[{design.Activities.IndexOf(act)}]." +
                          $"{nameof(ActivityDto.ActivityTimelineId)}");

                    if (act.BiomedicalConceptIds != null && act.BiomedicalConceptIds.Any())
                    {
                        act.BiomedicalConceptIds.ForEach(bc =>
                        {
                            if (!String.IsNullOrWhiteSpace(bc) && !biomedicalConceptIds.Contains(bc))
                                errors.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}." +
                                           $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                                           $"{nameof(StudyDesignDto.Activities)}[{design.Activities.IndexOf(act)}]." +
                                           $"{nameof(ActivityDto.BiomedicalConceptIds)}[{act.BiomedicalConceptIds.IndexOf(bc)}]");
                        });
                    }
                    if (act.BcCategoryIds != null && act.BcCategoryIds.Any())
                    {
                        act.BcCategoryIds.ForEach(bcCat =>
                        {
                            if (!String.IsNullOrWhiteSpace(bcCat) && !bcCategoryIds.Contains(bcCat))
                                errors.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}." +
                                           $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                                           $"{nameof(StudyDesignDto.Activities)}[{design.Activities.IndexOf(act)}]." +
                                           $"{nameof(ActivityDto.BcCategoryIds)}[{act.BcCategoryIds.IndexOf(bcCat)}]");
                        });
                    }
                    if (act.BcSurrogateIds != null && act.BcSurrogateIds.Any())
                    {
                        act.BcSurrogateIds.ForEach(bcSurr =>
                        {
                            if (!String.IsNullOrWhiteSpace(bcSurr) && !bcSurrogateIds.Contains(bcSurr))
                                errors.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}." +
                                           $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                                           $"{nameof(StudyDesignDto.Activities)}[{design.Activities.IndexOf(act)}]." +
                                           $"{nameof(ActivityDto.BcSurrogateIds)}[{act.BcSurrogateIds.IndexOf(bcSurr)}]");
                        });
                    }
                });
            }

            return errors;
        }

        public static List<string> ReferenceIntegrityValidationForEncounters(StudyDesignDto design, int indexOfDesign)
        {
            List<String> errors = new();

            if (design.Encounters != null && design.Encounters.Any())
            {
                List<string> encounterIds = design.Encounters.Select(enc => enc?.Id).ToList();
                encounterIds.RemoveAll(x => String.IsNullOrWhiteSpace(x));

                List<string> allTimingIds = design.StudyScheduleTimelines is not null && design.StudyScheduleTimelines.Any() ? design.StudyScheduleTimelines.Where(x => x.ScheduleTimelineInstances != null && x.ScheduleTimelineInstances.Any())
                                                  .SelectMany(x => x.ScheduleTimelineInstances).Where(x => x.ScheduledInstanceTimings != null && x.ScheduledInstanceTimings.Any())
                                                  .SelectMany(x => x.ScheduledInstanceTimings).Select(x => x.Id).ToList() : new List<string>();
                design.Encounters.ForEach(enc =>
                {
                    List<string> tempencounterIds = encounterIds.ToList();
                    tempencounterIds.RemoveAll(x => x == enc.Id);
                    if (!String.IsNullOrWhiteSpace(enc.PreviousEncounterId) && !tempencounterIds.Contains(enc.PreviousEncounterId))
                        errors.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}." +
                            $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                            $"{nameof(StudyDesignDto.Encounters)}[{design.Encounters.IndexOf(enc)}]." +
                            $"{nameof(EncounterDto.PreviousEncounterId)}");

                    if (!String.IsNullOrWhiteSpace(enc.NextEncounterId) && !tempencounterIds.Contains(enc.NextEncounterId))
                        errors.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}." +
                          $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                          $"{nameof(StudyDesignDto.Encounters)}[{design.Encounters.IndexOf(enc)}]." +
                          $"{nameof(EncounterDto.NextEncounterId)}");

                    if (!String.IsNullOrWhiteSpace(enc.EncounterScheduledAtTimingId) && !allTimingIds.Contains(enc.EncounterScheduledAtTimingId))
                        errors.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}." +
                          $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                          $"{nameof(StudyDesignDto.Encounters)}[{design.Encounters.IndexOf(enc)}]." +
                          $"{nameof(EncounterDto.EncounterScheduledAtTimingId)}");
                });
            }

            return errors;
        }

        public static List<string> ReferenceIntegrityValidationForStudyEstimands(StudyDesignDto design, int indexOfDesign)
        {
            List<String> errors = new();

            if (design.StudyEstimands != null && design.StudyEstimands.Any())
            {
                design.StudyEstimands.ForEach(estimand =>
                {
                    List<string> investigationalInterventionIds = design.StudyInvestigationalInterventions is null ? new List<string>() : design.StudyInvestigationalInterventions.Select(x => x.Id).ToList();
                    List<string> endpointIds = design.StudyObjectives is null ? new List<string>() : design.StudyObjectives.Select(x => x?.ObjectiveEndpoints).Where(y => y != null).SelectMany(x => x.Select(y => y.Id)).ToList();

                    if (!String.IsNullOrWhiteSpace(estimand.Treatment) && !investigationalInterventionIds.Contains(estimand.Treatment))
                        errors.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}." +
                            $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                            $"{nameof(StudyDesignDto.StudyEstimands)}[{design.StudyEstimands.IndexOf(estimand)}]." +
                            $"{nameof(EstimandDto.Treatment)}");

                    if (!String.IsNullOrWhiteSpace(estimand.VariableOfInterest) && !endpointIds.Contains(estimand.VariableOfInterest))
                        errors.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}." +
                            $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                            $"{nameof(StudyDesignDto.StudyEstimands)}[{design.StudyEstimands.IndexOf(estimand)}]." +
                            $"{nameof(EstimandDto.VariableOfInterest)}");
                });
            }

            return errors;
        }

        public static List<string> ReferenceIntegrityValidationForBcCategories(StudyDesignDto design, int indexOfDesign)
        {
            List<String> errors = new();

            if (design.BcCategories != null && design.BcCategories.Any())
            {
                List<string> biomedicalConceptIds = design.BiomedicalConcepts is null ? new List<string>() : design.BiomedicalConcepts.Select(x => x.Id).ToList();
                List<string> bcCategoryIds = design.BcCategories is null ? new List<string>() : design.BcCategories.Select(x => x.Id).ToList();
                List<string> bcSurrogateIds = design.BcSurrogates is null ? new List<string>() : design.BcSurrogates.Select(x => x.Id).ToList();
                design.BcCategories.ForEach(bcCat =>
                {
                    var tempCategoryIds = bcCategoryIds.ToList();
                    tempCategoryIds.RemoveAll(x => x == bcCat.Id);
                    if (bcCat.BcCategoryParentIds != null && bcCat.BcCategoryParentIds.Any())
                    {
                        bcCat.BcCategoryParentIds.ForEach(parent =>
                        {
                            if (!String.IsNullOrWhiteSpace(parent) && !tempCategoryIds.Contains(parent))
                                errors.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}." +
                                           $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                                           $"{nameof(StudyDesignDto.BcCategories)}[{design.BcCategories.IndexOf(bcCat)}]." +
                                           $"{nameof(BiomedicalConceptCategoryDto.BcCategoryParentIds)}[{bcCat.BcCategoryParentIds.IndexOf(parent)}]");
                        });
                    }
                    if (bcCat.BcCategoryChildrenIds != null && bcCat.BcCategoryChildrenIds.Any())
                    {
                        bcCat.BcCategoryChildrenIds.ForEach(child =>
                        {
                            if (!String.IsNullOrWhiteSpace(child) && !tempCategoryIds.Contains(child))
                                errors.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}." +
                                           $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                                           $"{nameof(StudyDesignDto.BcCategories)}[{design.BcCategories.IndexOf(bcCat)}]." +
                                           $"{nameof(BiomedicalConceptCategoryDto.BcCategoryChildrenIds)}[{bcCat.BcCategoryChildrenIds.IndexOf(child)}]");
                        });
                    }
                    if (bcCat.BcCategoryMemberIds != null && bcCat.BcCategoryMemberIds.Any())
                    {
                        bcCat.BcCategoryMemberIds.ForEach(member =>
                        {
                            if (!String.IsNullOrWhiteSpace(member) && !biomedicalConceptIds.Contains(member))
                                errors.Add($"{IdFieldPropertyName.ParentElement.ClinicalStudy}." +
                                           $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                                           $"{nameof(StudyDesignDto.BcCategories)}[{design.BcCategories.IndexOf(bcCat)}]." +
                                           $"{nameof(BiomedicalConceptCategoryDto.BcCategoryMemberIds)}[{bcCat.BcCategoryMemberIds.IndexOf(member)}]");
                        });
                    }
                });
            }

            return errors;
        }

        public static object GetErrors(List<string> errorList)
        {
            JObject errors = new();
            foreach (var error in errorList)
            {
                var listMessage = new List<string> { Constants.ErrorMessages.ErrorMessageForReferenceIntegrity };
                var newProperty = new JProperty(error, listMessage);
                errors.Add(newProperty);
            }
            return errors;
        }

        #endregion
    }

}
