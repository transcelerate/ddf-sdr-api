using AutoMapper;
using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
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
                            return _helper.RemoveStudyDesignElements(Constants.StudyDesignElements, studyDesigns, studyId);

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

        /// <summary>
        /// GET Study Designs of a Study
        /// </summary>
        /// <param name="studyId">Study ID</param>
        /// <param name="sdruploadversion">Version of study</param>
        /// <param name="studyWorkflowId">workdflowId</param>
        /// <param name="studyDesignId">study design Id</param>
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="object"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<object> GetSOA(string studyId, string studyDesignId, string studyWorkflowId, int sdruploadversion, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServiceV2)}; Method : {nameof(GetSOA)};");
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

                    var soa = SoA(study.ClinicalStudy.StudyDesigns);
                    soa.StudyId = study.ClinicalStudy.StudyId;
                    soa.StudyTitle = study.ClinicalStudy.StudyTitle;
                    if (!String.IsNullOrWhiteSpace(studyDesignId))
                    {                        
                        if (study.ClinicalStudy.StudyDesigns is null || !soa.StudyDesigns.Any(x => x.StudyDesignId == studyDesignId))
                            return Constants.ErrorMessages.StudyDesignNotFound;

                        if(!String.IsNullOrWhiteSpace(studyWorkflowId))
                        {
                            soa.StudyDesigns.RemoveAll(x => x.StudyDesignId != studyDesignId);
                            if(soa.StudyDesigns.First().StudyWorkflows is null || !soa.StudyDesigns.First().StudyWorkflows.Any(x => x.WorkFlowId == studyWorkflowId))
                                return Constants.ErrorMessages.WorkFlowNotFound;
                            soa.StudyDesigns.First().StudyWorkflows.RemoveAll(y => y.WorkFlowId != studyWorkflowId);
                            return soa;
                        }
                        soa.StudyDesigns.RemoveAll(x=> x.StudyDesignId != studyDesignId);
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
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServiceV2)}; Method : {nameof(GetSOA)};");
            }
        }

        public SoADto SoA(List<StudyDesignEntity> studyDesigns)
        {
            SoADto soADto = new SoADto();
            soADto.StudyDesigns = new List<StudyDesigns>();
            if (studyDesigns is not null && studyDesigns.Any())
            {
                studyDesigns.ForEach(design =>
                {
                    StudyDesigns studyDesignSoA = new StudyDesigns();
                    studyDesignSoA.StudyDesignId = design.Id;
                    studyDesignSoA.StudyDesignName = design.StudyDesignName;
                    studyDesignSoA.StudyDesignDescription = design.StudyDesignDescription;
                    studyDesignSoA.StudyWorkflows = new List<StudyWorkflows>();
                    List<EncounterEntity> encounters = GetOrderedEncounters(design.Encounters);
                    List<ActivityEntity> activities = GetOrderedActivities(design.Activities);
                    if(design.StudyWorkflows != null && design.StudyWorkflows.Any())
                    {                        
                        design.StudyWorkflows.ForEach(workFlow =>
                        {
                            StudyWorkflows studyWorkflowsA = new StudyWorkflows();
                            studyWorkflowsA.WorkFlowId = workFlow.Id;
                            studyWorkflowsA.WorkflowDescription = workFlow.WorkflowDescription;
                            if (activities != null && activities.Any() && encounters != null && encounters.Any())
                            {                                
                                List<WorkFlowItemEntity> workflowItems = workFlow.WorkflowItems;
                                if (workflowItems != null && workflowItems.Any())
                                {
                                    studyWorkflowsA.WorkFlowSoA = new WorkFlowSoA();
                                    studyWorkflowsA.WorkFlowSoA.SoA = new List<SoA>();
                                    studyWorkflowsA.WorkFlowSoA.OrderOfActivities = activities.Select(x => x.ActivityName).ToList();
                                    encounters.ForEach(encounter =>
                                    {
                                        SoA soA = new SoA();
                                        soA.EncounterName = encounter.EncounterName;
                                        soA.Activities = workflowItems.Where(x => x.WorkflowItemEncounterId == encounter.Id)
                                                                      .Where(x => activities.Where(y => y.Id == x.WorkflowItemActivityId).Any())
                                                                      .Select(x => activities.Where(y => y.Id == x.WorkflowItemActivityId).First().ActivityName)
                                                                      .ToList();
                                        studyWorkflowsA.WorkFlowSoA.SoA.Add(soA);
                                    });
                                }                                
                            }

                            studyDesignSoA.StudyWorkflows.Add(studyWorkflowsA);
                        });
                    }
                    soADto.StudyDesigns.Add(studyDesignSoA);
                });
            }

            return soADto;
        }
        public List<EncounterEntity> GetOrderedEncounters(List<EncounterEntity> encounters)
        {
            if (encounters != null && encounters.Any())
            {
                if (encounters.Count(x => String.IsNullOrWhiteSpace(x.PreviousEncounterId)) == 1 && encounters.Count(x => String.IsNullOrWhiteSpace(x.NextEncounterId)) == 1)
                {
                    List<EncounterEntity> encountersLinkedList = new List<EncounterEntity>();
                    encountersLinkedList.Add(encounters.Where(x => String.IsNullOrWhiteSpace(x.PreviousEncounterId)).FirstOrDefault());
                    for (int i = 1; i < encounters.Count; i++)
                    {
                        if (encounters.Where(x => x.PreviousEncounterId == encountersLinkedList[i-1].Id).Any() && encounters.Where(x => x.PreviousEncounterId == encountersLinkedList[i-1].Id).Count() == 1)                            
                            encountersLinkedList.Add(encounters.Where(x => x.PreviousEncounterId == encountersLinkedList[i-1].Id).First());
                        else
                            break;
                    }
                    return encountersLinkedList.Count == encounters.Count ? encountersLinkedList : encounters;
                }
            }
            return encounters;
        }

        public List<ActivityEntity> GetOrderedActivities(List<ActivityEntity> activities)
        {
            if (activities != null && activities.Any())
            {
                if (activities.Count(x => String.IsNullOrWhiteSpace(x.PreviousActivityId)) == 1 && activities.Count(x => String.IsNullOrWhiteSpace(x.NextActivityId)) == 1)
                {
                    List<ActivityEntity> activityLinkedList = new List<ActivityEntity>();
                    activityLinkedList.Add(activities.Where(x => String.IsNullOrWhiteSpace(x.PreviousActivityId)).FirstOrDefault());
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
                            return _helper.RemoveStudyDesignElements(listofelements, studyDesigns, studyId);
                        }
                        return Constants.ErrorMessages.StudyDesignNotFound;
                    }
                    else
                    {
                        var studyDesigns = _mapper.Map<List<StudyDesignDto>>(checkStudy.ClinicalStudy.StudyDesigns);
                        return study.ClinicalStudy.StudyDesigns is not null && study.ClinicalStudy.StudyDesigns.Any() ?
                            _helper.RemoveStudyDesignElements(listofelements, studyDesigns, studyId) : Constants.ErrorMessages.StudyDesignNotFound;
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
        /// GET Audit Trial
        /// </summary>
        /// <param name="fromDate">Start Date for Date Filter</param>
        /// <param name="toDate">End Date for Date Filter</param>
        /// <param name="studyId">Study ID</param>
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="object"/> with matching studyId <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<object> GetAuditTrail(string studyId, DateTime fromDate, DateTime toDate, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServiceV2)}; Method : {nameof(GetAuditTrail)};");
                List<AuditTrailResponseEntity> studies = await _clinicalStudyRepository.GetAuditTrail(studyId, fromDate, toDate);
                if (studies == null)
                {
                    return null;
                }
                else
                {
                    studies = await CheckAccessForStudyAudit(studyId, studies, user);
                    if (studies == null)
                        return Constants.ErrorMessages.Forbidden;
                    var auditTrailDtoList = _mapper.Map<List<AuditTrailDto>>(studies); //Mapping Entity to Dto 
                    AudiTrailResponseDto getStudyAuditDto = new AudiTrailResponseDto
                    {
                        StudyId = studyId,
                        AuditTrail = auditTrailDtoList
                    };

                    return getStudyAuditDto;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServiceV2)}; Method : {nameof(GetAuditTrail)};");
            }
        }


        /// <summary>
        /// Get AllStudy Id's
        /// </summary>
        /// <param name="fromDate">Start Date for Date Filter</param>
        /// <param name="toDate">End Date for Date Filter</param>
        /// <param name="studyTitle">Study Title Filter</param>
        /// <param name="user">Logged In User</param>
        /// <returns>
        /// A <see cref="List{StudyHistoryResponseEntity}"/> which has list of study ID's <br></br> <br></br>
        /// <see langword="null"/> If no study is matching with studyId
        /// </returns>
        public async Task<List<StudyHistoryResponseDto>> GetStudyHistory(DateTime fromDate, DateTime toDate, string studyTitle, LoggedInUser user)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServiceV2)}; Method : {nameof(GetStudyHistory)};");
                List<StudyHistoryResponseEntity> studies = await _clinicalStudyRepository.GetStudyHistory(fromDate, toDate, studyTitle, user); //Getting List of studyId, studyTitle and Version
                if (studies == null)
                {
                    return null;
                }
                else
                {
                    var groupStudy = studies.GroupBy(x => new { x.StudyId })
                                            .Select(g => new //StudyHistoryResponseDto
                                            {
                                                StudyId = g.Key.StudyId,
                                                SDRUploadVersion = _mapper.Map<List<UploadVersionDto>>(g.ToList()),
                                                Date = g.Max(x => x.EntryDateTime)
                                            }) // Grouping the Id's by studyId
                                            .OrderByDescending(x => x.Date)
                                            .ToList();

                    List<StudyHistoryResponseDto> studyHistory = JsonConvert.DeserializeObject<List<StudyHistoryResponseDto>>(JsonConvert.SerializeObject(groupStudy));

                    return studyHistory;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _logger.LogInformation($"Ended Service : {nameof(ClinicalStudyServiceV2)}; Method : {nameof(GetStudyHistory)};");
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
        public async Task<object> PostAllElements(StudyDto studyDTO, LoggedInUser user, string method)
        {
            try
            {
                _logger.LogInformation($"Started Service : {nameof(ClinicalStudyServiceV2)}; Method : {nameof(PostAllElements)};");
                if (!await CheckPermissionForAUser(user))
                    return Constants.ErrorMessages.PostRestricted;
                StudyEntity incomingStudyEntity = new StudyEntity
                {
                    ClinicalStudy = _mapper.Map<ClinicalStudyEntity>(studyDTO.ClinicalStudy),
                    AuditTrail = _helper.GetAuditTrail(user?.UserName),
                    _id = MongoDB.Bson.ObjectId.GenerateNewId()
                };

                if (method == HttpMethod.Post.Method) //POST Endpoint to create new study
                {
                    studyDTO = await CreateNewStudy(incomingStudyEntity).ConfigureAwait(false);
                }
                else //PUT Endpoint Create New Version for the study
                {
                    AuditTrailEntity existingAuditTrail = await _clinicalStudyRepository.GetUsdmVersionAsync(incomingStudyEntity.ClinicalStudy.StudyId, 0);                    

                    if (existingAuditTrail is null && method == HttpMethod.Put.Method) // If PUT Endpoint and study_uuid is not valid, return not valid study
                    {
                        return Constants.ErrorMessages.StudyIdNotFound;
                    }

                    if (existingAuditTrail.UsdmVersion == Constants.USDMVersions.V2) // If previus USDM version is same as incoming
                    {
                        StudyEntity existingStudyEntity = await _clinicalStudyRepository.GetStudyItemsAsync(incomingStudyEntity.ClinicalStudy.StudyId, 0);

                        if (_helper.IsSameStudy(incomingStudyEntity, existingStudyEntity))
                        {
                            studyDTO = await UpdateExistingStudy(incomingStudyEntity, existingStudyEntity).ConfigureAwait(false);
                        }
                        else
                        {
                            studyDTO = await CreateNewVersionForAStudy(incomingStudyEntity, existingStudyEntity.AuditTrail).ConfigureAwait(false);
                            await PushMessageToServiceBus(new ServiceBusMessageDto { Study_uuid = incomingStudyEntity.ClinicalStudy.StudyId, CurrentVersion = incomingStudyEntity.AuditTrail.SDRUploadVersion });
                        }
                    }
                    else // If previus USDM version is different from incoming
                    {
                        studyDTO = await CreateNewVersionForAStudy(incomingStudyEntity, existingAuditTrail).ConfigureAwait(false);
                        await PushMessageToServiceBus(new ServiceBusMessageDto { Study_uuid = incomingStudyEntity.ClinicalStudy.StudyId, CurrentVersion = incomingStudyEntity.AuditTrail.SDRUploadVersion });
                    }
                }

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
            incomingStudyEntity.AuditTrail.UsdmVersion = Constants.USDMVersions.V2;
            await _clinicalStudyRepository.UpdateStudyItemsAsync(incomingStudyEntity);
            return _mapper.Map<StudyDto>(incomingStudyEntity);
        }

        public async Task<StudyDto> CreateNewVersionForAStudy(StudyEntity incomingStudyEntity, AuditTrailEntity existingAuditTrailEntity)
        {
            //incomingStudyEntity = _helper.CheckForSections(incomingStudyEntity, existingStudyEntity);
            incomingStudyEntity.AuditTrail.SDRUploadVersion = existingAuditTrailEntity.SDRUploadVersion + 1;
            incomingStudyEntity.AuditTrail.UsdmVersion = Constants.USDMVersions.V2;
            await _clinicalStudyRepository.PostStudyItemsAsync(incomingStudyEntity);
            return _mapper.Map<StudyDto>(incomingStudyEntity);
        }

        #region Azure ServiceBus
        private async Task PushMessageToServiceBus(ServiceBusMessageDto serviceBusMessageDto)
        {
            ServiceBusSender sender = _serviceBusClient.CreateSender(Config.AzureServiceBusQueueName);

            string jsonMessageString = JsonConvert.SerializeObject(serviceBusMessageDto);
            ServiceBusMessage serializedMessage = new ServiceBusMessage(jsonMessageString);
            await sender.SendMessageAsync(serializedMessage);
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

                if (user.UserRole != Constants.Roles.Org_Admin && Config.isGroupFilterEnabled)
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

                if (user.UserRole != Constants.Roles.Org_Admin && Config.isGroupFilterEnabled)
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

                if (user.UserRole != Constants.Roles.Org_Admin && Config.isGroupFilterEnabled)
                {
                    var groups = await _clinicalStudyRepository.GetGroupsOfUser(user).ConfigureAwait(false);

                    if (groups != null && groups.Count > 0)
                    {
                        if (groups.Any(x => x.permission == Permissions.READ_WRITE.ToString()))
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
