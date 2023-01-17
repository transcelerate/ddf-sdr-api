using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using TransCelerate.SDR.Core.DTO;
using TransCelerate.SDR.Core.DTO.Study;
using TransCelerate.SDR.Core.Entities.Study;

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
                foreach (var item in Enum.GetNames(typeof(StudySections)))
                {
                    sections = sections.Select(t => t.Trim().ToLower()).ToArray();
                    if (!sections.Contains(item))
                    {
                        switch (item)
                        {
                            case "study_indications":
                                jsonObject.Property(nameof(GetStudySectionsDTO.studyIndications)).Remove();
                                break;
                            case "study_objectives":
                                jsonObject.Property(nameof(GetStudySectionsDTO.objectives)).Remove();
                                break;
                            case "study_design":
                                jsonObject.Property(nameof(GetStudySectionsDTO.studyDesigns)).Remove();
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
                var jsonObject = JObject.Parse(JsonConvert.SerializeObject(getStudySectionsDTO, new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver()
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    },
                    Formatting = Formatting.Indented

                }));
                jsonObject.Property(nameof(GetStudySectionsDTO.studyIndications)).Remove();
                jsonObject.Property(nameof(GetStudySectionsDTO.objectives)).Remove();
                if (getStudySectionsDTO.studyDesigns.Count() != 0)
                {
                    if (sections.Count() != 0)
                    {
                        foreach (var item in Enum.GetNames(typeof(StudyDesignSections)))
                        {
                            sections = sections.Select(t => t.Trim().ToLower()).ToArray();
                            if (!sections.Contains(item))
                            {
                                switch (item)
                                {
                                    case "study_planned_workflow":
                                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(GetStudyDesignsDTO.plannedWorkflows)).ToList().ForEach(x => x.Remove());
                                        break;
                                    case "study_target_populations":
                                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(GetStudyDesignsDTO.studyPopulations)).ToList().ForEach(x => x.Remove());
                                        break;
                                    case "study_cells":
                                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(GetStudyDesignsDTO.studyCells)).ToList().ForEach(x => x.Remove());
                                        break;
                                    case "study_investigational_interventions":
                                        jsonObject.Descendants().OfType<JProperty>().Where(attr => attr.Name == nameof(GetStudyDesignsDTO.investigationalInterventions)).ToList().ForEach(x => x.Remove());
                                        break;
                                    default:
                                        break;
                                }
                            }
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
        /// This method is for formatting study definitions to POST request format
        /// </summary>
        /// <param name="studyEntity">Study for formatting the response</param>
        /// <returns>
        /// A <see cref="object"/> After formating the JSON to the POST request format      
        /// </returns>
        public static object PostResponseRemoveSections(PostStudyDTO studyEntity)
        {
            var clinicalStudyJsonObject = JObject.Parse(JsonConvert.SerializeObject(studyEntity.clinicalStudy));
            JArray currentSectionArray = new JArray();
            clinicalStudyJsonObject.Property(nameof(ClinicalStudyDTO.currentSections)).Remove();

            if(studyEntity.clinicalStudy.currentSections !=null)
            {
                if(studyEntity.clinicalStudy.currentSections.Count > 0)
                {
                    foreach (var currentSections in studyEntity.clinicalStudy.currentSections)
                    {
                        var currentsSectionsJobject = JObject.Parse(JsonConvert.SerializeObject(currentSections));
                        switch (currentSections.sectionType)
                        {
                            case "STUDY_INDICATIONS":
                                currentsSectionsJobject.Properties().Where(attr => attr.Name != nameof(CurrentSectionsDTO.studyIndications) && attr.Name != nameof(CurrentSectionsDTO.id) && attr.Name != nameof(CurrentSectionsDTO.sectionType)).ToList().ForEach(x => x.Remove());
                                break;
                            case "OBJECTIVES":
                                currentsSectionsJobject.Properties().Where(attr => attr.Name != nameof(CurrentSectionsDTO.objectives) && attr.Name != nameof(CurrentSectionsDTO.id) && attr.Name != nameof(CurrentSectionsDTO.sectionType)).ToList().ForEach(x => x.Remove());
                                break;
                            case "STUDY_DESIGNS":
                                currentsSectionsJobject.Properties().Where(attr => attr.Name != nameof(CurrentSectionsDTO.id) && attr.Name != nameof(CurrentSectionsDTO.sectionType)).ToList().ForEach(x => x.Remove());
                                JArray studyDesignsJArray = new JArray();
                                foreach (var item in currentSections.studyDesigns)
                                {
                                    var studyDesignJobject = JObject.Parse(JsonConvert.SerializeObject(item));
                                    JArray currentSectionOfStudyDesignArray = new JArray();
                                    studyDesignJobject.Property(nameof(ClinicalStudyDTO.currentSections)).Remove();
                                    if(item.currentSections != null)
                                    {
                                        if(item.currentSections.Count>0)
                                        {
                                            foreach (var currSection in item.currentSections)
                                            {
                                                var currentSectionOfStudyDesignJobject = JObject.Parse(JsonConvert.SerializeObject(currSection));
                                                switch (currSection.sectionType)
                                                {
                                                    case "PLANNED_WORKFLOWS":
                                                        currentSectionOfStudyDesignJobject.Properties().Where(attr => attr.Name != nameof(CurrentSectionsDTO.plannedWorkflows) && attr.Name != nameof(CurrentSectionsDTO.id) && attr.Name != nameof(CurrentSectionsDTO.sectionType)).ToList().ForEach(x => x.Remove());
                                                        break;
                                                    case "STUDY_POPULATIONS":
                                                        currentSectionOfStudyDesignJobject.Properties().Where(attr => attr.Name != nameof(CurrentSectionsDTO.studyPopulations) && attr.Name != nameof(CurrentSectionsDTO.id) && attr.Name != nameof(CurrentSectionsDTO.sectionType)).ToList().ForEach(x => x.Remove());
                                                        break;
                                                    case "STUDY_CELLS":
                                                        currentSectionOfStudyDesignJobject.Properties().Where(attr => attr.Name != nameof(CurrentSectionsDTO.studyCells) && attr.Name != nameof(CurrentSectionsDTO.id) && attr.Name != nameof(CurrentSectionsDTO.sectionType)).ToList().ForEach(x => x.Remove());
                                                        break;
                                                    case "INVESTIGATIONAL_INTERVENTIONS":
                                                        currentSectionOfStudyDesignJobject.Properties().Where(attr => attr.Name != nameof(CurrentSectionsDTO.investigationalInterventions) && attr.Name != nameof(CurrentSectionsDTO.id) && attr.Name != nameof(CurrentSectionsDTO.sectionType)).ToList().ForEach(x => x.Remove());
                                                        break;
                                                    default:
                                                        break;
                                                }
                                                currentSectionOfStudyDesignArray.Add(currentSectionOfStudyDesignJobject);
                                            }
                                        }
                                    }
                                    studyDesignJobject.Add(nameof(ClinicalStudyDTO.currentSections), currentSectionOfStudyDesignArray);
                                    studyDesignsJArray.Add(studyDesignJobject);
                                }
                                currentsSectionsJobject.Add(nameof(CurrentSectionsDTO.studyDesigns), studyDesignsJArray);
                                break;
                            default:
                                break;
                        }
                        currentSectionArray.Add(currentsSectionsJobject);
                    }
                    
                }
            }
            clinicalStudyJsonObject.Add(nameof(ClinicalStudyDTO.currentSections), currentSectionArray);

            JObject returnJsonObject = new JObject();
            returnJsonObject.Add(nameof(PostStudyDTO.clinicalStudy), clinicalStudyJsonObject);
            returnJsonObject.Add(nameof(PostStudyDTO.auditTrail), JObject.Parse(JsonConvert.SerializeObject(studyEntity.auditTrail)));
            returnJsonObject.Add("links", JObject.Parse(JsonConvert.SerializeObject(LinksHelper.GetLinks(studyEntity.clinicalStudy.studyId,
                studyEntity.clinicalStudy.currentSections?.Where(x => x.studyDesigns != null).SelectMany(x => x.studyDesigns)?.Select(x => x.studyDesignId), 
                studyEntity.auditTrail.UsdmVersion, studyEntity.auditTrail.studyVersion), new JsonSerializerSettings
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
