using System;
using System.Collections.Generic;
using System.Text;
using TransCelerate.SDR.Core.DTO.Study;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class RemoveStudySections
    {
        public static object RemoveSections(string[] sections, GetStudySectionsDTO getStudySectionsDTO)
        {
            var jsonObject = JObject.Parse(JsonConvert.SerializeObject(getStudySectionsDTO));
            foreach (var item in Enum.GetNames(typeof(StudySections)))
            {
                sections = sections.Select(t => t.Trim().ToLower()).ToArray();               
                if (!sections.Contains(item))
                {
                    switch(item)
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

        public static object RemoveSectionsForStudyDesign(string[] sections, GetStudySectionsDTO getStudySectionsDTO)
        {
            var jsonObject = JObject.Parse(JsonConvert.SerializeObject(getStudySectionsDTO));
            jsonObject.Property(nameof(GetStudySectionsDTO.studyIndications)).Remove();
            jsonObject.Property(nameof(GetStudySectionsDTO.objectives)).Remove();
            if(getStudySectionsDTO.studyDesigns.Count()!=0)
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
    }
}
