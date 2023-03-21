using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using TransCelerate.SDR.Core.DTO;
using TransCelerate.SDR.Core.DTO.Study;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    /// <summary>
    /// This class is for removing sections from the study for sectional response
    /// </summary>
    public static class RemoveStudySections
    {
        /// <summary>
        /// This method is for removing study level sections
        /// </summary>
        /// <param name="sections">Study Sections array</param>
        /// <param name="getStudySectionsDTO">Study defenitions from Database</param>
        /// <returns>
        /// A <see cref="object"/> After removing sections which are not in the sections array        
        /// </returns>
        public static object RemoveSections(string[] sections, GetStudySectionsDTO getStudySectionsDTO)
        {
            try
            {
                var jsonObject = JObject.Parse(JsonConvert.SerializeObject(getStudySectionsDTO));
                jsonObject.Property("Links").Remove();
                foreach (var item in Enum.GetNames(typeof(StudySections)))
                {
                    sections = sections.Select(t => t.Trim().ToLower()).ToArray();
                    if (!sections.Contains(item))
                    {
                        switch (item)
                        {
                            case "study_indications":
                                jsonObject.Property(nameof(GetStudySectionsDTO.StudyIndications)).Remove();
                                break;
                            case "study_objectives":
                                jsonObject.Property(nameof(GetStudySectionsDTO.Objectives)).Remove();
                                break;
                            case "study_design":
                                jsonObject.Property(nameof(GetStudySectionsDTO.StudyDesigns)).Remove();
                                break;
                            default:
                                break;
                        }
                    }
                }
                return jsonObject;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This method is for removing study design level sections
        /// </summary>
        /// <param name="sections">Study Design Sections array</param>
        /// <param name="getStudySectionsDTO">Study defenitions from Database</param>
        /// <returns>
        /// A <see cref="object"/> After removing sections which are not in the sections array           
        /// </returns>
        public static object RemoveSectionsForStudyDesign(string[] sections, GetStudySectionsDTO getStudySectionsDTO)
        {
            try
            {
                var jsonObject = JObject.Parse(JsonConvert.SerializeObject(getStudySectionsDTO));
                jsonObject.Property(nameof(GetStudySectionsDTO.StudyIndications)).Remove();
                jsonObject.Property(nameof(GetStudySectionsDTO.Objectives)).Remove();
                if (getStudySectionsDTO.StudyDesigns.Count != 0)
                {
                    if (sections.Length != 0)
                    {
                        foreach (var item in Enum.GetNames(typeof(StudyDesignSections)))
                        {
                            sections = sections.Select(t => t.Trim().ToLower()).ToArray();
                            if (!sections.Contains(item))
                            {
                                switch (item)
                                {
                                    case "study_planned_workflow":
                                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(GetStudyDesignsDTO.PlannedWorkflows)).ToList().ForEach(x => x.Remove());
                                        break;
                                    case "study_target_populations":
                                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(GetStudyDesignsDTO.StudyPopulations)).ToList().ForEach(x => x.Remove());
                                        break;
                                    case "study_cells":
                                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(GetStudyDesignsDTO.StudyCells)).ToList().ForEach(x => x.Remove());
                                        break;
                                    case "study_investigational_interventions":
                                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(GetStudyDesignsDTO.InvestigationalInterventions)).ToList().ForEach(x => x.Remove());
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        jsonObject.Property("Links").Remove();
                    }
                }

                return jsonObject;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This method is for formatting study definitions to POST request format
        /// </summary>
        /// <param name="studyEntity">Study for formatting the response</param>
        /// <returns>
        /// A <see cref="object"/> After formating the JSON to the POST request format      
        /// </returns>
        public static object PostResponseRemoveSections(PostStudyDTO studyEntity)
        {
            var clinicalStudyJsonObject = JObject.Parse(JsonConvert.SerializeObject(studyEntity.ClinicalStudy));
            JArray currentSectionArray = new();
            clinicalStudyJsonObject.Property(nameof(ClinicalStudyDTO.CurrentSections)).Remove();

            if (studyEntity.ClinicalStudy.CurrentSections != null)
            {
                if (studyEntity.ClinicalStudy.CurrentSections.Count > 0)
                {
                    foreach (var currentSections in studyEntity.ClinicalStudy.CurrentSections)
                    {
                        var currentsSectionsJobject = JObject.Parse(JsonConvert.SerializeObject(currentSections));
                        switch (currentSections.SectionType)
                        {
                            case "STUDY_INDICATIONS":
                                currentsSectionsJobject.Properties().Where(attr => attr.Name != nameof(CurrentSectionsDTO.StudyIndications) && attr.Name != nameof(CurrentSectionsDTO.Id) && attr.Name != nameof(CurrentSectionsDTO.SectionType)).ToList().ForEach(x => x.Remove());
                                break;
                            case "OBJECTIVES":
                                currentsSectionsJobject.Properties().Where(attr => attr.Name != nameof(CurrentSectionsDTO.Objectives) && attr.Name != nameof(CurrentSectionsDTO.Id) && attr.Name != nameof(CurrentSectionsDTO.SectionType)).ToList().ForEach(x => x.Remove());
                                break;
                            case "STUDY_DESIGNS":
                                currentsSectionsJobject.Properties().Where(attr => attr.Name != nameof(CurrentSectionsDTO.Id) && attr.Name != nameof(CurrentSectionsDTO.SectionType)).ToList().ForEach(x => x.Remove());
                                JArray studyDesignsJArray = new();
                                foreach (var item in currentSections.StudyDesigns)
                                {
                                    var studyDesignJobject = JObject.Parse(JsonConvert.SerializeObject(item));
                                    JArray currentSectionOfStudyDesignArray = new();
                                    studyDesignJobject.Property(nameof(ClinicalStudyDTO.CurrentSections)).Remove();
                                    if (item.CurrentSections != null)
                                    {
                                        if (item.CurrentSections.Count > 0)
                                        {
                                            foreach (var currSection in item.CurrentSections)
                                            {
                                                var currentSectionOfStudyDesignJobject = JObject.Parse(JsonConvert.SerializeObject(currSection));
                                                switch (currSection.SectionType)
                                                {
                                                    case "PLANNED_WORKFLOWS":
                                                        currentSectionOfStudyDesignJobject.Properties().Where(attr => attr.Name != nameof(CurrentSectionsDTO.PlannedWorkflows) && attr.Name != nameof(CurrentSectionsDTO.Id) && attr.Name != nameof(CurrentSectionsDTO.SectionType)).ToList().ForEach(x => x.Remove());
                                                        break;
                                                    case "STUDY_POPULATIONS":
                                                        currentSectionOfStudyDesignJobject.Properties().Where(attr => attr.Name != nameof(CurrentSectionsDTO.StudyPopulations) && attr.Name != nameof(CurrentSectionsDTO.Id) && attr.Name != nameof(CurrentSectionsDTO.SectionType)).ToList().ForEach(x => x.Remove());
                                                        break;
                                                    case "STUDY_CELLS":
                                                        currentSectionOfStudyDesignJobject.Properties().Where(attr => attr.Name != nameof(CurrentSectionsDTO.StudyCells) && attr.Name != nameof(CurrentSectionsDTO.Id) && attr.Name != nameof(CurrentSectionsDTO.SectionType)).ToList().ForEach(x => x.Remove());
                                                        break;
                                                    case "INVESTIGATIONAL_INTERVENTIONS":
                                                        currentSectionOfStudyDesignJobject.Properties().Where(attr => attr.Name != nameof(CurrentSectionsDTO.InvestigationalInterventions) && attr.Name != nameof(CurrentSectionsDTO.Id) && attr.Name != nameof(CurrentSectionsDTO.SectionType)).ToList().ForEach(x => x.Remove());
                                                        break;
                                                    default:
                                                        break;
                                                }
                                                currentSectionOfStudyDesignArray.Add(currentSectionOfStudyDesignJobject);
                                            }
                                        }
                                    }
                                    studyDesignJobject.Add(nameof(ClinicalStudyDTO.CurrentSections), currentSectionOfStudyDesignArray);
                                    studyDesignsJArray.Add(studyDesignJobject);
                                }
                                currentsSectionsJobject.Add(nameof(CurrentSectionsDTO.StudyDesigns), studyDesignsJArray);
                                break;
                            default:
                                break;
                        }
                        currentSectionArray.Add(currentsSectionsJobject);
                    }

                }
            }
            clinicalStudyJsonObject.Add(nameof(ClinicalStudyDTO.CurrentSections), currentSectionArray);

            JObject returnJsonObject = new();
            returnJsonObject.Add(nameof(PostStudyDTO.ClinicalStudy), clinicalStudyJsonObject);
            returnJsonObject.Add(nameof(PostStudyDTO.AuditTrail), JObject.Parse(JsonConvert.SerializeObject(studyEntity.AuditTrail, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                Formatting = Formatting.Indented

            })));
            returnJsonObject.Add("links", JObject.Parse(JsonConvert.SerializeObject(LinksHelper.GetLinksForUi(studyEntity.ClinicalStudy.StudyId,
                studyEntity.ClinicalStudy.CurrentSections?.Where(x => x.StudyDesigns != null).SelectMany(x => x.StudyDesigns)?.Select(x => x.StudyDesignId)?.ToList(),
                studyEntity.AuditTrail.UsdmVersion, studyEntity.AuditTrail.StudyVersion), new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver()
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    },
                    Formatting = Formatting.Indented

                })));
            return returnJsonObject;
        }
    }
}
