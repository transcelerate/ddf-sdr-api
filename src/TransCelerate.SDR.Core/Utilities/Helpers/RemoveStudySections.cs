using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
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
        /// <param name="sections"></param>
        /// <param name="getStudySectionsDTO"></param>
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
        /// <param name="sections"></param>
        /// <param name="getStudySectionsDTO"></param>
        /// <returns>
        /// A <see cref="object"/> After removing sections which are not in the sections array           
        /// </returns>
        public static object RemoveSectionsForStudyDesign(string[] sections, GetStudySectionsDTO getStudySectionsDTO)
        {
            try
            {
                var jsonObject = JObject.Parse(JsonConvert.SerializeObject(getStudySectionsDTO));
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
    }
}
