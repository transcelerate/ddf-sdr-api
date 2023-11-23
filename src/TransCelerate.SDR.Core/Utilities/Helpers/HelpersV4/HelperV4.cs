using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using ObjectsComparer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.StudyV4;
using TransCelerate.SDR.Core.Entities.StudyV4;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.Core.Utilities.Helpers.HelpersV4
{
    /// <summary>
    /// This class is used as a helper for different funtionalities
    /// </summary>
    public class HelperV4 : IHelperV4
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
                UsdmVersion = Constants.USDMVersions.V3
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
                        if (!Constants.StudyElementsV4.Select(x => x.ToLower()).Contains(element.ToLower()))
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
                        if (!Constants.StudyDesignElementsV4.Select(x => x.ToLower()).Contains(element.ToLower()))
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
            foreach (var item in Constants.StudyElementsV4.Select(x => x.ToLower()))
            {
                sections = sections.Select(t => t.Trim().ToLower()).ToArray();
                if (!sections.Contains(item))
                {
                    if (item == nameof(StudyDto.StudyTitle).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDto.StudyTitle).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyDto.StudyPhase).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDto.StudyPhase).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyDto.StudyType).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDto.StudyType).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyDto.StudyIdentifiers).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDto.StudyIdentifiers).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyDto.StudyDesigns).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDto.StudyDesigns).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyDto.StudyProtocolVersions).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDto.StudyProtocolVersions).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyDto.StudyVersion).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDto.StudyVersion).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyDto.BusinessTherapeuticAreas).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDto.BusinessTherapeuticAreas).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyDto.StudyAcronym).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDto.StudyAcronym).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyDto.StudyRationale).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDto.StudyRationale).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
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
                foreach (var item in Constants.StudyDesignElementsV4.Select(x => x.ToLower()))
                {
                    if (sections != null && sections.Any())
                    {
                        sections = sections.Select(t => t.Trim().ToLower()).ToArray();
                        if (!sections.Contains(item))
                        {
                            if (item == nameof(StudyDesignDto.InterventionModel).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.InterventionModel).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.TrialIntentTypes).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.TrialIntentTypes).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.TrialType).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.TrialType).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.StudyInterventions).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.StudyInterventions).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.Indications).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.Indications).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.Populations).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.Populations).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.Objectives).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.Objectives).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.StudyCells).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.StudyCells).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.ScheduleTimelines).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.ScheduleTimelines).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.Estimands).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.Estimands).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.Name).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.Name).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.Description).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.Description).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.TherapeuticAreas).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.TherapeuticAreas).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.Activities).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.Activities).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.Encounters).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.Encounters).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.Rationale).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.Rationale).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.BlindingSchema).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.BlindingSchema).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.BiomedicalConcepts).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.BiomedicalConcepts).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.BcCategories).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.BcCategories).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.BcSurrogates).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.BcSurrogates).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.Arms).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.Arms).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.Epochs).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.Epochs).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.Elements).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.Elements).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                        }
                    }
                }
                studyDesingsJArray.Add(jsonObject);
            }

            return studyDesingsJArray;
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
                return JsonObjectCheck(incoming.Study, existing.Study);
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
                changedValues.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.StudyTitle)}");
            if (currentStudyVersion.Study.StudyVersion != previousStudyVersion.Study.StudyVersion)
                changedValues.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.StudyVersion)}");
            if (currentStudyVersion.Study.StudyRationale != previousStudyVersion.Study.StudyRationale)
                changedValues.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.StudyRationale)}");
            if (currentStudyVersion.Study.StudyAcronym != previousStudyVersion.Study.StudyAcronym)
                changedValues.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.StudyAcronym)}");
            if (GetDifferences<CodeEntity>(currentStudyVersion.Study.StudyType, previousStudyVersion.Study.StudyType).Any())
                changedValues.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.StudyType)}");

            //StudyPhase
            GetDifferenceForAliasCode(currentStudyVersion.Study.StudyPhase, previousStudyVersion.Study.StudyPhase).ForEach(x =>
            {
                changedValues.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.StudyPhase)}.{x}");
            });

            //BusinessTherapeuticAreas
            if (GetDifferences<List<CodeEntity>>(currentStudyVersion.Study.BusinessTherapeuticAreas, previousStudyVersion.Study.BusinessTherapeuticAreas).Any())
                changedValues.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.BusinessTherapeuticAreas)}");

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

        public List<string> GetDifferenceForAList<T>(List<T> currentVersion, List<T> previousVersion) where T : class, Entities.StudyV4.IId
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
        public static List<string> CheckForNumberOfElementsMismatch<T>(List<T> currentVersion, List<T> previousVersion) where T : class, Entities.StudyV4.IId
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
                tempList.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.StudyIdentifiers)}");
            if (currentVersion?.Count != previousVersion?.Count)
                if (currentVersion?.Count != previousVersion?.Count)
                    tempList.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.StudyIdentifiers)}");
            GetDifferenceForAList<StudyIdentifierEntity>(currentVersion, previousVersion).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.StudyIdentifiers)}.{x}");
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
                tempList.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.StudyProtocolVersions)}");
            if (currentVersion?.Count != previousVersion?.Count)
                tempList.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.StudyProtocolVersions)}");
            GetDifferenceForAList<StudyProtocolVersionEntity>(currentVersion, previousVersion).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.StudyProtocolVersions)}.{x}");
            });
            return tempList;
        }

        public List<string> GetDifferenceForStudyDesigns(List<StudyDesignEntity> currentVersion, List<StudyDesignEntity> previousVersion)
        {
            List<string> changedValues = new();
            List<string> formattedChangedValues = new();

            if ((currentVersion is null && previousVersion is not null) || (currentVersion is not null && previousVersion is null))
                formattedChangedValues.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.StudyDesigns)}");
            if (currentVersion?.Count != previousVersion?.Count)
                formattedChangedValues.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.StudyDesigns)}");
            changedValues.AddRange(GetDifferenceForEachStudyDesigns(currentVersion, previousVersion));

            if (changedValues.Any())
            {
                changedValues.ForEach(x =>
                {
                    string addRootPath = $"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.StudyDesigns)}.{x}";
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

                        if (currentStudyDesign.Name != previousStudyDesign.Name)
                            changedValues.Add($"{nameof(StudyDesignEntity.Name)}");

                        if (currentStudyDesign.Description != previousStudyDesign.Description)
                            changedValues.Add($"{nameof(StudyDesignEntity.Description)}");

                        //StudyRationale
                        if (currentStudyDesign.Rationale != previousStudyDesign.Rationale)
                            changedValues.Add($"{nameof(StudyDesignEntity.Rationale)}");

                        //Intervention Model
                        if (GetDifferences<CodeEntity>(currentStudyDesign.InterventionModel, previousStudyDesign.InterventionModel).Any())
                            changedValues.Add($"{nameof(StudyDesignEntity.InterventionModel)}");

                        //Trial Type
                        if (GetDifferences<List<CodeEntity>>(currentStudyDesign.TrialType, previousStudyDesign.TrialType).Any())
                            changedValues.Add($"{nameof(StudyDesignEntity.TrialType)}");

                        //Trial Intent Type
                        if (GetDifferences<List<CodeEntity>>(currentStudyDesign.TrialIntentTypes, previousStudyDesign.TrialIntentTypes).Any())
                            changedValues.Add($"{nameof(StudyDesignEntity.TrialIntentTypes)}");

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
                        GetDifferenceForAliasCode(currentStudyDesign.BlindingSchema, previousStudyDesign.BlindingSchema).ForEach(x =>
                        {
                            changedValues.Add($"{nameof(StudyDesignEntity.BlindingSchema)}.{x}");
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

                        //Epochs
                        GetDifferenceForAList<StudyEpochEntity>(currentStudyDesign.Epochs, previousStudyDesign.Epochs).ForEach(x =>
                        {
                            changedValues.Add($"{nameof(StudyDesignEntity.Epochs)}.{x}");
                        });
                        //Arms
                        GetDifferenceForAList<StudyArmEntity>(currentStudyDesign.Arms, previousStudyDesign.Arms).ForEach(x =>
                        {
                            changedValues.Add($"{nameof(StudyDesignEntity.Arms)}.{x}");
                        });
                        //StudyElements
                        GetDifferenceForAList<StudyElementEntity>(currentStudyDesign.Elements, previousStudyDesign.Elements).ForEach(x =>
                        {
                            changedValues.Add($"{nameof(StudyDesignEntity.Elements)}.{x}");
                        });
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
            tempList.RemoveAll(x => x.Contains($"{nameof(BiomedicalConceptEntity.Properties)}"));
            tempList.RemoveAll(x => x.Contains($"{nameof(BiomedicalConceptEntity.ConceptCode)}"));
            currentStudyDesign.BiomedicalConcepts?.ForEach(currentBc =>
            {
                var currentBcChangedValues = new List<string>();
                if (previousStudyDesign.BiomedicalConcepts != null && previousStudyDesign.BiomedicalConcepts.Any(x => x.Id == currentBc.Id))
                {
                    var previousBc = previousStudyDesign.BiomedicalConcepts.Find(x => x.Id == currentBc.Id);
                    GetDifferenceForAList<BiomedicalConceptPropertyEntity>(currentBc.Properties, previousBc.Properties).ForEach(x =>
                    {
                        currentBcChangedValues.Add($"{nameof(StudyDesignEntity.BiomedicalConcepts)}.{nameof(BiomedicalConceptEntity.Properties)}.{x}");
                    });
                    currentBcChangedValues.RemoveAll(x => x.Contains($"{nameof(BiomedicalConceptPropertyEntity.ResponseCodes)}"));
                    currentBcChangedValues.RemoveAll(x => x.Contains($"{nameof(BiomedicalConceptPropertyEntity.Code)}"));
                    currentBc.Properties?.ForEach(currentBcProp =>
                    {
                        if (previousBc.Properties != null && previousBc.Properties.Any(x => x.Id == currentBcProp.Id))
                        {
                            var previousBcProp = previousBc.Properties.Find(x => x.Id == currentBcProp.Id);

                            GetDifferenceForAList<ResponseCodeEntity>(currentBcProp.ResponseCodes, previousBcProp.ResponseCodes).ForEach(x =>
                            {
                                currentBcChangedValues.Add($"{nameof(StudyDesignEntity.BiomedicalConcepts)}.{nameof(BiomedicalConceptEntity.Properties)}.{nameof(BiomedicalConceptPropertyEntity.ResponseCodes)}.{x}");
                            });

                            GetDifferenceForAliasCode(currentBcProp.Code, previousBcProp.Code).ForEach(x =>
                            {
                                currentBcChangedValues.Add($"{nameof(StudyDesignEntity.BiomedicalConcepts)}.{nameof(BiomedicalConceptEntity.Properties)}.{nameof(BiomedicalConceptPropertyEntity.Code)}.{x}");
                            });
                        }
                    });
                    GetDifferenceForAliasCode(currentBc.ConceptCode, previousBc.ConceptCode).ForEach(x =>
                    {
                        currentBcChangedValues.Add($"{nameof(StudyDesignEntity.BiomedicalConcepts)}.{nameof(BiomedicalConceptEntity.ConceptCode)}.{x}");
                    });
                }
                tempList.AddRange(currentBcChangedValues);
            });
            return tempList;
        }
        public List<string> GetDifferenceForStudyIndications(StudyDesignEntity currentStudyDesign, StudyDesignEntity previousStudyDesign)
        {
            var tempList = new List<string>();
            if (currentStudyDesign.Indications?.Count != previousStudyDesign.Indications?.Count)
                tempList.Add($"{nameof(StudyDesignEntity.Indications)}");
            GetDifferenceForAList<IndicationEntity>(currentStudyDesign.Indications, previousStudyDesign.Indications).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDesignEntity.Indications)}.{x}");
            });
            return tempList;
        }

        public List<string> GetDifferenceForStudyObjectives(StudyDesignEntity currentStudyDesign, StudyDesignEntity previousStudyDesign)
        {
            var tempList = new List<string>();
            if (currentStudyDesign.Objectives?.Count != previousStudyDesign.Objectives?.Count)
                tempList.Add($"{nameof(StudyDesignEntity.Objectives)}");
            GetDifferenceForAList<ObjectiveEntity>(currentStudyDesign.Objectives.Select(x=>x as ObjectiveEntity).ToList(), previousStudyDesign.Objectives.Select(x => x as ObjectiveEntity).ToList()).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDesignEntity.Objectives)}.{x}");
            });
            tempList.RemoveAll(x => x.Contains($"{nameof(ObjectiveEntity.Endpoints)}"));
            currentStudyDesign.Objectives?.Select(x=>x as ObjectiveEntity).ToList().ForEach(currentObjective =>
            {
                if (previousStudyDesign.Objectives != null && previousStudyDesign.Objectives.Any(x => x.Id == currentObjective.Id))
                {
                    var previousObjective = previousStudyDesign.Objectives.Find(x => x.Id == currentObjective.Id);

                    GetDifferenceForAList<EndpointEntity>(currentObjective.Endpoints.Select(x => x as EndpointEntity).ToList(), (previousObjective as ObjectiveEntity).Endpoints.Select(x => x as EndpointEntity).ToList()).ForEach(x =>
                    {
                        tempList.Add($"{nameof(StudyDesignEntity.Objectives)}.{nameof(ObjectiveEntity.Endpoints)}.{x}");
                    });
                }
            });
            return tempList;
        }

        public List<string> GetDifferenceForStudyInvestigationalIntervention(StudyDesignEntity currentStudyDesign, StudyDesignEntity previousStudyDesign)
        {
            var tempList = new List<string>();
            if (currentStudyDesign.StudyInterventions?.Count != previousStudyDesign.StudyInterventions?.Count)
                tempList.Add($"{nameof(StudyDesignEntity.StudyInterventions)}");
            GetDifferenceForAList<StudyInterventionEntity>(currentStudyDesign.StudyInterventions, previousStudyDesign.StudyInterventions).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDesignEntity.StudyInterventions)}.{x}");
            });
            return tempList;
        }

        public List<string> GetDifferenceForStudyPopulations(StudyDesignEntity currentStudyDesign, StudyDesignEntity previousStudyDesign)
        {
            var tempList = new List<string>();
            if (currentStudyDesign.Populations?.Count != previousStudyDesign.Populations?.Count)
                tempList.Add($"{nameof(StudyDesignEntity.Populations)}");
            GetDifferenceForAList<StudyDesignPopulationEntity>(currentStudyDesign.Populations, previousStudyDesign.Populations).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDesignEntity.Populations)}.{x}");
            });
            return tempList;
        }

        public List<string> GetDifferenceForStudyEstimands(StudyDesignEntity currentStudyDesign, StudyDesignEntity previousStudyDesign)
        {
            var tempList = new List<string>();
            if (currentStudyDesign.Estimands?.Count != previousStudyDesign.Estimands?.Count)
                tempList.Add($"{nameof(StudyDesignEntity.Estimands)}");
            GetDifferenceForAList<EstimandEntity>(currentStudyDesign.Estimands, previousStudyDesign.Estimands).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDesignEntity.Estimands)}.{x}");
            });
            tempList.RemoveAll(x => x.Contains($"{nameof(EstimandEntity.IntercurrentEvents)}"));
            currentStudyDesign.Estimands?.ForEach(currentEstimand =>
            {
                if (previousStudyDesign.Estimands != null && previousStudyDesign.Estimands.Any(x => x.Id == currentEstimand.Id))
                {
                    var previousEstimand = previousStudyDesign.Estimands.Find(x => x.Id == currentEstimand.Id);
                    GetDifferenceForAList<InterCurrentEventEntity>(currentEstimand.IntercurrentEvents, previousEstimand.IntercurrentEvents).ForEach(x =>
                    {
                        tempList.Add($"{nameof(StudyDesignEntity.Estimands)}.{nameof(EstimandEntity.IntercurrentEvents)}.{x}");
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
            return tempList;
        }

        public List<string> GetDifferenceForStudyScheduleTimelines(StudyDesignEntity currentStudyDesign, StudyDesignEntity previousStudyDesign)
        {
            var tempList = new List<string>();
            if (currentStudyDesign.ScheduleTimelines?.Count != previousStudyDesign.ScheduleTimelines?.Count)
                tempList.Add($"{nameof(StudyDesignEntity.ScheduleTimelines)}");
            GetDifferenceForAList<ScheduleTimelineEntity>(currentStudyDesign.ScheduleTimelines, previousStudyDesign.ScheduleTimelines).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDesignEntity.ScheduleTimelines)}.{x}");
            });
            tempList.RemoveAll(x => x.Contains($"{nameof(ScheduleTimelineEntity.Instances)}"));
            tempList.RemoveAll(x => x.Contains($"{nameof(ScheduleTimelineEntity.Exits)}"));
            currentStudyDesign.ScheduleTimelines?.ForEach(currentTimeline =>
            {
                var scheduleTimelineChangeList = new List<string>();
                if (previousStudyDesign.ScheduleTimelines != null && previousStudyDesign.ScheduleTimelines.Any(x => x.Id == currentTimeline.Id))
                {
                    var previousTimeline = previousStudyDesign.ScheduleTimelines.Find(x => x.Id == currentTimeline.Id);
                    GetDifferenceForAList<ScheduleTimelineExitEntity>(currentTimeline.Exits, previousTimeline.Exits).ForEach(x =>
                    {
                        scheduleTimelineChangeList.Add($"{nameof(StudyDesignEntity.ScheduleTimelines)}.{nameof(ScheduleTimelineEntity.Exits)}.{x}");
                    });
                    GetDifferenceForScheduledInstances(currentTimeline.Instances, previousTimeline.Instances).ForEach(x =>
                    {
                        scheduleTimelineChangeList.Add($"{nameof(StudyDesignEntity.ScheduleTimelines)}.{nameof(ScheduleTimelineEntity.Instances)}.{x}");
                    });
                    scheduleTimelineChangeList.RemoveAll(x => x.Contains($"{nameof(ScheduledInstanceEntity.Timings)}"));
                    currentTimeline.Instances?.ForEach(currentInstance =>
                    {
                        var scheduleTimelineTimingChangeList = new List<string>();
                        if (previousTimeline.Instances != null && previousTimeline.Instances.Any(x => x.Id == currentInstance.Id))
                        {
                            var previousInstance = previousTimeline.Instances.Find(x => x.Id == currentInstance.Id);

                            GetDifferenceForAList<TimingEntity>(currentInstance.Timings, previousInstance.Timings).ForEach(x =>
                            {
                                scheduleTimelineTimingChangeList.Add($"{nameof(StudyDesignEntity.ScheduleTimelines)}.{nameof(ScheduleTimelineEntity.Instances)}.{nameof(ScheduledInstanceEntity.Timings)}.{x}");
                            });
                        }
                        scheduleTimelineChangeList.AddRange(scheduleTimelineTimingChangeList);
                    });
                }
                tempList.AddRange(scheduleTimelineChangeList);
            });
            return tempList;
        }

        public List<string> GetDifferenceForScheduledInstances<T>(List<T> currentVersion, List<T> previousVersion) where T : Entities.StudyV4.ScheduledInstanceEntity
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
                        () => errors.AddRange(ReferenceIntegrityValidationForStudyEpochs(design, study.Study.StudyDesigns.IndexOf(design))),

                        //StudyScheduleTimelines
                        () => errors.AddRange(ReferenceIntegrityValidationForStudyScheduleTimelines(design, study.Study.StudyDesigns.IndexOf(design))),

                        //Activities 
                        () => errors.AddRange(ReferenceIntegrityValidationForActivities(design, study.Study.StudyDesigns.IndexOf(design))),

                        //Encounters
                        () => errors.AddRange(ReferenceIntegrityValidationForEncounters(design, study.Study.StudyDesigns.IndexOf(design))),

                        //Endpoints & InvestigationalIntervention
                        () => errors.AddRange(ReferenceIntegrityValidationForStudyEstimands(design, study.Study.StudyDesigns.IndexOf(design))),

                        //BcCategories
                        () => errors.AddRange(ReferenceIntegrityValidationForBcCategories(design, study.Study.StudyDesigns.IndexOf(design))),

                        //BcCategories
                        () => errors.AddRange(ReferenceIntegrityValidationForStudyCells(design, study.Study.StudyDesigns.IndexOf(design)))
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

        public static List<string> ReferenceIntegrityValidationForStudyEpochs(StudyDesignDto design, int indexOfDesign)
        {
            List<String> errors = new();

            if (design.Epochs != null && design.Epochs.Any())
            {
                List<string> studyEpochIds = design.Epochs.Select(act => act?.Id).ToList();                
                design.Epochs.ForEach(epoch =>
                {
                    List<string> tempActIDs = studyEpochIds.ToList();
                    tempActIDs.RemoveAll(x => x == epoch.Id);
                    if (!String.IsNullOrWhiteSpace(epoch.PreviousId) && !tempActIDs.Contains(epoch.PreviousId))
                        errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                            $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                            $"{nameof(StudyDesignDto.Epochs)}[{design.Epochs.IndexOf(epoch)}]." +
                            $"{nameof(StudyEpochDto.PreviousId)}");

                    if (!String.IsNullOrWhiteSpace(epoch.NextId) && !tempActIDs.Contains(epoch.NextId))
                        errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                          $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                          $"{nameof(StudyDesignDto.Epochs)}[{design.Epochs.IndexOf(epoch)}]." +
                          $"{nameof(StudyEpochDto.NextId)}");
                });
            }

            return errors;
        }

        public static List<string> ReferenceIntegrityValidationForStudyScheduleTimelines(StudyDesignDto design, int indexOfDesign)
        {
            List<String> errors = new();

            if (design.ScheduleTimelines != null && design.ScheduleTimelines.Any())
            {
                List<string> scheduledTimelineIds = design.ScheduleTimelines.Select(x => x.Id).ToList();
                List<string> encounterIds = design.Encounters is null ? new List<string>() : design.Encounters.Select(x => x.Id).ToList();
                List<string> allScheduleInstanceIds = design.ScheduleTimelines.Where(x => x.Instances != null && x.Instances.Any()).SelectMany(x => x.Instances).Select(x => x.Id).ToList();
                List<string> allActivityIds = design.Activities is null ? new List<string>() : design.Activities.Select(x => x.Id).ToList();
                List<string> epochIds = design.Epochs is null ? new List<string>() : design.Epochs.Select(x => x.Id).ToList();
                design.ScheduleTimelines.ForEach(scheduleTimeline =>
                {
                    List<string> scheduledTimelineInstanceIds = scheduleTimeline.Instances is not null && scheduleTimeline.Instances.Any() ? scheduleTimeline.Instances.Select(x => x.Id).ToList() : new List<string>();
                    List<string> scheduleTimelineExitIds = scheduleTimeline.Exits is null ? new List<string>() : scheduleTimeline.Exits.Select(x => x.Id).ToList();                    

                    if (!String.IsNullOrWhiteSpace(scheduleTimeline.EntryId) && !scheduledTimelineInstanceIds.Contains(scheduleTimeline.EntryId))
                        errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                            $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                            $"{nameof(StudyDesignDto.ScheduleTimelines)}[{design.ScheduleTimelines.IndexOf(scheduleTimeline)}].{nameof(ScheduleTimelineDto.EntryId)}");

                    if (scheduleTimeline.Instances != null && scheduleTimeline.Instances.Any())
                    {
                        List<string> instanceIds = scheduleTimeline.Instances?.Select(ins => ins?.Id).ToList();
                        scheduleTimeline.Instances.ForEach(timelineInstance =>
                        {
                            List<string> tempInstanceIds = instanceIds.ToList();
                            tempInstanceIds.RemoveAll(x => x == timelineInstance.Id);

                            if (!String.IsNullOrWhiteSpace(timelineInstance.TimelineExitId) && !scheduleTimelineExitIds.Contains(timelineInstance.TimelineExitId))
                                errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                    $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                                    $"{nameof(StudyDesignDto.ScheduleTimelines)}[{design.ScheduleTimelines.IndexOf(scheduleTimeline)}]." +
                                    $"{nameof(ScheduleTimelineDto.Instances)}[{scheduleTimeline.Instances.IndexOf(timelineInstance)}]." +
                                    $"{nameof(ScheduledInstanceDto.TimelineExitId)}");                            

                            if (!String.IsNullOrWhiteSpace(timelineInstance.TimelineId) && !scheduledTimelineIds.Contains(timelineInstance.TimelineId))
                                errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                    $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                                    $"{nameof(StudyDesignDto.ScheduleTimelines)}[{design.ScheduleTimelines.IndexOf(scheduleTimeline)}]." +
                                    $"{nameof(ScheduleTimelineDto.Instances)}[{scheduleTimeline.Instances.IndexOf(timelineInstance)}]." +
                                    $"{nameof(ScheduledInstanceDto.TimelineId)}");

                            if (!String.IsNullOrWhiteSpace(timelineInstance.EpochId) && !epochIds.Contains(timelineInstance.EpochId))
                                errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                    $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                                    $"{nameof(StudyDesignDto.ScheduleTimelines)}[{design.ScheduleTimelines.IndexOf(scheduleTimeline)}]." +
                                    $"{nameof(ScheduleTimelineDto.Instances)}[{scheduleTimeline.Instances.IndexOf(timelineInstance)}]." +
                                    $"{nameof(ScheduledInstanceDto.EpochId)}");

                            if (!String.IsNullOrWhiteSpace(timelineInstance.DefaultConditionId) && !tempInstanceIds.Contains(timelineInstance.DefaultConditionId))
                                errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                    $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                                    $"{nameof(StudyDesignDto.ScheduleTimelines)}[{design.ScheduleTimelines.IndexOf(scheduleTimeline)}]." +
                                    $"{nameof(ScheduleTimelineDto.Instances)}[{scheduleTimeline.Instances.IndexOf(timelineInstance)}]." +
                                    $"{nameof(ScheduledInstanceDto.DefaultConditionId)}");

                            if (timelineInstance.GetType() == typeof(ScheduledActivityInstanceDto))
                            {
                                var activityTimelineInstance = timelineInstance as ScheduledActivityInstanceDto;
                                var activityIds = activityTimelineInstance.ActivityIds;
                                if (activityIds is not null && activityIds.Any())
                                {
                                    activityIds.ForEach(id =>
                                    {
                                        if (!String.IsNullOrWhiteSpace(id) && !allActivityIds.Contains(id))
                                            errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                                $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                                                $"{nameof(StudyDesignDto.ScheduleTimelines)}[{design.ScheduleTimelines.IndexOf(scheduleTimeline)}]." +
                                                $"{nameof(ScheduleTimelineDto.Instances)}[{scheduleTimeline.Instances.IndexOf(timelineInstance)}]." +
                                                $"{nameof(ScheduledActivityInstanceDto.ActivityIds)}[{activityIds.IndexOf(id)}]");
                                    });
                                }
                                if (!String.IsNullOrWhiteSpace(activityTimelineInstance.EncounterId) && !encounterIds.Contains(activityTimelineInstance.EncounterId))
                                    errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                        $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                                        $"{nameof(StudyDesignDto.ScheduleTimelines)}[{design.ScheduleTimelines.IndexOf(scheduleTimeline)}]." +
                                        $"{nameof(ScheduleTimelineDto.Instances)}[{scheduleTimeline.Instances.IndexOf(timelineInstance)}]." +
                                        $"{nameof(ScheduledActivityInstanceDto.EncounterId)}");                                
                            }

                            if (timelineInstance.Timings is not null && timelineInstance.Timings.Any())
                            {
                                timelineInstance.Timings.ForEach(timing =>
                                {
                                    if (!String.IsNullOrWhiteSpace(timing.RelativeFromScheduledInstanceId) && !allScheduleInstanceIds.Contains(timing.RelativeFromScheduledInstanceId))
                                        errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                            $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                                            $"{nameof(StudyDesignDto.ScheduleTimelines)}[{design.ScheduleTimelines.IndexOf(scheduleTimeline)}]." +
                                            $"{nameof(ScheduleTimelineDto.Instances)}[{scheduleTimeline.Instances.IndexOf(timelineInstance)}]." +
                                            $"{nameof(ScheduledInstanceDto.Timings)}[{timelineInstance.Timings.IndexOf(timing)}]." +
                                            $"{nameof(TimingDto.RelativeFromScheduledInstanceId)}");

                                    if (!String.IsNullOrWhiteSpace(timing.RelativeToScheduledInstanceId) && !allScheduleInstanceIds.Contains(timing.RelativeToScheduledInstanceId))
                                        errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                            $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                                            $"{nameof(StudyDesignDto.ScheduleTimelines)}[{design.ScheduleTimelines.IndexOf(scheduleTimeline)}]." +
                                            $"{nameof(ScheduleTimelineDto.Instances)}[{scheduleTimeline.Instances.IndexOf(timelineInstance)}]." +
                                            $"{nameof(ScheduledInstanceDto.Timings)}[{timelineInstance.Timings.IndexOf(timing)}]." +
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
                List<string> scheduleTimelineIds = design.ScheduleTimelines is not null && design.ScheduleTimelines.Any() ? design.ScheduleTimelines.Select(x => x.Id).ToList() : new List<string>();
                design.Activities.ForEach(act =>
                {
                    List<string> tempActIDs = activitiesIds.ToList();
                    tempActIDs.RemoveAll(x => x == act.Id);
                    if (!String.IsNullOrWhiteSpace(act.PreviousId) && !tempActIDs.Contains(act.PreviousId))
                        errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                            $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                            $"{nameof(StudyDesignDto.Activities)}[{design.Activities.IndexOf(act)}]." +
                            $"{nameof(ActivityDto.PreviousId)}");

                    if (!String.IsNullOrWhiteSpace(act.NextId) && !tempActIDs.Contains(act.NextId))
                        errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                          $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                          $"{nameof(StudyDesignDto.Activities)}[{design.Activities.IndexOf(act)}]." +
                          $"{nameof(ActivityDto.NextId)}");

                    if (!String.IsNullOrWhiteSpace(act.TimelineId) && !scheduleTimelineIds.Contains(act.TimelineId))
                        errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                          $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                          $"{nameof(StudyDesignDto.Activities)}[{design.Activities.IndexOf(act)}]." +
                          $"{nameof(ActivityDto.TimelineId)}");

                    if (act.BiomedicalConceptIds != null && act.BiomedicalConceptIds.Any())
                    {
                        act.BiomedicalConceptIds.ForEach(bc =>
                        {
                            if (!String.IsNullOrWhiteSpace(bc) && !biomedicalConceptIds.Contains(bc))
                                errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
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
                                errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
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
                                errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                           $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                                           $"{nameof(StudyDesignDto.Activities)}[{design.Activities.IndexOf(act)}]." +
                                           $"{nameof(ActivityDto.BcSurrogateIds)}[{act.BcSurrogateIds.IndexOf(bcSurr)}]");
                        });
                    }
                });
            }

            return errors;
        }

        public static List<string> ReferenceIntegrityValidationForStudyCells(StudyDesignDto design, int indexOfDesign)
        {
            List<String> errors = new();

            if (design.StudyCells != null && design.StudyCells.Any())
            {
                List<string> epochIds = design.Epochs is null ? new List<string>() : design.Epochs.Select(x => x.Id).ToList();
                List<string> armIds = design.Arms is null ? new List<string>() : design.Arms.Select(x => x.Id).ToList();
                List<string> studyElementIds = design.Elements is null ? new List<string>() : design.Elements.Select(x => x.Id).ToList();
                
                design.StudyCells.ForEach(cell =>
                {
                    if (!String.IsNullOrWhiteSpace(cell.ArmId) && !armIds.Contains(cell.ArmId))
                        errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                   $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                                   $"{nameof(StudyDesignDto.StudyCells)}[{design.StudyCells.IndexOf(cell)}]." +
                                   $"{nameof(StudyCellDto.ArmId)}");

                    if (!String.IsNullOrWhiteSpace(cell.EpochId) && !epochIds.Contains(cell.EpochId))
                        errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                   $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                                   $"{nameof(StudyDesignDto.StudyCells)}[{design.StudyCells.IndexOf(cell)}]." +
                                   $"{nameof(StudyCellDto.EpochId)}");

                    if (cell.ElementIds != null && cell.ElementIds.Any())
                    {
                        cell.ElementIds.ForEach(elementId =>
                        {
                            if (!String.IsNullOrWhiteSpace(elementId) && !studyElementIds.Contains(elementId))
                                errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                           $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                                           $"{nameof(StudyDesignDto.StudyCells)}[{design.StudyCells.IndexOf(cell)}]." +
                                           $"{nameof(StudyCellDto.ElementIds)}[{cell.ElementIds.IndexOf(elementId)}]");
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

                List<string> allTimingIds = design.ScheduleTimelines is not null && design.ScheduleTimelines.Any() ? design.ScheduleTimelines.Where(x => x.Instances != null && x.Instances.Any())
                                                  .SelectMany(x => x.Instances).Where(x => x.Timings != null && x.Timings.Any())
                                                  .SelectMany(x => x.Timings).Select(x => x.Id).ToList() : new List<string>();
                design.Encounters.ForEach(enc =>
                {
                    List<string> tempencounterIds = encounterIds.ToList();
                    tempencounterIds.RemoveAll(x => x == enc.Id);
                    if (!String.IsNullOrWhiteSpace(enc.PreviousId) && !tempencounterIds.Contains(enc.PreviousId))
                        errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                            $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                            $"{nameof(StudyDesignDto.Encounters)}[{design.Encounters.IndexOf(enc)}]." +
                            $"{nameof(EncounterDto.PreviousId)}");

                    if (!String.IsNullOrWhiteSpace(enc.NextId) && !tempencounterIds.Contains(enc.NextId))
                        errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                          $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                          $"{nameof(StudyDesignDto.Encounters)}[{design.Encounters.IndexOf(enc)}]." +
                          $"{nameof(EncounterDto.NextId)}");

                    if (!String.IsNullOrWhiteSpace(enc.ScheduledAtTimingId) && !allTimingIds.Contains(enc.ScheduledAtTimingId))
                        errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                          $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                          $"{nameof(StudyDesignDto.Encounters)}[{design.Encounters.IndexOf(enc)}]." +
                          $"{nameof(EncounterDto.ScheduledAtTimingId)}");
                });
            }

            return errors;
        }

        public static List<string> ReferenceIntegrityValidationForStudyEstimands(StudyDesignDto design, int indexOfDesign)
        {
            List<String> errors = new();

            if (design.Estimands != null && design.Estimands.Any())
            {
                design.Estimands.ForEach(estimand =>
                {
                    List<string> investigationalInterventionIds = design.StudyInterventions is null ? new List<string>() : design.StudyInterventions.Select(x => x.Id).ToList();
                    List<string> endpointIds = design.Objectives is null ? new List<string>() : design.Objectives.Select(x => x as ObjectiveDto).ToList().Select(x => x?.Endpoints).Where(y => y != null).SelectMany(x => x.Select(y => y.Id)).ToList();

                    if (!String.IsNullOrWhiteSpace(estimand.Treatment) && !investigationalInterventionIds.Contains(estimand.Treatment))
                        errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                            $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                            $"{nameof(StudyDesignDto.Estimands)}[{design.Estimands.IndexOf(estimand)}]." +
                            $"{nameof(EstimandDto.Treatment)}");

                    if (!String.IsNullOrWhiteSpace(estimand.VariableOfInterest) && !endpointIds.Contains(estimand.VariableOfInterest))
                        errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                            $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                            $"{nameof(StudyDesignDto.Estimands)}[{design.Estimands.IndexOf(estimand)}]." +
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
                    if (bcCat.ChildrenIds != null && bcCat.ChildrenIds.Any())
                    {
                        bcCat.ChildrenIds.ForEach(child =>
                        {
                            if (!String.IsNullOrWhiteSpace(child) && !tempCategoryIds.Contains(child))
                                errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                           $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                                           $"{nameof(StudyDesignDto.BcCategories)}[{design.BcCategories.IndexOf(bcCat)}]." +
                                           $"{nameof(BiomedicalConceptCategoryDto.ChildrenIds)}[{bcCat.ChildrenIds.IndexOf(child)}]");
                        });
                    }
                    if (bcCat.MemberIds != null && bcCat.MemberIds.Any())
                    {
                        bcCat.MemberIds.ForEach(member =>
                        {
                            if (!String.IsNullOrWhiteSpace(member) && !biomedicalConceptIds.Contains(member))
                                errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                           $"{nameof(StudyDto.StudyDesigns)}[{indexOfDesign}]." +
                                           $"{nameof(StudyDesignDto.BcCategories)}[{design.BcCategories.IndexOf(bcCat)}]." +
                                           $"{nameof(BiomedicalConceptCategoryDto.MemberIds)}[{bcCat.MemberIds.IndexOf(member)}]");
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

        #region GetDifference For Version Compare Endpoint
        /// <summary>
        /// Get the differences between two studies
        /// </summary>
        /// <param name="currentStudyVersion">Current study version</param>
        /// <param name="previousStudyVersion">Previous study version</param>
        /// <returns></returns>
        public List<string> GetChangedValuesForStudyComparison(StudyDefinitionsEntity currentStudyVersion, StudyDefinitionsEntity previousStudyVersion)
        {
            List<string> changedValues = new();

            if (currentStudyVersion.Study.StudyTitle != previousStudyVersion.Study.StudyTitle)
                changedValues.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.StudyTitle)}");
            if (currentStudyVersion.Study.StudyVersion != previousStudyVersion.Study.StudyVersion)
                changedValues.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.StudyVersion)}");
            if (currentStudyVersion.Study.StudyRationale != previousStudyVersion.Study.StudyRationale)
                changedValues.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.StudyRationale)}");
            if (currentStudyVersion.Study.StudyAcronym != previousStudyVersion.Study.StudyAcronym)
                changedValues.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.StudyAcronym)}");
            if (GetDifferencesForStudyComparison<CodeEntity>(currentStudyVersion.Study.StudyType, previousStudyVersion.Study.StudyType).Any())
                changedValues.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.StudyType)}");

            //StudyPhase
            GetDifferenceForAliasCodeForStudyComparison(currentStudyVersion.Study.StudyPhase, previousStudyVersion.Study.StudyPhase).ForEach(x =>
            {
                changedValues.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.StudyPhase)}{x}");
            });

            //BusinessTherapeuticAreas
            if (GetDifferencesForStudyComparison<List<CodeEntity>>(currentStudyVersion.Study.BusinessTherapeuticAreas, previousStudyVersion.Study.BusinessTherapeuticAreas).Any())
                changedValues.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.BusinessTherapeuticAreas)}");

            //StudyIdentifiers
            changedValues.AddRange(GetDifferenceForStudyIdentifiersForStudyComparison(currentStudyVersion.Study.StudyIdentifiers, previousStudyVersion.Study.StudyIdentifiers));
            //StudyProtocolVersion
            changedValues.AddRange(GetDifferenceForStudyProtocolVersionsForStudyComparison(currentStudyVersion.Study.StudyProtocolVersions, previousStudyVersion.Study.StudyProtocolVersions));
            //Study Designs
            changedValues.AddRange(GetDifferenceForStudyDesignsForStudyComparison(currentStudyVersion.Study.StudyDesigns, previousStudyVersion.Study.StudyDesigns));


            return FormatVersionCompareValues(changedValues);
        }

        public static List<string> GetDifferencesForStudyComparison<T>(T currentVersion, T previousVersion)
        {
            var comparer = new ObjectsComparer.Comparer<T>();
            bool isEqual = comparer.Compare(currentVersion, previousVersion, out var differences);
            return differences.Select(x => x.MemberPath).ToList();
        }

        public List<string> GetDifferenceForAListForStudyComparison<T>(List<T> currentVersion, List<T> previousVersion) where T : class, Entities.StudyV4.IId
        {
            List<string> changedValues = new();
            if (currentVersion != null && currentVersion.Any())
            {
                currentVersion.ForEach(currentItem =>
                {
                    List<string> tempChangedValues = new();
                    if (previousVersion != null && previousVersion.Any(x => x.Id == currentItem.Id))
                    {
                        //Get Differences for Array Items
                        var differences = GetDifferences<T>(currentItem, previousVersion.Find(x => x.Id == currentItem.Id));
                        differences.ForEach(x =>
                        {
                            tempChangedValues.Add($"[{currentVersion.IndexOf(currentItem)}].{x}");
                        });
                        if (currentVersion.IndexOf(currentItem) != previousVersion.IndexOf(previousVersion.Find(x => x.Id == currentItem.Id)))
                            tempChangedValues.Add($"{Constants.VersionCompareConstants.ArrayBrackets}.{nameof(T)}");

                        //Get Differences for Non-Array  Items                       
                        tempChangedValues.AddRange(GetDifferenceForNonArrayElementsForStudyComparison(currentItem, previousVersion.Find(x => x.Id == currentItem.Id), tempChangedValues,currentVersion.IndexOf(currentItem)));
                    }
                    else if (previousVersion != null && currentVersion?.Count == previousVersion?.Count && !previousVersion.Any(x => x.Id == currentItem.Id))
                    {
                        tempChangedValues.Add($"{Constants.VersionCompareConstants.ArrayBrackets}.{nameof(T)}");
                    }
                    changedValues.AddRange(tempChangedValues);
                });
            }
            else if ((currentVersion is null && previousVersion is not null) || (currentVersion is not null && previousVersion is null))
                changedValues.Add($"{Constants.VersionCompareConstants.ArrayBrackets}.{nameof(T)}");
            if (currentVersion?.Count != previousVersion?.Count)
                changedValues.Add($"{Constants.VersionCompareConstants.ArrayBrackets}.{nameof(T)}");
            
            return changedValues;
        }
        public static List<string> GetDifferenceForNonArrayElementsForStudyComparison<T>(T currentVersion, T previousVersion,List<string> changedValues,int index) where T : class, Entities.StudyV4.IId
        {
            if (typeof(T) == typeof(StudyIdentifierEntity))
            {
                var currentStudyIdentifier = currentVersion as StudyIdentifierEntity;
                var previousStudyIdentifier = previousVersion as StudyIdentifierEntity;

                if (currentStudyIdentifier.StudyIdentifierScope?.Id != previousStudyIdentifier.StudyIdentifierScope?.Id)
                {
                    changedValues.RemoveAll(x => x.Contains(nameof(StudyIdentifierEntity.StudyIdentifierScope)));
                    changedValues.Add($"[{index}].{nameof(StudyIdentifierEntity.StudyIdentifierScope)}");
                }
                else
                {                    
                    if (currentStudyIdentifier.StudyIdentifierScope?.OrganizationLegalAddress?.Id != previousStudyIdentifier.StudyIdentifierScope?.OrganizationLegalAddress?.Id)
                    {
                        changedValues.RemoveAll(x => x.Contains(nameof(OrganisationEntity.OrganizationLegalAddress)));
                        changedValues.Add($"[{index}].{nameof(StudyIdentifierEntity.StudyIdentifierScope)}.{nameof(OrganisationEntity.OrganizationLegalAddress)}");
                    }
                }
            }
            if (typeof(T) == typeof(StudyElementEntity))
            {
                var currentStudyElement = currentVersion as StudyElementEntity;
                var previousStudyElement = previousVersion as StudyElementEntity;

                if (currentStudyElement.TransitionEndRule?.Id != previousStudyElement.TransitionEndRule?.Id)
                {
                    changedValues.RemoveAll(x => x.Contains(nameof(StudyElementEntity.TransitionEndRule)));
                    changedValues.Add($"[{index}].{nameof(StudyElementEntity.TransitionEndRule)}");
                }
                if (currentStudyElement.TransitionStartRule?.Id != previousStudyElement.TransitionStartRule?.Id)
                {
                    changedValues.RemoveAll(x => x.Contains(nameof(StudyElementEntity.TransitionStartRule)));
                    changedValues.Add($"[{index}].{nameof(StudyElementEntity.TransitionStartRule)}");
                }
            }
            if (typeof(T) == typeof(EncounterEntity))
            {
                var currentStudyEncounter = currentVersion as EncounterEntity;
                var previousStudyEncounter = previousVersion as EncounterEntity;

                if (currentStudyEncounter.TransitionEndRule?.Id != previousStudyEncounter.TransitionEndRule?.Id)
                {
                    changedValues.RemoveAll(x => x.Contains(nameof(EncounterEntity.TransitionEndRule)));
                    changedValues.Add($"[{index}].{nameof(EncounterEntity.TransitionEndRule)}");
                }
                if (currentStudyEncounter.TransitionStartRule?.Id != previousStudyEncounter.TransitionStartRule?.Id)
                {
                    changedValues.RemoveAll(x => x.Contains(nameof(EncounterEntity.TransitionStartRule)));
                    changedValues.Add($"[{index}].{nameof(EncounterEntity.TransitionStartRule)}");
                }
            }
            if (typeof(T) == typeof(EstimandEntity))
            {
                var currentStudyEstimand = currentVersion as EstimandEntity;
                var previousStudyEstimand = previousVersion as EstimandEntity;

                if (currentStudyEstimand.AnalysisPopulation?.Id != previousStudyEstimand.AnalysisPopulation?.Id)
                {
                    changedValues.RemoveAll(x => x.Contains(nameof(EstimandEntity.AnalysisPopulation)));
                    changedValues.Add($"[{index}].{nameof(EstimandEntity.AnalysisPopulation)}");
                }
            }
            return changedValues;
        }
        public List<string> GetDifferenceForStudyIdentifiersForStudyComparison(List<StudyIdentifierEntity> currentVersion, List<StudyIdentifierEntity> previousVersion)
        {
            var tempList = new List<string>();
            if ((currentVersion is null && previousVersion is not null) || (currentVersion is not null && previousVersion is null))
                tempList.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.StudyIdentifiers)}{Constants.VersionCompareConstants.ArrayBrackets}");
            if (currentVersion?.Count != previousVersion?.Count)
                tempList.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.StudyIdentifiers)}{Constants.VersionCompareConstants.ArrayBrackets}");

            var differencesForIdentifiersSubElements = GetDifferenceForAListForStudyComparison<StudyIdentifierEntity>(currentVersion, previousVersion);

            GetDifferenceForAListForStudyComparison<StudyIdentifierEntity>(currentVersion, previousVersion).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.StudyIdentifiers)}{x}");
            });
            return tempList;
        }

        public static List<string> GetDifferenceForAliasCodeForStudyComparison(AliasCodeEntity currentVersion, AliasCodeEntity previousVersion)
        {
            var tempList = new List<string>();
            if ((currentVersion is null && previousVersion is not null) || (currentVersion is not null && previousVersion is null))
            {
                tempList.Add(".T");
                return tempList;
            }
            if (currentVersion?.Id != previousVersion?.Id)
            {
                tempList.Add(".T");
                return tempList;
            }

            GetDifferencesForStudyComparison<AliasCodeEntity>(currentVersion, previousVersion).ForEach(x =>
            {
                tempList.Add($".{x}");
            });
            return tempList;
        }

        public List<string> GetDifferenceForStudyProtocolVersionsForStudyComparison(List<StudyProtocolVersionEntity> currentVersion, List<StudyProtocolVersionEntity> previousVersion)
        {
            var tempList = new List<string>();
            if ((currentVersion is null && previousVersion is not null) || (currentVersion is not null && previousVersion is null))
                tempList.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.StudyProtocolVersions)}{Constants.VersionCompareConstants.ArrayBrackets}");
            if (currentVersion?.Count != previousVersion?.Count)
                tempList.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.StudyProtocolVersions)}{Constants.VersionCompareConstants.ArrayBrackets}");
            GetDifferenceForAListForStudyComparison<StudyProtocolVersionEntity>(currentVersion, previousVersion).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.StudyProtocolVersions)}{x}");
            });
            return tempList;
        }

        public List<string> GetDifferenceForStudyDesignsForStudyComparison(List<StudyDesignEntity> currentVersion, List<StudyDesignEntity> previousVersion)
        {
            List<string> changedValues = new();
            List<string> formattedChangedValues = new();

            if ((currentVersion is null && previousVersion is not null) || (currentVersion is not null && previousVersion is null))
                formattedChangedValues.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.StudyDesigns)}{Constants.VersionCompareConstants.ArrayBrackets}");
            if (currentVersion?.Count != previousVersion?.Count)
                formattedChangedValues.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.StudyDesigns)}{Constants.VersionCompareConstants.ArrayBrackets}");
            changedValues.AddRange(GetDifferenceForEachStudyDesignsForStudyComparison(currentVersion, previousVersion));

            if (changedValues.Any())
            {
                changedValues.ForEach(x =>
                {
                    string addRootPath = $"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.StudyDesigns)}{x}";
                    formattedChangedValues.Add(addRootPath);
                });
            }
            return formattedChangedValues;
        }

        public List<string> GetDifferenceForEachStudyDesignsForStudyComparison(List<StudyDesignEntity> currentVersion, List<StudyDesignEntity> previousVersion)
        {            
            List<string> formattedChangedValues = new();

            if (currentVersion != null && currentVersion.Any())
            {
                currentVersion.ForEach(currentStudyDesign =>
                {
                    List<string> changedValues = new();
                    if (previousVersion != null && previousVersion.Any(x => x.Id == currentStudyDesign.Id))
                    {
                        var previousStudyDesign = previousVersion.Find(x => x.Id == currentStudyDesign.Id);

                        if (currentStudyDesign.Name != previousStudyDesign.Name)
                            changedValues.Add($"{nameof(StudyDesignEntity.Name)}");

                        if (currentStudyDesign.Description != previousStudyDesign.Description)
                            changedValues.Add($"{nameof(StudyDesignEntity.Description)}");

                        //StudyRationale
                        if (currentStudyDesign.Rationale != previousStudyDesign.Rationale)
                            changedValues.Add($"{nameof(StudyDesignEntity.Rationale)}");

                        //Intervention Model
                        if (GetDifferencesForStudyComparison<CodeEntity>(currentStudyDesign.InterventionModel, previousStudyDesign.InterventionModel).Any())
                            changedValues.Add($"{nameof(StudyDesignEntity.InterventionModel)}");

                        //Trial Type
                        if (GetDifferencesForStudyComparison<List<CodeEntity>>(currentStudyDesign.TrialType, previousStudyDesign.TrialType).Any())
                            changedValues.Add($"{nameof(StudyDesignEntity.TrialType)}");

                        //Trial Intent Type
                        if (GetDifferencesForStudyComparison<List<CodeEntity>>(currentStudyDesign.TrialIntentTypes, previousStudyDesign.TrialIntentTypes).Any())
                            changedValues.Add($"{nameof(StudyDesignEntity.TrialIntentTypes)}");

                        //TherapeuticAreas
                        if (GetDifferencesForStudyComparison<List<CodeEntity>>(currentStudyDesign.TherapeuticAreas, previousStudyDesign.TherapeuticAreas).Any())
                            changedValues.Add($"{nameof(StudyDesignEntity.TherapeuticAreas)}");

                        //StudyIndications                                                
                        changedValues.AddRange(GetDifferenceForStudyIndicationsForStudyComparison(currentStudyDesign, previousStudyDesign));

                        //Investigational Intervention                        
                        changedValues.AddRange(GetDifferenceForStudyInvestigationalInterventionForStudyComparison(currentStudyDesign, previousStudyDesign));

                        //Study Populations                        
                        changedValues.AddRange(GetDifferenceForStudyPopulationsForStudyComparison(currentStudyDesign, previousStudyDesign));
                        //Study Objectives                        
                        changedValues.AddRange(GetDifferenceForStudyObjectivesForStudyComparison(currentStudyDesign, previousStudyDesign));

                        //Estimands                        
                        changedValues.AddRange(GetDifferenceForStudyEstimandsForStudyComparison(currentStudyDesign, previousStudyDesign));

                        //Study Cells                        
                        changedValues.AddRange(GetDifferenceForStudyCellsForStudyComparison(currentStudyDesign, previousStudyDesign));

                        //Workflows                        
                        changedValues.AddRange(GetDifferenceForStudyScheduleTimelinesForStudyComparison(currentStudyDesign, previousStudyDesign));

                        //DesignBlindingScheme
                        GetDifferenceForAliasCodeForStudyComparison(currentStudyDesign.BlindingSchema, previousStudyDesign.BlindingSchema).ForEach(x =>
                        {
                            changedValues.Add($"{nameof(StudyDesignEntity.BlindingSchema)}{x}");
                        });


                        //Encounters
                        GetDifferenceForAListForStudyComparison<EncounterEntity>(currentStudyDesign.Encounters, previousStudyDesign.Encounters).ForEach(x =>
                        {
                            changedValues.Add($"{nameof(StudyDesignEntity.Encounters)}{x}");
                        });

                        //Activities
                        changedValues.AddRange(GetDifferenceForActivitiesForStudyComparison(currentStudyDesign, previousStudyDesign));

                        //Biomedical Concept Category
                        GetDifferenceForAListForStudyComparison<BiomedicalConceptCategoryEntity>(currentStudyDesign.BcCategories, previousStudyDesign.BcCategories).ForEach(x =>
                        {
                            changedValues.Add($"{nameof(StudyDesignEntity.BcCategories)}{x}");
                        });

                        //Biomedical Concept Surrogate
                        GetDifferenceForAListForStudyComparison<BiomedicalConceptSurrogateEntity>(currentStudyDesign.BcSurrogates, previousStudyDesign.BcSurrogates).ForEach(x =>
                        {
                            changedValues.Add($"{nameof(StudyDesignEntity.BcSurrogates)}{x}");
                        });

                        //Biomedical Concepts
                        changedValues.AddRange(GetDifferenceForBiomedicalConceptsForStudyComparison(currentStudyDesign, previousStudyDesign));

                        //Epochs
                        GetDifferenceForAListForStudyComparison<StudyEpochEntity>(currentStudyDesign.Epochs, previousStudyDesign.Epochs).ForEach(x =>
                        {
                            changedValues.Add($"{nameof(StudyDesignEntity.Epochs)}{x}");
                        });
                        //Arms
                        GetDifferenceForAListForStudyComparison<StudyArmEntity>(currentStudyDesign.Arms, previousStudyDesign.Arms).ForEach(x =>
                        {
                            changedValues.Add($"{nameof(StudyDesignEntity.Arms)}{x}");
                        });
                        //StudyElements
                        GetDifferenceForAListForStudyComparison<StudyElementEntity>(currentStudyDesign.Elements, previousStudyDesign.Elements).ForEach(x =>
                        {
                            changedValues.Add($"{nameof(StudyDesignEntity.Elements)}{x}");
                        });

                        changedValues.ForEach(x => formattedChangedValues.Add($"[{currentVersion.IndexOf(currentStudyDesign)}].{x}"));
                    }

                    else if (currentVersion?.Count == previousVersion?.Count && previousVersion != null && !previousVersion.Any(x => x.Id == currentStudyDesign.Id))
                    {
                        formattedChangedValues.Add($"{Constants.VersionCompareConstants.ArrayBrackets}.T");
                    }                                        
                });
            }

            return formattedChangedValues;
        }
        public List<string> GetDifferenceForBiomedicalConceptsForStudyComparison(StudyDesignEntity currentStudyDesign, StudyDesignEntity previousStudyDesign)
        {
            var tempList = new List<string>();
            if (currentStudyDesign.BiomedicalConcepts?.Count != previousStudyDesign.BiomedicalConcepts?.Count)
                tempList.Add($"{nameof(StudyDesignEntity.BiomedicalConcepts)}{Constants.VersionCompareConstants.ArrayBrackets}");
            GetDifferenceForAListForStudyComparison<BiomedicalConceptEntity>(currentStudyDesign.BiomedicalConcepts, previousStudyDesign.BiomedicalConcepts).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDesignEntity.BiomedicalConcepts)}{x}");
            });
            tempList.RemoveAll(x => x.Contains($"{nameof(BiomedicalConceptEntity.Properties)}"));
            tempList.RemoveAll(x => x.Contains($"{nameof(BiomedicalConceptEntity.ConceptCode)}"));
            currentStudyDesign.BiomedicalConcepts?.ForEach(currentBc =>
            {
                var currentBcChangedValues = new List<string>();
                if (previousStudyDesign.BiomedicalConcepts != null && previousStudyDesign.BiomedicalConcepts.Any(x => x.Id == currentBc.Id))
                {
                    var previousBc = previousStudyDesign.BiomedicalConcepts.Find(x => x.Id == currentBc.Id);
                    GetDifferenceForAListForStudyComparison<BiomedicalConceptPropertyEntity>(currentBc.Properties, previousBc.Properties).ForEach(x =>
                    {
                        currentBcChangedValues.Add($"{nameof(StudyDesignEntity.BiomedicalConcepts)}[{currentStudyDesign.BiomedicalConcepts.IndexOf(currentBc)}].{nameof(BiomedicalConceptEntity.Properties)}{x}");
                    });
                    currentBcChangedValues.RemoveAll(x => x.Contains($"{nameof(BiomedicalConceptPropertyEntity.ResponseCodes)}"));
                    currentBcChangedValues.RemoveAll(x => x.Contains($"{nameof(BiomedicalConceptPropertyEntity.Code)}"));
                    currentBc.Properties?.ForEach(currentBcProp =>
                    {
                        if (previousBc.Properties != null && previousBc.Properties.Any(x => x.Id == currentBcProp.Id))
                        {
                            var previousBcProp = previousBc.Properties.Find(x => x.Id == currentBcProp.Id);

                            GetDifferenceForAListForStudyComparison<ResponseCodeEntity>(currentBcProp.ResponseCodes, previousBcProp.ResponseCodes).ForEach(x =>
                            {
                                currentBcChangedValues.Add($"{nameof(StudyDesignEntity.BiomedicalConcepts)}[{currentStudyDesign.BiomedicalConcepts.IndexOf(currentBc)}].{nameof(BiomedicalConceptEntity.Properties)}[{currentBc.Properties.IndexOf(currentBcProp)}].{nameof(BiomedicalConceptPropertyEntity.ResponseCodes)}{x}");
                            });

                            GetDifferenceForAliasCodeForStudyComparison(currentBcProp.Code, previousBcProp.Code).ForEach(x =>
                            {
                                currentBcChangedValues.Add($"{nameof(StudyDesignEntity.BiomedicalConcepts)}[{currentStudyDesign.BiomedicalConcepts.IndexOf(currentBc)}].{nameof(BiomedicalConceptEntity.Properties)}[{currentBc.Properties.IndexOf(currentBcProp)}].{nameof(BiomedicalConceptPropertyEntity.Code)}{x}");
                            });
                        }
                    });
                    GetDifferenceForAliasCodeForStudyComparison(currentBc.ConceptCode, previousBc.ConceptCode).ForEach(x =>
                    {
                        currentBcChangedValues.Add($"{nameof(StudyDesignEntity.BiomedicalConcepts)}[{currentStudyDesign.BiomedicalConcepts.IndexOf(currentBc)}].{nameof(BiomedicalConceptEntity.ConceptCode)}{x}");
                    });
                }
                tempList.AddRange(currentBcChangedValues);
            });
            return tempList;
        }
        public List<string> GetDifferenceForStudyIndicationsForStudyComparison(StudyDesignEntity currentStudyDesign, StudyDesignEntity previousStudyDesign)
        {
            var tempList = new List<string>();
            if (currentStudyDesign.Indications?.Count != previousStudyDesign.Indications?.Count)
                tempList.Add($"{nameof(StudyDesignEntity.Indications)}{Constants.VersionCompareConstants.ArrayBrackets}");
            GetDifferenceForAListForStudyComparison<IndicationEntity>(currentStudyDesign.Indications, previousStudyDesign.Indications).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDesignEntity.Indications)}{x}");
            });
            return tempList;
        }

        public List<string> GetDifferenceForStudyObjectivesForStudyComparison(StudyDesignEntity currentStudyDesign, StudyDesignEntity previousStudyDesign)
        {
            var tempList = new List<string>();
            if (currentStudyDesign.Objectives?.Count != previousStudyDesign.Objectives?.Count)
                tempList.Add($"{nameof(StudyDesignEntity.Objectives)}{Constants.VersionCompareConstants.ArrayBrackets}");
            GetDifferenceForAListForStudyComparison<ObjectiveEntity>(currentStudyDesign.Objectives.Select(x => x as ObjectiveEntity).ToList(), previousStudyDesign.Objectives.Select(x => x as ObjectiveEntity).ToList()).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDesignEntity.Objectives)}{x}");
            });
            tempList.RemoveAll(x => x.Contains($"{nameof(ObjectiveEntity.Endpoints)}"));
            currentStudyDesign.Objectives?.Select(x => x as ObjectiveEntity).ToList().ForEach(currentObjective =>
            {
                if (previousStudyDesign.Objectives != null && previousStudyDesign.Objectives.Any(x => x.Id == currentObjective.Id))
                {
                    var previousObjective = previousStudyDesign.Objectives.Find(x => x.Id == currentObjective.Id);

                    GetDifferenceForAListForStudyComparison<EndpointEntity>(currentObjective.Endpoints.Select(x => x as EndpointEntity).ToList(), (previousObjective as ObjectiveEntity).Endpoints.Select(x => x as EndpointEntity).ToList()).ForEach(x =>
                    {
                        tempList.Add($"{nameof(StudyDesignEntity.Objectives)}[{currentStudyDesign.Objectives.IndexOf(currentObjective)}].{nameof(ObjectiveEntity.Endpoints)}{x}");
                    });
                }
            });
            return tempList;
        }

        public List<string> GetDifferenceForStudyInvestigationalInterventionForStudyComparison(StudyDesignEntity currentStudyDesign, StudyDesignEntity previousStudyDesign)
        {
            var tempList = new List<string>();
            if (currentStudyDesign.StudyInterventions?.Count != previousStudyDesign.StudyInterventions?.Count)
                tempList.Add($"{nameof(StudyDesignEntity.StudyInterventions)}{Constants.VersionCompareConstants.ArrayBrackets}");
            GetDifferenceForAListForStudyComparison<StudyInterventionEntity>(currentStudyDesign.StudyInterventions, previousStudyDesign.StudyInterventions).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDesignEntity.StudyInterventions)}{x}");
            });
            return tempList;
        }

        public List<string> GetDifferenceForStudyPopulationsForStudyComparison(StudyDesignEntity currentStudyDesign, StudyDesignEntity previousStudyDesign)
        {
            var tempList = new List<string>();
            if (currentStudyDesign.Populations?.Count != previousStudyDesign.Populations?.Count)
                tempList.Add($"{nameof(StudyDesignEntity.Populations)}{Constants.VersionCompareConstants.ArrayBrackets}");
            GetDifferenceForAListForStudyComparison<StudyDesignPopulationEntity>(currentStudyDesign.Populations, previousStudyDesign.Populations).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDesignEntity.Populations)}{x}");
            });
            return tempList;
        }

        public List<string> GetDifferenceForStudyEstimandsForStudyComparison(StudyDesignEntity currentStudyDesign, StudyDesignEntity previousStudyDesign)
        {
            var tempList = new List<string>();
            if (currentStudyDesign.Estimands?.Count != previousStudyDesign.Estimands?.Count)
                tempList.Add($"{nameof(StudyDesignEntity.Estimands)}{Constants.VersionCompareConstants.ArrayBrackets}");
            GetDifferenceForAListForStudyComparison<EstimandEntity>(currentStudyDesign.Estimands, previousStudyDesign.Estimands).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDesignEntity.Estimands)}{x}");
            });
            tempList.RemoveAll(x => x.Contains($"{nameof(EstimandEntity.IntercurrentEvents)}"));
            currentStudyDesign.Estimands?.ForEach(currentEstimand =>
            {
                if (previousStudyDesign.Estimands != null && previousStudyDesign.Estimands.Any(x => x.Id == currentEstimand.Id))
                {
                    var previousEstimand = previousStudyDesign.Estimands.Find(x => x.Id == currentEstimand.Id);
                    GetDifferenceForAListForStudyComparison<InterCurrentEventEntity>(currentEstimand.IntercurrentEvents, previousEstimand.IntercurrentEvents).ForEach(x =>
                    {
                        tempList.Add($"{nameof(StudyDesignEntity.Estimands)}[{currentStudyDesign.Estimands.IndexOf(currentEstimand)}].{nameof(EstimandEntity.IntercurrentEvents)}{x}");
                    });
                }
            });
            return tempList;
        }

        public List<string> GetDifferenceForStudyCellsForStudyComparison(StudyDesignEntity currentStudyDesign, StudyDesignEntity previousStudyDesign)
        {
            var tempList = new List<string>();
            if (currentStudyDesign.StudyCells?.Count != previousStudyDesign.StudyCells?.Count)
                tempList.Add($"{nameof(StudyDesignEntity.StudyCells)}{Constants.VersionCompareConstants.ArrayBrackets}");
            GetDifferenceForAListForStudyComparison<StudyCellEntity>(currentStudyDesign.StudyCells, previousStudyDesign.StudyCells).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDesignEntity.StudyCells)}{x}");
            });
            return tempList;
        }

        public List<string> GetDifferenceForStudyScheduleTimelinesForStudyComparison(StudyDesignEntity currentStudyDesign, StudyDesignEntity previousStudyDesign)
        {
            var tempList = new List<string>();
            if (currentStudyDesign.ScheduleTimelines?.Count != previousStudyDesign.ScheduleTimelines?.Count)
                tempList.Add($"{nameof(StudyDesignEntity.ScheduleTimelines)}{Constants.VersionCompareConstants.ArrayBrackets}");
            GetDifferenceForAListForStudyComparison<ScheduleTimelineEntity>(currentStudyDesign.ScheduleTimelines, previousStudyDesign.ScheduleTimelines).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDesignEntity.ScheduleTimelines)}{x}");
            });
            tempList.RemoveAll(x => x.Contains($"{nameof(ScheduleTimelineEntity.Instances)}"));
            tempList.RemoveAll(x => x.Contains($"{nameof(ScheduleTimelineEntity.Exits)}"));
            currentStudyDesign.ScheduleTimelines?.ForEach(currentTimeline =>
            {
                var scheduleTimelineChangeList = new List<string>();
                if (previousStudyDesign.ScheduleTimelines != null && previousStudyDesign.ScheduleTimelines.Any(x => x.Id == currentTimeline.Id))
                {
                    var previousTimeline = previousStudyDesign.ScheduleTimelines.Find(x => x.Id == currentTimeline.Id);
                    GetDifferenceForAListForStudyComparison<ScheduleTimelineExitEntity>(currentTimeline.Exits, previousTimeline.Exits).ForEach(x =>
                    {
                        scheduleTimelineChangeList.Add($"{nameof(StudyDesignEntity.ScheduleTimelines)}[{currentStudyDesign.ScheduleTimelines.IndexOf(currentTimeline)}].{nameof(ScheduleTimelineEntity.Exits)}{x}");
                    });
                    GetDifferenceForScheduledInstancesForStudyComparison(currentTimeline.Instances, previousTimeline.Instances).ForEach(x =>
                    {
                        scheduleTimelineChangeList.Add($"{nameof(StudyDesignEntity.ScheduleTimelines)}[{currentStudyDesign.ScheduleTimelines.IndexOf(currentTimeline)}].{nameof(ScheduleTimelineEntity.Instances)}{x}");
                    });
                    scheduleTimelineChangeList.RemoveAll(x => x.Contains($"{nameof(ScheduledInstanceEntity.Timings)}"));
                    currentTimeline.Instances?.ForEach(currentInstance =>
                    {
                        var scheduleTimelineTimingChangeList = new List<string>();
                        if (previousTimeline.Instances != null && previousTimeline.Instances.Any(x => x.Id == currentInstance.Id))
                        {
                            var previousInstance = previousTimeline.Instances.Find(x => x.Id == currentInstance.Id);

                            GetDifferenceForAListForStudyComparison<TimingEntity>(currentInstance.Timings, previousInstance.Timings).ForEach(x =>
                            {
                                scheduleTimelineTimingChangeList.Add($"{nameof(StudyDesignEntity.ScheduleTimelines)}[{currentStudyDesign.ScheduleTimelines.IndexOf(currentTimeline)}].{nameof(ScheduleTimelineEntity.Instances)}[{currentTimeline.Instances?.IndexOf(currentInstance)}].{nameof(ScheduledInstanceEntity.Timings)}{x}");
                            });
                        }
                        scheduleTimelineChangeList.AddRange(scheduleTimelineTimingChangeList);
                    });
                }
                tempList.AddRange(scheduleTimelineChangeList);
            });
            return tempList;
        }

        public List<string> GetDifferenceForScheduledInstancesForStudyComparison<T>(List<T> currentVersion, List<T> previousVersion) where T : Entities.StudyV4.ScheduledInstanceEntity
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
                            changedValues.Add($"{Constants.VersionCompareConstants.ArrayBrackets}.{nameof(T)}");

                        if (previousItem.GetType() == currentItem.GetType())
                        {
                            
                            if (currentItem.GetType() == typeof(ScheduledActivityInstanceEntity))
                            {
                                var differences = GetDifferencesForStudyComparison(currentItem as ScheduledActivityInstanceEntity, previousItem as ScheduledActivityInstanceEntity);
                                if (differences.Any())
                                {
                                    differences.ForEach(x =>
                                    {
                                        changedValues.Add($"[{currentVersion.IndexOf(currentItem)}].{x}");
                                    });                                    
                                }                                    
                            }                                
                            if (currentItem.GetType() == typeof(ScheduledDecisionInstanceEntity))
                            {
                                var differences = GetDifferencesForStudyComparison(currentItem as ScheduledDecisionInstanceEntity, previousItem as ScheduledDecisionInstanceEntity);
                                if (differences.Any())
                                {
                                    differences.ForEach(difference => 
                                    {
                                        var conditionAssignmentsIndexRemoved = difference.Replace(Constants.StringToBeRemovedForChangeAudit.ConditionAssignmentsValue, "");
                                        changedValues.Add($"[{currentVersion.IndexOf(currentItem)}].{conditionAssignmentsIndexRemoved}");
                                    });
                                }
                            }
                        }
                        if (currentVersion.IndexOf(currentItem) != previousVersion.IndexOf(previousItem))
                            changedValues.Add($"{Constants.VersionCompareConstants.ArrayBrackets}.{nameof(T)}");
                    }
                    else if (previousVersion != null && currentVersion?.Count == previousVersion?.Count && !previousVersion.Any(x => x.Id == currentItem.Id))
                    {
                        changedValues.Add($"{Constants.VersionCompareConstants.ArrayBrackets}.{nameof(T)}");
                    }
                });
            }
            else if ((currentVersion is null && previousVersion is not null) || (currentVersion is not null && previousVersion is null))
                changedValues.Add($"{Constants.VersionCompareConstants.ArrayBrackets}.{nameof(T)}");
            if (currentVersion?.Count != previousVersion?.Count)
                changedValues.Add($"{Constants.VersionCompareConstants.ArrayBrackets}.{nameof(T)}");
            return changedValues;
        }
        public List<string> GetDifferenceForActivitiesForStudyComparison(StudyDesignEntity currentStudyDesign, StudyDesignEntity previousStudyDesign)
        {
            var tempList = new List<string>();
            if (currentStudyDesign.Activities?.Count != previousStudyDesign.Activities?.Count)
                tempList.Add($"{nameof(StudyDesignEntity.Activities)}{Constants.VersionCompareConstants.ArrayBrackets}");
            GetDifferenceForAListForStudyComparison<ActivityEntity>(currentStudyDesign.Activities, previousStudyDesign.Activities).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDesignEntity.Activities)}{x}");
            });
            tempList.RemoveAll(x => x.Contains($"{nameof(ActivityEntity.DefinedProcedures)}"));
            currentStudyDesign.Activities?.ForEach(currentActivitiy =>
            {
                if (previousStudyDesign.Activities != null && previousStudyDesign.Activities.Any(x => x.Id == currentActivitiy.Id))
                {
                    var previousActivity = previousStudyDesign.Activities.Find(x => x.Id == currentActivitiy.Id);

                    GetDifferenceForAListForStudyComparison<ProcedureEntity>(currentActivitiy.DefinedProcedures, previousActivity.DefinedProcedures).ForEach(x =>
                    {
                        tempList.Add($"{nameof(StudyDesignEntity.Activities)}[{currentStudyDesign.Activities.IndexOf(currentActivitiy)}].{nameof(ActivityEntity.DefinedProcedures)}{x}");
                    });
                }
            });
            return tempList;
        }

        public static List<string> FormatVersionCompareValues(List<string> changes)
        {
            List<string> formattedList = new();

            changes?.ForEach(change =>
            {
                //Remove Code field values
                var stringSegments = change.Split(".").ToList();
                if (Constants.CharactersToBeRemovedForVersionCompare.ToList().Any(x => x == stringSegments.Last()))
                {
                    //To remove code field property names
                    stringSegments = stringSegments.SkipLast(1).ToList();
                    //To remove the array bracket with the index number for code fields
                    var stringToRemoveArrayBracketForCode = stringSegments.Last();
                    stringSegments = stringSegments.SkipLast(1).ToList();
                    stringToRemoveArrayBracketForCode = Regex.Replace(stringToRemoveArrayBracketForCode, "[0-9]", string.Empty, RegexOptions.None, TimeSpan.FromMilliseconds(1000));
                    Constants.ParanthesisToBeRemovedForAudit.ToList().ForEach(character =>
                    {
                        stringToRemoveArrayBracketForCode = stringToRemoveArrayBracketForCode.Replace(character, string.Empty);
                    });
                   
                    stringSegments.Add(stringToRemoveArrayBracketForCode);
                }
                // Add [] for code array fields
                if (Constants.CodeFieldArrayElements.V3.ToList().Any(arrayField => arrayField == stringSegments.Last()))
                {
                    var stringToAddArrayBracketForCode = stringSegments.Last();
                    stringToAddArrayBracketForCode += Constants.VersionCompareConstants.ArrayBrackets;
                    stringSegments = stringSegments.SkipLast(1).ToList();
                    stringSegments.Add(stringToAddArrayBracketForCode);
                }
                //Remove T
                if (stringSegments.Last() == "T")
                    stringSegments = stringSegments.SkipLast(1).ToList();

                change = string.Join(".", stringSegments);


                formattedList.Add(change.ChangeToCamelCase());
            });
            
            return formattedList.Distinct().ToList();
        }
        #endregion
    }

}
