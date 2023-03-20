using System;
using System.Collections.Generic;
using TransCelerate.SDR.Core.Entities.Study;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    /// <summary>
    /// This class is to get Previous Items and Next Items for a Study
    /// </summary>
    public static class PreviousItemNextItemHelper
    {
        /// <summary>
        /// Get the ItemList from the Study
        /// </summary>
        /// <param name="studyEntity">Study Entity for which Previous and Next Items need to be created</param>
        /// <returns>
        /// A <see cref="StudyEntity"/> after creating previuosItems and NextItems Array        
        /// </returns>
        public static StudyEntity PreviousItemsNextItemsWraper(StudyEntity studyEntity)
        {
            try
            {
                if (studyEntity.ClinicalStudy.CurrentSections != null)
                {
                    if (studyEntity.ClinicalStudy.CurrentSections.FindAll(x => x.StudyDesigns != null).Count != 0)
                    {
                        List<StudyDesignEntity> studyDesignList = studyEntity.ClinicalStudy.CurrentSections.Find(x => x.StudyDesigns != null).StudyDesigns;
                        if (studyDesignList.Count != 0)
                        {
                            foreach (var studyDesign in studyDesignList)
                            {
                                if (studyDesign.CurrentSections != null)
                                {
                                    studyDesign.CurrentSections.FindAll(x => x.PlannedWorkflows != null)
                                                                  .ForEach(x => x.PlannedWorkflows
                                                                        .ForEach(p =>
                                                                        {
                                                                            if (p.WorkflowItemMatrix != null)
                                                                            {
                                                                                if (p.WorkflowItemMatrix.Matrix != null)
                                                                                {
                                                                                    p.WorkflowItemMatrix.Matrix
                                                                                            .ForEach(m => m.Items = GetPreviousNextItems(m.Items));
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
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Generate the Previous Items and Next Items
        /// </summary>
        /// <param name="itemEntities">Item List for which Previous and Next Items need to be created</param>
        /// <returns>
        /// A <see cref="List{ItemEntity}"/> after creating previuosItems and NextItems Array            
        /// </returns>
        public static List<ItemEntity> GetPreviousNextItems(List<ItemEntity> itemEntities)
        {
            try
            {
                if (itemEntities != null && itemEntities.Count != 0)
                {
                    for (int i = 0; i < itemEntities.Count; i++)
                    {
                        var previousItems = new List<string>();
                        var nextItems = new List<string>();
                        if (i == 0)
                        {
                            for (int j = 1; j < itemEntities.Count; j++)
                            {
                                nextItems.Add(itemEntities[j].ItemId);
                            }
                        }
                        else if (i == itemEntities.Count - 1)
                        {
                            for (int j = 0; j < i; j++)
                            {
                                previousItems.Add(itemEntities[j].ItemId);
                            }
                        }
                        else
                        {
                            for (int j = 0; j < i; j++)
                            {
                                previousItems.Add(itemEntities[j].ItemId);
                            }
                            for (int j = i + 1; j < itemEntities.Count; j++)
                            {
                                nextItems.Add(itemEntities[j].ItemId);
                            }
                        }
                        itemEntities[i].PreviousItemsInSequence = previousItems;
                        itemEntities[i].NextItemsInSequence = nextItems;
                    }
                }
                return itemEntities;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
