using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using TransCelerate.SDR.Core.Entities.Study;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class PostStudyElementsCheck
    {
        #region Full Study Check
        public static bool StudyComparison(StudyEntity incoming, StudyEntity existing)
        {
            try
            {                          
                RemoveId(incoming);
                RemoveId(existing);

                return JsonObjectCheck(incoming, existing);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool JsonObjectCheck(object incoming, object existing)
        {
            try
            {
                return JToken.DeepEquals(JObject.Parse(JsonConvert.SerializeObject(incoming)),
                               JObject.Parse(JsonConvert.SerializeObject(existing)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static StudyEntity RemoveId(StudyEntity studyEntity)
        {
            try
            {
                studyEntity.clinicalStudy.studyIdentifiers.ForEach(x => x.studyIdentifierId = null);
                if (studyEntity.clinicalStudy.currentSections != null)
                {
                    //Id Generation for section level
                    studyEntity.clinicalStudy.currentSections.ForEach(x => x.currentSectionsId = null);

                    if (studyEntity.clinicalStudy.currentSections.Any(x => x.studyDesigns != null))
                    {
                        studyEntity.clinicalStudy.currentSections
                                            .FindAll(x => x.studyDesigns != null)
                                            .ForEach(y => y.studyDesigns
                                            .FindAll(n => n.currentSections != null)
                                            .ForEach(z => z.currentSections
                                            .ForEach(i => i.currentSectionsId = null)));
                    }

                    //Id Generation for each section

                    //investigationalInterventionId
                    studyEntity.clinicalStudy.currentSections
                                            .FindAll(x => x.investigationalInterventions != null)
                                            .ForEach(x => x.investigationalInterventions
                                            .ForEach(y => y.investigationalInterventionId = null));

                    //ObjectiveId
                    studyEntity.clinicalStudy.currentSections
                                            .FindAll(x => x.objectives != null)
                                            .ForEach(x => x.objectives
                                            .ForEach(y => {
                                                y.ObjectiveId = null;
                                                y.endpoints.FindAll(x => x != null)
                                                           .ForEach(x => x.EndPointsId = null);
                                            }));

                    //studyIndicationId
                    studyEntity.clinicalStudy.currentSections
                                            .FindAll(x => x.studyIndications != null)
                                            .ForEach(x => x.studyIndications
                                            .ForEach(y => y.studyIndicationId = null));

                    //protocolId
                    if (studyEntity.clinicalStudy.currentSections.Any(x => x.studyProtocol != null))
                    {
                        studyEntity.clinicalStudy.currentSections.Find(x => x.studyProtocol != null)
                                                             .studyProtocol.protocolId = null;
                    }

                    //studyDesignId
                    studyEntity.clinicalStudy.currentSections
                                            .FindAll(x => x.studyDesigns != null)
                                            .ForEach(x => x.studyDesigns
                                            .ForEach(y => y.studyDesignId = null));

                    //studyPopulationId
                    studyEntity.clinicalStudy.currentSections
                                            .FindAll(x => x.studyDesigns != null)
                                            .ForEach(y => y.studyDesigns
                                            .FindAll(n => n.currentSections != null)
                                            .ForEach(z => z.currentSections
                                            .FindAll(n => n.studyPopulations != null)
                                            .ForEach(p => p.studyPopulations
                                            .ForEach(i => i.studyPopulationId = null))));

                    //plannedWorkFlowId and sub-elements Id's
                    studyEntity.clinicalStudy.currentSections
                                            .FindAll(x => x.studyDesigns != null)
                                            .ForEach(y => y.studyDesigns
                                            .FindAll(n => n.currentSections != null)
                                            .ForEach(z => z.currentSections
                                            .FindAll(n => n.plannedWorkflows != null)
                                            .ForEach(p => p.plannedWorkflows
                                            .ForEach(i => {
                                                i.plannedWorkFlowId = null;
                                                i.startPoint.pointInTimeId = null;
                                                i.endPoint.pointInTimeId = null;
                                                i.transitions.ForEach(t => {
                                                    t.transitionId = null;
                                                    t.fromPointInTime.pointInTimeId = null;
                                                    t.toPointInTime.pointInTimeId = null;
                                                    t.transitionRule.transitionRuleId = null;
                                                    t.transitionCriteria.ForEach(c => c.transitionCriteriaId = null);
                                                });
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
                                                i.studyCellId = null;
                                                i.studyArm.studyArmId = null;
                                                i.studyEpoch.studyEpochId = null;
                                                i.studyElements.ForEach(e => {
                                                    e.studyElementId = null;
                                                    e.startRule.RuleId = null;
                                                    e.endRule.RuleId = null;
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
        #endregion

        #region Study SectionCheck
        public static StudyEntity SectionCheck(StudyEntity incoming, StudyEntity existing)
        {
            try
            {
                existing.clinicalStudy.studyType = incoming.clinicalStudy.studyType;
                existing.clinicalStudy.studyTitle = incoming.clinicalStudy.studyTitle;
                existing.clinicalStudy.studyPhase = incoming.clinicalStudy.studyPhase ?? existing.clinicalStudy.studyPhase;
                existing.clinicalStudy.interventionModel = incoming.clinicalStudy.interventionModel ?? existing.clinicalStudy.interventionModel;
                existing.clinicalStudy.tag = incoming.clinicalStudy.tag ?? existing.clinicalStudy.tag;
                existing.clinicalStudy.status = incoming.clinicalStudy.status ?? existing.clinicalStudy.status;

                //For studyIdentifiers
                var studyIdentifiersList = new List<StudyIdentifierEntity>();
                foreach(var studyIdentifier in incoming.clinicalStudy.studyIdentifiers)
                {
                    if(existing.clinicalStudy.studyIdentifiers.Any(x=>x.studyIdentifierId==studyIdentifier.studyIdentifierId))
                    {
                        studyIdentifiersList.Add(studyIdentifier);
                        existing.clinicalStudy.studyIdentifiers.RemoveAll(x => x.studyIdentifierId == studyIdentifier.studyIdentifierId);
                    }
                    else
                    {
                        studyIdentifier.studyIdentifierId = IdGenerator.GenerateId();
                        studyIdentifiersList.Add(studyIdentifier);
                    }                    
                }
                existing.clinicalStudy.studyIdentifiers = studyIdentifiersList;

                //If there is no current sections in existing entity, map the whole incoming current section with new one
                if (existing.clinicalStudy.currentSections == null && incoming.clinicalStudy.currentSections != null)
                {
                    existing.clinicalStudy.currentSections = SectionIdGenerator.GenerateSectionId(incoming).clinicalStudy.currentSections;
                }
                else if(incoming.clinicalStudy.currentSections != null && existing.clinicalStudy.currentSections != null)
                {
                    List<InvestigationalInterventionEntity> existingInvestigationalInterventionEntities = existing.clinicalStudy.currentSections.FindAll(x => x.investigationalInterventions != null).Count() != 0 ? existing.clinicalStudy.currentSections.Find(x => x.investigationalInterventions != null).investigationalInterventions : new List<InvestigationalInterventionEntity>();
                    List<InvestigationalInterventionEntity> incomingInvestigationalInterventionEntities = incoming.clinicalStudy.currentSections.FindAll(x => x.investigationalInterventions != null).Count()!=0 ? incoming.clinicalStudy.currentSections.Find(x => x.investigationalInterventions != null).investigationalInterventions:new List<InvestigationalInterventionEntity>();

                    List<StudyObjectiveEntity> existingStudyObjectivesEntities = existing.clinicalStudy.currentSections.FindAll(x => x.objectives != null).Count()!=0 ? existing.clinicalStudy.currentSections.Find(x => x.objectives != null).objectives: new List<StudyObjectiveEntity>();
                    List<StudyObjectiveEntity> incomingStudyObjectivesEntities = incoming.clinicalStudy.currentSections.FindAll(x => x.objectives != null).Count() !=0 ? incoming.clinicalStudy.currentSections.Find(x => x.objectives != null).objectives: new List<StudyObjectiveEntity>();

                    List<StudyIndicationEntity> existingStudyIndicationEntities = existing.clinicalStudy.currentSections.FindAll(x => x.studyIndications != null).Count()!=0? existing.clinicalStudy.currentSections.Find(x => x.studyIndications != null).studyIndications:new List<StudyIndicationEntity>();
                    List<StudyIndicationEntity> incomingStudyIndicationEntities = incoming.clinicalStudy.currentSections.FindAll(x => x.studyIndications != null).Count() != 0 ? incoming.clinicalStudy.currentSections.Find(x => x.studyIndications != null).studyIndications : new List<StudyIndicationEntity>(); 

                    StudyProtocolEntity existingStudyProtocolEntities = existing.clinicalStudy.currentSections.FindAll(x => x.studyProtocol != null).Count()!=0 ? existing.clinicalStudy.currentSections.Find(x => x.studyProtocol != null).studyProtocol : new StudyProtocolEntity();
                    StudyProtocolEntity incomingStudyProtocolEntities = incoming.clinicalStudy.currentSections.FindAll(x => x.studyProtocol != null).Count() != 0 ? incoming.clinicalStudy.currentSections.Find(x => x.studyProtocol != null).studyProtocol : new StudyProtocolEntity();

                    List<StudyDesignEntity> existingStudyDesignEntities = existing.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).Count()!=0? existing.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns: new List<StudyDesignEntity>();
                    List<StudyDesignEntity> incomingStudyDesignEntities = incoming.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).Count() != 0 ? incoming.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns : new List<StudyDesignEntity>();

                    //Investigational Intervention Section
                    InvestigationalInvestigationSectionCheck(existing, existingInvestigationalInterventionEntities, incomingInvestigationalInterventionEntities);

                    //Study Objectives Section
                    StudyObjectivesSectionCheck(existing, existingStudyObjectivesEntities, incomingStudyObjectivesEntities);

                    //Study Indications Section
                    StudyIndicationSectionCheck(existing, existingStudyIndicationEntities, incomingStudyIndicationEntities);

                    //Study Protocol Section
                    StudyProtocolSectionCheck(existing, existingStudyProtocolEntities, incomingStudyProtocolEntities);

                    //Study Design Section
                    StudyDesignSectionCheck(existing, existingStudyDesignEntities, incomingStudyDesignEntities);
                }                
                return existing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Study Level Sections
        public static void InvestigationalInvestigationSectionCheck(StudyEntity existing,List<InvestigationalInterventionEntity> existingInvestigationalInterventionEntities, List<InvestigationalInterventionEntity> incomingInvestigationalInterventionEntities)
        {
            try
            {
                if (existingInvestigationalInterventionEntities.Count() == 0 && incomingInvestigationalInterventionEntities.Count() != 0)
                {
                    incomingInvestigationalInterventionEntities.ForEach(x => x.investigationalInterventionId = IdGenerator.GenerateId());
                    CurrentSectionsEntity currentSectionsEntity = new CurrentSectionsEntity()
                    {
                        currentSectionsId = IdGenerator.GenerateId(),
                        sectionType = StudySectionTypes.INVESTIGATIONAL_INTERVENTIONS.ToString(),
                        investigationalInterventions = incomingInvestigationalInterventionEntities
                    };
                    existing.clinicalStudy.currentSections.Add(currentSectionsEntity);
                }

                else if (existingInvestigationalInterventionEntities.Count() != 0 && incomingInvestigationalInterventionEntities.Count() != 0)
                {
                    var investigationalInterventionList = new List<InvestigationalInterventionEntity>();
                    foreach (var item in incomingInvestigationalInterventionEntities)
                    {
                        if (existingInvestigationalInterventionEntities.Any(x => x.investigationalInterventionId == item.investigationalInterventionId))
                        {
                            investigationalInterventionList.Add(item);
                            existing.clinicalStudy.currentSections.Find(x => x.investigationalInterventions != null).investigationalInterventions.RemoveAll(x => x.investigationalInterventionId == item.investigationalInterventionId);
                        }
                        else
                        {
                            item.investigationalInterventionId = IdGenerator.GenerateId();
                            investigationalInterventionList.Add(item);
                        }                        
                    }
                    existing.clinicalStudy.currentSections.FindAll(x => x.investigationalInterventions != null).ForEach(x => x.investigationalInterventions = investigationalInterventionList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void StudyObjectivesSectionCheck(StudyEntity existing, List<StudyObjectiveEntity> existingStudyObjectivesEntities, List<StudyObjectiveEntity> incomingStudyObjectivesEntities)
        {
            try
            {
                if (existingStudyObjectivesEntities.Count() == 0 && incomingStudyObjectivesEntities.Count() != 0)
                {
                    var newStudyObjectiveList = new List<StudyObjectiveEntity>();
                    foreach (var item in incomingStudyObjectivesEntities)
                    {
                        var newStudyObjective = SectionIdGenerator.StudyObjectivesIdGenerator(item);
                        newStudyObjectiveList.Add(newStudyObjective);
                    }
                    CurrentSectionsEntity currentSectionsEntity = new CurrentSectionsEntity()
                    {
                        currentSectionsId = IdGenerator.GenerateId(),
                        sectionType = StudySectionTypes.OBJECTIVES.ToString(),
                        objectives = newStudyObjectiveList
                    };
                    existing.clinicalStudy.currentSections.Add(currentSectionsEntity);
                }

                else if (existingStudyObjectivesEntities.Count() != 0 && incomingStudyObjectivesEntities.Count() != 0)
                {
                    var studyObjectivesList = new List<StudyObjectiveEntity>();
                    foreach (var item in incomingStudyObjectivesEntities)
                    {
                        if (existingStudyObjectivesEntities.Any(x => x.ObjectiveId == item.ObjectiveId))
                        {
                            var endPointList = new List<EndpointsEntity>();
                            foreach (var endpoint in item.endpoints)
                            {                                
                                if(existingStudyObjectivesEntities.Find(x=>x.ObjectiveId==item.ObjectiveId)
                                    .endpoints.Any(x=>x.EndPointsId==endpoint.EndPointsId))
                                {
                                    endPointList.Add(endpoint);
                                    existingStudyObjectivesEntities.Find(x => x.ObjectiveId == item.ObjectiveId)
                                    .endpoints.RemoveAll(x => x.EndPointsId == endpoint.EndPointsId);
                                }
                                else
                                {
                                    endpoint.EndPointsId = IdGenerator.GenerateId();
                                    endPointList.Add(endpoint);
                                }                                
                            }
                            item.endpoints = endPointList;
                            studyObjectivesList.Add(item);
                            existing.clinicalStudy.currentSections.Find(x => x.objectives != null).objectives.RemoveAll(x => x.ObjectiveId == item.ObjectiveId);
                        }
                        else
                        {
                            var studyObjective = SectionIdGenerator.StudyObjectivesIdGenerator(item);
                            studyObjectivesList.Add(studyObjective);
                        }                        
                    }
                    existing.clinicalStudy.currentSections.FindAll(x => x.objectives != null).ForEach(x => x.objectives = studyObjectivesList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void StudyIndicationSectionCheck(StudyEntity existing, List<StudyIndicationEntity> existingStudyIndicationEntities, List<StudyIndicationEntity> incomingStudyIndicationEntities)
        {
            try
            {
                if (existingStudyIndicationEntities.Count() == 0 && incomingStudyIndicationEntities.Count() != 0)
                {
                    incomingStudyIndicationEntities.ForEach(x => x.studyIndicationId = IdGenerator.GenerateId());
                    CurrentSectionsEntity currentSectionsEntity = new CurrentSectionsEntity()
                    {
                        currentSectionsId = IdGenerator.GenerateId(),
                        sectionType = StudySectionTypes.STUDY_INDICATIONS.ToString(),
                        studyIndications = incomingStudyIndicationEntities
                    };
                    existing.clinicalStudy.currentSections.Add(currentSectionsEntity);
                }

                else if (existingStudyIndicationEntities.Count() != 0 && incomingStudyIndicationEntities.Count() != 0)
                {
                    var studyIndicationList = new List<StudyIndicationEntity>();
                    foreach (var item in incomingStudyIndicationEntities)
                    {
                        if (existingStudyIndicationEntities.Any(x => x.studyIndicationId == item.studyIndicationId))
                        {
                            studyIndicationList.Add(item);
                            existing.clinicalStudy.currentSections.Find(x => x.studyIndications != null).studyIndications.RemoveAll(x => x.studyIndicationId == item.studyIndicationId);
                        }
                        else
                        {
                            item.studyIndicationId = IdGenerator.GenerateId();
                            studyIndicationList.Add(item);
                        }                        
                    }
                    existing.clinicalStudy.currentSections.FindAll(x => x.studyIndications != null).ForEach(x => x.studyIndications = studyIndicationList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void StudyProtocolSectionCheck(StudyEntity existing, StudyProtocolEntity existingSudyProtocol, StudyProtocolEntity incomingStudyProtocol)
        {
            try
            {
                if (existingSudyProtocol.protocolId == null && incomingStudyProtocol.protocolId != null)
                {
                    incomingStudyProtocol.protocolId = IdGenerator.GenerateId();
                    StudyProtocolEntity studyProtocolEntity = new StudyProtocolEntity();

                    CurrentSectionsEntity currentSectionsEntity = new CurrentSectionsEntity()
                    {
                        currentSectionsId = IdGenerator.GenerateId(),
                        sectionType = StudySectionTypes.STUDY_PROTOCOL.ToString(),
                        studyProtocol = incomingStudyProtocol
                    };
                    existing.clinicalStudy.currentSections.Add(currentSectionsEntity);
                }
                else if (existingSudyProtocol.protocolId != null && incomingStudyProtocol.protocolId != null)
                {
                    if (incomingStudyProtocol.protocolId == existingSudyProtocol.protocolId)
                    {
                        existingSudyProtocol = incomingStudyProtocol;
                    }
                    else
                    {
                        incomingStudyProtocol.protocolId = IdGenerator.GenerateId();
                        existingSudyProtocol = incomingStudyProtocol;
                    }
                    existing.clinicalStudy.currentSections.FindAll(x => x.studyProtocol != null).ForEach(x => x.studyProtocol = existingSudyProtocol);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void StudyDesignSectionCheck(StudyEntity existing, List<StudyDesignEntity> existingStudyDesignEntities,List<StudyDesignEntity> incomingStudyDesignEntities)
        {
            try
            {
                if (existingStudyDesignEntities.Count() == 0 && incomingStudyDesignEntities.Count() != 0)
                {
                    var newStudyDesignList = new List<StudyDesignEntity>();
                    foreach (var item in incomingStudyDesignEntities)
                    {
                        var newStudyDesign = SectionIdGenerator.StudyDesignIdGenerator(item);
                        newStudyDesignList.Add(newStudyDesign);
                    }
                    CurrentSectionsEntity currentSectionsEntity = new CurrentSectionsEntity()
                    {
                        currentSectionsId = IdGenerator.GenerateId(),
                        sectionType = StudySectionTypes.STUDY_DESIGNS.ToString(),
                        studyDesigns = newStudyDesignList
                    };
                    existing.clinicalStudy.currentSections.Add(currentSectionsEntity);
                }
                else if (existingStudyDesignEntities.Count() != 0 && incomingStudyDesignEntities.Count() != 0)
                {
                    var studyDesignList = new List<StudyDesignEntity>();
                    foreach (var item in incomingStudyDesignEntities)
                    {
                        if (existingStudyDesignEntities.Any(x => x.studyDesignId == item.studyDesignId))
                        {                            
                            List<PlannedWorkFlowEntity> existingPlannedWorkFlowEntities = existingStudyDesignEntities.Find(x => x.studyDesignId == item.studyDesignId).currentSections
                                                                                                                     .FindAll(x => x.plannedWorkflows != null).Count() != 0 ?
                                                                                          existingStudyDesignEntities.Find(x => x.studyDesignId == item.studyDesignId).currentSections
                                                                                                                     .Find(x => x.plannedWorkflows != null).plannedWorkflows : new List<PlannedWorkFlowEntity>();
                            List<PlannedWorkFlowEntity> incomingPlannedWorkFlowEntities = incomingStudyDesignEntities.Find(x => x.studyDesignId == item.studyDesignId).currentSections
                                                                                                                      .FindAll(x => x.plannedWorkflows != null).Count() != 0 ?
                                                                                           incomingStudyDesignEntities.Find(x => x.studyDesignId == item.studyDesignId).currentSections
                                                                                                                      .Find(x => x.plannedWorkflows != null).plannedWorkflows : new List<PlannedWorkFlowEntity>();

                            List<StudyPopulationEntity> existingStudyPopulationsEntities = existingStudyDesignEntities.Find(x => x.studyDesignId == item.studyDesignId).currentSections
                                                                                                                     .FindAll(x => x.studyPopulations != null).Count() != 0 ?
                                                                                          existingStudyDesignEntities.Find(x => x.studyDesignId == item.studyDesignId).currentSections
                                                                                                                     .Find(x => x.studyPopulations != null).studyPopulations : new List<StudyPopulationEntity>();
                            List<StudyPopulationEntity> incomingStudyPopulationsEntities = incomingStudyDesignEntities.Find(x => x.studyDesignId == item.studyDesignId).currentSections
                                                                                                                      .FindAll(x => x.studyPopulations != null).Count() != 0 ?
                                                                                           incomingStudyDesignEntities.Find(x => x.studyDesignId == item.studyDesignId).currentSections
                                                                                                                      .Find(x => x.studyPopulations != null).studyPopulations : new List<StudyPopulationEntity>();

                            List<StudyCellEntity> existingStudyCellEntities = existingStudyDesignEntities.Find(x => x.studyDesignId == item.studyDesignId).currentSections
                                                                                                                     .FindAll(x => x.studyCells != null).Count() != 0 ?
                                                                                          existingStudyDesignEntities.Find(x => x.studyDesignId == item.studyDesignId).currentSections
                                                                                                                     .Find(x => x.studyCells != null).studyCells : new List<StudyCellEntity>();
                            List<StudyCellEntity> incomingStudyCellEntities = incomingStudyDesignEntities.Find(x => x.studyDesignId == item.studyDesignId).currentSections
                                                                                                                      .FindAll(x => x.studyCells != null).Count() != 0 ?
                                                                                           incomingStudyDesignEntities.Find(x => x.studyDesignId == item.studyDesignId).currentSections
                                                                                                                      .Find(x => x.studyCells != null).studyCells : new List<StudyCellEntity>();

                            //Study Populations Section
                            StudyPopulationsSectionCheck(existingStudyDesignEntities.Find(x => x.studyDesignId == item.studyDesignId), existingStudyPopulationsEntities, incomingStudyPopulationsEntities);

                            //Study Cells Section
                            StudyCellsSectionCheck(existingStudyDesignEntities.Find(x => x.studyDesignId == item.studyDesignId), existingStudyCellEntities, incomingStudyCellEntities);

                            //Planned WorkFlow Section
                            StudyPlannedWorkFlowSectionCheck(existingStudyDesignEntities.Find(x => x.studyDesignId == item.studyDesignId), existingPlannedWorkFlowEntities, incomingPlannedWorkFlowEntities);
                            studyDesignList.Add(existingStudyDesignEntities.Find(x => x.studyDesignId == item.studyDesignId));
                            existing.clinicalStudy.currentSections.Find(x => x.studyDesigns != null).studyDesigns.RemoveAll(x => x.studyDesignId == item.studyDesignId);
                        }
                        else
                        {
                            var studyDesign = SectionIdGenerator.StudyDesignIdGenerator(item);
                            studyDesignList.Add(studyDesign);
                        }                        
                    }
                    existing.clinicalStudy.currentSections.FindAll(x => x.studyDesigns != null).ForEach(x => x.studyDesigns = studyDesignList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Study Design Sections
        public static void StudyPopulationsSectionCheck(StudyDesignEntity existingStudyDesign, List<StudyPopulationEntity> existingStudyPopulationEntities, List<StudyPopulationEntity> incomingStudyPopulationEntities)
        {
            try
            {
                if (existingStudyPopulationEntities.Count() == 0 && incomingStudyPopulationEntities.Count() != 0)
                {
                    existingStudyPopulationEntities.ForEach(x => x.studyPopulationId = IdGenerator.GenerateId());
                    CurrentSectionsEntity currentSectionsEntity = new CurrentSectionsEntity()
                    {
                        currentSectionsId = IdGenerator.GenerateId(),
                        sectionType = StudySectionTypes.STUDY_POPULATIONS.ToString(),
                        studyPopulations = existingStudyPopulationEntities
                    };
                    existingStudyDesign.currentSections.Add(currentSectionsEntity);
                }

                else if (existingStudyPopulationEntities.Count() != 0 && incomingStudyPopulationEntities.Count() != 0)
                {
                    var studyPopulationList = new List<StudyPopulationEntity>();
                    foreach (var item in incomingStudyPopulationEntities)
                    {
                        if (existingStudyPopulationEntities.Any(x => x.studyPopulationId == item.studyPopulationId))
                        {
                            studyPopulationList.Add(item);
                            existingStudyDesign.currentSections.Find(x => x.studyPopulations != null).studyPopulations.RemoveAll(x => x.studyPopulationId == item.studyPopulationId);
                        }
                        else
                        {                            
                            item.studyPopulationId = IdGenerator.GenerateId();
                            studyPopulationList.Add(item);
                        }
                    }
                    existingStudyDesign.currentSections.FindAll(x => x.studyPopulations != null).ForEach(x => x.studyPopulations = studyPopulationList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Planned WorkFlow Section
        public static void StudyPlannedWorkFlowSectionCheck(StudyDesignEntity existingStudyDesign, List<PlannedWorkFlowEntity> existingPlannedWorkFlowsEntities, List<PlannedWorkFlowEntity> incomingPlannedWorkFlowsEntities)
        {
            try
            {
                if (existingPlannedWorkFlowsEntities.Count() == 0 && incomingPlannedWorkFlowsEntities.Count() != 0)
                {
                    var newPlannedWorkFlowList = new List<PlannedWorkFlowEntity>();
                    foreach (var item in incomingPlannedWorkFlowsEntities)
                    {
                        var newPlannedWorkflow = SectionIdGenerator.StudyPlannedWorkFlowIdGenerator(item);
                        newPlannedWorkFlowList.Add(newPlannedWorkflow);
                    }
                    CurrentSectionsEntity currentSectionsEntity = new CurrentSectionsEntity()
                    {
                        currentSectionsId = IdGenerator.GenerateId(),
                        sectionType = StudySectionTypes.PLANNED_WORKFLOWS.ToString(),
                        plannedWorkflows = newPlannedWorkFlowList
                    };
                    existingStudyDesign.currentSections.Add(currentSectionsEntity);
                }

                else if (existingPlannedWorkFlowsEntities.Count() != 0 && incomingPlannedWorkFlowsEntities.Count() != 0)
                {
                    var plannedWorkFlowsList = new List<PlannedWorkFlowEntity>();
                    foreach (var item in incomingPlannedWorkFlowsEntities)
                    {
                        if (existingPlannedWorkFlowsEntities.Any(x => x.plannedWorkFlowId == item.plannedWorkFlowId))
                        {                            
                            PointInTimeSectionCheck(existingPlannedWorkFlowsEntities.Find(x => x.plannedWorkFlowId == item.plannedWorkFlowId).startPoint,item.startPoint);
                            PointInTimeSectionCheck(existingPlannedWorkFlowsEntities.Find(x => x.plannedWorkFlowId == item.plannedWorkFlowId).endPoint,item.endPoint);
                            TransitionSectionCheck(existingPlannedWorkFlowsEntities.Find(x => x.plannedWorkFlowId == item.plannedWorkFlowId).transitions,item.transitions);
                            plannedWorkFlowsList.Add(item);
                            existingStudyDesign.currentSections.Find(x => x.plannedWorkflows != null).plannedWorkflows.RemoveAll(x => x.plannedWorkFlowId == item.plannedWorkFlowId);
                        }
                        else
                        {
                            var plannedWorkFlows = SectionIdGenerator.StudyPlannedWorkFlowIdGenerator(item);
                            plannedWorkFlowsList.Add(plannedWorkFlows);
                        }
                    }
                    existingStudyDesign.currentSections.FindAll(x => x.plannedWorkflows != null).ForEach(x => x.plannedWorkflows = plannedWorkFlowsList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void TransitionSectionCheck(List<TransitionEntity> existingTransitionEntities, List<TransitionEntity> incomingTransitionEntities)
        {
            try
            {
                var transitionList = new List<TransitionEntity>();
                foreach (var item in incomingTransitionEntities)
                {
                    if (existingTransitionEntities.Any(x => x.transitionId == item.transitionId))
                    {
                        PointInTimeSectionCheck(existingTransitionEntities.Find(x => x.transitionId == item.transitionId).fromPointInTime, item.fromPointInTime);
                        PointInTimeSectionCheck(existingTransitionEntities.Find(x => x.transitionId == item.transitionId).toPointInTime, item.toPointInTime);
                        TransitionCriteriaSectionCheck(existingTransitionEntities.Find(x => x.transitionId == item.transitionId).transitionCriteria, item.transitionCriteria);
                        TransitionRuleSectionCheck(existingTransitionEntities.Find(x => x.transitionId == item.transitionId).transitionRule, item.transitionRule);
                        transitionList.Add(item);
                        existingTransitionEntities.RemoveAll(x => x.transitionId == item.transitionId);
                    }
                    else
                    {
                        item.transitionId = IdGenerator.GenerateId();
                        item.transitionCriteria.ForEach(x => x.transitionCriteriaId = IdGenerator.GenerateId());
                        transitionList.Add(item);
                    }

                }
                incomingTransitionEntities = transitionList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void TransitionCriteriaSectionCheck(List<TransitionCriteriaEntity> existingTransitionCriteriaEntities,List<TransitionCriteriaEntity> incomingTransitionCriteriaEntities)
        {
            try
            {
                var transitionCriteriaList = new List<TransitionCriteriaEntity>();
                foreach (var item in incomingTransitionCriteriaEntities)
                {
                    if (existingTransitionCriteriaEntities.Any(x => x.transitionCriteriaId == item.transitionCriteriaId))
                    {
                        transitionCriteriaList.Add(item);
                        existingTransitionCriteriaEntities.RemoveAll(x => x.transitionCriteriaId == item.transitionCriteriaId);
                    }
                    else
                    {
                        item.transitionCriteriaId = IdGenerator.GenerateId();
                        transitionCriteriaList.Add(item);
                    }
                }
                incomingTransitionCriteriaEntities = transitionCriteriaList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void TransitionRuleSectionCheck(TransitionRuleEntity existingTransitionRuleEntity, TransitionRuleEntity incomingTransitionRuleEntity)
        {
            try
            {
                if (existingTransitionRuleEntity.transitionRuleId == null && incomingTransitionRuleEntity.transitionRuleId != null)
                {
                    incomingTransitionRuleEntity.transitionRuleId = IdGenerator.GenerateId();
                }
                else if (existingTransitionRuleEntity.transitionRuleId != incomingTransitionRuleEntity.transitionRuleId)
                {
                    incomingTransitionRuleEntity.transitionRuleId = IdGenerator.GenerateId();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void PointInTimeSectionCheck(PointInTimeEntity existingPointInTime, PointInTimeEntity incomingPointInTime)
        {
            try
            {
                if (existingPointInTime.pointInTimeId == null && incomingPointInTime.pointInTimeId != null)
                {
                    incomingPointInTime.pointInTimeId = IdGenerator.GenerateId();
                }
                else if (existingPointInTime.pointInTimeId != incomingPointInTime.pointInTimeId)
                {
                    incomingPointInTime.pointInTimeId = IdGenerator.GenerateId();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion        

        #region Study Cells Section
        public static void StudyCellsSectionCheck(StudyDesignEntity existingStudyDesign, List<StudyCellEntity> existingStudyCellsEntities, List<StudyCellEntity> incomingStudyCellsEntities)
        {
            try
            {
                if (existingStudyCellsEntities.Count() == 0 && incomingStudyCellsEntities.Count() != 0)
                {
                    var newStudyCellList = new List<StudyCellEntity>();
                    foreach (var item in incomingStudyCellsEntities)
                    {
                        var newStudyCell = SectionIdGenerator.StudyCellsIdGenerator(item);
                        newStudyCellList.Add(newStudyCell);
                    }
                    CurrentSectionsEntity currentSectionsEntity = new CurrentSectionsEntity()
                    {
                        currentSectionsId = IdGenerator.GenerateId(),
                        sectionType = StudySectionTypes.STUDY_CELLS.ToString(),
                        studyCells = newStudyCellList
                    };
                    existingStudyDesign.currentSections.Add(currentSectionsEntity);
                }

                else if (existingStudyCellsEntities.Count() != 0 && incomingStudyCellsEntities.Count() != 0)
                {
                    var studyCellList = new List<StudyCellEntity>();
                    foreach (var item in incomingStudyCellsEntities)
                    {
                        if (existingStudyCellsEntities.Any(x => x.studyCellId == item.studyCellId))
                        {
                            StudyElementsSectionCheck(existingStudyCellsEntities.Find(x => x.studyCellId == item.studyCellId).studyElements, item.studyElements);
                            StudyArmSectionCheck(existingStudyCellsEntities.Find(x => x.studyCellId == item.studyCellId).studyArm, item.studyArm);
                            StudyEpochSectionCheck(existingStudyCellsEntities.Find(x => x.studyCellId == item.studyCellId).studyEpoch, item.studyEpoch);
                            studyCellList.Add(item);
                            existingStudyDesign.currentSections.Find(x => x.studyCells != null).studyCells.RemoveAll(x => x.studyCellId == item.studyCellId);
                        }
                        else
                        {
                            var studyCells = SectionIdGenerator.StudyCellsIdGenerator(item);
                            studyCellList.Add(studyCells);
                        }
                    }
                    existingStudyDesign.currentSections.FindAll(x => x.studyCells != null).ForEach(x => x.studyCells = studyCellList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void StudyElementsSectionCheck(List<StudyElementEntity> existingStudyElementsEntities, List<StudyElementEntity> incomingStudyElementsEntities)
        {
            try
            {
                var studyElementsList = new List<StudyElementEntity>();
                foreach (var item in incomingStudyElementsEntities)
                {
                    if (existingStudyElementsEntities.Any(x => x.studyElementId == item.studyElementId))
                    {
                        RuleSectionCheck(existingStudyElementsEntities.Find(x => x.studyElementId == item.studyElementId).startRule, item.startRule);
                        RuleSectionCheck(existingStudyElementsEntities.Find(x => x.studyElementId == item.studyElementId).endRule, item.endRule);
                        studyElementsList.Add(item);
                        existingStudyElementsEntities.RemoveAll(x => x.studyElementId == item.studyElementId);
                    }
                    else
                    {
                        item.studyElementId = IdGenerator.GenerateId();
                        item.startRule.RuleId = IdGenerator.GenerateId();
                        item.endRule.RuleId = IdGenerator.GenerateId();
                        studyElementsList.Add(item);
                    }
                }
                incomingStudyElementsEntities = studyElementsList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void RuleSectionCheck(RuleEntity existingRuleEntity, RuleEntity incomingRuleEntity)
        {
            try
            {
                if (existingRuleEntity.RuleId == null && incomingRuleEntity.RuleId != null)
                {
                    incomingRuleEntity.RuleId = IdGenerator.GenerateId();
                }
                else if (existingRuleEntity.RuleId != incomingRuleEntity.RuleId)
                {
                    incomingRuleEntity.RuleId = IdGenerator.GenerateId();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void StudyArmSectionCheck(StudyArmEntity existingStudyArm,StudyArmEntity incomingStudyArm)
        {
            try
            {
                if (existingStudyArm.studyArmId == null && incomingStudyArm.studyArmId != null)
                {
                    incomingStudyArm.studyArmId = IdGenerator.GenerateId();
                }
                else if (existingStudyArm.studyArmId != incomingStudyArm.studyArmId)
                {
                    incomingStudyArm.studyArmId = IdGenerator.GenerateId();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void StudyEpochSectionCheck(StudyEpochEntity existingStudyEpoch, StudyEpochEntity incomingStudyEpoch)
        {
            try
            {
                if (existingStudyEpoch.studyEpochId == null && incomingStudyEpoch.studyEpochId != null)
                {
                    incomingStudyEpoch.studyEpochId = IdGenerator.GenerateId();
                }
                else if (existingStudyEpoch.studyEpochId != incomingStudyEpoch.studyEpochId)
                {
                    incomingStudyEpoch.studyEpochId = IdGenerator.GenerateId();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion

        #endregion
    }
}
