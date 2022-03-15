using System;
using System.Linq;
using TransCelerate.SDR.Core.Entities.Study;


namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    /// <summary>
    /// This class is for generating Id for each Sections
    /// </summary>
    public static class SectionIdGenerator
    {
        /// <summary>
        /// Generate Id for all the sections
        /// </summary>
        /// <param name="studyEntity"></param>
        /// <returns>
        /// A <see cref="StudyEntity"/> after generating ID for all Sections        
        /// </returns>
        public static StudyEntity GenerateSectionId(StudyEntity studyEntity)
        {
            try
            {                

                if (studyEntity.clinicalStudy.currentSections != null)
                {
                    //Id Generation for section level
                    studyEntity.clinicalStudy.currentSections.ForEach(x => x.currentSectionsId = IdGenerator.GenerateId());

                    if (studyEntity.clinicalStudy.currentSections.Any(x => x.studyDesigns != null))
                    {
                        studyEntity.clinicalStudy.currentSections
                                            .FindAll(x => x.studyDesigns != null)
                                            .ForEach(y => y.studyDesigns
                                            .FindAll(n => n.currentSections != null)
                                            .ForEach(z => z.currentSections
                                            .ForEach(i => i.currentSectionsId = IdGenerator.GenerateId())));
                    }

                    //Id Generation for each section                  

                    //ObjectiveId
                    studyEntity.clinicalStudy.currentSections
                                            .FindAll(x => x.objectives != null)
                                            .ForEach(x => x.objectives
                                            .ForEach(y => StudyObjectivesIdGenerator(y)));

                    //studyIndicationId
                    studyEntity.clinicalStudy.currentSections
                                            .FindAll(x => x.studyIndications != null)
                                            .ForEach(x => x.studyIndications
                                            .ForEach(y => y.studyIndicationId = IdGenerator.GenerateId()));

                   
                    //studyDesignId
                    studyEntity.clinicalStudy.currentSections
                                            .FindAll(x => x.studyDesigns != null)
                                            .ForEach(x => x.studyDesigns
                                            .ForEach(y => y.studyDesignId = IdGenerator.GenerateId()));

                    //studyPopulationId
                    studyEntity.clinicalStudy.currentSections
                                            .FindAll(x => x.studyDesigns != null)
                                            .ForEach(y => y.studyDesigns
                                            .FindAll(n => n.currentSections != null)
                                            .ForEach(z => z.currentSections
                                            .FindAll(n => n.studyPopulations != null)
                                            .ForEach(p => p.studyPopulations
                                            .ForEach(i => i.studyPopulationId = IdGenerator.GenerateId()))));

                    //investigationalInterventionId
                    studyEntity.clinicalStudy.currentSections
                                            .FindAll(x => x.studyDesigns != null)
                                            .ForEach(y => y.studyDesigns
                                            .FindAll(n => n.currentSections != null)
                                            .ForEach(z => z.currentSections
                                            .FindAll(x => x.investigationalInterventions != null)
                                            .ForEach(x => x.investigationalInterventions
                                            .ForEach(y => y.investigationalInterventionId = IdGenerator.GenerateId()))));

                    //plannedWorkFlowId and sub-elements Id's
                    studyEntity.clinicalStudy.currentSections
                                            .FindAll(x => x.studyDesigns != null)
                                            .ForEach(y => y.studyDesigns
                                            .FindAll(n => n.currentSections != null)
                                            .ForEach(z => z.currentSections
                                            .FindAll(n => n.plannedWorkflows != null)
                                            .ForEach(p => p.plannedWorkflows
                                            .ForEach(i => StudyPlannedWorkFlowIdGenerator(i)))));

                    //studyCellId and sub-elements Id's
                    studyEntity.clinicalStudy.currentSections
                                            .FindAll(x => x.studyDesigns != null)
                                            .ForEach(y => y.studyDesigns
                                            .FindAll(n => n.currentSections != null)
                                            .ForEach(z => z.currentSections
                                            .FindAll(n => n.studyCells != null)
                                            .ForEach(p => p.studyCells
                                            .ForEach(i => StudyCellsIdGenerator(i)))));
                }
                return studyEntity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Generate Id for Study Objectives
        /// </summary>
        /// <param name="studyObjectivesEntity"></param>
        /// <returns>
        /// A <see cref="StudyObjectiveEntity"/> after generating ID for StudyObjective Section        
        /// </returns>
        public static StudyObjectiveEntity StudyObjectivesIdGenerator(StudyObjectiveEntity studyObjectivesEntity)
        {
            try
            {
                //study objectives Id
                studyObjectivesEntity.objectiveId = IdGenerator.GenerateId();
                if (studyObjectivesEntity.endpoints != null)
                    studyObjectivesEntity.endpoints.ForEach(x => x.endPointsId = IdGenerator.GenerateId());

                return studyObjectivesEntity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Generate Id for Study PlannedWorkFlows
        /// </summary>
        /// <param name="plannedWorkFlowEntity"></param>
        /// <returns>
        /// A <see cref="PlannedWorkFlowEntity"/> after generating ID for PlannedWorkFlow Section        
        /// </returns>
        public static PlannedWorkFlowEntity StudyPlannedWorkFlowIdGenerator(PlannedWorkFlowEntity plannedWorkFlowEntity)
        {
            try
            {
                //plannedWorkFlowId and sub-elements Id's         
                plannedWorkFlowEntity.plannedWorkFlowId = IdGenerator.GenerateId();
                plannedWorkFlowEntity.plannedWorkFlowId = IdGenerator.GenerateId();
                if (plannedWorkFlowEntity.startPoint != null)
                    plannedWorkFlowEntity.startPoint.pointInTimeId = IdGenerator.GenerateId();

                if (plannedWorkFlowEntity.endPoint != null)
                    plannedWorkFlowEntity.endPoint.pointInTimeId = IdGenerator.GenerateId();

                if (plannedWorkFlowEntity.workflowItemMatrix != null)
                {
                    plannedWorkFlowEntity.workflowItemMatrix.workFlowItemMatrixId = IdGenerator.GenerateId();
                    if (plannedWorkFlowEntity.workflowItemMatrix.matrix != null)
                    {
                        plannedWorkFlowEntity.workflowItemMatrix.matrix.FindAll(x => x != null).ForEach(m =>
                        {
                            m.matrixId = IdGenerator.GenerateId();
                            if (m.items != null)
                            {
                                m.items.FindAll(x => x != null).ForEach(item =>
                                {
                                    item.itemId = IdGenerator.GenerateId();
                                    if (item.fromPointInTime != null)
                                        item.fromPointInTime.pointInTimeId = IdGenerator.GenerateId();

                                    if (item.toPointInTime != null)
                                        item.toPointInTime.pointInTimeId = IdGenerator.GenerateId();

                                    if (item.activity != null)
                                    {
                                        item.activity.activityId = IdGenerator.GenerateId();
                                        if (item.activity.studyDataCollection != null)
                                            item.activity.studyDataCollection.FindAll(x => x != null).ForEach(sdc => sdc.studyDataCollectionId = IdGenerator.GenerateId());
                                    }
                                    if (item.encounter != null)
                                    {
                                        item.encounter.encounterId = IdGenerator.GenerateId();
                                        if (item.encounter.startRule != null)
                                            item.encounter.startRule.RuleId = IdGenerator.GenerateId();

                                        if (item.encounter.endRule != null)
                                            item.encounter.endRule.RuleId = IdGenerator.GenerateId();

                                        if (item.encounter.epoch != null)
                                            item.encounter.epoch.epochId = IdGenerator.GenerateId();
                                    }
                                });
                            }
                        });
                    }
                }

                return plannedWorkFlowEntity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Generate Id for Study Cells
        /// </summary>
        /// <param name="studyCellEntity"></param>
        /// <returns>
        /// A <see cref="StudyCellEntity"/> after generating ID for StudyCell Section        
        /// </returns>
        public static StudyCellEntity StudyCellsIdGenerator(StudyCellEntity studyCellEntity)
        {
            try
            {
                //studyCellId and sub-elements Id's
                studyCellEntity.studyCellId = IdGenerator.GenerateId();
                if (studyCellEntity.studyArm != null)
                    studyCellEntity.studyArm.studyArmId = IdGenerator.GenerateId();

                if (studyCellEntity.studyEpoch != null)
                    studyCellEntity.studyEpoch.studyEpochId = IdGenerator.GenerateId();

                if (studyCellEntity.studyElements != null)
                {
                    studyCellEntity.studyElements.FindAll(x => x != null).ForEach(e => {
                        e.studyElementId = IdGenerator.GenerateId();
                        e.startRule.RuleId = IdGenerator.GenerateId();
                        e.endRule.RuleId = IdGenerator.GenerateId();
                    });
                }

                return studyCellEntity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Generate Id for Study Designs
        /// </summary>
        /// <param name="studyDesignEntity"></param>
        /// <returns>
        /// A <see cref="StudyDesignEntity"/> after generating ID for StudyDesign Section        
        /// </returns>
        public static StudyDesignEntity StudyDesignIdGenerator(StudyDesignEntity studyDesignEntity)
        {
            try
            {
                //studyDesignId
                studyDesignEntity.studyDesignId = IdGenerator.GenerateId();

                if (studyDesignEntity.currentSections != null)
                {
                    //studyPopulationId
                    studyDesignEntity.currentSections.FindAll(n => n.studyPopulations != null)
                                                     .ForEach(p => p.studyPopulations
                                                     .ForEach(i => i.studyPopulationId = IdGenerator.GenerateId()));

                    //plannedWorkFlowId and sub-elements Id's
                    studyDesignEntity.currentSections.FindAll(n => n.plannedWorkflows != null)
                                                     .ForEach(p => p.plannedWorkflows
                                                     .ForEach(i => StudyPlannedWorkFlowIdGenerator(i)));

                    //studyCellId and sub-elements Id's
                    studyDesignEntity.currentSections.FindAll(n => n.studyCells != null)
                                                     .ForEach(p => p.studyCells
                                                     .ForEach(i => StudyCellsIdGenerator(i)));

                    //InvestigationalIntervention Id
                    studyDesignEntity.currentSections.FindAll(n => n.investigationalInterventions != null)
                                                     .ForEach(p => p.investigationalInterventions
                                                     .ForEach(i => i.investigationalInterventionId = IdGenerator.GenerateId()));
                }

                return studyDesignEntity;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
