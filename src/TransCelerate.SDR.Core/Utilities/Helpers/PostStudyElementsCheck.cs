using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Entities.Study;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class PostStudyElementsCheck
    {
        #region Full Study Check
        /// <summary>
        /// This method is used to check whether the incoming study and existing study are same
        /// </summary>
        /// <param name="incoming">Incoming study defenitions</param>
        /// <param name="existing">Existing study defenitions</param>
        /// <returns>        
        /// <see langword="true"/> If incoming and existing study entities are identical
        /// </returns>
        public static bool StudyComparison(StudyEntity incoming, StudyEntity existing)
        {
            try
            {
                RemoveId(incoming);
                RemoveId(existing);

                return JsonObjectCheck(incoming, existing);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// This method will check whether two Json objects are same or not
        /// </summary>
        /// <param name="incoming">Object of Incoming study defenitions</param>
        /// <param name="existing">Object of Existing study defenitions</param>
        /// <returns>        
        /// <see langword="true"/> If incoming and existing study entities are identical
        /// </returns>
        public static bool JsonObjectCheck(object incoming, object existing)
        {
            try
            {
                return JToken.DeepEquals(JObject.Parse(JsonConvert.SerializeObject(incoming)),
                               JObject.Parse(JsonConvert.SerializeObject(existing)));
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// This method will remove all the id fields in the study
        /// </summary>
        /// <param name="studyEntity">Study defenitions for which Id fields must be removed</param>
        /// <returns>
        /// A <see cref="StudyEntity"/> after removing all ID fields        
        /// </returns>
        public static StudyEntity RemoveId(StudyEntity studyEntity)
        {
            try
            {
                studyEntity.ClinicalStudy.StudyIdentifiers.ForEach(x => x.StudyIdentifierId = null);
                if (studyEntity.ClinicalStudy.CurrentSections != null)
                {
                    //Id Generation for section level
                    studyEntity.ClinicalStudy.CurrentSections.ForEach(x => x.CurrentSectionsId = null);

                    if (studyEntity.ClinicalStudy.CurrentSections.Any(x => x.StudyDesigns != null))
                    {
                        studyEntity.ClinicalStudy.CurrentSections
                                            .FindAll(x => x.StudyDesigns != null)
                                            .ForEach(y => y.StudyDesigns
                                            .FindAll(n => n.CurrentSections != null)
                                            .ForEach(z => z.CurrentSections
                                            .ForEach(i => i.CurrentSectionsId = null)));
                    }

                    //Remove Id for each section

                    //ObjectiveId
                    studyEntity.ClinicalStudy.CurrentSections
                                            .FindAll(x => x.Objectives != null)
                                            .ForEach(x => x.Objectives
                                            .ForEach(y =>
                                            {
                                                y.ObjectiveId = null;
                                                if (y.Endpoints != null)
                                                {
                                                    y.Endpoints.FindAll(x => x != null)
                                                           .ForEach(x => x.EndPointsId = null);
                                                }
                                            }));

                    //studyIndicationId
                    studyEntity.ClinicalStudy.CurrentSections
                                            .FindAll(x => x.StudyIndications != null)
                                            .ForEach(x => x.StudyIndications
                                            .ForEach(y => y.StudyIndicationId = null));

                    //studyDesignId
                    studyEntity.ClinicalStudy.CurrentSections
                                            .FindAll(x => x.StudyDesigns != null)
                                            .ForEach(x => x.StudyDesigns
                                            .ForEach(y => y.StudyDesignId = null));

                    //studyPopulationId
                    studyEntity.ClinicalStudy.CurrentSections
                                            .FindAll(x => x.StudyDesigns != null)
                                            .ForEach(y => y.StudyDesigns
                                            .FindAll(n => n.CurrentSections != null)
                                            .ForEach(z => z.CurrentSections
                                            .FindAll(n => n.StudyPopulations != null)
                                            .ForEach(p => p.StudyPopulations
                                            .ForEach(i => i.StudyPopulationId = null))));

                    //investigationalInterventionId
                    studyEntity.ClinicalStudy.CurrentSections
                                            .FindAll(x => x.StudyDesigns != null)
                                            .ForEach(y => y.StudyDesigns
                                            .FindAll(n => n.CurrentSections != null)
                                            .ForEach(z => z.CurrentSections
                                            .FindAll(x => x.InvestigationalInterventions != null)
                                            .ForEach(x => x.InvestigationalInterventions
                                            .ForEach(y => y.InvestigationalInterventionId = null))));

                    //plannedWorkFlowId and sub-elements Id's
                    studyEntity.ClinicalStudy.CurrentSections
                                            .FindAll(x => x.StudyDesigns != null)
                                            .ForEach(y => y.StudyDesigns
                                            .FindAll(n => n.CurrentSections != null)
                                            .ForEach(z => z.CurrentSections
                                            .FindAll(n => n.PlannedWorkflows != null)
                                            .ForEach(p => p.PlannedWorkflows
                                            .ForEach(i =>
                                            {
                                                i.PlannedWorkFlowId = null;
                                                if (i.StartPoint != null)
                                                    i.StartPoint.PointInTimeId = null;

                                                if (i.EndPoint != null)
                                                    i.EndPoint.PointInTimeId = null;

                                                if (i.WorkflowItemMatrix != null)
                                                {
                                                    i.WorkflowItemMatrix.WorkFlowItemMatrixId = null;
                                                    if (i.WorkflowItemMatrix.Matrix != null)
                                                    {
                                                        i.WorkflowItemMatrix.Matrix.FindAll(x => x != null).ForEach(m =>
                                                        {
                                                            m.MatrixId = null;
                                                            if (m.Items != null)
                                                            {
                                                                m.Items.FindAll(x => x != null).ForEach(item =>
                                                                {
                                                                    item.ItemId = null;
                                                                    if (item.FromPointInTime != null)
                                                                        item.FromPointInTime.PointInTimeId = null;

                                                                    if (item.ToPointInTime != null)
                                                                        item.ToPointInTime.PointInTimeId = null;

                                                                    if (item.Activity != null)
                                                                    {
                                                                        item.Activity.ActivityId = null;
                                                                        if (item.Activity.StudyDataCollection != null)
                                                                            item.Activity.StudyDataCollection.FindAll(x => x != null).ForEach(sdc => sdc.StudyDataCollectionId = null);
                                                                    }
                                                                    if (item.Encounter != null)
                                                                    {
                                                                        item.Encounter.EncounterId = null;
                                                                        if (item.Encounter.StartRule != null)
                                                                            item.Encounter.StartRule.RuleId = null;

                                                                        if (item.Encounter.EndRule != null)
                                                                            item.Encounter.EndRule.RuleId = null;

                                                                        if (item.Encounter.Epoch != null)
                                                                            item.Encounter.Epoch.EpochId = null;
                                                                    }
                                                                });
                                                            }
                                                        });
                                                    }
                                                }
                                            }))));

                    //studyCellId and sub-elements Id's
                    studyEntity.ClinicalStudy.CurrentSections
                                            .FindAll(x => x.StudyDesigns != null)
                                            .ForEach(y => y.StudyDesigns
                                            .FindAll(n => n.CurrentSections != null)
                                            .ForEach(z => z.CurrentSections
                                            .FindAll(n => n.StudyCells != null)
                                            .ForEach(p => p.StudyCells
                                            .ForEach(i =>
                                            {
                                                i.StudyCellId = null;
                                                if (i.StudyArm != null)
                                                    i.StudyArm.StudyArmId = null;

                                                if (i.StudyEpoch != null)
                                                    i.StudyEpoch.StudyEpochId = null;

                                                if (i.StudyElements != null)
                                                {
                                                    i.StudyElements.FindAll(x => x != null).ForEach(e =>
                                                    {
                                                        e.StudyElementId = null;
                                                        if (e.StartRule != null)
                                                            e.StartRule.RuleId = null;

                                                        if (e.EndRule != null)
                                                            e.EndRule.RuleId = null;
                                                    });
                                                }
                                            }))));
                }
                return studyEntity;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Study SectionCheck
        /// <summary>
        /// Section Level Study comparison for sectional POST
        /// </summary>
        /// <param name="incoming">Incoming Study Defenitions</param>
        /// <param name="existing">Existing Study Defenitions</param>
        /// <returns>
        /// A <see cref="StudyEntity"/> after checking the section for Posting the data to the database       
        /// </returns>
        public static StudyEntity SectionCheck(StudyEntity incoming, StudyEntity existing)
        {
            try
            {
                existing.ClinicalStudy.StudyType = incoming.ClinicalStudy.StudyType;
                existing.ClinicalStudy.StudyTitle = incoming.ClinicalStudy.StudyTitle;
                existing.ClinicalStudy.StudyPhase = incoming.ClinicalStudy.StudyPhase ?? existing.ClinicalStudy.StudyPhase;
                existing.ClinicalStudy.StudyTag = incoming.ClinicalStudy.StudyTag ?? existing.ClinicalStudy.StudyTag;
                existing.ClinicalStudy.StudyStatus = incoming.ClinicalStudy.StudyStatus ?? existing.ClinicalStudy.StudyStatus;
                if (incoming.ClinicalStudy.StudyProtocolReferences != null)
                {
                    if (incoming.ClinicalStudy.StudyProtocolReferences.Count > 0)
                    {
                        existing.ClinicalStudy.StudyProtocolReferences = incoming.ClinicalStudy.StudyProtocolReferences;
                    }
                }

                //For studyIdentifiers
                var studyIdentifiersList = new List<StudyIdentifierEntity>();
                foreach (var studyIdentifier in incoming.ClinicalStudy.StudyIdentifiers)
                {
                    if (existing.ClinicalStudy.StudyIdentifiers.Any(x => x.StudyIdentifierId == studyIdentifier.StudyIdentifierId))
                    {
                        studyIdentifiersList.Add(studyIdentifier);
                        existing.ClinicalStudy.StudyIdentifiers.RemoveAll(x => x.StudyIdentifierId == studyIdentifier.StudyIdentifierId);
                    }
                    else
                    {
                        studyIdentifier.StudyIdentifierId = IdGenerator.GenerateId();
                        studyIdentifiersList.Add(studyIdentifier);
                    }
                }
                existing.ClinicalStudy.StudyIdentifiers = studyIdentifiersList;

                //If there is no current sections in existing entity, map the whole incoming current section with new one
                if (existing.ClinicalStudy.CurrentSections == null && incoming.ClinicalStudy.CurrentSections != null)
                {
                    existing.ClinicalStudy.CurrentSections = SectionIdGenerator.GenerateSectionId(incoming).ClinicalStudy.CurrentSections;
                }
                //If there is current sections in existing entity, the we are proceeding with section level checks
                else if (incoming.ClinicalStudy.CurrentSections != null && existing.ClinicalStudy.CurrentSections != null)
                {

                    List<StudyObjectiveEntity> existingStudyObjectivesEntities = existing.ClinicalStudy.CurrentSections.FindAll(x => x.Objectives != null).Count != 0 ? existing.ClinicalStudy.CurrentSections.Find(x => x.Objectives != null).Objectives : new List<StudyObjectiveEntity>();
                    List<StudyObjectiveEntity> incomingStudyObjectivesEntities = incoming.ClinicalStudy.CurrentSections.FindAll(x => x.Objectives != null).Count != 0 ? incoming.ClinicalStudy.CurrentSections.Find(x => x.Objectives != null).Objectives : new List<StudyObjectiveEntity>();

                    List<StudyIndicationEntity> existingStudyIndicationEntities = existing.ClinicalStudy.CurrentSections.FindAll(x => x.StudyIndications != null).Count != 0 ? existing.ClinicalStudy.CurrentSections.Find(x => x.StudyIndications != null).StudyIndications : new List<StudyIndicationEntity>();
                    List<StudyIndicationEntity> incomingStudyIndicationEntities = incoming.ClinicalStudy.CurrentSections.FindAll(x => x.StudyIndications != null).Count != 0 ? incoming.ClinicalStudy.CurrentSections.Find(x => x.StudyIndications != null).StudyIndications : new List<StudyIndicationEntity>();

                    List<StudyDesignEntity> existingStudyDesignEntities = existing.ClinicalStudy.CurrentSections.FindAll(x => x.StudyDesigns != null).Count != 0 ? existing.ClinicalStudy.CurrentSections.Find(x => x.StudyDesigns != null).StudyDesigns : new List<StudyDesignEntity>();
                    List<StudyDesignEntity> incomingStudyDesignEntities = incoming.ClinicalStudy.CurrentSections.FindAll(x => x.StudyDesigns != null).Count != 0 ? incoming.ClinicalStudy.CurrentSections.Find(x => x.StudyDesigns != null).StudyDesigns : new List<StudyDesignEntity>();

                    Parallel.Invoke(
                                //Study Objectives Section
                                () => StudyObjectivesSectionCheck(existing, existingStudyObjectivesEntities, incomingStudyObjectivesEntities),
                                //Study Indications Section
                                () => StudyIndicationSectionCheck(existing, existingStudyIndicationEntities, incomingStudyIndicationEntities),
                                //Study Design Section
                                () => StudyDesignSectionCheck(existing, existingStudyDesignEntities, incomingStudyDesignEntities)
                            );
                }
                return existing;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Study Level Sections       
        /// <summary>
        /// Check for Study Objectives section 
        /// </summary>
        /// <param name="existing">Existing Study Defenitions</param>
        /// <param name="existingStudyObjectivesEntities">Existing Study Objectives</param>
        /// <param name="incomingStudyObjectivesEntities">Incoming Study Objectives</param>
        public static void StudyObjectivesSectionCheck(StudyEntity existing, List<StudyObjectiveEntity> existingStudyObjectivesEntities, List<StudyObjectiveEntity> incomingStudyObjectivesEntities)
        {
            try
            {
                if (existingStudyObjectivesEntities.Count == 0 && incomingStudyObjectivesEntities.Count != 0)
                {
                    var newStudyObjectiveList = new List<StudyObjectiveEntity>();
                    foreach (var item in incomingStudyObjectivesEntities)
                    {
                        var newStudyObjective = SectionIdGenerator.StudyObjectivesIdGenerator(item);
                        newStudyObjectiveList.Add(newStudyObjective);
                    }
                    CurrentSectionsEntity currentSectionsEntity = new()
                    {
                        CurrentSectionsId = IdGenerator.GenerateId(),
                        SectionType = StudySectionTypes.OBJECTIVES.ToString(),
                        Objectives = newStudyObjectiveList
                    };
                    existing.ClinicalStudy.CurrentSections.Add(currentSectionsEntity);
                }

                else if (existingStudyObjectivesEntities.Count != 0 && incomingStudyObjectivesEntities.Count != 0)
                {
                    var studyObjectivesList = new List<StudyObjectiveEntity>();
                    foreach (var item in incomingStudyObjectivesEntities)
                    {
                        if (existingStudyObjectivesEntities.Any(x => x.ObjectiveId == item.ObjectiveId))
                        {
                            if (item.Endpoints != null)
                            {
                                var endPointList = new List<EndpointsEntity>();
                                foreach (var endpoint in item.Endpoints)
                                {
                                    if (existingStudyObjectivesEntities.Find(x => x.ObjectiveId == item.ObjectiveId)
                                        .Endpoints != null)
                                    {
                                        if (existingStudyObjectivesEntities.Find(x => x.ObjectiveId == item.ObjectiveId)
                                        .Endpoints.Any(x => x.EndPointsId == endpoint.EndPointsId))
                                        {
                                            endPointList.Add(endpoint);
                                            existingStudyObjectivesEntities.Find(x => x.ObjectiveId == item.ObjectiveId)
                                            .Endpoints.RemoveAll(x => x.EndPointsId == endpoint.EndPointsId);
                                        }
                                        else
                                        {
                                            endpoint.EndPointsId = IdGenerator.GenerateId();
                                            endPointList.Add(endpoint);
                                        }
                                    }
                                    else
                                    {
                                        endpoint.EndPointsId = IdGenerator.GenerateId();
                                        endPointList.Add(endpoint);
                                    }
                                }
                                item.Endpoints = endPointList;
                            }
                            studyObjectivesList.Add(item);
                            existing.ClinicalStudy.CurrentSections.Find(x => x.Objectives != null).Objectives.RemoveAll(x => x.ObjectiveId == item.ObjectiveId);
                        }
                        else
                        {
                            var studyObjective = SectionIdGenerator.StudyObjectivesIdGenerator(item);
                            studyObjectivesList.Add(studyObjective);
                        }
                    }
                    existing.ClinicalStudy.CurrentSections.FindAll(x => x.Objectives != null).ForEach(x => x.Objectives = studyObjectivesList);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Check for Study Indication section 
        /// </summary>
        /// <param name="existing">Existing Study Defenitions</param>
        /// <param name="existingStudyIndicationEntities">Existing Study Indications</param>
        /// <param name="incomingStudyIndicationEntities">Incoming Study Indications</param>
        public static void StudyIndicationSectionCheck(StudyEntity existing, List<StudyIndicationEntity> existingStudyIndicationEntities, List<StudyIndicationEntity> incomingStudyIndicationEntities)
        {
            try
            {
                if (existingStudyIndicationEntities.Count == 0 && incomingStudyIndicationEntities.Count != 0)
                {
                    incomingStudyIndicationEntities.ForEach(x => x.StudyIndicationId = IdGenerator.GenerateId());
                    CurrentSectionsEntity currentSectionsEntity = new()
                    {
                        CurrentSectionsId = IdGenerator.GenerateId(),
                        SectionType = StudySectionTypes.STUDY_INDICATIONS.ToString(),
                        StudyIndications = incomingStudyIndicationEntities
                    };
                    existing.ClinicalStudy.CurrentSections.Add(currentSectionsEntity);
                }

                else if (existingStudyIndicationEntities.Count != 0 && incomingStudyIndicationEntities.Count != 0)
                {
                    var studyIndicationList = new List<StudyIndicationEntity>();
                    foreach (var item in incomingStudyIndicationEntities)
                    {
                        if (existingStudyIndicationEntities.Any(x => x.StudyIndicationId == item.StudyIndicationId))
                        {
                            studyIndicationList.Add(item);
                            existing.ClinicalStudy.CurrentSections.Find(x => x.StudyIndications != null).StudyIndications.RemoveAll(x => x.StudyIndicationId == item.StudyIndicationId);
                        }
                        else
                        {
                            item.StudyIndicationId = IdGenerator.GenerateId();
                            studyIndicationList.Add(item);
                        }
                    }
                    existing.ClinicalStudy.CurrentSections.FindAll(x => x.StudyIndications != null).ForEach(x => x.StudyIndications = studyIndicationList);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Check for Study Design section 
        /// </summary>
        /// <param name="existing">Existing Study Defenitions</param>
        /// <param name="existingStudyDesignEntities">Existing Study Designs</param>
        /// <param name="incomingStudyDesignEntities">Existing Study Designs</param>
        public static void StudyDesignSectionCheck(StudyEntity existing, List<StudyDesignEntity> existingStudyDesignEntities, List<StudyDesignEntity> incomingStudyDesignEntities)
        {
            try
            {
                if (existingStudyDesignEntities.Count == 0 && incomingStudyDesignEntities.Count != 0)
                {
                    var newStudyDesignList = new List<StudyDesignEntity>();
                    foreach (var item in incomingStudyDesignEntities)
                    {
                        var newStudyDesign = SectionIdGenerator.StudyDesignIdGenerator(item);
                        newStudyDesignList.Add(newStudyDesign);
                    }
                    CurrentSectionsEntity currentSectionsEntity = new()
                    {
                        CurrentSectionsId = IdGenerator.GenerateId(),
                        SectionType = StudySectionTypes.STUDY_DESIGNS.ToString(),
                        StudyDesigns = newStudyDesignList
                    };
                    existing.ClinicalStudy.CurrentSections.Add(currentSectionsEntity);
                }
                else if (existingStudyDesignEntities.Count != 0 && incomingStudyDesignEntities.Count != 0)
                {
                    var studyDesignList = new List<StudyDesignEntity>();
                    foreach (var item in incomingStudyDesignEntities)
                    {
                        if (existingStudyDesignEntities.Any(x => x.StudyDesignId == item.StudyDesignId))
                        {
                            existingStudyDesignEntities.Find(x => x.StudyDesignId == item.StudyDesignId).TrialIntentType = item.TrialIntentType;
                            existingStudyDesignEntities.Find(x => x.StudyDesignId == item.StudyDesignId).TrialType = item.TrialType;

                            if (existingStudyDesignEntities.Find(x => x.StudyDesignId == item.StudyDesignId).CurrentSections == null)
                            {
                                existingStudyDesignEntities.Find(x => x.StudyDesignId == item.StudyDesignId).CurrentSections = new List<CurrentSectionsEntity>();
                            }
                            List<PlannedWorkFlowEntity> existingPlannedWorkFlowEntities = existingStudyDesignEntities.Find(x => x.StudyDesignId == item.StudyDesignId).CurrentSections
                                                                                                                     .FindAll(x => x.PlannedWorkflows != null).Count != 0 ?
                                                                                          existingStudyDesignEntities.Find(x => x.StudyDesignId == item.StudyDesignId).CurrentSections
                                                                                                                     .Find(x => x.PlannedWorkflows != null).PlannedWorkflows : new List<PlannedWorkFlowEntity>();
                            List<PlannedWorkFlowEntity> incomingPlannedWorkFlowEntities = incomingStudyDesignEntities.Find(x => x.StudyDesignId == item.StudyDesignId).CurrentSections
                                                                                                                      .FindAll(x => x.PlannedWorkflows != null).Count != 0 ?
                                                                                           incomingStudyDesignEntities.Find(x => x.StudyDesignId == item.StudyDesignId).CurrentSections
                                                                                                                      .Find(x => x.PlannedWorkflows != null).PlannedWorkflows : new List<PlannedWorkFlowEntity>();

                            List<StudyPopulationEntity> existingStudyPopulationsEntities = existingStudyDesignEntities.Find(x => x.StudyDesignId == item.StudyDesignId).CurrentSections
                                                                                                                     .FindAll(x => x.StudyPopulations != null).Count != 0 ?
                                                                                          existingStudyDesignEntities.Find(x => x.StudyDesignId == item.StudyDesignId).CurrentSections
                                                                                                                     .Find(x => x.StudyPopulations != null).StudyPopulations : new List<StudyPopulationEntity>();
                            List<StudyPopulationEntity> incomingStudyPopulationsEntities = incomingStudyDesignEntities.Find(x => x.StudyDesignId == item.StudyDesignId).CurrentSections
                                                                                                                      .FindAll(x => x.StudyPopulations != null).Count != 0 ?
                                                                                           incomingStudyDesignEntities.Find(x => x.StudyDesignId == item.StudyDesignId).CurrentSections
                                                                                                                      .Find(x => x.StudyPopulations != null).StudyPopulations : new List<StudyPopulationEntity>();

                            List<StudyCellEntity> existingStudyCellEntities = existingStudyDesignEntities.Find(x => x.StudyDesignId == item.StudyDesignId).CurrentSections
                                                                                                                     .FindAll(x => x.StudyCells != null).Count != 0 ?
                                                                                          existingStudyDesignEntities.Find(x => x.StudyDesignId == item.StudyDesignId).CurrentSections
                                                                                                                     .Find(x => x.StudyCells != null).StudyCells : new List<StudyCellEntity>();
                            List<StudyCellEntity> incomingStudyCellEntities = incomingStudyDesignEntities.Find(x => x.StudyDesignId == item.StudyDesignId).CurrentSections
                                                                                                                      .FindAll(x => x.StudyCells != null).Count != 0 ?
                                                                                           incomingStudyDesignEntities.Find(x => x.StudyDesignId == item.StudyDesignId).CurrentSections
                                                                                                                      .Find(x => x.StudyCells != null).StudyCells : new List<StudyCellEntity>();

                            List<InvestigationalInterventionEntity> existingInvestigationalInterventionEntities = existingStudyDesignEntities.Find(x => x.StudyDesignId == item.StudyDesignId).CurrentSections
                                                                                                                     .FindAll(x => x.InvestigationalInterventions != null).Count != 0 ?
                                                                                          existingStudyDesignEntities.Find(x => x.StudyDesignId == item.StudyDesignId).CurrentSections
                                                                                                                     .Find(x => x.InvestigationalInterventions != null).InvestigationalInterventions : new List<InvestigationalInterventionEntity>();
                            List<InvestigationalInterventionEntity> incomingInvestigationalInterventionEntities = incomingStudyDesignEntities.Find(x => x.StudyDesignId == item.StudyDesignId).CurrentSections
                                                                                                                     .FindAll(x => x.InvestigationalInterventions != null).Count != 0 ?
                                                                                          incomingStudyDesignEntities.Find(x => x.StudyDesignId == item.StudyDesignId).CurrentSections
                                                                                                                     .Find(x => x.InvestigationalInterventions != null).InvestigationalInterventions : new List<InvestigationalInterventionEntity>();

                            Parallel.Invoke(
                                    //Study Populations Section
                                    () => StudyPopulationsSectionCheck(existingStudyDesignEntities.Find(x => x.StudyDesignId == item.StudyDesignId), existingStudyPopulationsEntities, incomingStudyPopulationsEntities),
                                    //Study Cells Section
                                    () => StudyCellsSectionCheck(existingStudyDesignEntities.Find(x => x.StudyDesignId == item.StudyDesignId), existingStudyCellEntities, incomingStudyCellEntities),
                                    //Planned WorkFlow Section
                                    () => StudyPlannedWorkFlowSectionCheck(existingStudyDesignEntities.Find(x => x.StudyDesignId == item.StudyDesignId), existingPlannedWorkFlowEntities, incomingPlannedWorkFlowEntities),
                                    //Investigational Intervention Section
                                    () => InvestigationalInvestigationSectionCheck(existingStudyDesignEntities.Find(x => x.StudyDesignId == item.StudyDesignId), existingInvestigationalInterventionEntities, incomingInvestigationalInterventionEntities)
                                );

                            studyDesignList.Add(existingStudyDesignEntities.Find(x => x.StudyDesignId == item.StudyDesignId));
                            existing.ClinicalStudy.CurrentSections.Find(x => x.StudyDesigns != null).StudyDesigns.RemoveAll(x => x.StudyDesignId == item.StudyDesignId);
                        }
                        else
                        {
                            var studyDesign = SectionIdGenerator.StudyDesignIdGenerator(item);
                            studyDesignList.Add(studyDesign);
                        }
                    }
                    existing.ClinicalStudy.CurrentSections.FindAll(x => x.StudyDesigns != null).ForEach(x => x.StudyDesigns = studyDesignList);
                }

                #region Previous and Next Items Logic
                PreviousItemNextItemHelper.PreviousItemsNextItemsWraper(existing);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Study Design Sections
        /// <summary>
        /// Check for Investigational Intervention section 
        /// </summary>
        /// <param name="existingStudyDesign">Existing Study Design</param>
        /// <param name="existingInvestigationalInterventionEntities">Existing InvestigationalIntervention</param>
        /// <param name="incomingInvestigationalInterventionEntities">Incoming InvestigationalIntervention</param>
        public static void InvestigationalInvestigationSectionCheck(StudyDesignEntity existingStudyDesign, List<InvestigationalInterventionEntity> existingInvestigationalInterventionEntities, List<InvestigationalInterventionEntity> incomingInvestigationalInterventionEntities)
        {
            try
            {
                if (existingInvestigationalInterventionEntities.Count == 0 && incomingInvestigationalInterventionEntities.Count != 0)
                {
                    incomingInvestigationalInterventionEntities.ForEach(x => x.InvestigationalInterventionId = IdGenerator.GenerateId());
                    CurrentSectionsEntity currentSectionsEntity = new()
                    {
                        CurrentSectionsId = IdGenerator.GenerateId(),
                        SectionType = StudySectionTypes.INVESTIGATIONAL_INTERVENTIONS.ToString(),
                        InvestigationalInterventions = incomingInvestigationalInterventionEntities
                    };
                    existingStudyDesign.CurrentSections.Add(currentSectionsEntity);
                }

                else if (existingInvestigationalInterventionEntities.Count != 0 && incomingInvestigationalInterventionEntities.Count != 0)
                {
                    var investigationalInterventionList = new List<InvestigationalInterventionEntity>();
                    foreach (var item in incomingInvestigationalInterventionEntities)
                    {
                        if (existingInvestigationalInterventionEntities.Any(x => x.InvestigationalInterventionId == item.InvestigationalInterventionId))
                        {
                            investigationalInterventionList.Add(item);
                            existingStudyDesign.CurrentSections.Find(x => x.InvestigationalInterventions != null).InvestigationalInterventions.RemoveAll(x => x.InvestigationalInterventionId == item.InvestigationalInterventionId);
                        }
                        else
                        {
                            item.InvestigationalInterventionId = IdGenerator.GenerateId();
                            investigationalInterventionList.Add(item);
                        }
                    }
                    existingStudyDesign.CurrentSections.FindAll(x => x.InvestigationalInterventions != null).ForEach(x => x.InvestigationalInterventions = investigationalInterventionList);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Check for Study Population section 
        /// </summary>
        /// <param name="existingStudyDesign">Existing Study Design</param>
        /// <param name="existingStudyPopulationEntities">Existing Study Populations</param>
        /// <param name="incomingStudyPopulationEntities">Incoming Study Populations</param>
        public static void StudyPopulationsSectionCheck(StudyDesignEntity existingStudyDesign, List<StudyPopulationEntity> existingStudyPopulationEntities, List<StudyPopulationEntity> incomingStudyPopulationEntities)
        {
            try
            {
                if (existingStudyPopulationEntities.Count == 0 && incomingStudyPopulationEntities.Count != 0)
                {
                    incomingStudyPopulationEntities.ForEach(x => x.StudyPopulationId = IdGenerator.GenerateId());
                    CurrentSectionsEntity currentSectionsEntity = new()
                    {
                        CurrentSectionsId = IdGenerator.GenerateId(),
                        SectionType = StudySectionTypes.STUDY_POPULATIONS.ToString(),
                        StudyPopulations = incomingStudyPopulationEntities
                    };
                    existingStudyDesign.CurrentSections.Add(currentSectionsEntity);
                }

                else if (existingStudyPopulationEntities.Count != 0 && incomingStudyPopulationEntities.Count != 0)
                {
                    var studyPopulationList = new List<StudyPopulationEntity>();
                    foreach (var item in incomingStudyPopulationEntities)
                    {
                        if (existingStudyPopulationEntities.Any(x => x.StudyPopulationId == item.StudyPopulationId))
                        {
                            studyPopulationList.Add(item);
                            existingStudyDesign.CurrentSections.Find(x => x.StudyPopulations != null).StudyPopulations.RemoveAll(x => x.StudyPopulationId == item.StudyPopulationId);
                        }
                        else
                        {
                            item.StudyPopulationId = IdGenerator.GenerateId();
                            studyPopulationList.Add(item);
                        }
                    }
                    existingStudyDesign.CurrentSections.FindAll(x => x.StudyPopulations != null).ForEach(x => x.StudyPopulations = studyPopulationList);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Planned WorkFlow Section
        /// <summary>
        /// Check for Study Planned WorkFlow section 
        /// </summary>
        /// <param name="existingStudyDesign">Existing Study Design</param>
        /// <param name="existingPlannedWorkFlowsEntities">Existing PlannedWorkflows</param>
        /// <param name="incomingPlannedWorkFlowsEntities">Incoming PlannedWorkflows</param>
        public static void StudyPlannedWorkFlowSectionCheck(StudyDesignEntity existingStudyDesign, List<PlannedWorkFlowEntity> existingPlannedWorkFlowsEntities, List<PlannedWorkFlowEntity> incomingPlannedWorkFlowsEntities)
        {
            try
            {
                if (existingPlannedWorkFlowsEntities.Count == 0 && incomingPlannedWorkFlowsEntities.Count != 0)
                {
                    var newPlannedWorkFlowList = new List<PlannedWorkFlowEntity>();
                    foreach (var item in incomingPlannedWorkFlowsEntities)
                    {
                        var newPlannedWorkflow = SectionIdGenerator.StudyPlannedWorkFlowIdGenerator(item);
                        newPlannedWorkFlowList.Add(newPlannedWorkflow);
                    }
                    CurrentSectionsEntity currentSectionsEntity = new()
                    {
                        CurrentSectionsId = IdGenerator.GenerateId(),
                        SectionType = StudySectionTypes.PLANNED_WORKFLOWS.ToString(),
                        PlannedWorkflows = newPlannedWorkFlowList
                    };
                    existingStudyDesign.CurrentSections.Add(currentSectionsEntity);
                }

                else if (existingPlannedWorkFlowsEntities.Count != 0 && incomingPlannedWorkFlowsEntities.Count != 0)
                {
                    var plannedWorkFlowsList = new List<PlannedWorkFlowEntity>();
                    foreach (var item in incomingPlannedWorkFlowsEntities)
                    {
                        if (existingPlannedWorkFlowsEntities.Any(x => x.PlannedWorkFlowId == item.PlannedWorkFlowId))
                        {
                            PointInTimeSectionCheck(existingPlannedWorkFlowsEntities.Find(x => x.PlannedWorkFlowId == item.PlannedWorkFlowId).StartPoint, item.StartPoint);
                            PointInTimeSectionCheck(existingPlannedWorkFlowsEntities.Find(x => x.PlannedWorkFlowId == item.PlannedWorkFlowId).EndPoint, item.EndPoint);
                            WorkFlowItemMatrixSectionCheck(existingPlannedWorkFlowsEntities.Find(x => x.PlannedWorkFlowId == item.PlannedWorkFlowId).WorkflowItemMatrix, item.WorkflowItemMatrix);
                            plannedWorkFlowsList.Add(item);
                            existingStudyDesign.CurrentSections.Find(x => x.PlannedWorkflows != null).PlannedWorkflows.RemoveAll(x => x.PlannedWorkFlowId == item.PlannedWorkFlowId);
                        }
                        else
                        {
                            var plannedWorkFlows = SectionIdGenerator.StudyPlannedWorkFlowIdGenerator(item);
                            plannedWorkFlowsList.Add(plannedWorkFlows);
                        }
                    }
                    existingStudyDesign.CurrentSections.FindAll(x => x.PlannedWorkflows != null).ForEach(x => x.PlannedWorkflows = plannedWorkFlowsList);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Check for PointInTime section 
        /// </summary>
        /// <param name="existingPointInTime">Existing PointInTime</param>
        /// <param name="incomingPointInTime">Incoming PointInTime</param>
        public static void PointInTimeSectionCheck(PointInTimeEntity existingPointInTime, PointInTimeEntity incomingPointInTime)
        {
            try
            {
                if (incomingPointInTime != null)
                {
                    if (existingPointInTime == null || (existingPointInTime.PointInTimeId == null && incomingPointInTime.PointInTimeId != null))
                    {
                        incomingPointInTime.PointInTimeId = IdGenerator.GenerateId();
                        existingPointInTime = incomingPointInTime;
                    }
                    else if (existingPointInTime == null || (existingPointInTime.PointInTimeId != incomingPointInTime.PointInTimeId))
                    {
                        incomingPointInTime.PointInTimeId = IdGenerator.GenerateId();
                        existingPointInTime = incomingPointInTime;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Check for WorkFlowItemMatrix section 
        /// </summary>
        /// <param name="existingWorkFlowItemMatrixEntity">Existing WorkFlowItemMatrix</param>
        /// <param name="incomingWorkFlowItemMatrixEntity">Incoming WorkFlowItemMatrix</param>
        public static void WorkFlowItemMatrixSectionCheck(WorkFlowItemMatrixEntity existingWorkFlowItemMatrixEntity, WorkFlowItemMatrixEntity incomingWorkFlowItemMatrixEntity)
        {
            try
            {
                if (incomingWorkFlowItemMatrixEntity != null)
                {
                    if ((existingWorkFlowItemMatrixEntity == null) || ((existingWorkFlowItemMatrixEntity.WorkFlowItemMatrixId == null && incomingWorkFlowItemMatrixEntity.WorkFlowItemMatrixId != null)
                    || (existingWorkFlowItemMatrixEntity.WorkFlowItemMatrixId != incomingWorkFlowItemMatrixEntity.WorkFlowItemMatrixId)))
                    {
                        incomingWorkFlowItemMatrixEntity.WorkFlowItemMatrixId = IdGenerator.GenerateId();
                        incomingWorkFlowItemMatrixEntity.Matrix.ForEach(m =>
                        {
                            m.MatrixId = IdGenerator.GenerateId();
                            if (m.Items != null)
                            {
                                m.Items.ForEach(item =>
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
                                            item.Activity.StudyDataCollection.ForEach(sdc => sdc.StudyDataCollectionId = IdGenerator.GenerateId());
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
                        existingWorkFlowItemMatrixEntity = incomingWorkFlowItemMatrixEntity;
                    }
                    else if (existingWorkFlowItemMatrixEntity != null && existingWorkFlowItemMatrixEntity.WorkFlowItemMatrixId == incomingWorkFlowItemMatrixEntity.WorkFlowItemMatrixId)
                    {
                        MatrixSectionCheck(existingWorkFlowItemMatrixEntity.Matrix, incomingWorkFlowItemMatrixEntity.Matrix);
                        existingWorkFlowItemMatrixEntity = incomingWorkFlowItemMatrixEntity;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Check for Matrix section 
        /// </summary>
        /// <param name="existingMatrixEntities">Existing Matrix</param>
        /// <param name="incomingMatrixEntities">Incoming Matrix</param>
        public static void MatrixSectionCheck(List<MatrixEntity> existingMatrixEntities, List<MatrixEntity> incomingMatrixEntities)
        {
            try
            {
                var matrixList = new List<MatrixEntity>();
                if (incomingMatrixEntities != null)
                {
                    foreach (var iterator in incomingMatrixEntities)
                    {
                        if (existingMatrixEntities != null && existingMatrixEntities.Any(x => x.MatrixId == iterator.MatrixId))
                        {
                            ItemSectionCheck(existingMatrixEntities.Find(x => x.MatrixId == iterator.MatrixId).Items, iterator.Items);
                            matrixList.Add(iterator);
                            existingMatrixEntities.RemoveAll(x => x.MatrixId == iterator.MatrixId);
                        }
                        else
                        {
                            iterator.MatrixId = IdGenerator.GenerateId();
                            if (iterator.Items != null)
                            {
                                iterator.Items.ForEach(item =>
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
                                            item.Activity.StudyDataCollection.ForEach(sdc => sdc.StudyDataCollectionId = IdGenerator.GenerateId());
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
                            matrixList.Add(iterator);
                        }

                    }
                }
                incomingMatrixEntities = matrixList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Check for Items section 
        /// </summary>
        /// <param name="existingItemEntities">Existing Items</param>
        /// <param name="incomingItemEntities">Incoming Items</param>
        public static void ItemSectionCheck(List<ItemEntity> existingItemEntities, List<ItemEntity> incomingItemEntities)
        {
            try
            {
                var itemList = new List<ItemEntity>();
                if (incomingItemEntities != null)
                {
                    foreach (var iterator in incomingItemEntities)
                    {
                        if (existingItemEntities != null && existingItemEntities.Any(x => x.ItemId == iterator.ItemId))
                        {
                            PointInTimeSectionCheck(existingItemEntities.Find(x => x.ItemId == iterator.ItemId).FromPointInTime, iterator.FromPointInTime);
                            PointInTimeSectionCheck(existingItemEntities.Find(x => x.ItemId == iterator.ItemId).ToPointInTime, iterator.ToPointInTime);
                            ActivitySectionCheck(existingItemEntities.Find(x => x.ItemId == iterator.ItemId).Activity, iterator.Activity);
                            EncounterSectionCheck(existingItemEntities.Find(x => x.ItemId == iterator.ItemId).Encounter, iterator.Encounter);
                            iterator.NextItemsInSequence = new List<string>();
                            iterator.PreviousItemsInSequence = new List<string>();
                            itemList.Add(iterator);
                            existingItemEntities.RemoveAll(x => x.ItemId == iterator.ItemId);
                        }
                        else
                        {
                            iterator.ItemId = IdGenerator.GenerateId();
                            if (iterator.FromPointInTime != null)
                                iterator.FromPointInTime.PointInTimeId = IdGenerator.GenerateId();
                            if (iterator.ToPointInTime != null)
                                iterator.ToPointInTime.PointInTimeId = IdGenerator.GenerateId();
                            if (iterator.Activity != null)
                            {
                                iterator.Activity.ActivityId = IdGenerator.GenerateId();
                                if (iterator.Activity.StudyDataCollection != null)
                                    iterator.Activity.StudyDataCollection.ForEach(sdc => sdc.StudyDataCollectionId = IdGenerator.GenerateId());
                            }
                            if (iterator.Encounter != null)
                            {
                                iterator.Encounter.EncounterId = IdGenerator.GenerateId();
                                if (iterator.Encounter.StartRule != null)
                                    iterator.Encounter.StartRule.RuleId = IdGenerator.GenerateId();
                                if (iterator.Encounter.EndRule != null)
                                    iterator.Encounter.EndRule.RuleId = IdGenerator.GenerateId();
                                if (iterator.Encounter.Epoch != null)
                                    iterator.Encounter.Epoch.EpochId = IdGenerator.GenerateId();
                            }
                            iterator.NextItemsInSequence = new List<string>();
                            iterator.PreviousItemsInSequence = new List<string>();
                            itemList.Add(iterator);
                        }

                    }
                }
                incomingItemEntities = itemList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Check for Activity section 
        /// </summary>
        /// <param name="existingActivityEntity">Existing Activity</param>
        /// <param name="incomingActivityEntity">Incoming Activity</param>
        public static void ActivitySectionCheck(ActivityEntity existingActivityEntity, ActivityEntity incomingActivityEntity)
        {
            try
            {
                if (incomingActivityEntity != null)
                {
                    if ((existingActivityEntity == null) || ((existingActivityEntity.ActivityId == null && incomingActivityEntity.ActivityId != null)
                    || (existingActivityEntity.ActivityId != incomingActivityEntity.ActivityId)))
                    {
                        incomingActivityEntity.ActivityId = IdGenerator.GenerateId();
                        if (incomingActivityEntity.StudyDataCollection != null)
                        {
                            incomingActivityEntity.StudyDataCollection.ForEach(sdc => sdc.StudyDataCollectionId = IdGenerator.GenerateId());
                        }
                    }
                    else if (existingActivityEntity != null && existingActivityEntity.ActivityId == incomingActivityEntity.ActivityId)
                    {
                        StudyDataCollectionSectionCheck(existingActivityEntity.StudyDataCollection, incomingActivityEntity.StudyDataCollection);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Check for StudyDataCollection section 
        /// </summary>
        /// <param name="existingStudyDataCollectionEntities">Existing StudyDataCollection</param>
        /// <param name="incomingStudyDataCollectionEntities">Incoming StudyDataCollection</param>
        public static void StudyDataCollectionSectionCheck(List<StudyDataCollectionEntity> existingStudyDataCollectionEntities, List<StudyDataCollectionEntity> incomingStudyDataCollectionEntities)
        {
            try
            {
                var studyDataCollectionList = new List<StudyDataCollectionEntity>();
                if (incomingStudyDataCollectionEntities != null)
                {
                    foreach (var iterator in incomingStudyDataCollectionEntities)
                    {
                        if (existingStudyDataCollectionEntities != null && existingStudyDataCollectionEntities.Any(x => x.StudyDataCollectionId == iterator.StudyDataCollectionId))
                        {
                            studyDataCollectionList.Add(iterator);
                            existingStudyDataCollectionEntities.RemoveAll(x => x.StudyDataCollectionId == iterator.StudyDataCollectionId);
                        }
                        else
                        {
                            iterator.StudyDataCollectionId = IdGenerator.GenerateId();
                            studyDataCollectionList.Add(iterator);
                        }

                    }
                }
                incomingStudyDataCollectionEntities = studyDataCollectionList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Check for Encounter section 
        /// </summary>
        /// <param name="existingEncounterEntity">Existing Encounter</param>
        /// <param name="incomingEncounterEntity">Incoming Encounter</param>
        public static void EncounterSectionCheck(EncounterEntity existingEncounterEntity, EncounterEntity incomingEncounterEntity)
        {
            try
            {
                if (incomingEncounterEntity != null)
                {
                    if ((existingEncounterEntity != null) && ((existingEncounterEntity.EncounterId == null && incomingEncounterEntity.EncounterId != null)
                    || (existingEncounterEntity.EncounterId != incomingEncounterEntity.EncounterId)))
                    {
                        incomingEncounterEntity.EncounterId = IdGenerator.GenerateId();
                        if (incomingEncounterEntity.StartRule != null)
                            incomingEncounterEntity.StartRule.RuleId = IdGenerator.GenerateId();
                        if (incomingEncounterEntity.EndRule != null)
                            incomingEncounterEntity.EndRule.RuleId = IdGenerator.GenerateId();
                        if (incomingEncounterEntity.Epoch != null)
                            incomingEncounterEntity.Epoch.EpochId = IdGenerator.GenerateId();
                    }
                    else if (existingEncounterEntity != null && existingEncounterEntity.EncounterId == incomingEncounterEntity.EncounterId)
                    {
                        RuleSectionCheck(existingEncounterEntity.StartRule, incomingEncounterEntity.StartRule);
                        RuleSectionCheck(existingEncounterEntity.EndRule, incomingEncounterEntity.EndRule);
                        EpochSectionCheck(existingEncounterEntity.Epoch, incomingEncounterEntity.Epoch);
                    }
                    else if (existingEncounterEntity == null)
                    {
                        incomingEncounterEntity.EncounterId = IdGenerator.GenerateId();
                        if (incomingEncounterEntity.StartRule != null)
                            incomingEncounterEntity.StartRule.RuleId = IdGenerator.GenerateId();
                        if (incomingEncounterEntity.EndRule != null)
                            incomingEncounterEntity.EndRule.RuleId = IdGenerator.GenerateId();
                        if (incomingEncounterEntity.Epoch != null)
                            incomingEncounterEntity.Epoch.EpochId = IdGenerator.GenerateId();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Check for Epoch section 
        /// </summary>
        /// <param name="existingEpochEntity">Existing Epoch</param>
        /// <param name="incomingEpochEntity">Incoming Epoch</param>
        public static void EpochSectionCheck(EpochEntity existingEpochEntity, EpochEntity incomingEpochEntity)
        {
            try
            {
                if (incomingEpochEntity != null)
                {
                    if ((existingEpochEntity != null) && ((existingEpochEntity.EpochId == null && incomingEpochEntity.EpochId != null)
                    || (existingEpochEntity.EpochId != incomingEpochEntity.EpochId)))
                    {
                        incomingEpochEntity.EpochId = IdGenerator.GenerateId();
                    }
                    else if (existingEpochEntity == null)
                    {
                        incomingEpochEntity.EpochId = IdGenerator.GenerateId();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion        

        #region Study Cells Section
        /// <summary>
        /// Check for StudyCells section 
        /// </summary>
        /// <param name="existingStudyDesign">Existing Study Design</param>
        /// <param name="existingStudyCellsEntities">Existing Study Cells</param>
        /// <param name="incomingStudyCellsEntities">Incoming Study Cells</param>
        public static void StudyCellsSectionCheck(StudyDesignEntity existingStudyDesign, List<StudyCellEntity> existingStudyCellsEntities, List<StudyCellEntity> incomingStudyCellsEntities)
        {
            try
            {
                if (existingStudyCellsEntities.Count == 0 && incomingStudyCellsEntities.Count != 0)
                {
                    var newStudyCellList = new List<StudyCellEntity>();
                    foreach (var item in incomingStudyCellsEntities)
                    {
                        var newStudyCell = SectionIdGenerator.StudyCellsIdGenerator(item);
                        newStudyCellList.Add(newStudyCell);
                    }
                    CurrentSectionsEntity currentSectionsEntity = new()
                    {
                        CurrentSectionsId = IdGenerator.GenerateId(),
                        SectionType = StudySectionTypes.STUDY_CELLS.ToString(),
                        StudyCells = newStudyCellList
                    };
                    existingStudyDesign.CurrentSections.Add(currentSectionsEntity);
                }

                else if (existingStudyCellsEntities.Count != 0 && incomingStudyCellsEntities.Count != 0)
                {
                    var studyCellList = new List<StudyCellEntity>();
                    foreach (var item in incomingStudyCellsEntities)
                    {
                        if (existingStudyCellsEntities.Any(x => x.StudyCellId == item.StudyCellId))
                        {
                            StudyElementsSectionCheck(existingStudyCellsEntities.Find(x => x.StudyCellId == item.StudyCellId).StudyElements, item.StudyElements);
                            StudyArmSectionCheck(existingStudyCellsEntities.Find(x => x.StudyCellId == item.StudyCellId).StudyArm, item.StudyArm);
                            StudyEpochSectionCheck(existingStudyCellsEntities.Find(x => x.StudyCellId == item.StudyCellId).StudyEpoch, item.StudyEpoch);
                            studyCellList.Add(item);
                            existingStudyDesign.CurrentSections.Find(x => x.StudyCells != null).StudyCells.RemoveAll(x => x.StudyCellId == item.StudyCellId);
                        }
                        else
                        {
                            var studyCells = SectionIdGenerator.StudyCellsIdGenerator(item);
                            studyCellList.Add(studyCells);
                        }
                    }
                    existingStudyDesign.CurrentSections.FindAll(x => x.StudyCells != null).ForEach(x => x.StudyCells = studyCellList);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Check for StudyElements section 
        /// </summary>
        /// <param name="existingStudyElementsEntities">Existing StudyElement</param>
        /// <param name="incomingStudyElementsEntities">Incoming StudyElement</param>
        public static void StudyElementsSectionCheck(List<StudyElementEntity> existingStudyElementsEntities, List<StudyElementEntity> incomingStudyElementsEntities)
        {
            try
            {
                var studyElementsList = new List<StudyElementEntity>();
                if (incomingStudyElementsEntities != null)
                {
                    foreach (var item in incomingStudyElementsEntities)
                    {
                        if (existingStudyElementsEntities != null && existingStudyElementsEntities.Any(x => x.StudyElementId == item.StudyElementId))
                        {
                            RuleSectionCheck(existingStudyElementsEntities.Find(x => x.StudyElementId == item.StudyElementId).StartRule, item.StartRule);
                            RuleSectionCheck(existingStudyElementsEntities.Find(x => x.StudyElementId == item.StudyElementId).EndRule, item.EndRule);
                            studyElementsList.Add(item);
                            existingStudyElementsEntities.RemoveAll(x => x.StudyElementId == item.StudyElementId);
                        }
                        else
                        {
                            item.StudyElementId = IdGenerator.GenerateId();
                            if (item.StartRule != null)
                                item.StartRule.RuleId = IdGenerator.GenerateId();
                            if (item.EndRule != null)
                                item.EndRule.RuleId = IdGenerator.GenerateId();
                            studyElementsList.Add(item);
                        }
                    }
                }
                incomingStudyElementsEntities = studyElementsList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Check for Rule section 
        /// </summary>
        /// <param name="existingRuleEntity">Existing Rule</param>
        /// <param name="incomingRuleEntity">Incoming Rule</param>
        public static void RuleSectionCheck(RuleEntity existingRuleEntity, RuleEntity incomingRuleEntity)
        {
            try
            {
                if (incomingRuleEntity != null)
                {
                    if (existingRuleEntity != null && existingRuleEntity.RuleId == null && incomingRuleEntity.RuleId != null)
                    {
                        incomingRuleEntity.RuleId = IdGenerator.GenerateId();
                    }
                    else if (existingRuleEntity != null && existingRuleEntity.RuleId != incomingRuleEntity.RuleId)
                    {
                        incomingRuleEntity.RuleId = IdGenerator.GenerateId();
                    }
                    else if (existingRuleEntity == null)
                    {
                        incomingRuleEntity.RuleId = IdGenerator.GenerateId();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Check for StudyArm section 
        /// </summary>
        /// <param name="existingStudyArm">Existing StudyArm</param>
        /// <param name="incomingStudyArm">Incoming StudyArm</param>
        public static void StudyArmSectionCheck(StudyArmEntity existingStudyArm, StudyArmEntity incomingStudyArm)
        {
            try
            {
                if (incomingStudyArm != null)
                {
                    if (existingStudyArm != null && existingStudyArm.StudyArmId == null && incomingStudyArm.StudyArmId != null)
                    {
                        incomingStudyArm.StudyArmId = IdGenerator.GenerateId();
                    }
                    else if (existingStudyArm != null && existingStudyArm.StudyArmId != incomingStudyArm.StudyArmId)
                    {
                        incomingStudyArm.StudyArmId = IdGenerator.GenerateId();
                    }
                    else if (existingStudyArm == null)
                    {
                        incomingStudyArm.StudyArmId = IdGenerator.GenerateId();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Check for StudyEpoch section 
        /// </summary>
        /// <param name="existingStudyEpoch">Existing StudyEpoch</param>
        /// <param name="incomingStudyEpoch">Incoming StudyEpoch</param>
        public static void StudyEpochSectionCheck(StudyEpochEntity existingStudyEpoch, StudyEpochEntity incomingStudyEpoch)
        {
            try
            {
                if (incomingStudyEpoch != null)
                {
                    if (existingStudyEpoch != null && existingStudyEpoch.StudyEpochId == null && incomingStudyEpoch.StudyEpochId != null)
                    {
                        incomingStudyEpoch.StudyEpochId = IdGenerator.GenerateId();
                    }
                    else if (existingStudyEpoch != null && existingStudyEpoch.StudyEpochId != incomingStudyEpoch.StudyEpochId)
                    {
                        incomingStudyEpoch.StudyEpochId = IdGenerator.GenerateId();
                    }
                    else if (existingStudyEpoch == null)
                    {
                        incomingStudyEpoch.StudyEpochId = IdGenerator.GenerateId();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
        #endregion

        #endregion
    }
}
