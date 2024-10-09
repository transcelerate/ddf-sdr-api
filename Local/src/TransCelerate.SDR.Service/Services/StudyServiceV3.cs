using AutoMapper;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Amqp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.StudyV3;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.StudyV3;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV3;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.DataAccess.Repositories;
using TransCelerate.SDR.Services.Interfaces;

namespace TransCelerate.SDR.Services.Services
{
    public class StudyServiceV3 : IStudyServiceV3
    {
        #region Variables
        private readonly IStudyRepositoryV3 _studyRepository;
        private readonly IMapper _mapper;
        private readonly ILogHelper _logger;
        private readonly IHelperV3 _helper;
        private readonly ServiceBusClient _serviceBusClient;
        private readonly IChangeAuditRepository _changeAuditRepositoy;
        #endregion

        #region Constructor
        public StudyServiceV3(IStudyRepositoryV3 studyRepository, IMapper mapper, ILogHelper logger, IHelperV3 helper, ServiceBusClient serviceBusClient, IChangeAuditRepository changeAuditRepository)
        {
            _changeAuditRepositoy = changeAuditRepository;
            _studyRepository = studyRepository;
            _mapper = mapper;
            _logger = logger;
            _helper = helper;
            _serviceBusClient = serviceBusClient;
        }
        #endregion

        #region GET Methods
        /// <summary>
        /// GET All Elements For a Study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="object"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<object> GetStudy(string studyId, int sdruploadversion, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(StudyServiceV3)}; Method : {nameof(GetStudy)};");
                studyId = studyId.Trim();

                StudyDefinitionsEntity study = await _studyRepository.GetStudyItemsAsync(studyId: studyId, sdruploadversion: sdruploadversion).ConfigureAwait(false);

                if (study == null)
                {
                    return null;
                }
                else
                {
                    StudyDefinitionsEntity checkStudy = await CheckAccessForAStudy(study, user);
                    if (checkStudy == null)
                        return Constants.ErrorMessages.Forbidden;
                    var studyDTO = _mapper.Map<StudyDefinitionsDto>(study);  //Mapping Entity to Dto
                    studyDTO.Links = LinksHelper.GetLinksForUi(study.Study.StudyId, study.Study.StudyDesigns?.Select(x => x.Id).ToList(), study.AuditTrail.UsdmVersion, study.AuditTrail.SDRUploadVersion);
                    return studyDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(StudyServiceV3)}; Method : {nameof(GetStudy)};");
            }
        }

        /// <summary>
        /// GET Partial Elements For a Study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <param name="listofelements">List of elements</param>
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="object"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<object> GetPartialStudyElements(string studyId, int sdruploadversion, LoggedInUser user, string[] listofelements)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(StudyServiceV3)}; Method : {nameof(GetPartialStudyElements)};");
                studyId = studyId.Trim();

                StudyDefinitionsEntity study = await _studyRepository.GetPartialStudyItemsAsync(studyId, sdruploadversion, listofelements).ConfigureAwait(false);

                if (study == null)
                {
                    return null;
                }
                else
                {
                    StudyDefinitionsEntity checkStudy = await CheckAccessForAStudy(study, user);
                    if (checkStudy == null)
                        return Constants.ErrorMessages.Forbidden;
                    var studyDTO = _mapper.Map<StudyDefinitionsDto>(study);  //Mapping Entity to Dto 
                    return _helper.RemoveStudyElements(listofelements, studyDTO);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(StudyServiceV3)}; Method : {nameof(GetPartialStudyElements)};");
            }
        }

        /// <summary>
        /// GET Study Designs of a Study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="studyDesignId">Study Design ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <param name="listofelements">List of elements</param>
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="object"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<object> GetStudyDesigns(string studyId, string studyDesignId, int sdruploadversion, LoggedInUser user, string[] listofelements)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(StudyServiceV3)}; Method : {nameof(GetStudy)};");
                if (!String.IsNullOrWhiteSpace(studyDesignId) || (listofelements is not null && listofelements.Any()))
                {
                    return await GetPartialStudyDesigns(studyId, studyDesignId, sdruploadversion, user, listofelements);
                }
                else
                {
                    studyId = studyId.Trim();

                    StudyDefinitionsEntity study = await _studyRepository.GetPartialStudyDesignItemsAsync(studyId: studyId, sdruploadversion: sdruploadversion).ConfigureAwait(false);

                    if (study == null)
                    {
                        return null;
                    }
                    else
                    {
                        StudyDefinitionsEntity checkStudy = await CheckAccessForAStudy(study, user);
                        if (checkStudy == null)
                            return Constants.ErrorMessages.Forbidden;

                        var studyDesigns = _mapper.Map<List<StudyDesignDto>>(checkStudy?.Study?.StudyDesigns);  //Mapping Entity to Dto

                        if (studyDesigns is not null && studyDesigns.Any())
                            return new StudyDesignsResponseDto
                            {
                                StudyDesigns = _helper.RemoveStudyDesignElements(Constants.StudyDesignElementsV3, studyDesigns, studyId),
                                Links = LinksHelper.GetLinks(study.Study.StudyId, study.Study.StudyDesigns?.Select(x => x.Id), study.AuditTrail.UsdmVersion, study.AuditTrail.SDRUploadVersion)
                            };

                        return Constants.ErrorMessages.StudyDesignNotFound;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(StudyServiceV3)}; Method : {nameof(GetStudy)};");
            }
        }

        public static List<EncounterEntity> GetOrderedEncounters(List<EncounterEntity> encounters)
        {
            if (encounters != null && encounters.Any())
            {
                if (encounters.Count(x => String.IsNullOrWhiteSpace(x.PreviousEncounterId)) == 1 && encounters.Count(x => String.IsNullOrWhiteSpace(x.NextEncounterId)) == 1)
                {
                    List<EncounterEntity> encountersLinkedList = new()
                    {
                        encounters.Where(x => String.IsNullOrWhiteSpace(x.PreviousEncounterId)).FirstOrDefault()
                    };
                    for (int i = 1; i < encounters.Count; i++)
                    {
                        if (encounters.Where(x => x.PreviousEncounterId == encountersLinkedList[i - 1].Id).Any() && encounters.Where(x => x.PreviousEncounterId == encountersLinkedList[i - 1].Id).Count() == 1)
                            encountersLinkedList.Add(encounters.Where(x => x.PreviousEncounterId == encountersLinkedList[i - 1].Id).First());
                        else
                            break;
                    }
                    return encountersLinkedList.Count == encounters.Count ? encountersLinkedList : encounters;
                }
            }
            return encounters;
        }

        public static List<ActivityEntity> GetOrderedActivities(List<ActivityEntity> activities)
        {
            if (activities != null && activities.Any())
            {
                if (activities.Count(x => String.IsNullOrWhiteSpace(x.PreviousActivityId)) == 1 && activities.Count(x => String.IsNullOrWhiteSpace(x.NextActivityId)) == 1)
                {
                    List<ActivityEntity> activityLinkedList = new()
                    {
                        activities.Where(x => String.IsNullOrWhiteSpace(x.PreviousActivityId)).FirstOrDefault()
                    };
                    for (int i = 1; i < activities.Count; i++)
                    {
                        if (activities.Where(x => x.PreviousActivityId == activityLinkedList[i - 1].Id).Any() && activities.Where(x => x.PreviousActivityId == activityLinkedList[i - 1].Id).Count() == 1)
                            activityLinkedList.Add(activities.Where(x => x.PreviousActivityId == activityLinkedList[i - 1].Id).First());
                        else
                            break;
                    }
                    return activityLinkedList.Count == activities.Count ? activityLinkedList : activities;
                }
            }
            return activities;
        }

        /// <summary>
        /// GET SoA for a Study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <param name="scheduleTimelineId">Schedule Timeline Id</param>
        /// <param name="studyDesignId">study design Id</param>
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="object"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<object> GetSOAV3(string studyId, string studyDesignId, string scheduleTimelineId, int sdruploadversion, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(StudyServiceV3)}; Method : {nameof(GetSOAV3)};");
                studyId = studyId.Trim();

                StudyDefinitionsEntity study = await _studyRepository.GetStudyItemsAsync(studyId: studyId, sdruploadversion: sdruploadversion).ConfigureAwait(false);
                if (study == null)
                {
                    return null;
                }
                else
                {
                    StudyDefinitionsEntity checkStudy = await CheckAccessForAStudy(study, user);
                    if (checkStudy == null)
                        return Constants.ErrorMessages.Forbidden;

                    var soa = SoAV3(study.Study.StudyDesigns);
                    soa.StudyId = study.Study.StudyId;
                    soa.StudyTitle = study.Study.StudyTitle;
                    if (!String.IsNullOrWhiteSpace(studyDesignId))
                    {
                        if (study.Study.StudyDesigns is null || !soa.StudyDesigns.Any(x => x.StudyDesignId == studyDesignId))
                            return Constants.ErrorMessages.StudyDesignNotFound;

                        if (!String.IsNullOrWhiteSpace(scheduleTimelineId))
                        {
                            soa.StudyDesigns.RemoveAll(x => x.StudyDesignId != studyDesignId);
                            if (soa.StudyDesigns.First().StudyScheduleTimelines is null || !soa.StudyDesigns.First().StudyScheduleTimelines.Any(x => x.ScheduleTimelineId == scheduleTimelineId))
                                return Constants.ErrorMessages.ScheduleTimelineNotFound;
                            soa.StudyDesigns.First().StudyScheduleTimelines.RemoveAll(y => y.ScheduleTimelineId != scheduleTimelineId);
                            return soa;
                        }
                        soa.StudyDesigns.RemoveAll(x => x.StudyDesignId != studyDesignId);
                        return soa;
                    }
                    return soa;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(StudyServiceV3)}; Method : {nameof(GetSOAV3)};");
            }
        }

        public SoADto SoAV3(List<StudyDesignEntity> studyDesigns)
        {
            SoADto soADto = new()
            {
                StudyDesigns = new List<StudyDesigns>()
            };
            if (studyDesigns is not null && studyDesigns.Any())
            {
                studyDesigns.ForEach(design =>
                {
                    StudyDesigns studyDesignSoA = new()
                    {
                        StudyDesignId = design.Id,
                        StudyDesignName = design.StudyDesignName,
                        StudyDesignDescription = design.StudyDesignDescription,
                        StudyScheduleTimelines = new List<ScheduleTimelines>()
                    };
                    List<EncounterEntity> encounters = GetOrderedEncounters(design.Encounters);
                    List<ActivityEntity> activities = GetOrderedActivities(design.Activities);

                    if (design.StudyScheduleTimelines != null && design.StudyScheduleTimelines.Any())
                    {
                        design.StudyScheduleTimelines.ForEach(scheduleTimeline =>
                        {
                            ScheduleTimelines studyTimelineSoA = _mapper.Map<ScheduleTimelines>(scheduleTimeline);
                            //Get ordered Instances
                            List<ScheduledInstanceEntity> scheduledInstances = GetOrderedInstances(scheduleTimeline.ScheduleTimelineInstances);

                            var scheduleActivityInstances = scheduleTimeline.ScheduleTimelineInstances?.Select(x => (x as ScheduledActivityInstanceEntity))
                                                                         .Where(x => x != null).ToList();
                            
                            if (scheduleActivityInstances != null && scheduleActivityInstances.Any())
                            {
                                var activitiesMappedToTimeLine = activities is not null && activities.Any() ? activities.Where(act => scheduleActivityInstances.Where(x => x.ActivityIds is not null && x.ActivityIds.Any()).SelectMany(instance => instance.ActivityIds).Contains(act.Id)).ToList() : new List<ActivityEntity>();
                                var encountersMappedToTimeLine = scheduleActivityInstances.Where(x => !String.IsNullOrWhiteSpace(x.ScheduledActivityInstanceEncounterId)).Select(x => x.ScheduledActivityInstanceEncounterId).ToList();
                                var timingsMappedToTimeline = scheduleActivityInstances.Where(x => x != null && x.ScheduledInstanceTimings is not null)
                                                                                       .SelectMany(x => x.ScheduledInstanceTimings).ToList();
                                if (activitiesMappedToTimeLine.Any() || encountersMappedToTimeLine.Any() || timingsMappedToTimeline.Any())
                                {
                                    studyTimelineSoA.ScheduleTimelineSoA = new()
                                    {
                                        SoA = new List<SoA>(),
                                        OrderOfActivities = activitiesMappedToTimeLine.Select(act => new OrderOfActivities
                                        {
                                            ActivityId = act.Id,
                                            ActivityName = act.ActivityName,
                                            ActivityDescription = act.ActivityDescription,
                                            ActivityIsConditional = act.ActivityIsConditional,
                                            ActivityIsConditionalReason = act.ActivityIsConditionalReason,
                                            ActivityTimelineId = act.ActivityTimelineId,
                                            ActivityTimelineName = String.IsNullOrWhiteSpace(act.ActivityTimelineId) ? string.Empty : design.StudyScheduleTimelines.FirstOrDefault(x => x.Id == act.ActivityTimelineId)?.ScheduleTimelineName,
                                            BiomedicalConcepts = design.BiomedicalConcepts.Where(bc => act.BiomedicalConceptIds != null && act.BiomedicalConceptIds.Any() && act.BiomedicalConceptIds.Contains(bc.Id)).Select(bc => bc.BcName).ToList(),
                                            FootnoteId = string.Empty,
                                            FootnoteDescription = act.ActivityIsConditional ? $"{act.ActivityName} : {act.ActivityIsConditionalReason}" : string.Empty,
                                            DefinedProcedures = act.DefinedProcedures?.Select(y => new ProcedureSoA
                                            {
                                                ProcedureId = y.Id,
                                                ProcedureName = y.ProcedureName,
                                                ProcedureDescription = y.ProcedureDescription,
                                                ProcedureIsConditional = y.ProcedureIsConditional,
                                                ProcedureIsConditionalReason = y.ProcedureIsConditionalReason,
                                                FootnoteId = string.Empty,                                                
                                                FootnoteDescription = y.ProcedureIsConditional ? $"{y.ProcedureName} : {y.ProcedureIsConditionalReason}" : string.Empty
                                            }).ToList()
                                        }).ToList()
                                    };
                                    // SoA for instances where encounter is mapped
                                    encounters?.Where(x => scheduleActivityInstances.Select(y => y.ScheduledActivityInstanceEncounterId).Contains(x.Id)).ToList().ForEach(encounter =>
                                    {
                                        string timingValue = design.StudyScheduleTimelines.Where(x => x.ScheduleTimelineInstances != null).SelectMany(x => x.ScheduleTimelineInstances)
                                                                                          .Where(x => x != null && x.ScheduledInstanceTimings is not null)
                                                                                          .SelectMany(x => x.ScheduledInstanceTimings)
                                                                                          .Where(x => x.Id == encounter.EncounterScheduledAtTimingId).FirstOrDefault()?.TimingValue;
                                        SoA soA = new()
                                        {
                                            EncounterId = encounter.Id,
                                            EncounterName = encounter.EncounterName,
                                            EncounterScheduledAtTimingValue = String.IsNullOrWhiteSpace(timingValue) ? string.Empty : timingValue,
                                            Timings = GetTimings(scheduleActivityInstances.Where(instance => instance.ScheduledActivityInstanceEncounterId == encounter.Id).ToList(), scheduledInstances)
                                        };

                                        studyTimelineSoA.ScheduleTimelineSoA.SoA.Add(soA);
                                    });
                                    // SoA for instances where encounter is not mapped
                                    if (scheduleActivityInstances.Where(x => String.IsNullOrWhiteSpace(x.ScheduledActivityInstanceEncounterId)).Any())
                                    {
                                        SoA soA = new()
                                        {
                                            EncounterId = string.Empty,
                                            EncounterName = string.Empty,
                                            EncounterScheduledAtTimingValue = string.Empty,
                                            Timings = GetTimings(scheduleActivityInstances.Where(x => String.IsNullOrWhiteSpace(x.ScheduledActivityInstanceEncounterId)).ToList(), scheduledInstances)
                                        };

                                        studyTimelineSoA.ScheduleTimelineSoA.SoA.Add(soA);
                                    }
                                }
                            }

                            studyDesignSoA.StudyScheduleTimelines.Add(studyTimelineSoA);
                        });
                    }
                    soADto.StudyDesigns.Add(studyDesignSoA);
                });
            }

            return soADto;
        }
        public List<TimingSoA> GetTimings(List<ScheduledActivityInstanceEntity> scheduledActivityInstances,List<ScheduledInstanceEntity> scheduledInstances)
        {
            if (scheduledActivityInstances is not null && scheduledActivityInstances.Any())
            {                
                //Add sequence number since the ordering based on defaultConditionId includes both ACTIVITY and DECISION Type instances
                int sequenceNumber = 0;
                var scheduledInstancesWithSeqNumber = scheduledInstances.Select(x => new
                {
                    x.Id,
                    SequenceNumber = ++sequenceNumber
                }).ToList();                
                //Assign sequence number for Activity Instances
                var orderedScheduledActivityInstances = scheduledActivityInstances.Select(x => new { 
                    x.ScheduledInstanceTimings,
                    x.ActivityIds,
                    scheduledInstancesWithSeqNumber.FirstOrDefault(y => y.Id == x.Id)?.SequenceNumber
                }).ToList();
                //Add Instances with valid timing values
                var instances = orderedScheduledActivityInstances.Where(x => x.ScheduledInstanceTimings is not null && x.ScheduledInstanceTimings.Any()).Select(x => new
                {                    
                    Timings = _mapper.Map<List<TimingSoA>>(x.ScheduledInstanceTimings),
                    x.ActivityIds,
                    x.SequenceNumber
                }).OrderBy(x => x.SequenceNumber).ToList();                
                //Add Instances without valid timing values
                instances.AddRange(orderedScheduledActivityInstances.Where(x => x.ScheduledInstanceTimings is null || !x.ScheduledInstanceTimings.Any()).Select(x => new
                {                    
                    Timings = _mapper.Map<List<TimingSoA>>(x.ScheduledInstanceTimings),
                    x.ActivityIds,
                    x.SequenceNumber
                }).OrderBy(x => x.SequenceNumber).ToList());
                //Add activities under timing for SoA Response
                instances?.ForEach(instance =>
                {
                    if (instance.Timings is not null && instance.Timings.Any())
                    {
                        instance.Timings.ForEach(y =>
                        {
                            y.Activities = new List<string>();
                            y.Activities.AddRange(instance.ActivityIds is not null && instance.ActivityIds.Any() ? instance.ActivityIds.Distinct().ToList() : new List<string>());
                        });
                    }
                    else
                    {
                        instance.Timings.Add(new TimingSoA { Activities = instance.ActivityIds is not null && instance.ActivityIds.Any() ? instance.ActivityIds.Distinct().ToList() : new List<string>() });
                    }
                });
                return instances.SelectMany(x => x.Timings).Any() ? instances.SelectMany(x => x.Timings).ToList() : new List<TimingSoA>();
            }
            return new List<TimingSoA>();
        }

        public static List<ScheduledInstanceEntity> GetOrderedInstances(List<ScheduledInstanceEntity> scheduledInstances)
        {
            if (scheduledInstances != null && scheduledInstances.Any())
            {
                if (scheduledInstances.Count(x => String.IsNullOrWhiteSpace(x.DefaultConditionId)) == 1)
                {
                    List<ScheduledInstanceEntity> instanceLinkedList = new()
                    {
                        scheduledInstances.Where(x => String.IsNullOrWhiteSpace(x.DefaultConditionId)).FirstOrDefault()
                    };
                    for (int i = 1; i < scheduledInstances.Count; i++)
                    {
                        if (scheduledInstances.Where(x => x.DefaultConditionId == instanceLinkedList[i - 1].Id).Any() && scheduledInstances.Where(x => x.DefaultConditionId == instanceLinkedList[i - 1].Id).Count() == 1)
                            instanceLinkedList.Add(scheduledInstances.Where(x => x.DefaultConditionId == instanceLinkedList[i - 1].Id).First());
                        else
                            break;
                    }
                    //Reverse it to get correct order
                    instanceLinkedList.Reverse();
                    return instanceLinkedList.Count == scheduledInstances.Count ? instanceLinkedList : scheduledInstances;
                }
            }
            return scheduledInstances;
        }
        /// <summary>
        /// GET Study Designs Elements of a Study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <param name="studyDesignId">StudyDesign Id </param>
        /// <param name="listofelements">List of study design elements</param>
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="object"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<object> GetPartialStudyDesigns(string studyId, string studyDesignId, int sdruploadversion, LoggedInUser user, string[] listofelements)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(StudyServiceV3)}; Method : {nameof(GetPartialStudyDesigns)};");
                studyId = studyId.Trim();

                var study = await _studyRepository.GetPartialStudyDesignItemsAsync(studyId, sdruploadversion).ConfigureAwait(false);

                if (study == null)
                {
                    return null;
                }
                else
                {
                    StudyDefinitionsEntity checkStudy = await CheckAccessForAStudy(study, user);
                    if (checkStudy == null)
                        return Constants.ErrorMessages.Forbidden;
                    if (!String.IsNullOrWhiteSpace(studyDesignId))
                    {
                        if (study.Study.StudyDesigns is not null && study.Study.StudyDesigns.Any(x => x.Id == studyDesignId))
                        {
                            var studyDesigns = _mapper.Map<List<StudyDesignDto>>(checkStudy.Study.StudyDesigns.Where(x => x.Id == studyDesignId).ToList());
                            JObject jObject = new()
                            {
                                { string.Concat(nameof(StudyDto.StudyDesigns)[..1].ToLower(), nameof(StudyDto.StudyDesigns).AsSpan(1)), JArray.Parse(JsonConvert.SerializeObject(_helper.RemoveStudyDesignElements(listofelements, studyDesigns, studyId))) }
                            };
                            if (listofelements == null)
                                jObject.Add("links", JObject.Parse(JsonConvert.SerializeObject(LinksHelper.GetLinks(study.Study.StudyId, study.Study.StudyDesigns?.Select(x => x.Id), study.AuditTrail.UsdmVersion, study.AuditTrail.SDRUploadVersion), _helper.GetSerializerSettingsForCamelCasing())));
                            return jObject;
                        }
                        return Constants.ErrorMessages.StudyDesignNotFound;
                    }
                    else
                    {
                        var studyDesigns = _mapper.Map<List<StudyDesignDto>>(checkStudy.Study.StudyDesigns);
                        JObject jObject = new()
                        {
                            { string.Concat(nameof(StudyDto.StudyDesigns)[..1].ToLower(), nameof(StudyDto.StudyDesigns).AsSpan(1)), JArray.Parse(JsonConvert.SerializeObject(_helper.RemoveStudyDesignElements(listofelements, studyDesigns, studyId))) }
                        };
                        if (listofelements == null)
                            jObject.Add("links", JObject.Parse(JsonConvert.SerializeObject(LinksHelper.GetLinks(study.Study.StudyId, study.Study.StudyDesigns?.Select(x => x.Id), study.AuditTrail.UsdmVersion, study.AuditTrail.SDRUploadVersion), _helper.GetSerializerSettingsForCamelCasing())));
                        return study.Study.StudyDesigns is not null && study.Study.StudyDesigns.Any() ?
                            jObject : Constants.ErrorMessages.StudyDesignNotFound;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(StudyServiceV3)}; Method : {nameof(GetPartialStudyDesigns)};");
            }
        }

        /// <summary>
        /// GET eCPT Elements For a Study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <param name="studyDesignId">studyDesignId</param>
        /// <param name="user">Logged in user</param>
        /// <returns>
        /// A <see cref="object"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<object> GeteCPTV3(string studyId, int sdruploadversion, string studyDesignId, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(CommonServices)}; Method : {nameof(GeteCPTV3)};");
                studyId = studyId.Trim();

                var study = await _studyRepository.GetStudyItemsAsync(studyId: studyId, sdruploadversion: sdruploadversion).ConfigureAwait(false);

                if (study == null)
                {
                    return null;
                }
                else
                {
                    StudyDefinitionsEntity checkStudy = await CheckAccessForAStudy(study, user);
                    if (checkStudy == null)
                        return Constants.ErrorMessages.Forbidden;

                    var studyDTO = _mapper.Map<StudyDefinitionsDto>(study);

                    if (studyDTO.Study.StudyDesigns == null || !studyDTO.Study.StudyDesigns.Any())
                        return Constants.ErrorMessages.StudyDesignNotFoundCPT;

                    if (studyDesignId != null)
                    {
                        if (studyDTO.Study.StudyDesigns.Any(x => x.Id == studyDesignId))
                            studyDTO.Study.StudyDesigns.RemoveAll(x => x.Id != studyDesignId);
                        else
                            return Constants.ErrorMessages.StudyDesignIdNotFoundCPT;
                    }

                    var eCPT = GetCPTDataV3(studyDTO.Study, study.AuditTrail);

                    return eCPT;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(CommonServices)}; Method : {nameof(GeteCPTV3)};");
            }
        }

        public Core.DTO.eCPT.ECPTDto GetCPTDataV3(StudyDto studyDto, AuditTrailEntity auditTrail)
        {
            var links = LinksHelper.GetLinks(studyDto.StudyId, studyDto.StudyDesigns.Select(c => c.Id).ToList(), auditTrail.UsdmVersion, auditTrail.SDRUploadVersion);
            Core.DTO.eCPT.StudyDetailsDto studyDetailsDto = new()
            {
                StudyId = studyDto.StudyId,
                StudyTitle = studyDto.StudyTitle,

                UsdmVersion = auditTrail.UsdmVersion,
                SDRUploadVersion = auditTrail.SDRUploadVersion,
                Links = new
                {
                    links.StudyDefinitions,
                    links.RevisionHistory
                },
            };
            List<Core.DTO.eCPT.StudyDesignDto> studyeCPTDtos = new();
            if (studyDto.StudyDesigns != null && studyDto.StudyDesigns.Any())
            {

                studyDto.StudyDesigns.ForEach(design =>
                {
                    Core.DTO.eCPT.StudyDesignDto studyeCPTDto = new()
                    {
                        StudyDesignId = design.Id,
                        StudyDesignLink = links.StudyDesigns.Find(x => x.StudyDesignId == design.Id).StudyDesignLink,
                        StudyDesignName = design.StudyDesignName,
                        ECPTData = new Core.DTO.eCPT.ECPTDataDto
                        {
                            TitlePage = new Core.DTO.eCPT.TitlePageDto
                            {
                                Acronym = studyDto.StudyAcronym,
                                AmendmentNumber = studyDto.StudyProtocolVersions != null && studyDto.StudyProtocolVersions.Any() ? ECPTHelper.GetOrderedStudyProtocolsV3(studyDto.StudyProtocolVersions).ProtocolAmendment : null,
                                ApprovalDate = studyDto.StudyProtocolVersions != null && studyDto.StudyProtocolVersions.Any() ? ECPTHelper.GetOrderedStudyProtocolsV3(studyDto.StudyProtocolVersions).ProtocolEffectiveDate : null,
                                ConditionDisease = design.StudyIndications != null && design.StudyIndications.Any() ?
                                                   design.StudyIndications.Count == 1 ?
                                                   design.StudyIndications.FirstOrDefault().IndicationDescription
                                                   : $"{String.Join(", ", design.StudyIndications.Select(x => x.IndicationDescription).ToArray(), 0, design.StudyIndications.Count - 1)} and {design.StudyIndications.Select(x => x.IndicationDescription).LastOrDefault()}"
                                                   : null,
                                RegulatoryAgencyIdentifierNumbers = studyDto.StudyIdentifiers.Where(x => Constants.IdType.RegulatoryAgencyIdentifierNumberConstants.Any(y => y.ToLower() == x.StudyIdentifierScope?.OrganisationType?.Decode?.ToLower())).Any() ? _mapper.Map<List<Core.DTO.eCPT.RegulatoryAgencyIdentifierNumberDto>>(studyDto.StudyIdentifiers.Where(x => Constants.IdType.RegulatoryAgencyIdentifierNumberConstants.Any(y => y.ToLower() == x.StudyIdentifierScope?.OrganisationType?.Decode?.ToLower()))) : null,
                                SponsorName = studyDto.StudyIdentifiers.Where(x => x.StudyIdentifierScope.OrganisationType.Decode.Equals(Constants.IdType.SPONSOR_ID_V1, StringComparison.OrdinalIgnoreCase)).Select(x => x.StudyIdentifierScope.OrganisationName).FirstOrDefault(),
                                SponsorLegalAddress = studyDto.StudyIdentifiers.Where(x => x.StudyIdentifierScope.OrganisationType.Decode.Equals(Constants.IdType.SPONSOR_ID_V1, StringComparison.OrdinalIgnoreCase)).Select(x => x.StudyIdentifierScope.OrganizationLegalAddress).FirstOrDefault() == null ? null
                                                                : studyDto.StudyIdentifiers.Where(x => x.StudyIdentifierScope.OrganisationType.Decode.Equals(Constants.IdType.SPONSOR_ID_V1, StringComparison.OrdinalIgnoreCase)).Select(x => x.StudyIdentifierScope.OrganizationLegalAddress).Select(x => $"{x.Line}, {x.City}, {x.District}, {x.State}, {x.PostalCode}, {x.Country?.Decode}").FirstOrDefault(),
                                StudyPhase = ECPTHelper.GetCptMappingValue(Constants.SdrCptMasterDataEntities.StudyPhase, studyDto.StudyPhase?.StandardCode?.Code) ?? studyDto.StudyPhase?.StandardCode?.Decode,
                                Protocol = new Core.DTO.eCPT.ProtocolDto
                                {
                                    ProtocolID = studyDto.StudyIdentifiers.Where(x => x.StudyIdentifierScope.OrganisationType.Decode.Equals(Constants.IdType.SPONSOR_ID_V1, StringComparison.OrdinalIgnoreCase)).Select(x => x.StudyIdentifier).FirstOrDefault(),
                                    ProtocolShortTitle = studyDto.StudyProtocolVersions != null && studyDto.StudyProtocolVersions.Any() ? ECPTHelper.GetOrderedStudyProtocolsV3(studyDto.StudyProtocolVersions).BriefTitle : null,
                                    ProtocolTitle = studyDto.StudyTitle
                                }
                            },
                            ProtocolSummary = new Core.DTO.eCPT.ProtocolSummaryDto
                            {
                                Synopsis = new Core.DTO.eCPT.SynopsisDto
                                {
                                    NumberofParticipants = design.StudyPopulations?.Sum(x => int.Parse(Convert.ToString(x.PlannedNumberOfParticipants))).ToString(),
                                    PrimaryPurpose = design.TrialIntentTypes != null && design.TrialIntentTypes.Any() ?
                                                   design.TrialIntentTypes.Count == 1 ? ECPTHelper.GetCptMappingValue(Constants.SdrCptMasterDataEntities.TrialIntentType, design.TrialIntentTypes.FirstOrDefault().Code) ?? design.TrialIntentTypes.FirstOrDefault().Decode
                                                   : $"{String.Join(", ", design.TrialIntentTypes.Select(x => ECPTHelper.GetCptMappingValue(Constants.SdrCptMasterDataEntities.TrialIntentType, x.Code) ?? x.Decode).ToArray(), 0, design.TrialIntentTypes.Count - 1)}" +
                                                   $" and {design.TrialIntentTypes.Select(x => ECPTHelper.GetCptMappingValue(Constants.SdrCptMasterDataEntities.TrialIntentType, x.Code) ?? x.Decode).LastOrDefault()}"
                                                   : null,
                                    EnrollmentTarget = design.StudyPopulations != null && design.StudyPopulations.Any() ?
                                                      design.StudyPopulations.Count == 1 ? design.StudyPopulations.FirstOrDefault().PopulationDescription
                                                      : $"{String.Join(", ", design.StudyPopulations.Select(x => x.PopulationDescription).ToArray(), 0, design.StudyPopulations.Count - 1)} and {design.StudyPopulations.Select(x => x.PopulationDescription).LastOrDefault()}"
                                                      : null,
                                    InterventionModel = ECPTHelper.GetCptMappingValue(Constants.SdrCptMasterDataEntities.InterventionModel, design?.InterventionModel?.Code) ?? design?.InterventionModel?.Decode,
                                    NumberofArms = design.StudyArms != null && design.StudyArms.Any() ?
                                                    design.StudyArms.Select(x => x.Id).Distinct().Count().ToString() : 0.ToString()
                                }
                            },
                            PageHeader = new Core.DTO.eCPT.PageHeaderDto
                            {
                                VersionNumber = studyDto.StudyProtocolVersions != null && studyDto.StudyProtocolVersions.Any() ? ECPTHelper.GetOrderedStudyProtocolsV3(studyDto.StudyProtocolVersions).ProtocolVersion : null,
                            },
                            StudyPopulation = new Core.DTO.eCPT.StudyPopulationDto
                            {
                                InclusionCriteria = new Core.DTO.eCPT.InclusionCriteriaDto
                                {

                                    PlannedMaximumAgeofSubjects = design.StudyPopulations != null && design.StudyPopulations.Any() ?
                                                                           ECPTHelper.CheckForMaxMin(design.StudyPopulations.Select(x => x.PlannedMaximumAgeOfParticipants).ToList(), true) : null,

                                    PlannedMinimumAgeofSubjects = design.StudyPopulations != null && design.StudyPopulations.Any() ?
                                                                           ECPTHelper.CheckForMaxMin(design.StudyPopulations.Select(x => x.PlannedMinimumAgeOfParticipants).ToList(), false) : null,

                                    SexofParticipants = ECPTHelper.GetPlannedSexOfParticipantsV3(design.StudyPopulations)
                                }
                            },
                            Introduction = new Core.DTO.eCPT.IntroductionDto
                            {
                                StudyRationale = studyDto.StudyRationale
                            },
                            StudyDesign = new Core.DTO.eCPT.StudyDesignCptDto
                            {
                                ScientificRationaleForStudyDesign = design.StudyDesignRationale
                            },
                            StatisticalConsiderations = new Core.DTO.eCPT.StatisticalConsiderationsDto
                            {
                                PopulationsForAnalyses = design.StudyEstimands != null && design.StudyEstimands.Any() ?
                                                   design.StudyEstimands.Count == 1 ?
                                                   design.StudyEstimands.FirstOrDefault().AnalysisPopulation.PopulationDescription
                                                   : $"{String.Join(", ", design.StudyEstimands.Select(x => x.AnalysisPopulation.PopulationDescription).ToArray(), 0, design.StudyEstimands.Count - 1)} and {design.StudyEstimands.Select(x => x.AnalysisPopulation.PopulationDescription).LastOrDefault()}"
                                                   : null,
                            },
                            ObjectivesEndpointsAndEstimands = ECPTHelper.GetObjectivesEndpointsAndEstimandsDtoV3(design.StudyObjectives, _mapper),
                            StudyInterventionsAndConcomitantTherapy = new Core.DTO.eCPT.StudyInterventionsAndConcomitantTherapyDto
                            {
                                StudyInterventionsAdministered = design.StudyInvestigationalInterventions != null && design.StudyInvestigationalInterventions.Any() ?
                                           _mapper.Map<List<Core.DTO.eCPT.StudyInterventionsAdministeredDto>>(design.StudyInvestigationalInterventions)
                                           : null,
                                StudyArms = design.StudyArms != null && design.StudyArms.Any() ?                                           
                                           _mapper.Map<List<Core.DTO.eCPT.StudyArmDto>>(design.StudyArms)
                                           : null
                            }

                        }
                    };
                    studyeCPTDtos.Add(studyeCPTDto);
                });
            }
            return new Core.DTO.eCPT.ECPTDto
            {
                StudyDesign = studyeCPTDtos,
                StudyDetails = studyDetailsDto
            };
        }


        /// <summary>
        /// GET Differences between two versions of a study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdrUploadVersionOne">First Version of study</param> 
        /// <param name="sdrUploadVersionTwo">Second Version of study</param>
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="object"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<object> GetDifferences(string studyId, int sdrUploadVersionOne, int sdrUploadVersionTwo, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(StudyServiceV3)}; Method : {nameof(GetDifferences)};");
                studyId = studyId.Trim();

                StudyDefinitionsEntity studyOne = await _studyRepository.GetStudyItemsAsync(studyId: studyId, sdruploadversion: sdrUploadVersionOne).ConfigureAwait(false);
                StudyDefinitionsEntity studyTwo = await _studyRepository.GetStudyItemsAsync(studyId: studyId, sdruploadversion: sdrUploadVersionTwo).ConfigureAwait(false);

                if (studyOne == null && studyTwo == null)
                {
                    return null;
                }
                else if (studyOne == null || studyTwo == null)
                {
                    return Constants.ErrorMessages.OneVersionNotFound;
                }
                else
                {
                    StudyDefinitionsEntity checkStudy = await CheckAccessForAStudy(studyOne, user);
                    if (checkStudy == null)
                        return Constants.ErrorMessages.ForbiddenForAStudy;
                    checkStudy = await CheckAccessForAStudy(studyTwo, user);
                    if (checkStudy == null)
                        return Constants.ErrorMessages.ForbiddenForAStudy;

                    return new VersionCompareDto
                    {
                        StudyId = studyId,
                        LHS = new VersionDetails { EntryDateTime = studyOne.AuditTrail.EntryDateTime, SDRUploadVersion = studyOne.AuditTrail.SDRUploadVersion},
                        RHS = new VersionDetails { EntryDateTime = studyTwo.AuditTrail.EntryDateTime, SDRUploadVersion = studyTwo.AuditTrail.SDRUploadVersion},
                        ElementsChanged = _helper.GetChangedValuesForStudyComparison(studyOne, studyTwo)
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(StudyServiceV3)}; Method : {nameof(GetDifferences)};");
            }
        }
        #endregion

        #region POST Methods
        /// <summary>
        /// POST All Elements For a Study
        /// </summary>
        /// <param name="studyDTO">Study for Inserting/Updating in Database</param>        
        /// <param name="user">Logged In User</param>
        /// <param name="method">POST/PUT</param>
        /// <returns>
        /// A <see cref="object"/> which has study ID and study design ID's <br></br> <br></br>
        /// <see langword="null"/> If the insert is not done
        /// </returns>
        public async Task<object> PostAllElements(StudyDefinitionsDto studyDTO, LoggedInUser user, string method)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(StudyServiceV3)}; Method : {nameof(PostAllElements)};");
                if (!await CheckPermissionForAUser(user))
                    return Constants.ErrorMessages.PostRestricted;
                StudyDefinitionsEntity incomingStudyEntity = new()
                {
                    Study = _mapper.Map<StudyEntity>(studyDTO.Study),
                    AuditTrail = _helper.GetAuditTrail(user?.UserName),
                    Id = MongoDB.Bson.ObjectId.GenerateNewId()
                };

                if (method == HttpMethod.Post.Method) //POST Endpoint to create new study
                {
                    studyDTO = await CreateNewStudy(incomingStudyEntity).ConfigureAwait(false);
                }
                else //PUT Endpoint Create New Version for the study
                {
                    AuditTrailEntity existingAuditTrail = await _studyRepository.GetUsdmVersionAsync(incomingStudyEntity.Study.StudyId, 0);

                    if (existingAuditTrail is null) // If PUT Endpoint and study_uuid is not valid, return not valid study
                    {
                        return Constants.ErrorMessages.NotValidStudyId;
                    }

                    if (existingAuditTrail.UsdmVersion == Constants.USDMVersions.V2) // If previus USDM version is same as incoming
                    {
                        StudyDefinitionsEntity existingStudyEntity = await _studyRepository.GetStudyItemsAsync(incomingStudyEntity.Study.StudyId, 0);

                        if (_helper.IsSameStudy(incomingStudyEntity, existingStudyEntity))
                        {
                            studyDTO = await UpdateExistingStudy(incomingStudyEntity, existingStudyEntity).ConfigureAwait(false);
                        }
                        else
                        {
                            studyDTO = await CreateNewVersionForAStudy(incomingStudyEntity, existingStudyEntity.AuditTrail).ConfigureAwait(false);
                            await PushMessageToServiceBus(new Core.DTO.Common.ServiceBusMessageDto { Study_uuid = incomingStudyEntity.Study.StudyId, CurrentVersion = incomingStudyEntity.AuditTrail.SDRUploadVersion });
                        }
                    }
                    else // If previus USDM version is different from incoming
                    {
                        studyDTO = await CreateNewVersionForAStudy(incomingStudyEntity, existingAuditTrail).ConfigureAwait(false);
                        await PushMessageToServiceBus(new Core.DTO.Common.ServiceBusMessageDto { Study_uuid = incomingStudyEntity.Study.StudyId, CurrentVersion = incomingStudyEntity.AuditTrail.SDRUploadVersion });
                    }
                }
                studyDTO.Links = LinksHelper.GetLinksForUi(studyDTO.Study.StudyId, studyDTO.Study.StudyDesigns?.Select(x => x.Id).ToList(), studyDTO.AuditTrail.UsdmVersion, studyDTO.AuditTrail.SDRUploadVersion);
                return studyDTO;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(StudyServiceV3)}; Method : {nameof(PostAllElements)};");
            }
        }

        public async Task<StudyDefinitionsDto> CreateNewStudy(StudyDefinitionsEntity studyEntity)
        {
            //studyEntity = _helper.GeneratedSectionId(studyEntity);
            studyEntity.Study.StudyId = IdGenerator.GenerateId();
            studyEntity.AuditTrail.SDRUploadVersion = 1;
            await _studyRepository.PostStudyItemsAsync(studyEntity);
            await _changeAuditRepositoy.InsertChangeAudit(studyEntity.Study.StudyId, studyEntity.AuditTrail.SDRUploadVersion, studyEntity.AuditTrail.EntryDateTime);
            return _mapper.Map<StudyDefinitionsDto>(studyEntity);
        }

        public async Task<StudyDefinitionsDto> UpdateExistingStudy(StudyDefinitionsEntity incomingStudyEntity, StudyDefinitionsEntity existingStudyEntity)
        {
            existingStudyEntity.AuditTrail.EntryDateTime = incomingStudyEntity.AuditTrail.EntryDateTime;
            incomingStudyEntity.AuditTrail.SDRUploadVersion = existingStudyEntity.AuditTrail.SDRUploadVersion;
            incomingStudyEntity.AuditTrail.UsdmVersion = Constants.USDMVersions.V2;
            await _studyRepository.UpdateStudyItemsAsync(incomingStudyEntity);
            return _mapper.Map<StudyDefinitionsDto>(incomingStudyEntity);
        }

        public async Task<StudyDefinitionsDto> CreateNewVersionForAStudy(StudyDefinitionsEntity incomingStudyEntity, AuditTrailEntity existingAuditTrailEntity)
        {
            //incomingStudyEntity = _helper.CheckForSections(incomingStudyEntity, existingStudyEntity);
            incomingStudyEntity.AuditTrail.SDRUploadVersion = existingAuditTrailEntity.SDRUploadVersion + 1;
            incomingStudyEntity.AuditTrail.UsdmVersion = Constants.USDMVersions.V2;
            await _studyRepository.PostStudyItemsAsync(incomingStudyEntity);
            return _mapper.Map<StudyDefinitionsDto>(incomingStudyEntity);
        }

        #region Azure ServiceBus
        private async Task PushMessageToServiceBus(Core.DTO.Common.ServiceBusMessageDto serviceBusMessageDto)
        {
            //Execute the service bus only when Service Bus ConnectionString and Queue name are available in the configuration
            if (!String.IsNullOrWhiteSpace(Config.AzureServiceBusConnectionString) && !String.IsNullOrWhiteSpace(Config.AzureServiceBusQueueName))
            {
                ServiceBusSender sender = _serviceBusClient.CreateSender(Config.AzureServiceBusQueueName);

                string jsonMessageString = JsonConvert.SerializeObject(serviceBusMessageDto);
                ServiceBusMessage serializedMessage = new(jsonMessageString);
                await sender.SendMessageAsync(serializedMessage);
            }
        }
        #endregion
        #endregion

        #region UserGroups
        /// <summary>
        /// Check access for the study
        /// </summary>
        /// <param name="study">Study for which user access have to be checked</param>   
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="StudyDefinitionsEntity"/> if the user have access <br></br> <br></br>
        /// <see langword="null"/> If user doesn't have access to the study
        /// </returns>
        public async Task<StudyDefinitionsEntity> CheckAccessForAStudy(StudyDefinitionsEntity study, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(StudyServiceV3)}; Method : {nameof(CheckAccessForAStudy)};");

                if (user.UserRole != Constants.Roles.Org_Admin && Config.IsGroupFilterEnabled)
                {
                    var groups = await _studyRepository.GetGroupsOfUser(user).ConfigureAwait(false);

                    if (groups != null && groups.Count > 0)
                    {
                        Tuple<List<string>, List<string>> groupFilters = GroupFilters.GetGroupFilters(groups);
                        if (groupFilters.Item2.Contains(study.Study.StudyId))
                            return study;
                        else if (groupFilters.Item1.Contains(Constants.StudyType.ALL.ToLower()))
                            return study;
                        else if (groupFilters.Item1.Contains(study.Study.StudyType?.Decode?.ToLower()))
                            return study;
                        else
                            return null;
                    }
                    else
                    {
                        // Filter should not give any results
                        return null;
                    }
                }
                else
                    return study;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(StudyServiceV3)}; Method : {nameof(CheckAccessForAStudy)};");
            }
        }


        /// <summary>
        /// Check READ_WRITE Permission for a user
        /// </summary>    
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// <see langword="true"/> If the user have READ_WRITE access in any of the groups <br></br> <br></br>
        /// <see langword="false"/> If the user does not have READ_WRITE access in any of the groups
        /// </returns>
        public async Task<bool> CheckPermissionForAUser(LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(StudyServiceV3)}; Method : {nameof(CheckPermissionForAUser)};");

                if (user.UserRole != Constants.Roles.Org_Admin && Config.IsGroupFilterEnabled)
                {
                    var groups = await _studyRepository.GetGroupsOfUser(user).ConfigureAwait(false);

                    if (groups != null && groups.Count > 0)
                    {
                        if (groups.Any(x => x.Permission == Permissions.READ_WRITE.ToString()))
                            return true;
                        else
                            return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                    return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(StudyServiceV3)}; Method : {nameof(CheckPermissionForAUser)};");
            }
        }
        #endregion

        #region Delete Method
        /// <summary>
        /// GET All Elements For a Study
        /// </summary>
        /// <param name="studyId">Study ID</param>        
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="object"/> Delete Object
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<object> DeleteStudy(string studyId, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(StudyServiceV3)}; Method : {nameof(DeleteStudy)};");
                studyId = studyId.Trim();

                long count = await _studyRepository.CountAsync(studyId).ConfigureAwait(false);

                if (count == 0)
                {
                    return Constants.ErrorMessages.NotValidStudyId;
                }
                else
                {
                    _logger.LogCriitical($"Delete Request; study_uuid = {studyId} ; Requested By: {user.UserName} ; Requester Role: {user.UserRole}; Count: {count}");
                    var deleteResponse = await _studyRepository.DeleteStudyAsync(studyId).ConfigureAwait(false);
                    _logger.LogInformation($"Delete Completed: {deleteResponse.IsAcknowledged} ; Deleted Count : {deleteResponse.DeletedCount}");
                    return deleteResponse;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(StudyServiceV3)}; Method : {nameof(DeleteStudy)};");
            }
        }

        #endregion

        #region Check Access For A Study
        public async Task<bool> GetAccessForAStudy(string studyId, int sdruploadversion, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(StudyServiceV3)}; Method : {nameof(GetAccessForAStudy)};");
                studyId = studyId.Trim();

                StudyDefinitionsEntity study = study = await _studyRepository.GetStudyItemsForCheckingAccessAsync(studyId: studyId, 0).ConfigureAwait(false);

                StudyDefinitionsEntity checkStudy = await CheckAccessForAStudy(study, user);
                if (checkStudy == null)
                    return false;

                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(StudyServiceV3)}; Method : {nameof(GetAccessForAStudy)};");
            }
        }
        #endregion
    }
}
