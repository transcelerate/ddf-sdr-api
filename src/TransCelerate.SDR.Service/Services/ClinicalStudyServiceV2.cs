﻿using AutoMapper;
using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.DTO.StudyV2;
using TransCelerate.SDR.Core.DTO.Token;
using TransCelerate.SDR.Core.Entities.StudyV2;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.Core.Utilities.Helpers.HelpersV2;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.DataAccess.Repositories;
using TransCelerate.SDR.Services.Interfaces;

namespace TransCelerate.SDR.Services.Services
{
    public class ClinicalStudyServiceV2 : IClinicalStudyServiceV2
    {
        #region Variables
        private readonly IClinicalStudyRepositoryV2 _clinicalStudyRepository;
        private readonly IMapper _mapper;
        private readonly ILogHelper _logger;
        private readonly IHelperV2 _helper;
        private readonly ServiceBusClient _serviceBusClient;
        private readonly IChangeAuditRepository _changeAuditRepositoy;
        #endregion

        #region Constructor
        public ClinicalStudyServiceV2(IClinicalStudyRepositoryV2 clinicalStudyRepository, IMapper mapper, ILogHelper logger, IHelperV2 helper, ServiceBusClient serviceBusClient, IChangeAuditRepository changeAuditRepository)
        {
            _changeAuditRepositoy = changeAuditRepository;
            _clinicalStudyRepository = clinicalStudyRepository;
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
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServiceV2)}; Method : {nameof(GetStudy)};");
                studyId = studyId.Trim();

                StudyEntity study = await _clinicalStudyRepository.GetStudyItemsAsync(studyId: studyId, sdruploadversion: sdruploadversion).ConfigureAwait(false);

                if (study == null)
                {
                    return null;
                }
                else
                {
                    StudyEntity checkStudy = await CheckAccessForAStudy(study, user);
                    if (checkStudy == null)
                        return Constants.ErrorMessages.Forbidden;
                    var studyDTO = _mapper.Map<StudyDto>(study);  //Mapping Entity to Dto
                    studyDTO.Links = LinksHelper.GetLinksForUi(study.ClinicalStudy.StudyId, study.ClinicalStudy.StudyDesigns?.Select(x => x.Id).ToList(), study.AuditTrail.UsdmVersion, study.AuditTrail.SDRUploadVersion);
                    return studyDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServiceV2)}; Method : {nameof(GetStudy)};");
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
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServiceV2)}; Method : {nameof(GetPartialStudyElements)};");
                studyId = studyId.Trim();

                StudyEntity study = await _clinicalStudyRepository.GetPartialStudyItemsAsync(studyId, sdruploadversion, listofelements).ConfigureAwait(false);

                if (study == null)
                {
                    return null;
                }
                else
                {
                    StudyEntity checkStudy = await CheckAccessForAStudy(study, user);
                    if (checkStudy == null)
                        return Constants.ErrorMessages.Forbidden;
                    var studyDTO = _mapper.Map<StudyDto>(study);  //Mapping Entity to Dto 
                    return _helper.RemoveStudyElements(listofelements, studyDTO);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServiceV2)}; Method : {nameof(GetPartialStudyElements)};");
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
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServiceV2)}; Method : {nameof(GetStudy)};");
                if (!String.IsNullOrWhiteSpace(studyDesignId) || (listofelements is not null && listofelements.Any()))
                {
                    return await GetPartialStudyDesigns(studyId, studyDesignId, sdruploadversion, user, listofelements);
                }
                else
                {
                    studyId = studyId.Trim();

                    StudyEntity study = await _clinicalStudyRepository.GetPartialStudyDesignItemsAsync(studyId: studyId, sdruploadversion: sdruploadversion).ConfigureAwait(false);

                    if (study == null)
                    {
                        return null;
                    }
                    else
                    {
                        StudyEntity checkStudy = await CheckAccessForAStudy(study, user);
                        if (checkStudy == null)
                            return Constants.ErrorMessages.Forbidden;

                        var studyDesigns = _mapper.Map<List<StudyDesignDto>>(checkStudy?.ClinicalStudy?.StudyDesigns);  //Mapping Entity to Dto

                        if (studyDesigns is not null && studyDesigns.Any())
                            return new StudyDesignsResponseDto
                            {
                                StudyDesigns = _helper.RemoveStudyDesignElements(Constants.StudyDesignElementsV2, studyDesigns, studyId),
                                Links = LinksHelper.GetLinks(study.ClinicalStudy.StudyId, study.ClinicalStudy.StudyDesigns?.Select(x => x.Id), study.AuditTrail.UsdmVersion, study.AuditTrail.SDRUploadVersion)
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
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServiceV2)}; Method : {nameof(GetStudy)};");
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
        public async Task<object> GetSOAV2(string studyId, string studyDesignId, string scheduleTimelineId, int sdruploadversion, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServiceV2)}; Method : {nameof(GetSOAV2)};");
                studyId = studyId.Trim();

                StudyEntity study = await _clinicalStudyRepository.GetStudyItemsAsync(studyId: studyId, sdruploadversion: sdruploadversion).ConfigureAwait(false);
                if (study == null)
                {
                    return null;
                }
                else
                {
                    StudyEntity checkStudy = await CheckAccessForAStudy(study, user);
                    if (checkStudy == null)
                        return Constants.ErrorMessages.Forbidden;

                    var soa = SoAV2(study.ClinicalStudy.StudyDesigns);
                    soa.StudyId = study.ClinicalStudy.StudyId;
                    soa.StudyTitle = study.ClinicalStudy.StudyTitle;
                    if (!String.IsNullOrWhiteSpace(studyDesignId))
                    {
                        if (study.ClinicalStudy.StudyDesigns is null || !soa.StudyDesigns.Any(x => x.StudyDesignId == studyDesignId))
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
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServiceV2)}; Method : {nameof(GetSOAV2)};");
            }
        }

        public SoADto SoAV2(List<StudyDesignEntity> studyDesigns)
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
                            var scheduleActivityInstances = scheduleTimeline.ScheduleTimelineInstances?.Select(x => (x as ScheduledActivityInstanceEntity))
                                                                         .Where(x => x != null).ToList();
                            
                            if (scheduleActivityInstances != null && scheduleActivityInstances.Any())
                            {
                                var activitiesMappedToTimeLine = activities is not null && activities.Any() ? activities.Where(act => scheduleActivityInstances.Where(x => x.ActivityIds is not null && x.ActivityIds.Any()).SelectMany(instance => instance.ActivityIds).Contains(act.Id)).ToList() : new List<ActivityEntity>();
                                var encountersMappedToTimeLine = scheduleActivityInstances.Where(x => !String.IsNullOrWhiteSpace(x.ScheduledInstanceEncounterId)).Select(x => x.ScheduledInstanceEncounterId).ToList();
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
                                            DefinedProcedures = new List<ProcedureSoA>()
                                        }).ToList()
                                    };
                                    // SoA for instances where encounter is mapped
                                    encounters?.Where(x => scheduleActivityInstances.Select(y => y.ScheduledInstanceEncounterId).Contains(x.Id)).ToList().ForEach(encounter =>
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
                                            Timings = GetTimings(scheduleActivityInstances.Where(instance => instance.ScheduledInstanceEncounterId == encounter.Id).ToList())
                                        };

                                        studyTimelineSoA.ScheduleTimelineSoA.SoA.Add(soA);
                                    });
                                    // SoA for instances where encounter is not mapped
                                    if (scheduleActivityInstances.Where(x => String.IsNullOrWhiteSpace(x.ScheduledInstanceEncounterId)).Any())
                                    {
                                        SoA soA = new()
                                        {
                                            EncounterId = string.Empty,
                                            EncounterName = string.Empty,
                                            EncounterScheduledAtTimingValue = string.Empty,
                                            Timings = GetTimings(scheduleActivityInstances.Where(x => String.IsNullOrWhiteSpace(x.ScheduledInstanceEncounterId)).ToList())
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
        public List<TimingSoA> GetTimings(List<ScheduledActivityInstanceEntity> scheduledActivityInstances)
        {
            if (scheduledActivityInstances is not null && scheduledActivityInstances.Any())
            {
                var instances = scheduledActivityInstances.Where(x => x.ScheduledInstanceTimings is not null && x.ScheduledInstanceTimings.Any()).Select(x => new
                {
                    x.ScheduleSequenceNumber,
                    Timings = _mapper.Map<List<TimingSoA>>(x.ScheduledInstanceTimings),
                    x.ActivityIds
                }).OrderBy(x => x.ScheduleSequenceNumber).ToList();

                instances.AddRange(scheduledActivityInstances.Where(x => x.ScheduledInstanceTimings is null || !x.ScheduledInstanceTimings.Any()).Select(x => new
                {
                    x.ScheduleSequenceNumber,
                    Timings = _mapper.Map<List<TimingSoA>>(x.ScheduledInstanceTimings),
                    x.ActivityIds
                }).OrderBy(x => x.ScheduleSequenceNumber).ToList());
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
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServiceV2)}; Method : {nameof(GetPartialStudyDesigns)};");
                studyId = studyId.Trim();

                var study = await _clinicalStudyRepository.GetPartialStudyDesignItemsAsync(studyId, sdruploadversion).ConfigureAwait(false);

                if (study == null)
                {
                    return null;
                }
                else
                {
                    StudyEntity checkStudy = await CheckAccessForAStudy(study, user);
                    if (checkStudy == null)
                        return Constants.ErrorMessages.Forbidden;
                    if (!String.IsNullOrWhiteSpace(studyDesignId))
                    {
                        if (study.ClinicalStudy.StudyDesigns is not null && study.ClinicalStudy.StudyDesigns.Any(x => x.Id == studyDesignId))
                        {
                            var studyDesigns = _mapper.Map<List<StudyDesignDto>>(checkStudy.ClinicalStudy.StudyDesigns.Where(x => x.Id == studyDesignId).ToList());
                            JObject jObject = new()
                            {
                                { string.Concat(nameof(ClinicalStudyDto.StudyDesigns)[..1].ToLower(), nameof(ClinicalStudyDto.StudyDesigns).AsSpan(1)), JArray.Parse(JsonConvert.SerializeObject(_helper.RemoveStudyDesignElements(listofelements, studyDesigns, studyId))) }
                            };
                            if (listofelements == null)
                                jObject.Add("links", JObject.Parse(JsonConvert.SerializeObject(LinksHelper.GetLinks(study.ClinicalStudy.StudyId, study.ClinicalStudy.StudyDesigns?.Select(x => x.Id), study.AuditTrail.UsdmVersion, study.AuditTrail.SDRUploadVersion), _helper.GetSerializerSettingsForCamelCasing())));
                            return jObject;
                        }
                        return Constants.ErrorMessages.StudyDesignNotFound;
                    }
                    else
                    {
                        var studyDesigns = _mapper.Map<List<StudyDesignDto>>(checkStudy.ClinicalStudy.StudyDesigns);
                        JObject jObject = new()
                        {
                            { string.Concat(nameof(ClinicalStudyDto.StudyDesigns)[..1].ToLower(), nameof(ClinicalStudyDto.StudyDesigns).AsSpan(1)), JArray.Parse(JsonConvert.SerializeObject(_helper.RemoveStudyDesignElements(listofelements, studyDesigns, studyId))) }
                        };
                        if (listofelements == null)
                            jObject.Add("links", JObject.Parse(JsonConvert.SerializeObject(LinksHelper.GetLinks(study.ClinicalStudy.StudyId, study.ClinicalStudy.StudyDesigns?.Select(x => x.Id), study.AuditTrail.UsdmVersion, study.AuditTrail.SDRUploadVersion), _helper.GetSerializerSettingsForCamelCasing())));
                        return study.ClinicalStudy.StudyDesigns is not null && study.ClinicalStudy.StudyDesigns.Any() ?
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
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServiceV2)}; Method : {nameof(GetPartialStudyDesigns)};");
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
        public async Task<object> GeteCPTV2(string studyId, int sdruploadversion, string studyDesignId, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(CommonServices)}; Method : {nameof(GeteCPTV2)};");
                studyId = studyId.Trim();

                var study = await _clinicalStudyRepository.GetStudyItemsAsync(studyId: studyId, sdruploadversion: sdruploadversion).ConfigureAwait(false);

                if (study == null)
                {
                    return null;
                }
                else
                {
                    StudyEntity checkStudy = await CheckAccessForAStudy(study, user);
                    if (checkStudy == null)
                        return Constants.ErrorMessages.Forbidden;

                    var studyDTO = _mapper.Map<StudyDto>(study);

                    if (studyDTO.ClinicalStudy.StudyDesigns == null || !studyDTO.ClinicalStudy.StudyDesigns.Any())
                        return Constants.ErrorMessages.StudyDesignNotFoundCPT;

                    if (studyDesignId != null)
                    {
                        if (studyDTO.ClinicalStudy.StudyDesigns.Any(x => x.Id == studyDesignId))
                            studyDTO.ClinicalStudy.StudyDesigns.RemoveAll(x => x.Id != studyDesignId);
                        else
                            return Constants.ErrorMessages.StudyDesignIdNotFoundCPT;
                    }

                    var eCPT = GetCPTDataV2(studyDTO.ClinicalStudy, study.AuditTrail);

                    return eCPT;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(CommonServices)}; Method : {nameof(GeteCPTV2)};");
            }
        }

        public Core.DTO.eCPT.ECPTDto GetCPTDataV2(ClinicalStudyDto clinicalStudyDto, AuditTrailEntity auditTrail)
        {
            var links = LinksHelper.GetLinks(clinicalStudyDto.StudyId, clinicalStudyDto.StudyDesigns.Select(c => c.Id).ToList(), auditTrail.UsdmVersion, auditTrail.SDRUploadVersion);
            Core.DTO.eCPT.StudyDetailsDto studyDetailsDto = new()
            {
                StudyId = clinicalStudyDto.StudyId,
                StudyTitle = clinicalStudyDto.StudyTitle,

                UsdmVersion = auditTrail.UsdmVersion,
                SDRUploadVersion = auditTrail.SDRUploadVersion,
                Links = new
                {
                    links.StudyDefinitions,
                    links.RevisionHistory
                },
            };
            List<Core.DTO.eCPT.StudyDesignDto> studyeCPTDtos = new();
            if (clinicalStudyDto.StudyDesigns != null && clinicalStudyDto.StudyDesigns.Any())
            {
                clinicalStudyDto.StudyDesigns.ForEach(design =>
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
                                Acronym = clinicalStudyDto.StudyAcronym,
                                AmendmentNumber = clinicalStudyDto.StudyProtocolVersions != null && clinicalStudyDto.StudyProtocolVersions.Any() ? ECPTHelper.GetOrderedStudyProtocolsV2(clinicalStudyDto.StudyProtocolVersions).ProtocolAmendment : null,
                                ApprovalDate = clinicalStudyDto.StudyProtocolVersions != null && clinicalStudyDto.StudyProtocolVersions.Any() ? ECPTHelper.GetOrderedStudyProtocolsV2(clinicalStudyDto.StudyProtocolVersions).ProtocolEffectiveDate : null,
                                ConditionDisease = design.StudyIndications != null && design.StudyIndications.Any() ?
                                                   design.StudyIndications.Count == 1 ?
                                                   design.StudyIndications.FirstOrDefault().IndicationDescription
                                                   : $"{String.Join(',', design.StudyIndications.Select(x => x.IndicationDescription).ToArray(), 0, design.StudyIndications.Count - 1)} and {design.StudyIndications.Select(x => x.IndicationDescription).LastOrDefault()}"
                                                   : null,
                                RegulatoryAgencyId = clinicalStudyDto.StudyIdentifiers.Where(x => x.StudyIdentifierScope.OrganisationType.Decode.Equals(Constants.IdType.REGULATORY_AGENCY, StringComparison.OrdinalIgnoreCase)).Select(x => x.StudyIdentifierScope.OrganisationIdentifierScheme).FirstOrDefault(),
                                RegulatoryAgencyNumber = clinicalStudyDto.StudyIdentifiers.Where(x => x.StudyIdentifierScope.OrganisationType.Decode.Equals(Constants.IdType.REGULATORY_AGENCY, StringComparison.OrdinalIgnoreCase)).Select(x => x.StudyIdentifier).FirstOrDefault(),
                                SponsorName = clinicalStudyDto.StudyIdentifiers.Where(x => x.StudyIdentifierScope.OrganisationType.Decode.Equals(Constants.IdType.SPONSOR_ID_V1, StringComparison.OrdinalIgnoreCase)).Select(x => x.StudyIdentifierScope.OrganisationName).FirstOrDefault(),
                                SponsorLegalAddress = clinicalStudyDto.StudyIdentifiers.Where(x => x.StudyIdentifierScope.OrganisationType.Decode.Equals(Constants.IdType.SPONSOR_ID_V1, StringComparison.OrdinalIgnoreCase)).Select(x => x.StudyIdentifierScope.OrganizationLegalAddress).FirstOrDefault() == null ? null
                                                                : clinicalStudyDto.StudyIdentifiers.Where(x => x.StudyIdentifierScope.OrganisationType.Decode.Equals(Constants.IdType.SPONSOR_ID_V1, StringComparison.OrdinalIgnoreCase)).Select(x => x.StudyIdentifierScope.OrganizationLegalAddress).Select(x => $"{x.Text},{x.Line},{x.City},{x.District},{x.State},{x.PostalCode},{x.Country?.Decode}").FirstOrDefault(),
                                StudyPhase = ECPTHelper.GetCptMappingValue(Constants.SdrCptMasterDataEntities.StudyPhase, clinicalStudyDto.StudyPhase?.StandardCode?.Code) ?? clinicalStudyDto.StudyPhase?.StandardCode?.Decode,
                                Protocol = new Core.DTO.eCPT.ProtocolDto
                                {
                                    ProtocolID = clinicalStudyDto.StudyIdentifiers.Where(x => x.StudyIdentifierScope.OrganisationType.Decode.Equals(Constants.IdType.SPONSOR_ID_V1, StringComparison.OrdinalIgnoreCase)).Select(x => x.StudyIdentifier).FirstOrDefault(),
                                    ProtocolShortTitle = clinicalStudyDto.StudyProtocolVersions != null && clinicalStudyDto.StudyProtocolVersions.Any() ? ECPTHelper.GetOrderedStudyProtocolsV2(clinicalStudyDto.StudyProtocolVersions).BriefTitle : null,
                                    ProtocolTitle = clinicalStudyDto.StudyTitle
                                }
                            },
                            ProtocolSummary = new Core.DTO.eCPT.ProtocolSummaryDto
                            {
                                Synopsis = new Core.DTO.eCPT.SynopsisDto
                                {
                                    NumberofParticipants = design.StudyPopulations?.Sum(x => int.Parse(Convert.ToString(x.PlannedNumberOfParticipants))).ToString(),
                                    PrimaryPurpose = design.TrialIntentType != null && design.TrialIntentType.Any() ?
                                                   design.TrialIntentType.Count == 1 ? ECPTHelper.GetCptMappingValue(Constants.SdrCptMasterDataEntities.TrialIntentType, design.TrialIntentType.FirstOrDefault().Code) ?? design.TrialIntentType.FirstOrDefault().Decode
                                                   : $"{String.Join(',', design.TrialIntentType.Select(x => ECPTHelper.GetCptMappingValue(Constants.SdrCptMasterDataEntities.TrialIntentType, x.Code) ?? x.Decode).ToArray(), 0, design.TrialIntentType.Count - 1)}" +
                                                   $" and {design.TrialIntentType.Select(x => ECPTHelper.GetCptMappingValue(Constants.SdrCptMasterDataEntities.TrialIntentType, x.Code) ?? x.Decode).LastOrDefault()}"
                                                   : null,
                                    EnrollmentTarget = design.StudyPopulations != null && design.StudyPopulations.Any() ?
                                                      design.StudyPopulations.Count == 1 ? design.StudyPopulations.FirstOrDefault().PopulationDescription
                                                      : $"{String.Join(',', design.StudyPopulations.Select(x => x.PopulationDescription).ToArray(), 0, design.StudyPopulations.Count - 1)} and {design.StudyPopulations.Select(x => x.PopulationDescription).LastOrDefault()}"
                                                      : null,
                                    InterventionModel = ECPTHelper.GetCptMappingValue(Constants.SdrCptMasterDataEntities.InterventionModel, design?.InterventionModel?.Code) ?? design?.InterventionModel?.Decode,
                                    NumberofArms = design.StudyCells != null && design.StudyCells.Any() ?
                                                    design.StudyCells.Where(x => x.StudyArm != null).Any() ?
                                                    design.StudyCells.Where(x => x.StudyArm != null).Select(x => x.StudyArm.Id).Distinct().Count().ToString() : 0.ToString() : 0.ToString()
                                }
                            },
                            PageHeader = new Core.DTO.eCPT.PageHeaderDto
                            {
                                VersionNumber = clinicalStudyDto.StudyProtocolVersions != null && clinicalStudyDto.StudyProtocolVersions.Any() ? ECPTHelper.GetOrderedStudyProtocolsV2(clinicalStudyDto.StudyProtocolVersions).ProtocolVersion : null,
                            },
                            StudyPopulation = new Core.DTO.eCPT.StudyPopulationDto
                            {
                                InclusionCriteria = new Core.DTO.eCPT.InclusionCriteriaDto
                                {

                                    PlannedMaximumAgeofSubjects = design.StudyPopulations != null && design.StudyPopulations.Any() ?
                                                                           ECPTHelper.CheckForMaxMin(design.StudyPopulations.Select(x => x.PlannedMaximumAgeOfParticipants).ToList(), true) : null,

                                    PlannedMinimumAgeofSubjects = design.StudyPopulations != null && design.StudyPopulations.Any() ?
                                                                           ECPTHelper.CheckForMaxMin(design.StudyPopulations.Select(x => x.PlannedMinimumAgeOfParticipants).ToList(), false) : null,

                                    SexofParticipants = ECPTHelper.GetPlannedSexOfParticipantsV2(design.StudyPopulations)
                                }
                            },
                            Introduction = new Core.DTO.eCPT.IntroductionDto
                            {
                                StudyRationale = clinicalStudyDto.StudyRationale
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
                                                   : $"{String.Join(',', design.StudyEstimands.Select(x => x.AnalysisPopulation.PopulationDescription).ToArray(), 0, design.StudyEstimands.Count - 1)} and {design.StudyEstimands.Select(x => x.AnalysisPopulation.PopulationDescription).LastOrDefault()}"
                                                   : null,
                            },
                            ObjectivesEndpointsAndEstimands = ECPTHelper.GetObjectivesEndpointsAndEstimandsDtoV2(design.StudyObjectives, _mapper),
                            StudyInterventionsAndConcomitantTherapy = new Core.DTO.eCPT.StudyInterventionsAndConcomitantTherapyDto
                            {
                                StudyInterventionsAdministered = design.StudyInvestigationalInterventions != null && design.StudyInvestigationalInterventions.Any() ?
                                           _mapper.Map<List<Core.DTO.eCPT.StudyInterventionsAdministeredDto>>(design.StudyInvestigationalInterventions)
                                           : null,
                                StudyArms = design.StudyCells != null && design.StudyCells.Any() ?
                                           design.StudyCells.Where(x => x.StudyArm != null).Any() ?
                                           _mapper.Map<List<Core.DTO.eCPT.StudyArmDto>>(design.StudyCells.Where(x => x.StudyArm != null).Select(x => x.StudyArm).ToList())
                                           : null : null
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
        public async Task<object> PostAllElements(StudyDto studyDTO, LoggedInUser user, string method)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServiceV2)}; Method : {nameof(PostAllElements)};");
                if (!await CheckPermissionForAUser(user))
                    return Constants.ErrorMessages.PostRestricted;
                StudyEntity incomingStudyEntity = new()
                {
                    ClinicalStudy = _mapper.Map<ClinicalStudyEntity>(studyDTO.ClinicalStudy),
                    AuditTrail = _helper.GetAuditTrail(user?.UserName),
                    Id = MongoDB.Bson.ObjectId.GenerateNewId()
                };

                if (method == HttpMethod.Post.Method) //POST Endpoint to create new study
                {
                    studyDTO = await CreateNewStudy(incomingStudyEntity).ConfigureAwait(false);
                }
                else //PUT Endpoint Create New Version for the study
                {
                    AuditTrailEntity existingAuditTrail = await _clinicalStudyRepository.GetUsdmVersionAsync(incomingStudyEntity.ClinicalStudy.StudyId, 0);

                    if (existingAuditTrail is null) // If PUT Endpoint and study_uuid is not valid, return not valid study
                    {
                        return Constants.ErrorMessages.NotValidStudyId;
                    }

                    if (existingAuditTrail.UsdmVersion == Constants.USDMVersions.V1_9) // If previus USDM version is same as incoming
                    {
                        StudyEntity existingStudyEntity = await _clinicalStudyRepository.GetStudyItemsAsync(incomingStudyEntity.ClinicalStudy.StudyId, 0);

                        if (_helper.IsSameStudy(incomingStudyEntity, existingStudyEntity))
                        {
                            studyDTO = await UpdateExistingStudy(incomingStudyEntity, existingStudyEntity).ConfigureAwait(false);
                        }
                        else
                        {
                            studyDTO = await CreateNewVersionForAStudy(incomingStudyEntity, existingStudyEntity.AuditTrail).ConfigureAwait(false);
                            await PushMessageToServiceBus(new Core.DTO.Common.ServiceBusMessageDto { Study_uuid = incomingStudyEntity.ClinicalStudy.StudyId, CurrentVersion = incomingStudyEntity.AuditTrail.SDRUploadVersion });
                        }
                    }
                    // Uncomment below lines based on Story #810
                    //else if (existingAuditTrail.UsdmVersion == Constants.USDMVersions.MVP || existingAuditTrail.UsdmVersion == Constants.USDMVersions.V1)// If previus USDM version is different from incoming
                    //{
                    //    studyDTO = await CreateNewVersionForAStudy(incomingStudyEntity, existingAuditTrail).ConfigureAwait(false);
                    //    await PushMessageToServiceBus(new Core.DTO.Common.ServiceBusMessageDto { Study_uuid = incomingStudyEntity.ClinicalStudy.StudyId, CurrentVersion = incomingStudyEntity.AuditTrail.SDRUploadVersion });
                    //}
                    //else
                    //{
                    //    return Constants.ErrorMessages.DowngradeError;
                    //}
                    else
                    {
                        studyDTO = await CreateNewVersionForAStudy(incomingStudyEntity, existingAuditTrail).ConfigureAwait(false);
                        await PushMessageToServiceBus(new Core.DTO.Common.ServiceBusMessageDto { Study_uuid = incomingStudyEntity.ClinicalStudy.StudyId, CurrentVersion = incomingStudyEntity.AuditTrail.SDRUploadVersion });
                    }
                }
                studyDTO.Links = LinksHelper.GetLinksForUi(studyDTO.ClinicalStudy.StudyId, studyDTO.ClinicalStudy.StudyDesigns?.Select(x => x.Id).ToList(), studyDTO.AuditTrail.UsdmVersion, studyDTO.AuditTrail.SDRUploadVersion);
                return studyDTO;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServiceV2)}; Method : {nameof(PostAllElements)};");
            }
        }

        public async Task<StudyDto> CreateNewStudy(StudyEntity studyEntity)
        {
            //studyEntity = _helper.GeneratedSectionId(studyEntity);
            studyEntity.ClinicalStudy.StudyId = IdGenerator.GenerateId();
            studyEntity.AuditTrail.SDRUploadVersion = 1;
            await _clinicalStudyRepository.PostStudyItemsAsync(studyEntity);
            await _changeAuditRepositoy.InsertChangeAudit(studyEntity.ClinicalStudy.StudyId, studyEntity.AuditTrail.SDRUploadVersion, studyEntity.AuditTrail.EntryDateTime);
            return _mapper.Map<StudyDto>(studyEntity);
        }

        public async Task<StudyDto> UpdateExistingStudy(StudyEntity incomingStudyEntity, StudyEntity existingStudyEntity)
        {
            existingStudyEntity.AuditTrail.EntryDateTime = incomingStudyEntity.AuditTrail.EntryDateTime;
            incomingStudyEntity.AuditTrail.SDRUploadVersion = existingStudyEntity.AuditTrail.SDRUploadVersion;
            incomingStudyEntity.AuditTrail.UsdmVersion = Constants.USDMVersions.V1_9;
            await _clinicalStudyRepository.UpdateStudyItemsAsync(incomingStudyEntity);
            return _mapper.Map<StudyDto>(incomingStudyEntity);
        }

        public async Task<StudyDto> CreateNewVersionForAStudy(StudyEntity incomingStudyEntity, AuditTrailEntity existingAuditTrailEntity)
        {
            //incomingStudyEntity = _helper.CheckForSections(incomingStudyEntity, existingStudyEntity);
            incomingStudyEntity.AuditTrail.SDRUploadVersion = existingAuditTrailEntity.SDRUploadVersion + 1;
            incomingStudyEntity.AuditTrail.UsdmVersion = Constants.USDMVersions.V1_9;
            await _clinicalStudyRepository.PostStudyItemsAsync(incomingStudyEntity);
            return _mapper.Map<StudyDto>(incomingStudyEntity);
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
        /// A <see cref="StudyEntity"/> if the user have access <br></br> <br></br>
        /// <see langword="null"/> If user doesn't have access to the study
        /// </returns>
        public async Task<StudyEntity> CheckAccessForAStudy(StudyEntity study, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServiceV2)}; Method : {nameof(CheckAccessForAStudy)};");

                if (user.UserRole != Constants.Roles.Org_Admin && Config.IsGroupFilterEnabled)
                {
                    var groups = await _clinicalStudyRepository.GetGroupsOfUser(user).ConfigureAwait(false);

                    if (groups != null && groups.Count > 0)
                    {
                        Tuple<List<string>, List<string>> groupFilters = GroupFilters.GetGroupFilters(groups);
                        if (groupFilters.Item2.Contains(study.ClinicalStudy.StudyId))
                            return study;
                        else if (groupFilters.Item1.Contains(Constants.StudyType.ALL.ToLower()))
                            return study;
                        else if (groupFilters.Item1.Contains(study.ClinicalStudy.StudyType?.Decode?.ToLower()))
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
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServiceV2)}; Method : {nameof(CheckAccessForAStudy)};");
            }
        }

        /// <summary>
        /// Check access for the Study Aduit
        /// </summary>
        /// <param name="studyId">StudyId of the study</param>   
        /// <param name="studies">Study List for which user access have to be checked</param>   
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="List{AuditTrailResponseEntity}"/> if the user have access <br></br> <br></br>
        /// <see langword="null"/> If user doesn't have access to the study
        /// </returns>
        public async Task<List<AuditTrailResponseEntity>> CheckAccessForStudyAudit(string studyId, List<AuditTrailResponseEntity> studies, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServiceV2)}; Method : {nameof(CheckAccessForStudyAudit)};");

                if (user.UserRole != Constants.Roles.Org_Admin && Config.IsGroupFilterEnabled)
                {
                    var groups = await _clinicalStudyRepository.GetGroupsOfUser(user).ConfigureAwait(false);

                    if (groups != null && groups.Count > 0)
                    {
                        Tuple<List<string>, List<string>> groupFilters = GroupFilters.GetGroupFilters(groups);
                        if (groupFilters.Item2.Contains(studyId))
                            return studies;
                        else if (groupFilters.Item1.Contains(Constants.StudyType.ALL.ToLower()))
                            return studies;
                        else
                        {
                            studies.RemoveAll(x => !groupFilters.Item1.Contains(x.StudyType?.Decode?.ToLower()));
                            return studies.Count > 0 ? studies : null;
                        }
                    }
                    else
                    {
                        // Filter should not give any results
                        return null;
                    }
                }
                else
                    return studies;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServiceV2)}; Method : {nameof(CheckAccessForStudyAudit)};");
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
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServiceV2)}; Method : {nameof(CheckPermissionForAUser)};");

                if (user.UserRole != Constants.Roles.Org_Admin && Config.IsGroupFilterEnabled)
                {
                    var groups = await _clinicalStudyRepository.GetGroupsOfUser(user).ConfigureAwait(false);

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
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServiceV2)}; Method : {nameof(CheckPermissionForAUser)};");
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
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServiceV2)}; Method : {nameof(DeleteStudy)};");
                studyId = studyId.Trim();

                long count = await _clinicalStudyRepository.CountAsync(studyId).ConfigureAwait(false);

                if (count == 0)
                {
                    return Constants.ErrorMessages.NotValidStudyId;
                }
                else
                {
                    _logger.LogCriitical($"Delete Request; study_uuid = {studyId} ; Requested By: {user.UserName} ; Requester Role: {user.UserRole}; Count: {count}");
                    var deleteResponse = await _clinicalStudyRepository.DeleteStudyAsync(studyId).ConfigureAwait(false);
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
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServiceV2)}; Method : {nameof(DeleteStudy)};");
            }
        }

        #endregion

        #region Check Access For A Study
        public async Task<bool> GetAccessForAStudy(string studyId, int sdruploadversion, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServiceV2)}; Method : {nameof(GetAccessForAStudy)};");
                studyId = studyId.Trim();

                StudyEntity study = study = await _clinicalStudyRepository.GetStudyItemsForCheckingAccessAsync(studyId: studyId, 0).ConfigureAwait(false);

                StudyEntity checkStudy = await CheckAccessForAStudy(study, user);
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
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServiceV2)}; Method : {nameof(GetAccessForAStudy)};");
            }
        }
        #endregion
    }
}
