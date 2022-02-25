using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using TransCelerate.SDR.Core.Entities.Study;


namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class SectionIdGenerator
    {
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
                                            .ForEach(y => {
                                                y.objectiveId = IdGenerator.GenerateId();
                                                y.endpoints.FindAll(x=>x!=null)
                                                           .ForEach(x => x.endPointsId = IdGenerator.GenerateId());
                                            }));

                    //studyIndicationId
                    studyEntity.clinicalStudy.currentSections
                                            .FindAll(x => x.studyIndications != null)
                                            .ForEach(x => x.studyIndications
                                            .ForEach(y => y.studyIndicationId = IdGenerator.GenerateId()));

                    //Removed Study Protocol
                    //protocolId
                    //if(studyEntity.clinicalStudy.currentSections.Any(x => x.studyProtocol != null))
                    //{
                    //    studyEntity.clinicalStudy.currentSections.Find(x => x.studyProtocol != null)
                    //                                         .studyProtocol.protocolId = IdGenerator.GenerateId();
                    //}
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
                                            .ForEach(i => {
                                                i.plannedWorkFlowId = IdGenerator.GenerateId();
                                                i.startPoint.pointInTimeId = IdGenerator.GenerateId();
                                                i.endPoint.pointInTimeId = IdGenerator.GenerateId();
                                                i.workflowItemMatrix.workFlowItemMatrixId = IdGenerator.GenerateId();
                                                i.workflowItemMatrix.matrix.ForEach(m =>
                                                {
                                                    m.matrixId = IdGenerator.GenerateId();
                                                    m.items.ForEach(item =>
                                                    {
                                                        item.itemId = IdGenerator.GenerateId();
                                                        item.fromPointInTime.pointInTimeId = IdGenerator.GenerateId();
                                                        item.toPointInTime.pointInTimeId = IdGenerator.GenerateId();
                                                        item.activity.activityId = IdGenerator.GenerateId();
                                                        item.activity.studyDataCollection.ForEach(sdc => sdc.studyDataCollectionId = IdGenerator.GenerateId());
                                                        item.encounter.encounterId = IdGenerator.GenerateId();
                                                        item.encounter.startRule.RuleId = IdGenerator.GenerateId();
                                                        item.encounter.endRule.RuleId = IdGenerator.GenerateId();
                                                        item.encounter.epoch.epochId = IdGenerator.GenerateId(); 
                                                    });
                                                });
                                                //i.transitions.ForEach(t => {
                                                //    t.transitionId = IdGenerator.GenerateId();
                                                //    t.fromPointInTime.pointInTimeId = IdGenerator.GenerateId();
                                                //    t.toPointInTime.pointInTimeId = IdGenerator.GenerateId();
                                                //    t.transitionRule.transitionRuleId = IdGenerator.GenerateId();
                                                //    t.transitionCriteria.ForEach(c => c.transitionCriteriaId = IdGenerator.GenerateId());
                                                //});
                                            }))));

                    //studyCellId and sub-elements Id's
                    studyEntity.clinicalStudy.currentSections
                                            .FindAll(x => x.studyDesigns != null)
                                            .ForEach(y => y.studyDesigns
                                            .FindAll(n => n.currentSections != null)
                                            .ForEach(z => z.currentSections
                                            .FindAll(n => n.studyCells != null)
                                            .ForEach(p => p.studyCells
                                            .ForEach(i => {
                                                i.studyCellId = IdGenerator.GenerateId();
                                                i.studyArm.studyArmId = IdGenerator.GenerateId();
                                                i.studyEpoch.studyEpochId = IdGenerator.GenerateId();
                                                i.studyElements.ForEach(e => {
                                                    e.studyElementId = IdGenerator.GenerateId();
                                                    e.startRule.RuleId = IdGenerator.GenerateId();
                                                    e.endRule.RuleId = IdGenerator.GenerateId();
                                                });
                                            }))));
                }
                return studyEntity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static StudyObjectiveEntity StudyObjectivesIdGenerator(StudyObjectiveEntity studyObjectivesEntity)
        {
            //study objectives Id
            studyObjectivesEntity.objectiveId = IdGenerator.GenerateId();
            studyObjectivesEntity.endpoints.ForEach(x => x.endPointsId = IdGenerator.GenerateId());
          
            return studyObjectivesEntity;
        }       
        public static PlannedWorkFlowEntity StudyPlannedWorkFlowIdGenerator(PlannedWorkFlowEntity plannedWorkFlowEntity)
        {
            //plannedWorkFlowId and sub-elements Id's         
            plannedWorkFlowEntity.plannedWorkFlowId = IdGenerator.GenerateId();
            plannedWorkFlowEntity.startPoint.pointInTimeId = IdGenerator.GenerateId();
            plannedWorkFlowEntity.endPoint.pointInTimeId = IdGenerator.GenerateId();
            plannedWorkFlowEntity.workflowItemMatrix.workFlowItemMatrixId = IdGenerator.GenerateId();
            plannedWorkFlowEntity.workflowItemMatrix.matrix.ForEach(m =>
                                {
                                    m.matrixId = IdGenerator.GenerateId();
                                    m.items.ForEach(item =>
                                    {
                                        item.itemId = IdGenerator.GenerateId();
                                        item.fromPointInTime.pointInTimeId = IdGenerator.GenerateId();
                                        item.toPointInTime.pointInTimeId = IdGenerator.GenerateId();
                                        item.activity.activityId = IdGenerator.GenerateId();
                                        item.activity.studyDataCollection.ForEach(sdc => sdc.studyDataCollectionId = IdGenerator.GenerateId());
                                        item.encounter.encounterId = IdGenerator.GenerateId();
                                        item.encounter.startRule.RuleId = IdGenerator.GenerateId();
                                        item.encounter.endRule.RuleId = IdGenerator.GenerateId();
                                        item.encounter.epoch.epochId = IdGenerator.GenerateId();
                                    });
                                });
            //plannedWorkFlowEntity.transitions.ForEach(t => {
            //                             t.transitionId = IdGenerator.GenerateId();
            //                             t.fromPointInTime.pointInTimeId = IdGenerator.GenerateId();
            //                             t.toPointInTime.pointInTimeId = IdGenerator.GenerateId();
            //                             t.transitionRule.transitionRuleId = IdGenerator.GenerateId();
            //                             t.transitionCriteria.ForEach(c => c.transitionCriteriaId = IdGenerator.GenerateId());
            //                         });

            return plannedWorkFlowEntity;
        }
        public static StudyCellEntity StudyCellsIdGenerator(StudyCellEntity studyCellEntity)
        {
            //studyCellId and sub-elements Id's
            studyCellEntity.studyCellId = IdGenerator.GenerateId();
            studyCellEntity.studyArm.studyArmId = IdGenerator.GenerateId();
            studyCellEntity.studyEpoch.studyEpochId = IdGenerator.GenerateId();
            studyCellEntity.studyElements.ForEach(e => {
                                            e.studyElementId = IdGenerator.GenerateId();
                                            e.startRule.RuleId = IdGenerator.GenerateId();
                                            e.endRule.RuleId = IdGenerator.GenerateId();
                                        });
            
            return studyCellEntity;
        }

        public static StudyDesignEntity StudyDesignIdGenerator(StudyDesignEntity studyDesignEntity)
        {
            //studyDesignId
            studyDesignEntity.studyDesignId = IdGenerator.GenerateId();
            
            if(studyDesignEntity.currentSections!=null)
            {
                //studyPopulationId
                studyDesignEntity.currentSections.FindAll(n => n.studyPopulations != null)
                                                 .ForEach(p => p.studyPopulations
                                                 .ForEach(i => i.studyPopulationId = IdGenerator.GenerateId()));

                //plannedWorkFlowId and sub-elements Id's
                studyDesignEntity.currentSections.FindAll(n => n.plannedWorkflows != null)
                                                 .ForEach(p => p.plannedWorkflows
                                                 .ForEach(i => {
                                                     i.plannedWorkFlowId = IdGenerator.GenerateId();
                                                     i.startPoint.pointInTimeId = IdGenerator.GenerateId();
                                                     i.endPoint.pointInTimeId = IdGenerator.GenerateId();
                                                     i.workflowItemMatrix.workFlowItemMatrixId = IdGenerator.GenerateId();
                                                     i.workflowItemMatrix.matrix.ForEach(m =>
                                                     {
                                                         m.matrixId = IdGenerator.GenerateId();
                                                         m.items.ForEach(item =>
                                                         {
                                                             item.itemId = IdGenerator.GenerateId();
                                                             item.fromPointInTime.pointInTimeId = IdGenerator.GenerateId();
                                                             item.toPointInTime.pointInTimeId = IdGenerator.GenerateId();
                                                             item.activity.activityId = IdGenerator.GenerateId();
                                                             item.activity.studyDataCollection.ForEach(sdc => sdc.studyDataCollectionId = IdGenerator.GenerateId());
                                                             item.encounter.encounterId = IdGenerator.GenerateId();
                                                             item.encounter.startRule.RuleId = IdGenerator.GenerateId();
                                                             item.encounter.endRule.RuleId = IdGenerator.GenerateId();
                                                             item.encounter.epoch.epochId = IdGenerator.GenerateId();
                                                         });
                                                     });
                                                     //i.transitions.ForEach(t => {
                                                     //    t.transitionId = IdGenerator.GenerateId();
                                                     //    t.fromPointInTime.pointInTimeId = IdGenerator.GenerateId();
                                                     //    t.toPointInTime.pointInTimeId = IdGenerator.GenerateId();
                                                     //    t.transitionRule.transitionRuleId = IdGenerator.GenerateId();
                                                     //    t.transitionCriteria.ForEach(c => c.transitionCriteriaId = IdGenerator.GenerateId());
                                                     //});
                                                 }));

                //studyCellId and sub-elements Id's
                studyDesignEntity.currentSections.FindAll(n => n.studyCells != null)
                                                 .ForEach(p => p.studyCells
                                                 .ForEach(i => {
                                                     i.studyCellId = IdGenerator.GenerateId();
                                                     i.studyArm.studyArmId = IdGenerator.GenerateId();
                                                     i.studyEpoch.studyEpochId = IdGenerator.GenerateId();
                                                     i.studyElements.ForEach(e => {
                                                         e.studyElementId = IdGenerator.GenerateId();
                                                         e.startRule.RuleId = IdGenerator.GenerateId();
                                                         e.endRule.RuleId = IdGenerator.GenerateId();
                                                     });
                                                 }));

                //InvestigationalIntervention Id
                studyDesignEntity.currentSections.FindAll(n => n.investigationalInterventions != null)
                                                 .ForEach(p => p.investigationalInterventions
                                                 .ForEach(i => i.investigationalInterventionId = IdGenerator.GenerateId()));
            }
            
            return studyDesignEntity;
        }
    }
}
