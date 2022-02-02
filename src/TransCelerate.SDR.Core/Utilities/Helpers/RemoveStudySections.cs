using System;
using System.Collections.Generic;
using System.Text;
using TransCelerate.SDR.Core.DTO.Study;
using System.Linq;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class RemoveStudySections
    {
        public static GetStudySectionsDTO RemoveSections(string[] sections, GetStudySectionsDTO getStudySectionsDTO)
        {
            foreach (var item in Enum.GetNames(typeof(StudySections)))
            {
                sections = sections.Select(t => t.Trim().ToLower()).ToArray();
                if (!sections.Contains(item))
                {
                    switch(item)
                    {
                        case "study_indications":
                            getStudySectionsDTO.studyIndications = null;
                            break;
                        case "study_objectives":
                            getStudySectionsDTO.objectives = null;
                            break;
                        case "study_investigational_interventions":
                            getStudySectionsDTO.investigationalInterventions = null;
                            break;                      
                        case "study_protocol":
                            getStudySectionsDTO.studyProtocol = null;
                            break;
                        case "study_design":
                            getStudySectionsDTO.studyDesigns = null;
                            break;
                        default:
                            break;                            
                    }
                }
            }
            return getStudySectionsDTO;
        }

        public static GetStudySectionsDTO RemoveSectionsForStudyDesign(string[] sections, GetStudySectionsDTO getStudySectionsDTO)
        {
            getStudySectionsDTO.studyIndications = null;
            getStudySectionsDTO.objectives = null;
            getStudySectionsDTO.investigationalInterventions = null;
            getStudySectionsDTO.studyProtocol = null;              
            if(sections.Count()!=0)
            {
                foreach (var item in Enum.GetNames(typeof(StudyDesignSections)))
                {
                    sections = sections.Select(t => t.Trim().ToLower()).ToArray();
                    if (!sections.Contains(item))
                    {
                        switch (item)
                        {
                            case "study_planned_workflow":
                                getStudySectionsDTO.studyDesigns.ForEach(x => x.plannedWorkflows.RemoveAll(y => y != null));
                                break;
                            case "study_target_populations":
                                getStudySectionsDTO.studyDesigns.ForEach(x => x.studyPopulations.RemoveAll(y => y != null));
                                break;
                            case "study_cells":
                                getStudySectionsDTO.studyDesigns.ForEach(x => x.studyCells.RemoveAll(y => y != null));
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return getStudySectionsDTO;
        }
    }
}
