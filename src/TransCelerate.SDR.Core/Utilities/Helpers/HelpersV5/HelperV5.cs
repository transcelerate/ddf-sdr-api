using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using ObjectsComparer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.StudyV5;
using TransCelerate.SDR.Core.Entities.StudyV5;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.Core.Utilities.Helpers.HelpersV5
{
    /// <summary>
    /// This class is used as a helper for different funtionalities
    /// </summary>
    public class HelperV5 : IHelperV5
    {
        /// <summary>
        /// Get Audit Trail fields for the POST Api
        /// </summary>
        /// <param name="usdmVersion"></param>
        /// <returns></returns>
        public AuditTrailEntity GetAuditTrail(string usdmVersion)
        {
            return new AuditTrailEntity
            {
                EntryDateTime = DateTime.UtcNow,
                UsdmVersion = usdmVersion
            };
        }

        public JsonSerializerSettings GetSerializerSettingsForCamelCasingAndEscapeHandling()
        {
            return new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                StringEscapeHandling = StringEscapeHandling.EscapeNonAscii,
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
            listofElementsArray = listofelements?.Split(Constants.Separators.Comma);
            if (listofelements is not null)
            {
                if (listofElementsArray is not null)
                {
                    foreach (string element in listofElementsArray)
                    {
                        if (!Constants.StudyElementsV5.Select(x => x.ToLower()).Contains(element.ToLower()))
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
            listofElementsArray = listofelements?.Split(Constants.Separators.Comma);
            if (listofelements is not null)
            {
                if (listofElementsArray is not null)
                {
                    foreach (string element in listofElementsArray)
                    {
                        if (!Constants.StudyDesignElementsV5.Select(x => x.ToLower()).Contains(element.ToLower()))
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
            var serializer = GetSerializerSettingsForCamelCasingAndEscapeHandling();
            var jsonObject = JObject.Parse(JsonConvert.SerializeObject(studyDTO, serializer));
            jsonObject.Property(string.Concat(nameof(StudyDefinitionsEntity.AuditTrail)[..1].ToLower(), nameof(StudyDefinitionsEntity.AuditTrail).AsSpan(1))).Remove();
            jsonObject.Property("links").Remove();
            jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDto.DocumentedBy).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
            foreach (var item in Constants.StudyElementsV5.Select(x => x.ToLower()))
            {
                sections = sections.Select(t => t.Trim().ToLower()).ToArray();
                if (!sections.Contains(item))
                {
                    if (item == nameof(StudyVersionDto.Titles).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyVersionDto.Titles).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyVersionDto.StudyIdentifiers).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyVersionDto.StudyIdentifiers).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyVersionDto.StudyDesigns).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyVersionDto.StudyDesigns).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyVersionDto.DocumentVersionIds).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyVersionDto.DocumentVersionIds).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyVersionDto.VersionIdentifier).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyVersionDto.VersionIdentifier).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyVersionDto.BusinessTherapeuticAreas).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyVersionDto.BusinessTherapeuticAreas).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyVersionDto.Rationale).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyVersionDto.Rationale).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyVersionDto.DateValues).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyVersionDto.DateValues).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyVersionDto.Amendments).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyVersionDto.Amendments).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyVersionDto.Conditions).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyVersionDto.Conditions).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyVersionDto.BcSurrogates).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyVersionDto.BcSurrogates).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyVersionDto.BcCategories).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyVersionDto.BcCategories).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyVersionDto.Dictionaries).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyVersionDto.Dictionaries).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyVersionDto.BiomedicalConcepts).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyVersionDto.BiomedicalConcepts).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyDto.Name).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDto.Name).ChangeToCamelCase() && attr.Parent.Path == nameof(StudyDefinitionsDto.Study).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyDto.Description).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDto.Description).ChangeToCamelCase() && attr.Parent.Path == nameof(StudyDefinitionsDto.Study).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                    else if (item == nameof(StudyDto.Label).ToLower())
                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDto.Label).ChangeToCamelCase() && attr.Parent.Path == nameof(StudyDefinitionsDto.Study).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
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
            var serializer = GetSerializerSettingsForCamelCasingAndEscapeHandling();
            var studyDesingsJArray = new JArray();


            foreach (var studyDesign in studyDesigns)
            {
                var jsonObject = JObject.Parse(JsonConvert.SerializeObject(studyDesign, serializer));
                foreach (var item in Constants.StudyDesignElementsV5.Select(x => x.ToLower()))
                {
                    if (sections != null && sections.Any())
                    {
                        sections = sections.Select(t => t.Trim().ToLower()).ToArray();
                        if (!sections.Contains(item))
                        {
                            if (item == nameof(StudyDesignDto.Name).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.Name).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.Label).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.Label).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.Description).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.Description).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.StudyInterventionIds).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.StudyInterventionIds).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.Indications).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.Indications).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.Population).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.Population).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.Objectives).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.Objectives).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.StudyCells).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.StudyCells).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.ScheduleTimelines).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.ScheduleTimelines).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.Estimands).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.Estimands).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.TherapeuticAreas).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.TherapeuticAreas).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.Activities).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.Activities).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.Encounters).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.Encounters).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.Rationale).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.Rationale).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.Arms).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.Arms).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.Epochs).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.Epochs).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.Elements).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.Elements).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.DocumentVersionIds).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.DocumentVersionIds).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.Characteristics).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.Characteristics).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.StudyPhase).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.StudyPhase).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
                            else if (item == nameof(StudyDesignDto.StudyType).ToLower())
                                jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(StudyDesignDto.StudyType).ChangeToCamelCase()).ToList().ForEach(x => x.Remove());
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

            changedValues = GetDifferences(currentStudyVersion, previousStudyVersion);

            changedValues.RemoveAll(x => x.Contains(nameof(StudyEntity.Versions)));
            changedValues.RemoveAll(x => x.Contains(nameof(StudyEntity.DocumentedBy)));
            changedValues.RemoveAll(x => x.Contains(nameof(StudyDefinitionsEntity.AuditTrail.EntryDateTime)));
            changedValues.RemoveAll(x => x.Contains(nameof(StudyDefinitionsEntity.AuditTrail.SDRUploadVersion)));

            changedValues.AddRange(GetChangedValuesForStudyVersion(currentStudyVersion.Study.Versions, previousStudyVersion.Study.Versions));

            //if (currentStudyVersion.Study.DocumentedBy?.Id != previousStudyVersion.Study.DocumentedBy?.Id) //old code
            //if (
            //   !currentStudyVersion.Study.DocumentedBy.Select(doc => doc.Id).OrderBy(id => id)
            // .SequenceEqual(previousStudyVersion.Study.DocumentedBy.Select(doc => doc.Id).OrderBy(id => id)))
            //changedValues.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.DocumentedBy)}");

            var currentIds = currentStudyVersion.Study.DocumentedBy?.Select(doc => doc.Id).OrderBy(id => id) ?? Enumerable.Empty<string>();
            var previousIds = previousStudyVersion.Study.DocumentedBy?.Select(doc => doc.Id).OrderBy(id => id) ?? Enumerable.Empty<string>();
            if (!currentIds.SequenceEqual(previousIds))
            {
                changedValues.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.DocumentedBy)}");
            }

            else
            {
                var tempList = new List<string>();
                tempList.AddRange(GetDifferences(currentStudyVersion.Study.DocumentedBy, previousStudyVersion.Study.DocumentedBy));
                tempList.RemoveAll(x => x.Contains(nameof(StudyDefinitionDocumentEntity.Versions)));
                //tempList.AddRange(GetDifferenceForStudyDefinitionDocumentVersions(currentStudyVersion.Study.DocumentedBy?.Versions, previousStudyVersion.Study.DocumentedBy?.Versions));
                if (currentStudyVersion.Study.DocumentedBy != null && previousStudyVersion.Study.DocumentedBy != null)
                {
                    tempList.AddRange(currentStudyVersion.Study.DocumentedBy
                        .SelectMany(currentDocument =>
                            GetDifferenceForStudyDefinitionDocumentVersions(
                                currentDocument.Versions,
                                previousStudyVersion.Study.DocumentedBy
                                    .FirstOrDefault(doc => doc.Id == currentDocument.Id)?.Versions)
                        ));
                }
                changedValues.AddRange(tempList);
            }

            return FormatVersionCompareValues(changedValues);
        }
        public List<string> GetChangedValuesForStudyVersion(List<StudyVersionEntity> currentVersion, List<StudyVersionEntity> previousVersion)
        {
            List<string> changedValues = new();
            List<string> formattedChangedValues = new();

            if (currentVersion?.Count != previousVersion?.Count)
                changedValues.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.Versions)}");

            changedValues.AddRange(GetDifferenceForAListForStudyComparison(currentVersion, previousVersion));
            changedValues.RemoveAll(x => x.Contains($"{nameof(StudyVersionEntity.DateValues)}"));
            changedValues.RemoveAll(x => x.Contains($"{nameof(StudyVersionEntity.Amendments)}"));
            changedValues.RemoveAll(x => x.Contains($"{nameof(StudyVersionEntity.StudyIdentifiers)}"));
            changedValues.RemoveAll(x => x.Contains($"{nameof(StudyVersionEntity.BusinessTherapeuticAreas)}"));
            changedValues.RemoveAll(x => x.Contains($"{nameof(StudyVersionEntity.StudyDesigns)}"));

            currentVersion?.ForEach(currVer =>
            {
                if (previousVersion != null && previousVersion.Any(x => x.Id == currVer.Id))
                {
                    var prevVer = previousVersion.Find(x => x.Id == currVer.Id);
                    var currentVersionIndex = currentVersion.IndexOf(currVer);
                    if (currVer.VersionIdentifier != prevVer.VersionIdentifier)
                        changedValues.Add($"{nameof(StudyVersionEntity.VersionIdentifier)}");
                    if (currVer.Rationale != prevVer.Rationale)
                        changedValues.Add($"{nameof(StudyVersionEntity.Rationale)}");

                    //Titles
                    GetDifferenceForAList<StudyTitleEntity>(currVer.Titles, prevVer.Titles).ForEach(x =>
                    {
                        changedValues.Add($"{nameof(StudyVersionEntity.Titles)}{x}");
                    });

                    //BusinessTherapeuticAreas
                    if (GetDifferences<List<CodeEntity>>(currVer.BusinessTherapeuticAreas, prevVer.BusinessTherapeuticAreas).Any())
                        changedValues.Add($"{nameof(StudyVersionEntity.BusinessTherapeuticAreas)}");
                    //StudyIdentifiers
                    GetDifferenceForStudyIdentifiers(currVer.StudyIdentifiers, prevVer.StudyIdentifiers).ForEach(x =>
                    {
                        changedValues.Add($"{x}");
                    });
                    //DateValues
                    GetDifferenceForGovernanceDate(currVer.DateValues, prevVer.DateValues).ForEach(x =>
                    {
                        changedValues.Add($".{nameof(StudyVersionEntity.DateValues)}.{x}");
                    });
                    //Ammendments
                    GetDifferenceForStudyAmendments(currVer.Amendments, prevVer.Amendments).ForEach(x =>
                    {
                        changedValues.Add($".{nameof(StudyVersionEntity.Amendments)}.{x}");
                    });
                    //Study Designs
                    GetDifferenceForStudyDesigns(currVer.StudyDesigns, prevVer.StudyDesigns).ForEach(x =>
                    {
                        changedValues.Add($"{x}");
                    });
                    //Conditions
                    GetDifferenceForAList<ConditionEntity>(currVer.Conditions, prevVer.Conditions).ForEach(x =>
                    {
                        changedValues.Add($"{nameof(StudyVersionEntity.Conditions)}.{x}");
                    });
                    //Biomedical Concept Surrogate
                    GetDifferenceForAList<BiomedicalConceptSurrogateEntity>(currVer.BcSurrogates, prevVer.BcSurrogates).ForEach(x =>
                    {
                        changedValues.Add($"{nameof(StudyVersionEntity.BcSurrogates)}.{x}");
                    });
                    //Biomedical Concept Category
                    GetDifferenceForAList<BiomedicalConceptCategoryEntity>(currVer.BcCategories, prevVer.BcCategories).ForEach(x =>
                    {
                        changedValues.Add($"{nameof(StudyVersionEntity.BcCategories)}.{x}");
                    });

                    //Biomedical Concepts
                    changedValues.AddRange(GetDifferenceForBiomedicalConcepts(currVer, prevVer));
                }
            });

            if (changedValues.Any())
            {
                changedValues.ForEach(x =>
                {
                    string addRootPath = $"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.Versions)}{x}";
                    formattedChangedValues.Add(addRootPath);
                });
            }
            return formattedChangedValues;
        }
        public static List<string> GetDifferences<T>(T currentVersion, T previousVersion)
        {
            var comparer = new ObjectsComparer.Comparer<T>();
            bool isEqual = comparer.Compare(currentVersion, previousVersion, out var differences);
            return differences.Select(x => x.MemberPath).ToList();
        }

        public List<string> GetDifferenceForAList<T>(List<T> currentVersion, List<T> previousVersion) where T : class, Entities.StudyV5.IId
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
        public List<string> GetDifferenceForAList(List<string> currentVersion, List<string> previousVersion)
        {
            List<string> changedValues = new();

            if (currentVersion?.Count != previousVersion?.Count ||
                !currentVersion.SequenceEqual(previousVersion))
            {
                changedValues.Add("List<string>");
            }

            return changedValues;
        }

        public static List<string> CheckForNumberOfElementsMismatch<T>(List<T> currentVersion, List<T> previousVersion) where T : class, Entities.StudyV5.IId
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
                tempList.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.Versions)}.{nameof(StudyVersionEntity.StudyIdentifiers)}");
            if (currentVersion?.Count != previousVersion?.Count)
                if (currentVersion?.Count != previousVersion?.Count)
                    tempList.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.Versions)}.{nameof(StudyVersionEntity.StudyIdentifiers)}");
            GetDifferenceForAList<StudyIdentifierEntity>(currentVersion, previousVersion).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.Versions)}.{nameof(StudyVersionEntity.StudyIdentifiers)}.{x}");
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

        public List<string> GetDifferenceForStudyDefinitionDocumentVersions(List<StudyDefinitionDocumentVersionEntity> currentVersion, List<StudyDefinitionDocumentVersionEntity> previousVersion)
        {
            var tempList = new List<string>();

            if (currentVersion?.Count != previousVersion?.Count)
                tempList.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.DocumentedBy)}.{nameof(StudyDefinitionDocumentEntity.Versions)}");


            tempList.AddRange(GetDifferenceForAList(currentVersion, previousVersion));

            tempList.RemoveAll(x => x.Contains($"{nameof(StudyDefinitionDocumentVersionEntity.DateValues)}"));
            tempList.RemoveAll(x => x.Contains($"{nameof(StudyDefinitionDocumentVersionEntity.Contents)}"));

            currentVersion?.ForEach(currProtDocVersion =>
            {
                if (previousVersion != null && previousVersion.Any(x => x.Id == currProtDocVersion.Id))
                {
                    var prevProtDocVersion = previousVersion.Find(x => x.Id == currProtDocVersion.Id);

                    GetDifferenceForGovernanceDate(currProtDocVersion.DateValues, prevProtDocVersion.DateValues).ForEach(x =>
                    {
                        tempList.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.DocumentedBy)}.{nameof(StudyDefinitionDocumentEntity.Versions)}.{x}");
                    });
                    GetDifferenceForAList<NarrativeContentEntity>(currProtDocVersion.Contents, prevProtDocVersion.Contents).ForEach(x =>
                    {
                        tempList.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.DocumentedBy)}.{nameof(StudyDefinitionDocumentEntity.Versions)}.{x}");
                    });
                }
            });

            return tempList;
        }

        public List<string> GetDifferenceForStudyDesigns(List<StudyDesignEntity> currentVersion, List<StudyDesignEntity> previousVersion)
        {
            List<string> changedValues = new();
            List<string> formattedChangedValues = new();

            if ((currentVersion is null && previousVersion is not null) || (currentVersion is not null && previousVersion is null))
                formattedChangedValues.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.Versions)}.{nameof(StudyVersionEntity.StudyDesigns)}");
            if (currentVersion?.Count != previousVersion?.Count)
                formattedChangedValues.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.Versions)}.{nameof(StudyVersionEntity.StudyDesigns)}");
            changedValues.AddRange(GetDifferenceForEachStudyDesigns(currentVersion, previousVersion));

            if (changedValues.Any())
            {
                changedValues.ForEach(x =>
                {
                    string addRootPath = $"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.Versions)}.{nameof(StudyVersionEntity.StudyDesigns)}.{x}";
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

                        if (currentStudyDesign.Label != previousStudyDesign.Label)
                            changedValues.Add($"{nameof(StudyDesignEntity.Label)}");

                        //StudyRationale
                        if (currentStudyDesign.Rationale != previousStudyDesign.Rationale)
                            changedValues.Add($"{nameof(StudyDesignEntity.Rationale)}");

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

                        //Timelines                        
                        changedValues.AddRange(GetDifferenceForStudyScheduleTimelines(currentStudyDesign, previousStudyDesign));

                        //Encounters
                        GetDifferenceForAList<EncounterEntity>(currentStudyDesign.Encounters, previousStudyDesign.Encounters).ForEach(x =>
                        {
                            changedValues.Add($"{nameof(StudyDesignEntity.Encounters)}.{x}");
                        });

                        //Activities
                        changedValues.AddRange(GetDifferenceForActivities(currentStudyDesign, previousStudyDesign));

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
                        //StudyPhase
                        GetDifferenceForAliasCode(currentStudyDesign.StudyPhase, previousStudyDesign.StudyPhase).ForEach(x =>
                        {
                            changedValues.Add($"{nameof(StudyDesignEntity.StudyPhase)}.{x}");
                        });
                        //StudyType
                        GetDifferencesForStudyComparison(currentStudyDesign.StudyType, previousStudyDesign.StudyType).ForEach(x =>
                        {
                            changedValues.Add($"{nameof(StudyDesignEntity.StudyType)}.{x}");
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
        public List<string> GetDifferenceForBiomedicalConcepts(StudyVersionEntity currentStudyVersion, StudyVersionEntity previousStudyVersion)
        {
            var tempList = new List<string>();
            if (currentStudyVersion.BiomedicalConcepts?.Count != previousStudyVersion.BiomedicalConcepts?.Count)
                tempList.Add($"{nameof(StudyVersionEntity.BiomedicalConcepts)}");
            GetDifferenceForAList<BiomedicalConceptEntity>(currentStudyVersion.BiomedicalConcepts, previousStudyVersion.BiomedicalConcepts).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyVersionEntity.BiomedicalConcepts)}.{x}");
            });
            tempList.RemoveAll(x => x.Contains($"{nameof(BiomedicalConceptEntity.Properties)}"));
            tempList.RemoveAll(x => x.Contains($"{nameof(BiomedicalConceptEntity.Code)}"));
            currentStudyVersion.BiomedicalConcepts?.ForEach(currentBc =>
            {
                var currentBcChangedValues = new List<string>();
                if (previousStudyVersion.BiomedicalConcepts != null && previousStudyVersion.BiomedicalConcepts.Any(x => x.Id == currentBc.Id))
                {
                    var previousBc = previousStudyVersion.BiomedicalConcepts.Find(x => x.Id == currentBc.Id);
                    GetDifferenceForAList<BiomedicalConceptPropertyEntity>(currentBc.Properties, previousBc.Properties).ForEach(x =>
                    {
                        currentBcChangedValues.Add($"{nameof(StudyVersionEntity.BiomedicalConcepts)}.{nameof(BiomedicalConceptEntity.Properties)}.{x}");
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
                                currentBcChangedValues.Add($"{nameof(StudyVersionEntity.BiomedicalConcepts)}.{nameof(BiomedicalConceptEntity.Properties)}.{nameof(BiomedicalConceptPropertyEntity.ResponseCodes)}.{x}");
                            });

                            GetDifferenceForAliasCode(currentBcProp.Code, previousBcProp.Code).ForEach(x =>
                            {
                                currentBcChangedValues.Add($"{nameof(StudyVersionEntity.BiomedicalConcepts)}.{nameof(BiomedicalConceptEntity.Properties)}.{nameof(BiomedicalConceptPropertyEntity.Code)}.{x}");
                            });
                        }
                    });
                    GetDifferenceForAliasCode(currentBc.Code, previousBc.Code).ForEach(x =>
                    {
                        currentBcChangedValues.Add($"{nameof(StudyVersionEntity.BiomedicalConcepts)}.{nameof(BiomedicalConceptEntity.Code)}.{x}");
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
            GetDifferenceForAList<ObjectiveEntity>(currentStudyDesign.Objectives.Select(x => x).ToList(), previousStudyDesign.Objectives.Select(x => x).ToList()).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDesignEntity.Objectives)}.{x}");
            });
            tempList.RemoveAll(x => x.Contains($"{nameof(ObjectiveEntity.Endpoints)}"));
            currentStudyDesign.Objectives?.Select(x => x).ToList().ForEach(currentObjective =>
            {
                if (previousStudyDesign.Objectives != null && previousStudyDesign.Objectives.Any(x => x.Id == currentObjective.Id))
                {
                    var previousObjective = previousStudyDesign.Objectives.Find(x => x.Id == currentObjective.Id);

                    GetDifferenceForAList<EndpointEntity>(currentObjective.Endpoints.Select(x => x).ToList(), previousObjective.Endpoints.Select(x => x).ToList()).ForEach(x =>
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
            if (currentStudyDesign.StudyInterventionIds?.Count != previousStudyDesign.StudyInterventionIds?.Count)
                tempList.Add($"{nameof(StudyDesignEntity.StudyInterventionIds)}");
            GetDifferenceForAList(currentStudyDesign.StudyInterventionIds, previousStudyDesign.StudyInterventionIds).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDesignEntity.StudyInterventionIds)}.{x}");
            });
            return tempList;
        }

        public List<string> GetDifferenceForStudyPopulations(StudyDesignEntity currentStudyDesign, StudyDesignEntity previousStudyDesign)
        {
            var tempList = new List<string>();
            if (currentStudyDesign.Population?.Id != previousStudyDesign.Population?.Id)
                tempList.Add($"{nameof(StudyDesignEntity.Population)}");

            else if (currentStudyDesign.Population is not null && previousStudyDesign is not null)
            {
                GetDifferences<StudyDesignPopulationEntity>(currentStudyDesign.Population, previousStudyDesign.Population).ForEach(x =>
                {
                    tempList.Add($"{nameof(StudyDesignEntity.Population)}{x}");
                });
                tempList.RemoveAll(x => x.Contains($"{nameof(StudyDesignPopulationEntity.Cohorts)}"));
                tempList.RemoveAll(x => x.Contains($"{nameof(StudyDesignPopulationEntity.CriterionIds)}"));

                GetDifferenceForAList<StudyCohortEntity>(currentStudyDesign.Population.Cohorts, previousStudyDesign.Population.Cohorts).ForEach(x =>
                {
                    tempList.Add($"{nameof(StudyDesignEntity.Population)}.{nameof(StudyDesignPopulationEntity.Cohorts)}{x}");
                });
                GetDifferenceForAList(currentStudyDesign.Population.CriterionIds, previousStudyDesign.Population.CriterionIds).ForEach(x =>
                {
                    tempList.Add($"{nameof(StudyDesignEntity.Population)}.{nameof(StudyDesignPopulationEntity.CriterionIds)}{x}");
                });
                tempList.RemoveAll(x => x.Contains($"{nameof(StudyCohortEntity.Characteristics)}"));
                currentStudyDesign.Population.Cohorts?.ForEach(currCohort =>
                {
                    if (previousStudyDesign.Population.Cohorts != null && previousStudyDesign.Population.Cohorts.Any(x => x.Id == currCohort.Id))
                    {
                        var prevCohort = previousStudyDesign.Population.Cohorts.Find(x => x.Id == currCohort.Id);
                        GetDifferenceForAList<CharacteristicEntity>(currCohort.Characteristics, currCohort.Characteristics).ForEach(x =>
                        {
                            tempList.Add($"{nameof(StudyDesignEntity.Population)}.{nameof(StudyDesignPopulationEntity.Cohorts)}.{nameof(StudyCohortEntity.Characteristics)}{x}");
                        });
                    }
                });
            }
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
                    GetDifferenceForAList<IntercurrentEventEntity>(currentEstimand.IntercurrentEvents, previousEstimand.IntercurrentEvents).ForEach(x =>
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
                    GetDifferenceForAList<TimingEntity>(currentTimeline.Timings, previousTimeline.Timings).ForEach(x =>
                    {
                        scheduleTimelineChangeList.Add($"{nameof(StudyDesignEntity.ScheduleTimelines)}.{nameof(ScheduleTimelineEntity.Timings)}.{x}");
                    });
                }
                tempList.AddRange(scheduleTimelineChangeList);
            });
            return tempList;
        }

        public List<string> GetDifferenceForScheduledInstances<T>(List<T> currentVersion, List<T> previousVersion) where T : Entities.StudyV5.ScheduledInstanceEntity
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

        public List<string> GetDifferenceForGovernanceDate(List<GovernanceDateEntity> currentVersion, List<GovernanceDateEntity> previousVersion)
        {
            var tempList = new List<string>();
            if (currentVersion?.Count != previousVersion?.Count)
                tempList.Add($"");

            tempList.AddRange(GetDifferenceForAList(currentVersion, previousVersion));

            tempList.RemoveAll(x => x.Contains($"{nameof(GovernanceDateEntity.GeographicScopes)}"));

            currentVersion?.ForEach(currGovDate =>
            {
                if (previousVersion != null && previousVersion.Any(x => x.Id == currGovDate.Id))
                {
                    var prevGovDate = previousVersion.Find(x => x.Id == currGovDate.Id);

                    GetDifferenceForAList<GeographicScopeEntity>(currGovDate.GeographicScopes, prevGovDate.GeographicScopes).ForEach(x =>
                    {
                        tempList.Add($"{x}");
                    });
                }
            });
            return tempList;
        }
        public List<string> GetDifferenceForStudyAmendments(List<StudyAmendmentEntity> currentVersion, List<StudyAmendmentEntity> previousVersion)
        {
            var tempList = new List<string>();
            if (currentVersion?.Count != previousVersion?.Count)
                tempList.Add($"");

            tempList.AddRange(GetDifferenceForAList(currentVersion, previousVersion));

            tempList.RemoveAll(x => x.Contains($"{nameof(StudyAmendmentEntity.Enrollments)}"));
            tempList.RemoveAll(x => x.Contains($"{nameof(StudyAmendmentEntity.SecondaryReasons)}"));

            currentVersion?.ForEach(currAmendment =>
            {
                if (previousVersion != null && previousVersion.Any(x => x.Id == currAmendment.Id))
                {
                    var prevAmendment = previousVersion.Find(x => x.Id == currAmendment.Id);

                    GetDifferenceForAList<SubjectEnrollmentEntity>(currAmendment.Enrollments, prevAmendment.Enrollments).ForEach(x =>
                    {
                        tempList.Add($"{x}");
                    });
                    GetDifferenceForAList<StudyAmendmentReasonEntity>(currAmendment.SecondaryReasons, prevAmendment.SecondaryReasons).ForEach(x =>
                    {
                        tempList.Add($"{x}");
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
            if (study.Study.Versions != null && study.Study.Versions.Any())
            {
                if (study.Study.Versions.FirstOrDefault().StudyDesigns != null && study.Study.Versions.FirstOrDefault().StudyDesigns.Any())
                {
                    int studyVersionIndex = 0;
                    //Document Version Id
                    errors.AddRange(ReferenceIntegrityValidationForStudyVersionAndStudyDesign(study));

                    // Study Documents
                    if (study.Study.DocumentedBy != null)
                    {
                        List<string> allDocumentIds = study.Study.DocumentedBy.Select(doc => doc.Id).ToList();
                        study.Study.DocumentedBy.ForEach(document =>
                        {
                            var documentIndex = study.Study.DocumentedBy.IndexOf(document);
                            errors.AddRange(ReferenceIntegrityValidationForDocument(document, documentIndex, allDocumentIds));
                        });
                    }

                    //Design elements
                    var studyVersion = study.Study.Versions.FirstOrDefault();
                    var studyDesigns = studyVersion?.StudyDesigns;
                    List<string> studyVersionAndDesignIds = study.Study.Versions.Select(x => x.Id).ToList();

                    studyDesigns?.ForEach(design =>
                    {
                        studyVersionAndDesignIds.AddRange(studyDesigns.Select(x => x.Id));
                        var designIndex = studyDesigns.IndexOf(design);
                        Parallel.Invoke(
                            new ParallelOptions
                            {
                                MaxDegreeOfParallelism = 3
                            },
                            //Study Epoch & Encounters
                            () => errors.AddRange(ReferenceIntegrityValidationForStudyEpochs(design, designIndex, studyVersionIndex)),

                            //StudyScheduleTimelines
                            () => errors.AddRange(ReferenceIntegrityValidationForStudyScheduleTimelines(design, designIndex, studyVersionIndex)),

                            //Activities 
                            () => errors.AddRange(ReferenceIntegrityValidationForActivities(studyVersion, design, designIndex, studyVersionIndex)),

                            //Encounters
                            () => errors.AddRange(ReferenceIntegrityValidationForEncounters(design, designIndex, studyVersionIndex)),

                            //Endpoints & InvestigationalIntervention
                            () => errors.AddRange(ReferenceIntegrityValidationForStudyEstimands(design, designIndex, studyVersionIndex)),

                            //BcCategories
                            () => errors.AddRange(ReferenceIntegrityValidationForBcCategories(studyVersion, design, designIndex, studyVersionIndex)),

                            //BcCategories
                            () => errors.AddRange(ReferenceIntegrityValidationForStudyCells(design, designIndex, studyVersionIndex)),

                            //Eligibility Criteria
                            () => errors.AddRange(ReferenceIntegrityValidationForEligibilityCriteriaAndCharacteristcs(studyVersion, design, designIndex, studyVersionIndex, studyVersionAndDesignIds)),

                            //Arms
                            () => errors.AddRange(ReferenceIntegrityValidationForStudyArms(design, designIndex, studyVersionIndex)),

                            //Elements
                            () => errors.AddRange(ReferenceIntegrityValidationForStudyElements(design, designIndex, studyVersionIndex)),

                            //Conditions
                            () => errors.AddRange(ReferenceIntegrityValidationForConditions(studyVersion, design, designIndex, studyVersionIndex)),

                            //Procedures
                            () => errors.AddRange(ReferenceIntegrityValidationForProcedures(design, designIndex, studyVersionIndex)),

                            //Objectives & Endpoints
                            () => errors.AddRange(ReferenceIntegrityValidationForObjectivesAndEndpoints(studyVersion, design, designIndex, studyVersionIndex))
                        );

                    });
                }
            }
            List<string> formattedErrors = new();
            errors.ForEach(element =>
            {
                formattedErrors.Add(string.Join(".", element?.Split(".").Select(key => $"{key?[..1]?.ToLower()}{key?[1..]}")));
            });
            referenceErrors = GetErrors(formattedErrors);
            return errors.Any();
        }

        public static List<string> ReferenceIntegrityValidationForStudyVersionAndStudyDesign(StudyDefinitionsDto study)
        {
            List<String> errors = new();

            if (study.Study.Versions != null && study.Study.Versions.Any())
            {
                //List<string> studyDocumentVersionIds = study.Study.DocumentedBy != null && study.Study.DocumentedBy.Versions != null ? study.Study.DocumentedBy.Versions.Select(doc => doc?.Id).ToList() : new();
                List<string> studyDocumentVersionIds = study.Study.DocumentedBy != null
                 ? study.Study.DocumentedBy
                .Where(document => document.Versions != null)
                .SelectMany(document => document.Versions.Select(version => version?.Id))
                .ToList()
                : new();

                study.Study.Versions.ForEach(version =>
                {
                    if (version.DocumentVersionIds != null && version.DocumentVersionIds.Any() &&
                    !version.DocumentVersionIds.Any(id => studyDocumentVersionIds.Contains(id)))
                        errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                        $"{nameof(StudyDto.Versions)}[{study.Study.Versions.IndexOf(version)}]." +
                        $"{nameof(StudyVersionDto.DocumentVersionIds)}");

                    List<string> studyAmendmentIds = version.Amendments != null ? version.Amendments.Select(ammendment => ammendment.Id).ToList() : new();
                    version.Amendments?.ForEach(ammendment =>
                    {
                        List<string> tempAmmendmentIds = studyAmendmentIds.ToList();
                        tempAmmendmentIds.RemoveAll(x => x == ammendment.Id);
                        if (!String.IsNullOrWhiteSpace(ammendment.PreviousId) && !tempAmmendmentIds.Contains(ammendment.PreviousId))
                            errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                            $"{nameof(StudyDto.Versions)}[{study.Study.Versions.IndexOf(version)}]." +
                            $"{nameof(StudyVersionDto.Amendments)}.[{version.Amendments.IndexOf(ammendment)}]." +
                            $"{nameof(StudyAmendmentDto.PreviousId)}");
                    });

                    List<string> validStudyInterventionIds = version.StudyInterventions != null ? version.StudyInterventions.Select(studyIntervention => studyIntervention.Id).ToList() : new();

                    version.StudyDesigns?.ForEach(design =>
                    {
                        design.DocumentVersionIds.ForEach(documentVersionId =>
                        {
                            if (!String.IsNullOrWhiteSpace(documentVersionId) && !studyDocumentVersionIds.Contains(documentVersionId))
                                errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                    $"{nameof(StudyDto.Versions)}[{study.Study.Versions.IndexOf(version)}]." +
                                    $"{nameof(StudyVersionDto.StudyDesigns)}[{version.StudyDesigns.IndexOf(design)}]." +
                                    $"{nameof(StudyDesignDto.DocumentVersionIds)}");
                        });

                        design.StudyInterventionIds?.ForEach(interventionId =>
                        {
                            if (!string.IsNullOrWhiteSpace(interventionId) && !validStudyInterventionIds.Contains(interventionId))
                            {
                                errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                    $"{nameof(StudyDto.Versions)}[{study.Study.Versions.IndexOf(version)}]." +
                                    $"{nameof(StudyVersionDto.StudyDesigns)}[{version.StudyDesigns.IndexOf(design)}]." +
                                    $"{nameof(StudyDesignDto.StudyInterventionIds)}[{design.StudyInterventionIds.IndexOf(interventionId)}]");
                            }
                        });
                    });
                });
            }

            return errors;
        }
        public static List<string> ReferenceIntegrityValidationForDocument(StudyDefinitionDocumentDto document, int documentIndex, List<string> allDocumentIds)
        {
            List<string> errors = new();

            var otherDocumentIds = allDocumentIds.Where(id => id != document.Id).ToList();

            if (document.ChildIds != null)
            {
                document.ChildIds.ForEach(childId =>
                {
                    if (!string.IsNullOrWhiteSpace(childId) && !otherDocumentIds.Contains(childId))
                    {
                        errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                $"{nameof(StudyDto.DocumentedBy)}[{documentIndex}]." +
                                $"{nameof(StudyDefinitionDocumentDto.ChildIds)}[{document.ChildIds.IndexOf(childId)}]");
                    }
                });
            }

            if (document.Versions != null && document.Versions.Any())
            {
                errors.AddRange(ReferenceIntegrityValidationForDocumentVersions(document.Versions, documentIndex));
            }

            return errors;
        }

        public static List<string> ReferenceIntegrityValidationForDocumentVersions(List<StudyDefinitionDocumentVersionDto> documentVersions, int documentIndex)
        {
            List<String> errors = new();

            documentVersions.ForEach(documentVersion =>
            {
                if (documentVersion.Contents != null && documentVersion.Contents.Any())
                {
                    List<string> contentIds = documentVersion.Contents.Select(x => x.Id).ToList();
                    documentVersion.Contents.ForEach(content =>
                    {
                        var tempContentIds = contentIds.ToList();
                        tempContentIds.RemoveAll(x => x == content.Id);

                        content?.ChildIds?.ForEach(child =>
                        {
                            if (!String.IsNullOrWhiteSpace(child) && !tempContentIds.Contains(child))
                                errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                        $"{nameof(StudyDto.DocumentedBy)}[{documentIndex}]." +
                                        $"{nameof(StudyDefinitionDocumentDto.Versions)}[{documentVersions.IndexOf(documentVersion)}]." +
                                        $"{nameof(StudyDefinitionDocumentVersionDto.Contents)}[{documentVersion.Contents.IndexOf(content)}]." +
                                        $"{nameof(NarrativeContentDto.ChildIds)}[{content.ChildIds.IndexOf(child)}]");
                        });

                        if (!String.IsNullOrWhiteSpace(content.PreviousId) && !tempContentIds.Contains(content.PreviousId))
                            errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                    $"{nameof(StudyDto.DocumentedBy)}[{documentIndex}]." +
                                    $"{nameof(StudyDefinitionDocumentDto.Versions)}[{documentVersions.IndexOf(documentVersion)}]." +
                                    $"{nameof(StudyDefinitionDocumentVersionDto.Contents)}[{documentVersion.Contents.IndexOf(content)}]." +
                                    $"{nameof(NarrativeContentDto.PreviousId)}");

                        if (!String.IsNullOrWhiteSpace(content.NextId) && !tempContentIds.Contains(content.NextId))
                            errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                    $"{nameof(StudyDto.DocumentedBy)}[{documentIndex}]." +
                                    $"{nameof(StudyDefinitionDocumentDto.Versions)}[{documentVersions.IndexOf(documentVersion)}]." +
                                    $"{nameof(StudyDefinitionDocumentVersionDto.Contents)}[{documentVersion.Contents.IndexOf(content)}]." +
                                    $"{nameof(NarrativeContentDto.NextId)}");
                    });
                }
            });

            return errors;
        }
        public static List<string> ReferenceIntegrityValidationForStudyEpochs(StudyDesignDto design, int indexOfDesign, int studyVersionIndex)
        {
            List<String> errors = new();

            if (design.Epochs != null && design.Epochs.Any())
            {
                List<string> studyEpochIds = design.Epochs != null ? design.Epochs.Select(act => act?.Id).ToList() : new();
                design.Epochs.ForEach(epoch =>
                {
                    List<string> tempActIDs = studyEpochIds.ToList();
                    tempActIDs.RemoveAll(x => x == epoch.Id);
                    if (!String.IsNullOrWhiteSpace(epoch.PreviousId) && !tempActIDs.Contains(epoch.PreviousId))
                        errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                            $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                            $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                            $"{nameof(StudyDesignDto.Epochs)}[{design.Epochs.IndexOf(epoch)}]." +
                            $"{nameof(StudyEpochDto.PreviousId)}");

                    if (!String.IsNullOrWhiteSpace(epoch.NextId) && !tempActIDs.Contains(epoch.NextId))
                        errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                            $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                          $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                          $"{nameof(StudyDesignDto.Epochs)}[{design.Epochs.IndexOf(epoch)}]." +
                          $"{nameof(StudyEpochDto.NextId)}");
                });
            }

            return errors;
        }

        public static List<string> ReferenceIntegrityValidationForStudyElements(StudyDesignDto design, int indexOfDesign, int studyVersionIndex)
        {
            List<String> errors = new();

            if (design.Elements != null && design.Elements.Any())
            {
                List<string> studyInterventionIds = design.StudyInterventionIds != null ? design.StudyInterventionIds.ToList() : new();
                design.Elements.ForEach(element =>
                {
                    element.StudyInterventionIds.ForEach(x =>
                    {
                        if (!String.IsNullOrWhiteSpace(x) && !studyInterventionIds.Contains(x))
                            errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                                $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                                $"{nameof(StudyDesignDto.Elements)}[{design.Elements.IndexOf(element)}]." +
                                $"{nameof(StudyElementDto.StudyInterventionIds)}[{element.StudyInterventionIds.IndexOf(x)}]");
                    });
                });
            }

            return errors;
        }

        public static List<string> ReferenceIntegrityValidationForStudyArms(StudyDesignDto design, int indexOfDesign, int studyVersionIndex)
        {
            List<String> errors = new();

            if (design.Arms != null && design.Arms.Any())
            {
                string populationId = design.Population != null ? design.Population.Id : string.Empty;
                design.Arms?.ForEach(arm =>
                {
                    arm?.PopulationIds?.ForEach(popId =>
                    {
                        if (!String.IsNullOrWhiteSpace(popId) && !populationId.Equals(popId))
                            errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                                $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                                $"{nameof(StudyDesignDto.Arms)}[{design.Arms.IndexOf(arm)}]." +
                                $"{nameof(StudyArmDto.PopulationIds)}.[{arm.PopulationIds.IndexOf(popId)}]");
                    });
                });
            }

            return errors;
        }

        public static List<string> ReferenceIntegrityValidationForEligibilityCriteriaAndCharacteristcs(StudyVersionDto version, StudyDesignDto design, int indexOfDesign, int studyVersionIndex, List<string> studyVersionAndDesignIds)
        {
            List<String> errors = new();

            if (design != null && design.Population != null)
            {
                List<string> dictionaryIds = version.Dictionaries.Select(x => x.Id).ToList();
                // List<string> eligibilityCriteriaIds = design.Population.CriterionIds != null ? design.Population.CriterionIds.Select(act => act?.Id).ToList() : new();
                design.Population.CriterionIds?.ForEach(crit =>
                {
                    //List<string> tempEliCritIDs = eligibilityCriteriaIds.ToList();
                    // tempEliCritIDs.RemoveAll(x => x == crit.Id);
                    //if (!String.IsNullOrWhiteSpace(crit.PreviousId) && !tempEliCritIDs.Contains(crit.PreviousId))
                    //    errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                    //        $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                    //        $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                    //        $"{nameof(StudyDesignDto.Population)}" +
                    //        $"{nameof(StudyDesignPopulationDto.CriterionIds)}[{design.Population.CriterionIds.IndexOf(crit)}]." +
                    //        $"{nameof(EligibilityCriterionDto.PreviousId)}");

                    //if (!String.IsNullOrWhiteSpace(crit.NextId) && !tempEliCritIDs.Contains(crit.NextId))
                    //    errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                    //        $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                    //      $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                    //      $"{nameof(StudyDesignDto.Population)}" +
                    //      $"{nameof(StudyDesignPopulationDto.CriterionIds)}[{design.Population.CriterionIds.IndexOf(crit)}]." +
                    //      $"{nameof(EligibilityCriterionDto.NextId)}");

                    //if (!String.IsNullOrWhiteSpace(crit.ContextId) && !studyVersionAndDesignIds.Contains(crit.ContextId))
                    //    errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                    //        $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                    //      $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                    //      $"{nameof(StudyDesignDto.Population)}" +
                    //      $"{nameof(StudyDesignPopulationDto.CriterionIds)}[{design.Population.CriterionIds.IndexOf(crit)}]." +
                    //      $"{nameof(EligibilityCriterionDto.ContextId)}");

                    //if (!String.IsNullOrWhiteSpace(crit.DictionaryId) && !dictionaryIds.Contains(crit.DictionaryId))
                    //    errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                    //        $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                    //      $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                    //      $"{nameof(StudyDesignDto.Population)}" +
                    //      $"{nameof(StudyDesignPopulationDto.CriterionIds)}[{design.Population.CriterionIds.IndexOf(crit)}]." +
                    //      $"{nameof(EligibilityCriterionDto.DictionaryId)}");

                });

                design.Population.Cohorts?.ForEach(coh =>
                {
                    //List<string> cohortEligCritIds = coh.CriterionIds != null ? coh.CriterionIds.Select(act => act?.Id).ToList() : new();
                    coh.CriterionIds?.ForEach(crit =>
                    {
                        //List<string> tempEliCritIDs = cohortEligCritIds.ToList();
                        //tempEliCritIDs.RemoveAll(x => x == crit.Id);
                        //if (!String.IsNullOrWhiteSpace(crit.PreviousId) && !tempEliCritIDs.Contains(crit.PreviousId))
                        //    errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                        //        $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                        //        $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                        //        $"{nameof(StudyDesignDto.Population)}" +
                        //        $"{nameof(StudyDesignPopulationDto.Cohorts)}[{design.Population.Cohorts.IndexOf(coh)}]." +
                        //        $"{nameof(StudyCohortDto.CriterionIds)}[{coh.CriterionIds.IndexOf(crit)}]." +
                        //        $"{nameof(EligibilityCriterionDto.PreviousId)}");

                        //if (!String.IsNullOrWhiteSpace(crit.NextId) && !tempEliCritIDs.Contains(crit.NextId))
                        //    errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                        //      $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                        //      $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                        //      $"{nameof(StudyDesignDto.Population)}" +
                        //      $"{nameof(StudyDesignPopulationDto.Cohorts)}[{design.Population.Cohorts.IndexOf(coh)}]." +
                        //      $"{nameof(StudyCohortDto.CriterionIds)}[{coh.CriterionIds.IndexOf(crit)}]." +
                        //      $"{nameof(EligibilityCriterionDto.NextId)}");

                        //if (!String.IsNullOrWhiteSpace(crit.ContextId) && !studyVersionAndDesignIds.Contains(crit.ContextId))
                        //    errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                        //      $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                        //      $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                        //      $"{nameof(StudyDesignDto.Population)}" +
                        //      $"{nameof(StudyDesignPopulationDto.Cohorts)}[{design.Population.Cohorts.IndexOf(coh)}]." +
                        //      $"{nameof(StudyCohortDto.CriterionIds)}[{coh.CriterionIds.IndexOf(crit)}]." +
                        //      $"{nameof(EligibilityCriterionDto.ContextId)}");

                        //if (!String.IsNullOrWhiteSpace(crit.DictionaryId) && !dictionaryIds.Contains(crit.DictionaryId))
                        //    errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                        //        $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                        //      $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                        //      $"{nameof(StudyDesignDto.Population)}" +
                        //      $"{nameof(StudyDesignPopulationDto.Cohorts)}[{design.Population.Cohorts.IndexOf(coh)}]." +
                        //      $"{nameof(StudyCohortDto.CriterionIds)}[{coh.CriterionIds.IndexOf(crit)}]." +
                        //      $"{nameof(EligibilityCriterionDto.DictionaryId)}");

                    });

                    coh.Characteristics.ForEach(characteristic =>
                    {
                        if (!String.IsNullOrWhiteSpace(characteristic.DictionaryId) && !dictionaryIds.Contains(characteristic.DictionaryId))
                            errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                              $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                              $"{nameof(StudyDesignDto.Population)}" +
                              $"{nameof(StudyDesignPopulationDto.Cohorts)}[{design.Population.Cohorts.IndexOf(coh)}]." +
                              $"{nameof(StudyCohortDto.Characteristics)}[{coh.Characteristics.IndexOf(characteristic)}]." +
                              $"{nameof(CharacteristicDto.DictionaryId)}");
                    });

                });
            }

            return errors;
        }

        public static List<string> ReferenceIntegrityValidationForStudyScheduleTimelines(StudyDesignDto design, int indexOfDesign, int studyVersionIndex)
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
                            $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                            $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                            $"{nameof(StudyDesignDto.ScheduleTimelines)}[{design.ScheduleTimelines.IndexOf(scheduleTimeline)}].{nameof(ScheduleTimelineDto.EntryId)}");

                    if (scheduleTimeline.Timings is not null && scheduleTimeline.Timings.Any())
                    {
                        scheduleTimeline.Timings.ForEach(timing =>
                        {
                            if (!String.IsNullOrWhiteSpace(timing.RelativeFromScheduledInstanceId) && !allScheduleInstanceIds.Contains(timing.RelativeFromScheduledInstanceId))
                                errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                    $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                                    $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                                    $"{nameof(StudyDesignDto.ScheduleTimelines)}[{design.ScheduleTimelines.IndexOf(scheduleTimeline)}]." +
                                    $"{nameof(ScheduleTimelineDto.Timings)}[{scheduleTimeline.Timings.IndexOf(timing)}]." +
                                    $"{nameof(TimingDto.RelativeFromScheduledInstanceId)}");

                            if (!String.IsNullOrWhiteSpace(timing.RelativeToScheduledInstanceId) && !allScheduleInstanceIds.Contains(timing.RelativeToScheduledInstanceId))
                                errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                    $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                                    $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                                    $"{nameof(StudyDesignDto.ScheduleTimelines)}[{design.ScheduleTimelines.IndexOf(scheduleTimeline)}]." +
                                    $"{nameof(ScheduleTimelineDto.Timings)}[{scheduleTimeline.Timings.IndexOf(timing)}]." +
                                    $"{nameof(TimingDto.RelativeToScheduledInstanceId)}");
                        });
                    }

                    if (scheduleTimeline.Instances != null && scheduleTimeline.Instances.Any())
                    {
                        List<string> instanceIds = scheduleTimeline.Instances?.Select(ins => ins?.Id).ToList();
                        scheduleTimeline.Instances.ForEach(timelineInstance =>
                        {
                            List<string> tempInstanceIds = instanceIds.ToList();
                            tempInstanceIds.RemoveAll(x => x == timelineInstance.Id);

                            if (!String.IsNullOrWhiteSpace(timelineInstance.TimelineExitId) && !scheduleTimelineExitIds.Contains(timelineInstance.TimelineExitId))
                                errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                    $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                                    $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                                    $"{nameof(StudyDesignDto.ScheduleTimelines)}[{design.ScheduleTimelines.IndexOf(scheduleTimeline)}]." +
                                    $"{nameof(ScheduleTimelineDto.Instances)}[{scheduleTimeline.Instances.IndexOf(timelineInstance)}]." +
                                    $"{nameof(ScheduledInstanceDto.TimelineExitId)}");

                            if (!String.IsNullOrWhiteSpace(timelineInstance.TimelineId) && !scheduledTimelineIds.Contains(timelineInstance.TimelineId))
                                errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                    $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                                    $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                                    $"{nameof(StudyDesignDto.ScheduleTimelines)}[{design.ScheduleTimelines.IndexOf(scheduleTimeline)}]." +
                                    $"{nameof(ScheduleTimelineDto.Instances)}[{scheduleTimeline.Instances.IndexOf(timelineInstance)}]." +
                                    $"{nameof(ScheduledInstanceDto.TimelineId)}");

                            if (!String.IsNullOrWhiteSpace(timelineInstance.EpochId) && !epochIds.Contains(timelineInstance.EpochId))
                                errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                    $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                                    $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                                    $"{nameof(StudyDesignDto.ScheduleTimelines)}[{design.ScheduleTimelines.IndexOf(scheduleTimeline)}]." +
                                    $"{nameof(ScheduleTimelineDto.Instances)}[{scheduleTimeline.Instances.IndexOf(timelineInstance)}]." +
                                    $"{nameof(ScheduledInstanceDto.EpochId)}");

                            if (!String.IsNullOrWhiteSpace(timelineInstance.DefaultConditionId) && !tempInstanceIds.Contains(timelineInstance.DefaultConditionId))
                                errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                    $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                                    $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
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
                                                $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                                                $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                                                $"{nameof(StudyDesignDto.ScheduleTimelines)}[{design.ScheduleTimelines.IndexOf(scheduleTimeline)}]." +
                                                $"{nameof(ScheduleTimelineDto.Instances)}[{scheduleTimeline.Instances.IndexOf(timelineInstance)}]." +
                                                $"{nameof(ScheduledActivityInstanceDto.ActivityIds)}[{activityIds.IndexOf(id)}]");
                                    });
                                }
                                if (!String.IsNullOrWhiteSpace(activityTimelineInstance.EncounterId) && !encounterIds.Contains(activityTimelineInstance.EncounterId))
                                    errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                        $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                                        $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                                        $"{nameof(StudyDesignDto.ScheduleTimelines)}[{design.ScheduleTimelines.IndexOf(scheduleTimeline)}]." +
                                        $"{nameof(ScheduleTimelineDto.Instances)}[{scheduleTimeline.Instances.IndexOf(timelineInstance)}]." +
                                        $"{nameof(ScheduledActivityInstanceDto.EncounterId)}");
                            }

                            if (timelineInstance.GetType() == typeof(ScheduledDecisionInstanceDto))
                            {
                                var decisionTimelineInstance = timelineInstance as ScheduledDecisionInstanceDto;
                                decisionTimelineInstance.ConditionAssignments.ForEach(assignment =>
                                {
                                    if (!String.IsNullOrWhiteSpace(assignment.ConditionTargetId) && !instanceIds.Contains(assignment.ConditionTargetId))
                                        errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                            $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                                            $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                                            $"{nameof(StudyDesignDto.ScheduleTimelines)}[{design.ScheduleTimelines.IndexOf(scheduleTimeline)}]." +
                                            $"{nameof(ScheduleTimelineDto.Instances)}[{scheduleTimeline.Instances.IndexOf(timelineInstance)}]." +
                                            $"{nameof(ScheduledDecisionInstanceDto.ConditionAssignments)}[{decisionTimelineInstance.ConditionAssignments.IndexOf(assignment)}]");
                                }
                                );
                            }
                        });
                    }
                });
            }

            return errors;
        }

        public static List<string> ReferenceIntegrityValidationForActivities(StudyVersionDto version, StudyDesignDto design, int indexOfDesign, int studyVersionIndex)
        {
            List<String> errors = new();

            if (design.Activities != null && design.Activities.Any())
            {
                List<string> bcCategoryIds = version.BcCategories is null ? new List<string>() : version.BcCategories.Select(x => x.Id).ToList();
                List<string> bcSurrogateIds = version.BcSurrogates is null ? new List<string>() : version.BcSurrogates.Select(x => x.Id).ToList();
                List<string> biomedicalConceptIds = version.BiomedicalConcepts is null ? new List<string>() : version.BiomedicalConcepts.Select(x => x.Id).ToList();
                List<string> activitiesIds = design.Activities.Select(act => act?.Id).ToList();
                activitiesIds.RemoveAll(x => String.IsNullOrWhiteSpace(x));
                List<string> scheduleTimelineIds = design.ScheduleTimelines is not null && design.ScheduleTimelines.Any() ? design.ScheduleTimelines.Select(x => x.Id).ToList() : new List<string>();
                design.Activities.ForEach(act =>
                {
                    List<string> tempActIDs = activitiesIds.ToList();
                    tempActIDs.RemoveAll(x => x == act.Id);
                    if (!String.IsNullOrWhiteSpace(act.PreviousId) && !tempActIDs.Contains(act.PreviousId))
                        errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                            $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                            $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                            $"{nameof(StudyDesignDto.Activities)}[{design.Activities.IndexOf(act)}]." +
                            $"{nameof(ActivityDto.PreviousId)}");

                    if (!String.IsNullOrWhiteSpace(act.NextId) && !tempActIDs.Contains(act.NextId))
                        errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                            $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                          $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                          $"{nameof(StudyDesignDto.Activities)}[{design.Activities.IndexOf(act)}]." +
                          $"{nameof(ActivityDto.NextId)}");

                    if (!String.IsNullOrWhiteSpace(act.TimelineId) && !scheduleTimelineIds.Contains(act.TimelineId))
                        errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                            $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                          $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                          $"{nameof(StudyDesignDto.Activities)}[{design.Activities.IndexOf(act)}]." +
                          $"{nameof(ActivityDto.TimelineId)}");

                    if (act.BiomedicalConceptIds != null && act.BiomedicalConceptIds.Any())
                    {
                        act.BiomedicalConceptIds.ForEach(bc =>
                        {
                            if (!String.IsNullOrWhiteSpace(bc) && !biomedicalConceptIds.Contains(bc))
                                errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                    $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                                           $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
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
                                    $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                                           $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
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
                                    $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                                           $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                                           $"{nameof(StudyDesignDto.Activities)}[{design.Activities.IndexOf(act)}]." +
                                           $"{nameof(ActivityDto.BcSurrogateIds)}[{act.BcSurrogateIds.IndexOf(bcSurr)}]");
                        });
                    }
                });
            }

            return errors;
        }

        public static List<string> ReferenceIntegrityValidationForStudyCells(StudyDesignDto design, int indexOfDesign, int studyVersionIndex)
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
                            $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                                   $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                                   $"{nameof(StudyDesignDto.StudyCells)}[{design.StudyCells.IndexOf(cell)}]." +
                                   $"{nameof(StudyCellDto.ArmId)}");

                    if (!String.IsNullOrWhiteSpace(cell.EpochId) && !epochIds.Contains(cell.EpochId))
                        errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                            $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                                   $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                                   $"{nameof(StudyDesignDto.StudyCells)}[{design.StudyCells.IndexOf(cell)}]." +
                                   $"{nameof(StudyCellDto.EpochId)}");

                    if (cell.ElementIds != null && cell.ElementIds.Any())
                    {
                        cell.ElementIds.ForEach(elementId =>
                        {
                            if (!String.IsNullOrWhiteSpace(elementId) && !studyElementIds.Contains(elementId))
                                errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                    $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                                           $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                                           $"{nameof(StudyDesignDto.StudyCells)}[{design.StudyCells.IndexOf(cell)}]." +
                                           $"{nameof(StudyCellDto.ElementIds)}[{cell.ElementIds.IndexOf(elementId)}]");
                        });
                    }
                });
            }

            return errors;
        }

        public static List<string> ReferenceIntegrityValidationForEncounters(StudyDesignDto design, int indexOfDesign, int studyVersionIndex)
        {
            List<String> errors = new();

            if (design.Encounters != null && design.Encounters.Any())
            {
                List<string> encounterIds = design.Encounters.Select(enc => enc?.Id).ToList();
                encounterIds.RemoveAll(x => String.IsNullOrWhiteSpace(x));

                List<string> allTimingIds = design.ScheduleTimelines is not null && design.ScheduleTimelines.Any() ?
                                            design.ScheduleTimelines.Where(x => x.Timings != null && x.Timings.Any())
                                                  .SelectMany(x => x.Timings).Select(x => x.Id).ToList() : new List<string>();
                design.Encounters.ForEach(enc =>
                {
                    List<string> tempencounterIds = encounterIds.ToList();
                    tempencounterIds.RemoveAll(x => x == enc.Id);
                    if (!String.IsNullOrWhiteSpace(enc.PreviousId) && !tempencounterIds.Contains(enc.PreviousId))
                        errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                            $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                            $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                            $"{nameof(StudyDesignDto.Encounters)}[{design.Encounters.IndexOf(enc)}]." +
                            $"{nameof(EncounterDto.PreviousId)}");

                    if (!String.IsNullOrWhiteSpace(enc.NextId) && !tempencounterIds.Contains(enc.NextId))
                        errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                            $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                          $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                          $"{nameof(StudyDesignDto.Encounters)}[{design.Encounters.IndexOf(enc)}]." +
                          $"{nameof(EncounterDto.NextId)}");

                    if (!String.IsNullOrWhiteSpace(enc.ScheduledAtId) && !allTimingIds.Contains(enc.ScheduledAtId))
                        errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                            $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                          $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                          $"{nameof(StudyDesignDto.Encounters)}[{design.Encounters.IndexOf(enc)}]." +
                          $"{nameof(EncounterDto.ScheduledAtId)}");
                });
            }

            return errors;
        }

        public static List<string> ReferenceIntegrityValidationForStudyEstimands(StudyDesignDto design, int indexOfDesign, int studyVersionIndex)
        {
            List<String> errors = new();

            if (design.Estimands != null && design.Estimands.Any())
            {
                design.Estimands.ForEach(estimand =>
                {
                    List<string> investigationalInterventionIds = design.StudyInterventionIds is null ? new List<string>() : design.StudyInterventionIds.ToList();
                    List<string> endpointIds = design.Objectives is null ? new List<string>() : design.Objectives.Select(x => x).ToList().Select(x => x?.Endpoints).Where(y => y != null).SelectMany(x => x.Select(y => y.Id)).ToList();
                    List<string> analysisPopulationIds = design.AnalysisPopulations is null ? new List<string>() : design.AnalysisPopulations.Select(x => x.Id).ToList();

                    estimand.InterventionIds.ForEach(interventionId =>
                    {
                        if (!String.IsNullOrWhiteSpace(interventionId) && !investigationalInterventionIds.Contains(interventionId))
                            errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                                $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                                $"{nameof(StudyDesignDto.Estimands)}[{design.Estimands.IndexOf(estimand)}]." +
                                $"{nameof(EstimandDto.InterventionIds)}");
                    });

                    if (!String.IsNullOrWhiteSpace(estimand.VariableOfInterestId) && !endpointIds.Contains(estimand.VariableOfInterestId))
                        errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                            $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                            $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                            $"{nameof(StudyDesignDto.Estimands)}[{design.Estimands.IndexOf(estimand)}]." +
                            $"{nameof(EstimandDto.VariableOfInterestId)}");

                    if (!String.IsNullOrWhiteSpace(estimand.AnalysisPopulationId) && !analysisPopulationIds.Contains(estimand.AnalysisPopulationId))
                        errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                            $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                            $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                            $"{nameof(StudyDesignDto.Estimands)}[{design.Estimands.IndexOf(estimand)}]." +
                            $"{nameof(EstimandDto.AnalysisPopulationId)}");
                });
            }

            return errors;
        }

        public static List<string> ReferenceIntegrityValidationForBcCategories(StudyVersionDto version, StudyDesignDto design, int indexOfDesign, int studyVersionIndex)
        {
            List<String> errors = new();

            if (version.BcCategories != null && version.BcCategories.Any())
            {
                List<string> bcCategoryIds = version.BcCategories is null ? new List<string>() : version.BcCategories.Select(x => x.Id).ToList();
                List<string> bcSurrogateIds = version.BcSurrogates is null ? new List<string>() : version.BcSurrogates.Select(x => x.Id).ToList();
                List<string> biomedicalConceptIds = version.BiomedicalConcepts is null ? new List<string>() : version.BiomedicalConcepts.Select(x => x.Id).ToList();
                version.BcCategories.ForEach(bcCat =>
                {
                    var tempCategoryIds = bcCategoryIds.ToList();
                    tempCategoryIds.RemoveAll(x => x == bcCat.Id);
                    if (bcCat.ChildIds != null && bcCat.ChildIds.Any())
                    {
                        bcCat.ChildIds.ForEach(child =>
                        {
                            if (!String.IsNullOrWhiteSpace(child) && !tempCategoryIds.Contains(child))
                                errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                    $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                                           $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                                           $"{nameof(StudyVersionDto.BcCategories)}[{version.BcCategories.IndexOf(bcCat)}]." +
                                           $"{nameof(BiomedicalConceptCategoryDto.ChildIds)}[{bcCat.ChildIds.IndexOf(child)}]");
                        });
                    }
                    if (bcCat.MemberIds != null && bcCat.MemberIds.Any())
                    {
                        bcCat.MemberIds.ForEach(member =>
                        {
                            if (!String.IsNullOrWhiteSpace(member) && !biomedicalConceptIds.Contains(member))
                                errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                    $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                                           $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                                           $"{nameof(StudyVersionDto.BcCategories)}[{version.BcCategories.IndexOf(bcCat)}]." +
                                           $"{nameof(BiomedicalConceptCategoryDto.MemberIds)}[{bcCat.MemberIds.IndexOf(member)}]");
                        });
                    }
                });
            }

            return errors;
        }

        public static List<string> ReferenceIntegrityValidationForConditions(StudyVersionDto version, StudyDesignDto design, int indexOfDesign, int studyVersionIndex)
        {
            List<String> errors = new();

            if (version.Conditions != null && version.Conditions.Any())
            {
                List<string> dictionaryIds = version.Dictionaries.Select(x => x.Id).ToList();
                List<string> bcCategoryIds = version.BcCategories is null ? new List<string>() : version.BcCategories.Select(x => x.Id).ToList();
                List<string> bcSurrogateIds = version.BcSurrogates is null ? new List<string>() : version.BcSurrogates.Select(x => x.Id).ToList();
                List<string> biomedicalConceptIds = version.BiomedicalConcepts is null ? new List<string>() : version.BiomedicalConcepts.Select(x => x.Id).ToList();
                List<string> activitiesIds = design.Activities is null ? new List<string>() : design.Activities.Select(x => x.Id).ToList();
                List<string> procedureIds = design.Activities is null ? new List<string>() : design.Activities.Where(x => x.DefinedProcedures is not null && x.DefinedProcedures.Any()).SelectMany(y => y.DefinedProcedures).Select(z => z.Id).ToList();
                List<string> scheduleActivityInstanceIds = design.ScheduleTimelines is not null && design.ScheduleTimelines.Any() ?
                                                           design.ScheduleTimelines.SelectMany(x => x.Instances).Where(y => y.InstanceType == nameof(ScheduledActivityInstanceDto).RemoveDto())
                                                           .Select(z => z.Id)
                                                           .ToList() : new List<string>();

                version.Conditions.ForEach(condition =>
                {
                    if (condition.DictionaryId != null)
                    {
                        if (!String.IsNullOrWhiteSpace(condition.DictionaryId) && !dictionaryIds.Contains(condition.DictionaryId))
                            errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                              $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                              $"{nameof(StudyVersionDto.Conditions)}[{version.Conditions.IndexOf(condition)}]." +
                              $"{nameof(ConditionDto.DictionaryId)}");
                    }

                    if (condition.AppliesToIds != null && condition.AppliesToIds.Any())
                    {
                        condition.AppliesToIds.ForEach(appliesTo =>
                        {
                            if (!String.IsNullOrWhiteSpace(appliesTo)
                            && !procedureIds.Contains(appliesTo)
                            && !activitiesIds.Contains(appliesTo)
                            && !bcCategoryIds.Contains(appliesTo)
                            && !biomedicalConceptIds.Contains(appliesTo)
                            && !bcSurrogateIds.Contains(appliesTo))
                                errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                    $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                                           $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                                           $"{nameof(StudyVersionDto.Conditions)}[{version.Conditions.IndexOf(condition)}]." +
                                           $"{nameof(ConditionDto.AppliesToIds)}[{condition.AppliesToIds.IndexOf(appliesTo)}]");
                        });
                    }
                    if (condition.ContextIds != null && condition.ContextIds.Any())
                    {
                        condition.ContextIds.ForEach(contextId =>
                        {
                            if (!String.IsNullOrWhiteSpace(contextId)
                            && !activitiesIds.Contains(contextId)
                            && !scheduleActivityInstanceIds.Contains(contextId))
                                errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                    $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                                           $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                                           $"{nameof(StudyVersionDto.Conditions)}[{version.Conditions.IndexOf(condition)}]." +
                                           $"{nameof(ConditionDto.ContextIds)}[{condition.ContextIds.IndexOf(contextId)}]");
                        });
                    }
                });
            }

            return errors;
        }

        public static List<string> ReferenceIntegrityValidationForProcedures(StudyDesignDto design, int indexOfDesign, int studyVersionIndex)
        {
            List<String> errors = new();

            if (design.Activities != null && design.Activities.Any())
            {
                List<string> studyInterventionIds = design.StudyInterventionIds != null ? design.StudyInterventionIds.ToList() : new();
                design.Activities.ForEach(activity =>
                {
                    activity.DefinedProcedures?.ForEach(proc =>
                    {
                        if (!String.IsNullOrWhiteSpace(proc.StudyInterventionId) && !studyInterventionIds.Contains(proc.StudyInterventionId))
                            errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                                $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                                $"{nameof(StudyDesignDto.Activities)}[{design.Activities.IndexOf(activity)}]." +
                                $"{nameof(ActivityDto.DefinedProcedures)}[{activity.DefinedProcedures.IndexOf(proc)}]." +
                                $"{nameof(ProcedureDto.StudyInterventionId)}");
                    });
                });
            }

            return errors;
        }


        public static List<string> ReferenceIntegrityValidationForObjectivesAndEndpoints(StudyVersionDto version, StudyDesignDto design, int indexOfDesign, int studyVersionIndex)
        {
            List<String> errors = new();

            if (design.Objectives != null && design.Objectives.Any())
            {
                List<string> dictionaryIds = version.Dictionaries.Select(x => x.Id).ToList();

                design.Objectives.ForEach(objective =>
                {
                    if (objective.DictionaryId != null)
                    {
                        if (!String.IsNullOrWhiteSpace(objective.DictionaryId) && !dictionaryIds.Contains(objective.DictionaryId))
                            errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                              $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                              $"{nameof(StudyDesignDto.Objectives)}[{design.Objectives.IndexOf(objective)}]." +
                              $"{nameof(ObjectiveDto.DictionaryId)}");
                    }

                    if (objective.Endpoints != null && objective.Endpoints.Any())
                    {
                        objective.Endpoints.ForEach(endpoint =>
                        {
                            if (!String.IsNullOrWhiteSpace(endpoint.DictionaryId) && !dictionaryIds.Contains(endpoint.DictionaryId))
                                errors.Add($"{nameof(StudyDefinitionsDto.Study)}." +
                                    $"{nameof(StudyDto.Versions)}[{studyVersionIndex}]." +
                                  $"{nameof(StudyVersionDto.StudyDesigns)}[{indexOfDesign}]." +
                                  $"{nameof(StudyDesignDto.Objectives)}[{design.Objectives.IndexOf(objective)}]." +
                                  $"{nameof(ObjectiveDto.Endpoints)}[{objective.Endpoints.IndexOf(endpoint)}]." +
                                  $"{nameof(EndpointDto.DictionaryId)}");

                        });
                    }
                });
            }

            return errors;
        }

        public static List<string> ReferenceIntegrityValidationForStudyRole(StudyRoleDto studyRole, StudyDefinitionsDto study, int studyRoleIndex)
        {
            List<string> errors = new();

            if (studyRole?.AppliesToIds != null && studyRole.AppliesToIds.Any())
            {
                List<string> studyVersionIds = study.Study?.Versions != null
                    ? study.Study.Versions.Select(version => version.Id).ToList()
                    : new();

                List<string> studyDesignIds = new();
                if (study.Study?.Versions != null)
                {
                    foreach (var version in study.Study.Versions)
                    {
                        if (version.StudyDesigns != null)
                        {
                            studyDesignIds.AddRange(version.StudyDesigns.Select(design => design.Id));
                        }
                    }
                }

                studyRole.AppliesToIds.ForEach(appliesToId =>
                {
                    if (string.IsNullOrWhiteSpace(appliesToId))
                    {
                        errors.Add($"{nameof(StudyRoleDto)}[{studyRoleIndex}]." +
                                $"{nameof(StudyRoleDto.AppliesToIds)}[{studyRole.AppliesToIds.IndexOf(appliesToId)}]");
                    }
                    else if (!studyVersionIds.Contains(appliesToId) && !studyDesignIds.Contains(appliesToId))
                    {
                        errors.Add($"{nameof(StudyRoleDto)}[{studyRoleIndex}]." +
                                $"{nameof(StudyRoleDto.AppliesToIds)}[{studyRole.AppliesToIds.IndexOf(appliesToId)}]");
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

            changedValues = GetDifferencesForStudyComparison(currentStudyVersion, previousStudyVersion);

            changedValues.RemoveAll(x => x.Contains(nameof(StudyEntity.Versions)));
            changedValues.RemoveAll(x => x.Contains(nameof(StudyEntity.DocumentedBy)));
            changedValues.RemoveAll(x => x.Contains(nameof(StudyDefinitionsEntity.AuditTrail)));

            changedValues.AddRange(GetChangedValuesForStudyVersionForStudyComparison(currentStudyVersion.Study.Versions, previousStudyVersion.Study.Versions));

            //if (currentStudyVersion.Study.DocumentedBy?.Id != previousStudyVersion.Study.DocumentedBy?.Id)
            //    changedValues.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.DocumentedBy)}");
            var currentIds = currentStudyVersion.Study.DocumentedBy?.Select(doc => doc.Id).OrderBy(id => id).ToList();
            var previousIds = previousStudyVersion.Study.DocumentedBy?.Select(doc => doc.Id).OrderBy(id => id).ToList();

            if (!currentIds.SequenceEqual(previousIds))
            {
                changedValues.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.DocumentedBy)}");
            }
            else
            {
                var tempList = new List<string>();
                tempList.AddRange(GetDifferencesForStudyComparison(currentStudyVersion.Study.DocumentedBy, previousStudyVersion.Study.DocumentedBy));
                tempList.RemoveAll(x => x.Contains(nameof(StudyDefinitionDocumentEntity.Versions)));
                //tempList.AddRange(GetDifferenceForStudyProtocolDocumentVersionsForStudyComparison(currentStudyVersion.Study.DocumentedBy?.Versions, previousStudyVersion.Study.DocumentedBy?.Versions));
                if (currentStudyVersion.Study.DocumentedBy != null && previousStudyVersion.Study.DocumentedBy != null)
                {
                    tempList.AddRange(
                        currentStudyVersion.Study.DocumentedBy
                            .Where(currentDocument => previousStudyVersion.Study.DocumentedBy
                                .Any(previousDocument => previousDocument.Id == currentDocument.Id))
                            .SelectMany(currentDocument =>
                            {
                                var previousDocument = previousStudyVersion.Study.DocumentedBy
                                    .First(doc => doc.Id == currentDocument.Id);

                                return GetDifferenceForStudyProtocolDocumentVersionsForStudyComparison(
                                    currentDocument.Versions,
                                    previousDocument.Versions
                                );
                            }));
                }

                changedValues.AddRange(tempList);
            }

            return FormatVersionCompareValues(changedValues);
        }
        public List<string> GetChangedValuesForStudyVersionForStudyComparison(List<StudyVersionEntity> currentVersion, List<StudyVersionEntity> previousVersion)
        {
            List<string> changedValues = new();
            List<string> formattedChangedValues = new();

            if (currentVersion?.Count != previousVersion?.Count)
                changedValues.Add($"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.Versions)}{Constants.VersionCompareConstants.ArrayBrackets}");

            changedValues.AddRange(GetDifferenceForAListForStudyComparison(currentVersion, previousVersion));
            changedValues.RemoveAll(x => x.Contains($"{nameof(StudyVersionEntity.Titles)}"));
            changedValues.RemoveAll(x => x.Contains($"{nameof(StudyVersionEntity.DateValues)}"));
            changedValues.RemoveAll(x => x.Contains($"{nameof(StudyVersionEntity.Amendments)}"));
            changedValues.RemoveAll(x => x.Contains($"{nameof(StudyVersionEntity.StudyIdentifiers)}"));
            changedValues.RemoveAll(x => x.Contains($"{nameof(StudyVersionEntity.BusinessTherapeuticAreas)}"));
            changedValues.RemoveAll(x => x.Contains($"{nameof(StudyVersionEntity.StudyDesigns)}"));

            currentVersion?.ForEach(currVer =>
            {
                if (previousVersion != null && previousVersion.Any(x => x.Id == currVer.Id))
                {
                    var prevVer = previousVersion.Find(x => x.Id == currVer.Id);
                    var currentVersionIndex = currentVersion.IndexOf(currVer);
                    if (currVer.VersionIdentifier != prevVer.VersionIdentifier)
                        changedValues.Add($"[{currentVersionIndex}].{nameof(StudyVersionEntity.VersionIdentifier)}");
                    if (currVer.Rationale != prevVer.Rationale)
                        changedValues.Add($"[{currentVersionIndex}].{nameof(StudyVersionEntity.Rationale)}");

                    //Titles
                    GetDifferenceForAListForStudyComparison(currVer.Titles, prevVer.Titles).ForEach(x =>
                    {
                        changedValues.Add($"[{currentVersionIndex}].{nameof(StudyVersionEntity.Titles)}{x}");
                    });

                    //BusinessTherapeuticAreas
                    if (GetDifferencesForStudyComparison<List<CodeEntity>>(currVer.BusinessTherapeuticAreas, prevVer.BusinessTherapeuticAreas).Any())
                        changedValues.Add($"[{currentVersionIndex}].{nameof(StudyVersionEntity.BusinessTherapeuticAreas)}");
                    //StudyIdentifiers
                    GetDifferenceForStudyIdentifiersForStudyComparison(currVer.StudyIdentifiers, prevVer.StudyIdentifiers).ForEach(x =>
                    {
                        changedValues.Add($"[{currentVersionIndex}].{x}");
                    });
                    //DateValues
                    GetDifferenceForGovernanceDateForStudyComparison(currVer.DateValues, prevVer.DateValues).ForEach(x =>
                    {
                        changedValues.Add($"[{currentVersionIndex}].{x}");
                    });
                    //Ammendments
                    GetDifferenceForStudyAmendmentsForStudyComparison(currVer.Amendments, prevVer.Amendments).ForEach(x =>
                    {
                        changedValues.Add($"[{currentVersionIndex}].{x}");
                    });
                    //Study Designs
                    GetDifferenceForStudyDesignsForStudyComparison(currVer.StudyDesigns, prevVer.StudyDesigns).ForEach(x =>
                    {
                        changedValues.Add($"[{currentVersionIndex}].{x}");
                    });
                    //Conditions
                    GetDifferenceForAListForStudyComparison<ConditionEntity>(currVer.Conditions, prevVer.Conditions).ForEach(x =>
                    {
                        changedValues.Add($"[{currentVersionIndex}].{x}");
                    });
                    //Biomedical Concept Surrogate
                    GetDifferenceForAListForStudyComparison<BiomedicalConceptSurrogateEntity>(currVer.BcSurrogates, prevVer.BcSurrogates).ForEach(x =>
                    {
                        changedValues.Add($"[{currentVersionIndex}].{x}");
                    });
                    //Biomedical Concept Category
                    GetDifferenceForAListForStudyComparison<BiomedicalConceptCategoryEntity>(currVer.BcCategories, prevVer.BcCategories).ForEach(x =>
                    {
                        changedValues.Add($"[{currentVersionIndex}].{x}");
                    });
                    //Syntax Template Dictionaries
                    GetDifferenceForAListForStudyComparison<SyntaxTemplateDictionaryEntity>(currVer.Dictionaries, prevVer.Dictionaries).ForEach(x =>
                    {
                        changedValues.Add($"{nameof(StudyVersionEntity.Dictionaries)}{x}");
                    });
                    //Biomedical Concepts
                    changedValues.AddRange(GetDifferenceForBiomedicalConceptsForStudyComparison(currVer, prevVer));
                }
            });

            if (changedValues.Any())
            {
                changedValues.ForEach(x =>
                {
                    string addRootPath = $"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.Versions)}{x}";
                    formattedChangedValues.Add(addRootPath);
                });
            }
            return formattedChangedValues;
        }
        public static List<string> GetDifferencesForStudyComparison<T>(T currentVersion, T previousVersion)
        {
            var comparer = new ObjectsComparer.Comparer<T>();
            bool isEqual = comparer.Compare(currentVersion, previousVersion, out var differences);
            return differences.Select(x => x.MemberPath).ToList();
        }

        public List<string> GetDifferenceForAListForStudyComparison<T>(List<T> currentVersion, List<T> previousVersion) where T : class, Entities.StudyV5.IId
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
                        tempChangedValues.AddRange(GetDifferenceForNonArrayElementsForStudyComparison(currentItem, previousVersion.Find(x => x.Id == currentItem.Id), tempChangedValues, currentVersion.IndexOf(currentItem)));
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
        public List<string> GetDifferenceForAListForStudyComparison(List<string> currentVersion, List<string> previousVersion)
        {
            List<string> changedValues = new();

            if (currentVersion != null && currentVersion.Any())
            {
                currentVersion.ForEach(currentItem =>
                {
                    if (previousVersion == null || !previousVersion.Contains(currentItem))
                    {
                        changedValues.Add($"Added: {currentItem}");
                    }
                });

                previousVersion?.ForEach(previousItem =>
                {
                    if (!currentVersion.Contains(previousItem))
                    {
                        changedValues.Add($"Removed: {previousItem}");
                    }
                });
            }
            else if ((currentVersion is null && previousVersion is not null) || (currentVersion is not null && previousVersion is null))
            {
                changedValues.Add("List structure changed");
            }

            if (currentVersion?.Count != previousVersion?.Count)
            {
                changedValues.Add("List count mismatch");
            }

            return changedValues;
        }

        public static List<string> GetDifferenceForNonArrayElementsForStudyComparison<T>(T currentVersion, T previousVersion, List<string> changedValues, int index) where T : class, Entities.StudyV5.IId
        {
            if (typeof(T) == typeof(StudyIdentifierEntity))
            {
                var currentStudyIdentifier = currentVersion as StudyIdentifierEntity;
                var previousStudyIdentifier = previousVersion as StudyIdentifierEntity;

                if (currentStudyIdentifier.ScopeId != previousStudyIdentifier.ScopeId)
                {
                    changedValues.RemoveAll(x => x.Contains(nameof(StudyIdentifierEntity.ScopeId)));
                    changedValues.Add($"[{index}].{nameof(StudyIdentifierEntity.ScopeId)}");
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

                if (currentStudyEstimand.AnalysisPopulationId != previousStudyEstimand.AnalysisPopulationId)
                {
                    changedValues.RemoveAll(x => x.Contains(nameof(EstimandEntity.AnalysisPopulationId)));
                    changedValues.Add($"[{index}].{nameof(EstimandEntity.AnalysisPopulationId)}");
                }
            }
            if (typeof(T) == typeof(GeographicScopeEntity))
            {
                var currentGeoScope = currentVersion as GeographicScopeEntity;
                var previousGeoScope = previousVersion as GeographicScopeEntity;

                if (currentGeoScope.Code?.Id != previousGeoScope.Code?.Id)
                {
                    changedValues.RemoveAll(x => x.Contains(nameof(GeographicScopeEntity.Code)));
                    changedValues.Add($"[{index}].{nameof(GeographicScopeEntity.Code)}");
                }
            }
            return changedValues;
        }
        public List<string> GetDifferenceForStudyIdentifiersForStudyComparison(List<StudyIdentifierEntity> currentVersion, List<StudyIdentifierEntity> previousVersion)
        {
            var tempList = new List<string>();
            if ((currentVersion is null && previousVersion is not null) || (currentVersion is not null && previousVersion is null))
                tempList.Add($"{nameof(StudyVersionEntity.StudyIdentifiers)}{Constants.VersionCompareConstants.ArrayBrackets}");
            if (currentVersion?.Count != previousVersion?.Count)
                tempList.Add($"{nameof(StudyVersionEntity.StudyIdentifiers)}{Constants.VersionCompareConstants.ArrayBrackets}");

            var differencesForIdentifiersSubElements = GetDifferenceForAListForStudyComparison<StudyIdentifierEntity>(currentVersion, previousVersion);

            GetDifferenceForAListForStudyComparison<StudyIdentifierEntity>(currentVersion, previousVersion).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyVersionEntity.StudyIdentifiers)}{x}");
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

        public List<string> GetDifferenceForStudyProtocolDocumentVersionsForStudyComparison(List<StudyDefinitionDocumentVersionEntity> currentVersion, List<StudyDefinitionDocumentVersionEntity> previousVersion)
        {
            var tempList = new List<string>();
            var formattedChangedValues = new List<string>();

            if (currentVersion?.Count != previousVersion?.Count)
                tempList.Add($"{nameof(StudyDefinitionDocumentEntity.Versions)}{Constants.VersionCompareConstants.ArrayBrackets}");


            tempList.AddRange(GetDifferenceForAListForStudyComparison(currentVersion, previousVersion));

            tempList.RemoveAll(x => x.Contains($"{nameof(StudyDefinitionDocumentVersionEntity.DateValues)}"));
            tempList.RemoveAll(x => x.Contains($"{nameof(StudyDefinitionDocumentVersionEntity.Contents)}"));

            currentVersion?.ForEach(currProtDocVersion =>
            {
                if (previousVersion != null && previousVersion.Any(x => x.Id == currProtDocVersion.Id))
                {
                    var prevProtDocVersion = previousVersion.Find(x => x.Id == currProtDocVersion.Id);

                    GetDifferenceForGovernanceDateForStudyComparison(currProtDocVersion.DateValues, prevProtDocVersion.DateValues).ForEach(x =>
                    {
                        tempList.Add($"{nameof(StudyDefinitionDocumentEntity.Versions)}[{currentVersion.IndexOf(currProtDocVersion)}]{x}");
                    });
                    GetDifferenceForAListForStudyComparison<NarrativeContentEntity>(currProtDocVersion.Contents, prevProtDocVersion.Contents).ForEach(x =>
                    {
                        tempList.Add($"{nameof(StudyDefinitionDocumentEntity.Versions)}[{currentVersion.IndexOf(currProtDocVersion)}]{x}");
                    });
                }
            });
            if (tempList.Any())
            {
                tempList.ForEach(x =>
                {
                    string addRootPath = $"{nameof(StudyDefinitionsEntity.Study)}.{nameof(StudyEntity.DocumentedBy)}{x}";
                    formattedChangedValues.Add(addRootPath);
                });
            }
            return formattedChangedValues;
        }

        public List<string> GetDifferenceForStudyDesignsForStudyComparison(List<StudyDesignEntity> currentVersion, List<StudyDesignEntity> previousVersion)
        {
            List<string> changedValues = new();
            List<string> formattedChangedValues = new();

            if ((currentVersion is null && previousVersion is not null) || (currentVersion is not null && previousVersion is null))
                formattedChangedValues.Add($"{nameof(StudyVersionEntity.StudyDesigns)}{Constants.VersionCompareConstants.ArrayBrackets}");
            if (currentVersion?.Count != previousVersion?.Count)
                formattedChangedValues.Add($"{nameof(StudyVersionEntity.StudyDesigns)}{Constants.VersionCompareConstants.ArrayBrackets}");
            changedValues.AddRange(GetDifferenceForEachStudyDesignsForStudyComparison(currentVersion, previousVersion));

            if (changedValues.Any())
            {
                changedValues.ForEach(x =>
                {
                    string addRootPath = $"{nameof(StudyVersionEntity.StudyDesigns)}{x}";
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

                        if (currentStudyDesign.Label != previousStudyDesign.Label)
                            changedValues.Add($"{nameof(StudyDesignEntity.Label)}");

                        //StudyDesignRationale
                        if (currentStudyDesign.Rationale != previousStudyDesign.Rationale)
                            changedValues.Add($"{nameof(StudyDesignEntity.Rationale)}");

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

                        //Timelines                        
                        changedValues.AddRange(GetDifferenceForStudyScheduleTimelinesForStudyComparison(currentStudyDesign, previousStudyDesign));

                        //Encounters
                        GetDifferenceForAListForStudyComparison<EncounterEntity>(currentStudyDesign.Encounters, previousStudyDesign.Encounters).ForEach(x =>
                        {
                            changedValues.Add($"{nameof(StudyDesignEntity.Encounters)}{x}");
                        });
                        //Activities
                        changedValues.AddRange(GetDifferenceForActivitiesForStudyComparison(currentStudyDesign, previousStudyDesign));

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
                        //StudyPhase
                        GetDifferenceForAliasCodeForStudyComparison(currentStudyDesign.StudyPhase, previousStudyDesign.StudyPhase).ForEach(x =>
                        {
                            changedValues.Add($"{nameof(StudyDesignEntity.StudyPhase)}{x}");
                        });
                        GetDifferencesForStudyComparison(currentStudyDesign.StudyType, previousStudyDesign.StudyType).ForEach(x =>
                        {
                            changedValues.Add($"{nameof(StudyDesignEntity.StudyType)}");
                        });
                    }

                    else if (currentVersion?.Count == previousVersion?.Count && previousVersion != null && !previousVersion.Any(x => x.Id == currentStudyDesign.Id))
                    {
                        formattedChangedValues.Add($"{Constants.VersionCompareConstants.ArrayBrackets}.T");
                    }
                });
            }

            return formattedChangedValues;
        }
        public List<string> GetDifferenceForBiomedicalConceptsForStudyComparison(StudyVersionEntity currentStudyVersion, StudyVersionEntity previousStudyVersion)
        {
            var tempList = new List<string>();
            if (currentStudyVersion.BiomedicalConcepts?.Count != previousStudyVersion.BiomedicalConcepts?.Count)
                tempList.Add($"{nameof(StudyVersionEntity.BiomedicalConcepts)}{Constants.VersionCompareConstants.ArrayBrackets}");
            GetDifferenceForAListForStudyComparison<BiomedicalConceptEntity>(currentStudyVersion.BiomedicalConcepts, previousStudyVersion.BiomedicalConcepts).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyVersionEntity.BiomedicalConcepts)}{x}");
            });
            tempList.RemoveAll(x => x.Contains($"{nameof(BiomedicalConceptEntity.Properties)}"));
            tempList.RemoveAll(x => x.Contains($"{nameof(BiomedicalConceptEntity.Code)}"));
            currentStudyVersion.BiomedicalConcepts?.ForEach(currentBc =>
            {
                var currentBcChangedValues = new List<string>();
                if (previousStudyVersion.BiomedicalConcepts != null && previousStudyVersion.BiomedicalConcepts.Any(x => x.Id == currentBc.Id))
                {
                    var previousBc = previousStudyVersion.BiomedicalConcepts.Find(x => x.Id == currentBc.Id);
                    GetDifferenceForAListForStudyComparison<BiomedicalConceptPropertyEntity>(currentBc.Properties, previousBc.Properties).ForEach(x =>
                    {
                        currentBcChangedValues.Add($"{nameof(StudyVersionEntity.BiomedicalConcepts)}[{currentStudyVersion.BiomedicalConcepts.IndexOf(currentBc)}].{nameof(BiomedicalConceptEntity.Properties)}{x}");
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
                                currentBcChangedValues.Add($"{nameof(StudyVersionEntity.BiomedicalConcepts)}[{currentStudyVersion.BiomedicalConcepts.IndexOf(currentBc)}].{nameof(BiomedicalConceptEntity.Properties)}[{currentBc.Properties.IndexOf(currentBcProp)}].{nameof(BiomedicalConceptPropertyEntity.ResponseCodes)}{x}");
                            });

                            GetDifferenceForAliasCodeForStudyComparison(currentBcProp.Code, previousBcProp.Code).ForEach(x =>
                            {
                                currentBcChangedValues.Add($"{nameof(StudyVersionEntity.BiomedicalConcepts)}[{currentStudyVersion.BiomedicalConcepts.IndexOf(currentBc)}].{nameof(BiomedicalConceptEntity.Properties)}[{currentBc.Properties.IndexOf(currentBcProp)}].{nameof(BiomedicalConceptPropertyEntity.Code)}{x}");
                            });
                        }
                    });
                    GetDifferenceForAliasCodeForStudyComparison(currentBc.Code, previousBc.Code).ForEach(x =>
                    {
                        currentBcChangedValues.Add($"{nameof(StudyVersionEntity.BiomedicalConcepts)}[{currentStudyVersion.BiomedicalConcepts.IndexOf(currentBc)}].{nameof(BiomedicalConceptEntity.Code)}{x}");
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
            GetDifferenceForAListForStudyComparison<ObjectiveEntity>(currentStudyDesign.Objectives.Select(x => x).ToList(), previousStudyDesign.Objectives.Select(x => x).ToList()).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDesignEntity.Objectives)}{x}");
            });
            tempList.RemoveAll(x => x.Contains($"{nameof(ObjectiveEntity.Endpoints)}"));
            currentStudyDesign.Objectives?.ForEach(currentObjective =>
            {
                if (previousStudyDesign.Objectives != null && previousStudyDesign.Objectives.Any(x => x.Id == currentObjective.Id))
                {
                    var previousObjective = previousStudyDesign.Objectives.Find(x => x.Id == currentObjective.Id);

                    GetDifferenceForAListForStudyComparison<EndpointEntity>(currentObjective.Endpoints.Select(x => x).ToList(), previousObjective.Endpoints.Select(x => x).ToList()).ForEach(x =>
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
            if (currentStudyDesign.StudyInterventionIds?.Count != previousStudyDesign.StudyInterventionIds?.Count)
                tempList.Add($"{nameof(StudyDesignEntity.StudyInterventionIds)}{Constants.VersionCompareConstants.ArrayBrackets}");
            GetDifferenceForAListForStudyComparison(currentStudyDesign.StudyInterventionIds, previousStudyDesign.StudyInterventionIds).ForEach(x =>
            {
                tempList.Add($"{nameof(StudyDesignEntity.StudyInterventionIds)}{x}");
            });
            return tempList;
        }

        public List<string> GetDifferenceForStudyPopulationsForStudyComparison(StudyDesignEntity currentStudyDesign, StudyDesignEntity previousStudyDesign)
        {
            var tempList = new List<string>();
            if (currentStudyDesign.Population?.Id != previousStudyDesign.Population?.Id)
                tempList.Add($"{nameof(StudyDesignEntity.Population)}");

            else if (currentStudyDesign.Population is not null && previousStudyDesign is not null)
            {
                GetDifferencesForStudyComparison<StudyDesignPopulationEntity>(currentStudyDesign.Population, previousStudyDesign.Population).ForEach(x =>
                {
                    tempList.Add($"{nameof(StudyDesignEntity.Population)}{x}");
                });
                tempList.RemoveAll(x => x.Contains($"{nameof(StudyDesignPopulationEntity.Cohorts)}"));
                tempList.RemoveAll(x => x.Contains($"{nameof(StudyDesignPopulationEntity.CriterionIds)}"));

                GetDifferenceForAListForStudyComparison<StudyCohortEntity>(currentStudyDesign.Population.Cohorts, previousStudyDesign.Population.Cohorts).ForEach(x =>
                {
                    tempList.Add($"{nameof(StudyDesignEntity.Population)}.{nameof(StudyDesignPopulationEntity.Cohorts)}{x}");
                });
                GetDifferenceForAListForStudyComparison(currentStudyDesign.Population.CriterionIds, previousStudyDesign.Population.CriterionIds).ForEach(x =>
                {
                    tempList.Add($"{nameof(StudyDesignEntity.Population)}.{nameof(StudyDesignPopulationEntity.CriterionIds)}{x}");
                });
                tempList.RemoveAll(x => x.Contains($"{nameof(StudyCohortEntity.Characteristics)}"));
                currentStudyDesign.Population.Cohorts?.ForEach(currCohort =>
                {
                    if (previousStudyDesign.Population.Cohorts != null && previousStudyDesign.Population.Cohorts.Any(x => x.Id == currCohort.Id))
                    {
                        var prevCohort = previousStudyDesign.Population.Cohorts.Find(x => x.Id == currCohort.Id);
                        GetDifferenceForAListForStudyComparison<CharacteristicEntity>(currCohort.Characteristics, currCohort.Characteristics).ForEach(x =>
                        {
                            tempList.Add($"{nameof(StudyDesignEntity.Population)}.{nameof(StudyDesignPopulationEntity.Cohorts)}[{currentStudyDesign.Population.Cohorts.IndexOf(currCohort)}].{nameof(StudyCohortEntity.Characteristics)}{x}");
                        });
                    }
                });
            }
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
                    GetDifferenceForAListForStudyComparison<IntercurrentEventEntity>(currentEstimand.IntercurrentEvents, previousEstimand.IntercurrentEvents).ForEach(x =>
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
                    GetDifferenceForAListForStudyComparison<TimingEntity>(currentTimeline.Timings, previousTimeline.Timings).ForEach(x =>
                    {
                        scheduleTimelineChangeList.Add($"{nameof(StudyDesignEntity.ScheduleTimelines)}[{currentStudyDesign.ScheduleTimelines.IndexOf(currentTimeline)}].{nameof(ScheduleTimelineEntity.Timings)}{x}");
                    });
                }
                tempList.AddRange(scheduleTimelineChangeList);
            });
            return tempList;
        }

        public List<string> GetDifferenceForScheduledInstancesForStudyComparison<T>(List<T> currentVersion, List<T> previousVersion) where T : Entities.StudyV5.ScheduledInstanceEntity
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
        public List<string> GetDifferenceForGovernanceDateForStudyComparison(List<GovernanceDateEntity> currentVersion, List<GovernanceDateEntity> previousVersion)
        {
            var tempList = new List<string>();
            if (currentVersion?.Count != previousVersion?.Count)
                tempList.Add($"{nameof(StudyVersionEntity.DateValues)}{Constants.VersionCompareConstants.ArrayBrackets}");

            tempList.AddRange(GetDifferenceForAListForStudyComparison(currentVersion, previousVersion));

            tempList.RemoveAll(x => x.Contains($"{nameof(GovernanceDateEntity.GeographicScopes)}"));

            currentVersion?.ForEach(currGovDate =>
            {
                if (previousVersion != null && previousVersion.Any(x => x.Id == currGovDate.Id))
                {
                    var prevGovDate = previousVersion.Find(x => x.Id == currGovDate.Id);

                    GetDifferenceForAListForStudyComparison<GeographicScopeEntity>(currGovDate.GeographicScopes, prevGovDate.GeographicScopes).ForEach(x =>
                    {
                        tempList.Add($"{nameof(StudyVersionEntity.DateValues)}[{currentVersion.IndexOf(currGovDate)}]{x}");
                    });
                }
            });
            return tempList;
        }
        public List<string> GetDifferenceForStudyAmendmentsForStudyComparison(List<StudyAmendmentEntity> currentVersion, List<StudyAmendmentEntity> previousVersion)
        {
            var tempList = new List<string>();
            if (currentVersion?.Count != previousVersion?.Count)
                tempList.Add($"{nameof(StudyVersionEntity.Amendments)}{Constants.VersionCompareConstants.ArrayBrackets}");

            tempList.AddRange(GetDifferenceForAListForStudyComparison(currentVersion, previousVersion));

            tempList.RemoveAll(x => x.Contains($"{nameof(StudyAmendmentEntity.Enrollments)}"));
            tempList.RemoveAll(x => x.Contains($"{nameof(StudyAmendmentEntity.SecondaryReasons)}"));

            currentVersion?.ForEach(currAmendment =>
            {
                if (previousVersion != null && previousVersion.Any(x => x.Id == currAmendment.Id))
                {
                    var prevAmendment = previousVersion.Find(x => x.Id == currAmendment.Id);

                    GetDifferenceForAListForStudyComparison<SubjectEnrollmentEntity>(currAmendment.Enrollments, prevAmendment.Enrollments).ForEach(x =>
                    {
                        tempList.Add($"{nameof(StudyVersionEntity.Amendments)}[{currentVersion.IndexOf(currAmendment)}]{x}");
                    });
                    GetDifferenceForAListForStudyComparison<StudyAmendmentReasonEntity>(currAmendment.SecondaryReasons, prevAmendment.SecondaryReasons).ForEach(x =>
                    {
                        tempList.Add($"{nameof(StudyVersionEntity.Amendments)}[{currentVersion.IndexOf(currAmendment)}]{x}");
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
                if (stringSegments.Last() == "")
                {
                    stringSegments.RemoveAt(stringSegments.Count - 1);
                }
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
