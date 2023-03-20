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
        /// <param name="studyEntity">Study for which Id's to be generated</param>
        /// <returns>
        /// A <see cref="StudyEntity"/> after generating ID for all Sections        
        /// </returns>
        public static StudyEntity GenerateSectionId(StudyEntity studyEntity)
        {
            try
            {

                if (studyEntity.ClinicalStudy.CurrentSections != null)
                {
                    //Id Generation for section level
                    studyEntity.ClinicalStudy.CurrentSections.ForEach(x => x.CurrentSectionsId = IdGenerator.GenerateId());

                    if (studyEntity.ClinicalStudy.CurrentSections.Any(x => x.StudyDesigns != null))
                    {
                        studyEntity.ClinicalStudy.CurrentSections
                                            .FindAll(x => x.StudyDesigns != null)
                                            .ForEach(y => y.StudyDesigns
                                            .FindAll(n => n.CurrentSections != null)
                                            .ForEach(z => z.CurrentSections
                                            .ForEach(i => i.CurrentSectionsId = IdGenerator.GenerateId())));
                    }

                    //Id Generation for each section                  

                    //ObjectiveId
                    studyEntity.ClinicalStudy.CurrentSections
                                            .FindAll(x => x.Objectives != null)
                                            .ForEach(x => x.Objectives
                                            .ForEach(y => StudyObjectivesIdGenerator(y)));

                    //studyIndicationId
                    studyEntity.ClinicalStudy.CurrentSections
                                            .FindAll(x => x.StudyIndications != null)
                                            .ForEach(x => x.StudyIndications
                                            .ForEach(y => y.StudyIndicationId = IdGenerator.GenerateId()));


                    //studyDesignId
                    studyEntity.ClinicalStudy.CurrentSections
                                            .FindAll(x => x.StudyDesigns != null)
                                            .ForEach(x => x.StudyDesigns
                                            .ForEach(y => y.StudyDesignId = IdGenerator.GenerateId()));

                    //studyPopulationId
                    studyEntity.ClinicalStudy.CurrentSections
                                            .FindAll(x => x.StudyDesigns != null)
                                            .ForEach(y => y.StudyDesigns
                                            .FindAll(n => n.CurrentSections != null)
                                            .ForEach(z => z.CurrentSections
                                            .FindAll(n => n.StudyPopulations != null)
                                            .ForEach(p => p.StudyPopulations
                                            .ForEach(i => i.StudyPopulationId = IdGenerator.GenerateId()))));

                    //investigationalInterventionId
                    studyEntity.ClinicalStudy.CurrentSections
                                            .FindAll(x => x.StudyDesigns != null)
                                            .ForEach(y => y.StudyDesigns
                                            .FindAll(n => n.CurrentSections != null)
                                            .ForEach(z => z.CurrentSections
                                            .FindAll(x => x.InvestigationalInterventions != null)
                                            .ForEach(x => x.InvestigationalInterventions
                                            .ForEach(y => y.InvestigationalInterventionId = IdGenerator.GenerateId()))));

                    //plannedWorkFlowId and sub-elements Id's
                    studyEntity.ClinicalStudy.CurrentSections
                                            .FindAll(x => x.StudyDesigns != null)
                                            .ForEach(y => y.StudyDesigns
                                            .FindAll(n => n.CurrentSections != null)
                                            .ForEach(z => z.CurrentSections
                                            .FindAll(n => n.PlannedWorkflows != null)
                                            .ForEach(p => p.PlannedWorkflows
                                            .ForEach(i => StudyPlannedWorkFlowIdGenerator(i)))));

                    //studyCellId and sub-elements Id's
                    studyEntity.ClinicalStudy.CurrentSections
                                            .FindAll(x => x.StudyDesigns != null)
                                            .ForEach(y => y.StudyDesigns
                                            .FindAll(n => n.CurrentSections != null)
                                            .ForEach(z => z.CurrentSections
                                            .FindAll(n => n.StudyCells != null)
                                            .ForEach(p => p.StudyCells
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
        /// <param name="studyObjectivesEntity">Study Objectives for which Id's to be generated</param>
        /// <returns>
        /// A <see cref="StudyObjectiveEntity"/> after generating ID for StudyObjective Section        
        /// </returns>
        public static StudyObjectiveEntity StudyObjectivesIdGenerator(StudyObjectiveEntity studyObjectivesEntity)
        {
            try
            {
                //study objectives Id
                studyObjectivesEntity.ObjectiveId = IdGenerator.GenerateId();
                if (studyObjectivesEntity.Endpoints != null)
                    studyObjectivesEntity.Endpoints.ForEach(x => x.EndPointsId = IdGenerator.GenerateId());

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
        /// <param name="plannedWorkFlowEntity">Planned workflow for which Id's to be generated</param>
        /// <returns>
        /// A <see cref="PlannedWorkFlowEntity"/> after generating ID for PlannedWorkFlow Section        
        /// </returns>
        public static PlannedWorkFlowEntity StudyPlannedWorkFlowIdGenerator(PlannedWorkFlowEntity plannedWorkFlowEntity)
        {
            try
            {
                //plannedWorkFlowId and sub-elements Id's         
                plannedWorkFlowEntity.PlannedWorkFlowId = IdGenerator.GenerateId();
                plannedWorkFlowEntity.PlannedWorkFlowId = IdGenerator.GenerateId();
                if (plannedWorkFlowEntity.StartPoint != null)
                    plannedWorkFlowEntity.StartPoint.PointInTimeId = IdGenerator.GenerateId();

                if (plannedWorkFlowEntity.EndPoint != null)
                    plannedWorkFlowEntity.EndPoint.PointInTimeId = IdGenerator.GenerateId();

                if (plannedWorkFlowEntity.WorkflowItemMatrix != null)
                {
                    plannedWorkFlowEntity.WorkflowItemMatrix.WorkFlowItemMatrixId = IdGenerator.GenerateId();
                    if (plannedWorkFlowEntity.WorkflowItemMatrix.Matrix != null)
                    {
                        plannedWorkFlowEntity.WorkflowItemMatrix.Matrix.FindAll(x => x != null).ForEach(m =>
                        {
                            m.MatrixId = IdGenerator.GenerateId();
                            if (m.Items != null)
                            {
                                m.Items.FindAll(x => x != null).ForEach(item =>
                                {
                                    item.ItemId = IdGenerator.GenerateId();
                                    if (item.FromPointInTime != null)
                                        item.FromPointInTime.PointInTimeId = IdGenerator.GenerateId();

                                    if (item.ToPointInTime != null)
                                        item.ToPointInTime.PointInTimeId = IdGenerator.GenerateId();

                                    if (item.Activity != null)
                                    {
                                        item.Activity.ActivityId = IdGenerator.GenerateId();
                                        if (item.Activity.StudyDataCollection != null)
                                            item.Activity.StudyDataCollection.FindAll(x => x != null).ForEach(sdc => sdc.StudyDataCollectionId = IdGenerator.GenerateId());
                                    }
                                    if (item.Encounter != null)
                                    {
                                        item.Encounter.EncounterId = IdGenerator.GenerateId();
                                        if (item.Encounter.StartRule != null)
                                            item.Encounter.StartRule.RuleId = IdGenerator.GenerateId();

                                        if (item.Encounter.EndRule != null)
                                            item.Encounter.EndRule.RuleId = IdGenerator.GenerateId();

                                        if (item.Encounter.Epoch != null)
                                            item.Encounter.Epoch.EpochId = IdGenerator.GenerateId();
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
        /// <param name="studyCellEntity">Study Cells for which Id's to be generated</param>
        /// <returns>
        /// A <see cref="StudyCellEntity"/> after generating ID for StudyCell Section        
        /// </returns>
        public static StudyCellEntity StudyCellsIdGenerator(StudyCellEntity studyCellEntity)
        {
            try
            {
                //studyCellId and sub-elements Id's
                studyCellEntity.StudyCellId = IdGenerator.GenerateId();
                if (studyCellEntity.StudyArm != null)
                    studyCellEntity.StudyArm.StudyArmId = IdGenerator.GenerateId();

                if (studyCellEntity.StudyEpoch != null)
                    studyCellEntity.StudyEpoch.StudyEpochId = IdGenerator.GenerateId();

                if (studyCellEntity.StudyElements != null)
                {
                    studyCellEntity.StudyElements.FindAll(x => x != null).ForEach(e =>
                    {
                        e.StudyElementId = IdGenerator.GenerateId();
                        if (e.StartRule != null)
                            e.StartRule.RuleId = IdGenerator.GenerateId();
                        if (e.EndRule != null)
                            e.EndRule.RuleId = IdGenerator.GenerateId();
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
        /// <param name="studyDesignEntity">Study Design for which Id's to be generated</param>
        /// <returns>
        /// A <see cref="StudyDesignEntity"/> after generating ID for StudyDesign Section        
        /// </returns>
        public static StudyDesignEntity StudyDesignIdGenerator(StudyDesignEntity studyDesignEntity)
        {
            try
            {
                //studyDesignId
                studyDesignEntity.StudyDesignId = IdGenerator.GenerateId();

                if (studyDesignEntity.CurrentSections != null)
                {

                    studyDesignEntity.CurrentSections.ForEach(x => x.CurrentSectionsId = IdGenerator.GenerateId());
                    //studyPopulationId
                    studyDesignEntity.CurrentSections.FindAll(n => n.StudyPopulations != null)
                                                     .ForEach(p => p.StudyPopulations
                                                     .ForEach(i => i.StudyPopulationId = IdGenerator.GenerateId()));

                    //plannedWorkFlowId and sub-elements Id's
                    studyDesignEntity.CurrentSections.FindAll(n => n.PlannedWorkflows != null)
                                                     .ForEach(p => p.PlannedWorkflows
                                                     .ForEach(i => StudyPlannedWorkFlowIdGenerator(i)));

                    //studyCellId and sub-elements Id's
                    studyDesignEntity.CurrentSections.FindAll(n => n.StudyCells != null)
                                                     .ForEach(p => p.StudyCells
                                                     .ForEach(i => StudyCellsIdGenerator(i)));

                    //InvestigationalIntervention Id
                    studyDesignEntity.CurrentSections.FindAll(n => n.InvestigationalInterventions != null)
                                                     .ForEach(p => p.InvestigationalInterventions
                                                     .ForEach(i => i.InvestigationalInterventionId = IdGenerator.GenerateId()));
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
