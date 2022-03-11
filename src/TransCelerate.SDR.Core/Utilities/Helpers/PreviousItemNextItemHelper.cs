using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransCelerate.SDR.Core.Entities.Study;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class PreviousItemNextItemHelper
    {
        public static StudyEntity PreviousItemsNextItemsWraper(StudyEntity studyEntity)
        {
            if (studyEntity.clinicalStudy.currentSections != null)
            {
                if (studyEntity.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).Count() != 0)
                {
                    List<StudyDesignEntity> studyDesignList = studyEntity.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns;
                    if (studyDesignList.Count() != 0)
                    {                        
                        foreach (var studyDesign in studyDesignList)
                        {                            
                            if(studyDesign.currentSections != null)
                            {
                                studyDesign.currentSections.FindAll(x => x.plannedWorkflows != null)
                                                              .ForEach(x => x.plannedWorkflows
                                                                    .ForEach(p =>
                                                                    {
                                                                        if (p.workflowItemMatrix != null)
                                                                        {
                                                                            if (p.workflowItemMatrix.matrix != null)
                                                                            {
                                                                                p.workflowItemMatrix.matrix
                                                                                        .ForEach(m => m.items = GetPreviousNextItems(m.items));
                                                                            }
                                                                        }
                                                                    }));
                            }
                        }
                    }
                    
                }
            }            
            return studyEntity;
        }
        public static List<ItemEntity> GetPreviousNextItems(List<ItemEntity> itemEntities)
        {
            if (itemEntities!=null && itemEntities.Count() != 0)
            {
                for (int i = 0; i < itemEntities.Count(); i++)
                {
                    var previousItems = new List<string>();
                    var nextItems = new List<string>();
                    if (i == 0)
                    {
                        for (int j = 1; j < itemEntities.Count(); j++)
                        {
                            nextItems.Add(itemEntities[j].itemId);
                        }
                    }
                    else if (i == itemEntities.Count() - 1)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            previousItems.Add(itemEntities[j].itemId);
                        }
                    }
                    else
                    {
                        for (int j = 0; j < i; j++)
                        {
                            previousItems.Add(itemEntities[j].itemId);
                        }
                        for (int j = i + 1; j < itemEntities.Count(); j++)
                        {
                            nextItems.Add(itemEntities[j].itemId);
                        }
                    }
                    itemEntities[i].previousItemsInSequence = previousItems;
                    itemEntities[i].nextItemsInSequence = nextItems;
                }
            }
            return itemEntities;
        }
    }
}
