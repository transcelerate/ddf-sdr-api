using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TransCelerate.SDR.Core.DTO.eCPT;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class ECPTHelper
    {
        public static string GetCptMappingValue(string entity, string code)
        {
            string cptMappingValue = null;
            if (code != null)
            {
                var sdrCptMapping = SdrCptMapping.SdrCptMasterDataMapping.Where(x => x.Entity == entity).FirstOrDefault();
                if (sdrCptMapping != null && sdrCptMapping.Mapping != null && sdrCptMapping.Mapping.Any())
                {
                    var mappings = sdrCptMapping.Mapping;
                    cptMappingValue = mappings.Where(x => x.Code.ToLower() == code.ToLower()).Any() ?
                                      mappings.Where(x => x.Code.ToLower() == code.ToLower()).Select(x => !String.IsNullOrWhiteSpace(x.CPT) ? x.CPT : x.CDISC).FirstOrDefault() : null;
                }
            }
            return cptMappingValue;
        }

        public static string CheckForMaxMin(List<string> ageOfSubjects, bool isMax)
        {
            if (ageOfSubjects.Count == 1)
            {
                return ageOfSubjects[0];
            }
            if (ageOfSubjects.Where(x => int.TryParse(x, out int number)).Count() != ageOfSubjects.Count)
            {
                return string.Empty;
            }
            return isMax ? ageOfSubjects.Where(x => int.TryParse(x, out int number)).Max(y => int.Parse(y)).ToString() : ageOfSubjects.Where(x => int.TryParse(x, out int number)).Min(y => int.Parse(y)).ToString();
        }

        #region eCPT Helper V3
        public static string GetPlannedSexOfParticipantsV3(List<TransCelerate.SDR.Core.DTO.StudyV3.StudyDesignPopulationDto> studyDesignPopulations)
        {
            if (studyDesignPopulations != null && studyDesignPopulations.Any())
            {
                List<TransCelerate.SDR.Core.DTO.StudyV3.CodeDto> plannedSexofParticipants = studyDesignPopulations.Where(x => x.PlannedSexOfParticipants != null).SelectMany(x => x.PlannedSexOfParticipants).ToList();
                if (plannedSexofParticipants.Any())
                {
                    if (plannedSexofParticipants.Count == 1)
                    {
                        return GetCptMappingValue(Constants.SdrCptMasterDataEntities.SexofParticipants, plannedSexofParticipants[0].Code) ?? plannedSexofParticipants[0].Decode;
                    }
                    var cptMappingForPlannedSexofParticipants = plannedSexofParticipants.Select(x => new
                    {
                        code = x.Code,
                        decode = x.Decode,
                        cptValue = GetCptMappingValue(Constants.SdrCptMasterDataEntities.SexofParticipants, x.Code)
                    });
                    if ((cptMappingForPlannedSexofParticipants.Any(x => x.cptValue == Constants.PlannedSexOfParticipants.Male) && cptMappingForPlannedSexofParticipants.Any(x => x.cptValue == Constants.PlannedSexOfParticipants.Female)) || cptMappingForPlannedSexofParticipants.Any(x => x.cptValue == Constants.PlannedSexOfParticipants.MaleOrFemale))
                    {
                        return Constants.PlannedSexOfParticipants.MaleOrFemale;
                    }
                    else if (cptMappingForPlannedSexofParticipants.Any(x => x.cptValue == Constants.PlannedSexOfParticipants.Male))
                    {
                        return Constants.PlannedSexOfParticipants.Male;
                    }
                    else if (cptMappingForPlannedSexofParticipants.Any(x => x.cptValue == Constants.PlannedSexOfParticipants.Female))
                    {
                        return Constants.PlannedSexOfParticipants.Female;
                    }
                    else
                        return String.Empty;
                }
            }
            return null;
        }
        public static TransCelerate.SDR.Core.DTO.StudyV3.StudyProtocolVersionDto GetOrderedStudyProtocolsV3(List<TransCelerate.SDR.Core.DTO.StudyV3.StudyProtocolVersionDto> studyProtocolVersions)
        {
            var protocolsWithDateAndVersions = studyProtocolVersions.Where(x => DateTime.TryParse(x.ProtocolEffectiveDate, out var date) && decimal.TryParse(x.ProtocolVersion, out decimal version)).ToList();

            if (!protocolsWithDateAndVersions.Any())
                return studyProtocolVersions.FirstOrDefault();
            else if (protocolsWithDateAndVersions.Count == 1)
                return protocolsWithDateAndVersions.FirstOrDefault();
            else
                return protocolsWithDateAndVersions.OrderByDescending(x => DateTime.Parse(x.ProtocolEffectiveDate)).ThenByDescending(x => decimal.Parse(x.ProtocolVersion)).FirstOrDefault();
        }
        public static ObjectivesEndpointsAndEstimandsDto GetObjectivesEndpointsAndEstimandsDtoV3(List<TransCelerate.SDR.Core.DTO.StudyV3.ObjectiveDto> objectives, IMapper mapper)
        {
            ObjectivesEndpointsAndEstimandsDto objectivesEndpointsAndEstimandsDto = new();
            if (objectives != null && objectives.Any())
            {
                var objectiveMapping = SdrCptMapping.SdrCptMasterDataMapping.Where(x => x.Entity == Constants.SdrCptMasterDataEntities.ObjectiveLevel).FirstOrDefault().Mapping;
                var primaryObjective = objectives.Where(x => x.ObjectiveLevel?.Code == objectiveMapping.Where(x => x.CDISC == Constants.IdType.STUDY_PRIMARY_OBJECTIVE).Select(x => x.Code).FirstOrDefault());
                var secondaryObjective = objectives.Where(x => x.ObjectiveLevel?.Code == objectiveMapping.Where(x => x.CDISC == Constants.IdType.STUDY_SECONDARY_OBJECTIVE).Select(x => x.Code).FirstOrDefault());
                if (primaryObjective != null && primaryObjective.Any())
                {
                    objectivesEndpointsAndEstimandsDto.PrimaryObjectives = mapper.Map<List<ObjectivesDto>>(primaryObjective);
                }
                if (secondaryObjective != null && secondaryObjective.Any())
                {
                    objectivesEndpointsAndEstimandsDto.SecondaryObjectives = mapper.Map<List<ObjectivesDto>>(secondaryObjective);
                }
            }
            return objectivesEndpointsAndEstimandsDto;
        }
        #endregion

        #region eCPT Helper V4
        public static string GetPlannedSexOfParticipantsFromCodeListV4(this List<TransCelerate.SDR.Core.DTO.StudyV4.CodeDto> plannedSexofParticipants)
        {
            if (plannedSexofParticipants != null && plannedSexofParticipants.Any())
            {
                if (plannedSexofParticipants.Any())
                {
                    if (plannedSexofParticipants.Count == 1)
                    {
                        return GetCptMappingValue(Constants.SdrCptMasterDataEntities.SexofParticipants, plannedSexofParticipants[0].Code) ?? plannedSexofParticipants[0].Decode;
                    }
                    var cptMappingForPlannedSexofParticipants = plannedSexofParticipants.Select(x => new
                    {
                        code = x.Code,
                        decode = x.Decode,
                        cptValue = GetCptMappingValue(Constants.SdrCptMasterDataEntities.SexofParticipants, x.Code)
                    });
                    if ((cptMappingForPlannedSexofParticipants.Any(x => x.cptValue == Constants.PlannedSexOfParticipants.Male) && cptMappingForPlannedSexofParticipants.Any(x => x.cptValue == Constants.PlannedSexOfParticipants.Female)) || cptMappingForPlannedSexofParticipants.Any(x => x.cptValue == Constants.PlannedSexOfParticipants.MaleOrFemale))
                    {
                        return Constants.PlannedSexOfParticipants.MaleOrFemale;
                    }
                    else if (cptMappingForPlannedSexofParticipants.Any(x => x.cptValue == Constants.PlannedSexOfParticipants.Male))
                    {
                        return Constants.PlannedSexOfParticipants.Male;
                    }
                    else if (cptMappingForPlannedSexofParticipants.Any(x => x.cptValue == Constants.PlannedSexOfParticipants.Female))
                    {
                        return Constants.PlannedSexOfParticipants.Female;
                    }
                    else
                        return String.Empty;
                }
            }
            return null;
        }
        public static TransCelerate.SDR.Core.DTO.StudyV4.StudyProtocolDocumentVersionDto GetStudyProtocolVersionsV4(this Core.DTO.StudyV4.StudyVersionDto studyVersion, List<TransCelerate.SDR.Core.DTO.StudyV4.StudyProtocolDocumentVersionDto> studyProtocolVersions)
        {
            if (!String.IsNullOrWhiteSpace(studyVersion.DocumentVersionId))
            {
                return (studyProtocolVersions.Find(x => x.Id == studyVersion.DocumentVersionId));
            }
            return null;
        }
        public static string GetNumberOfParticipantsV4(this Core.DTO.StudyV4.StudyDesignPopulationDto population)
        {
            if (population is not null && population.PlannedEnrollmentNumber is not null)
            {
                if (Convert.ToInt32(population.PlannedEnrollmentNumber.MaxValue) == Convert.ToInt32(population.PlannedEnrollmentNumber.MinValue))
                    return population.PlannedEnrollmentNumber.MaxValue.ToString();
                else
                    return $"{population.PlannedEnrollmentNumber.MinValue} to {population.PlannedEnrollmentNumber.MaxValue}";
            }
            else if (population is not null && population.PlannedEnrollmentNumber is null)
            {
                if (population.Cohorts is not null && population.Cohorts.Any())
                {
                    var plannedEnrollmentNumbers = population.Cohorts.Where(x => x.PlannedEnrollmentNumber is not null).Select(x => x.PlannedEnrollmentNumber);
                    if (plannedEnrollmentNumbers.Min(x => Convert.ToInt32(x.MinValue)) != plannedEnrollmentNumbers.Max(x => Convert.ToInt32(x.MaxValue)))
                    {
                        return $"{plannedEnrollmentNumbers.Min(x => x.MinValue)} to {plannedEnrollmentNumbers.Max(x => x.MaxValue)}";
                    }
                    else
                        return $"{plannedEnrollmentNumbers.Max(x => x.MaxValue)}";
                }
            }
            return null;
        }

        public static string GetPlannedSexOfParticipantsV4(this Core.DTO.StudyV4.StudyDesignPopulationDto population)
        {
            if (population is not null && population.PlannedSex is not null && population.PlannedSex.Any())
            {
                var plannedSexOfParticipants = population.PlannedSex;
                if (population.Cohorts is not null && population.Cohorts.Any())
                {
                    plannedSexOfParticipants.AddRange(population.Cohorts.Where(x => x.PlannedSex is not null && x.PlannedSex.Any()).SelectMany(x => x.PlannedSex).ToList());
                }
                return plannedSexOfParticipants.GetPlannedSexOfParticipantsFromCodeListV4();
            }
            return null;
        }
        public static string GetAgeV4(this Core.DTO.StudyV4.StudyDesignPopulationDto population, bool isMax)
        {
            if (population is not null && population.PlannedAge is not null)
            {
                var plannedAges = new List<Core.DTO.StudyV4.RangeDto> { population.PlannedAge };
                if (population.Cohorts is not null && population.Cohorts.Any())
                {
                    plannedAges.AddRange(population.Cohorts.Where(x => x.PlannedAge is not null).Select(x => x.PlannedAge).ToList());
                }
                return isMax ? plannedAges.Max(x => x.MaxValue).ToString() : plannedAges.Min(x => x.MinValue).ToString();
            }
            return null;
        }
        public static ObjectivesEndpointsAndEstimandsDto GetObjectivesEndpointsAndEstimandsDtoV4(List<TransCelerate.SDR.Core.DTO.StudyV4.ObjectiveDto> objectives, IMapper mapper)
        {
            ObjectivesEndpointsAndEstimandsDto objectivesEndpointsAndEstimandsDto = new();
            if (objectives != null && objectives.Any())
            {
                var objectiveMapping = SdrCptMapping.SdrCptMasterDataMapping.Where(x => x.Entity == Constants.SdrCptMasterDataEntities.ObjectiveLevel).FirstOrDefault().Mapping;
                var primaryObjective = objectives.Where(x => x.Level?.Code == objectiveMapping.Where(x => x.CDISC == Constants.IdType.STUDY_PRIMARY_OBJECTIVE).Select(x => x.Code).FirstOrDefault());
                var secondaryObjective = objectives.Where(x => x.Level?.Code == objectiveMapping.Where(x => x.CDISC == Constants.IdType.STUDY_SECONDARY_OBJECTIVE).Select(x => x.Code).FirstOrDefault());
                if (primaryObjective != null && primaryObjective.Any())
                {
                    objectivesEndpointsAndEstimandsDto.PrimaryObjectives = mapper.Map<List<ObjectivesDto>>(primaryObjective);
                }
                if (secondaryObjective != null && secondaryObjective.Any())
                {
                    objectivesEndpointsAndEstimandsDto.SecondaryObjectives = mapper.Map<List<ObjectivesDto>>(secondaryObjective);
                }
            }
            return objectivesEndpointsAndEstimandsDto;
        }

        public static string GetAmendmentNumber(this List<Core.DTO.StudyV4.StudyAmendmentDto> amendments)
        {
            if (amendments is not null && amendments.Any())
            {
                // If there is only one amendment, take the first one
                if (amendments.Count == 1)
                    return amendments.First().Number;
                else
                {
                    // If there are more than one amendment, but the previous Id is not referred for more than one or none amendment, then take 1st amendment
                    if (amendments.Where(x => String.IsNullOrWhiteSpace(x.PreviousId)).Count() != 1)
                        return amendments.First().Number;
                    else
                    {
                        // If there are more than one amendment, that is not referred in previousId, then take 1st amendment
                        if (amendments.Where(x => !amendments.Select(y => y.PreviousId).Contains(x.Id)).Count() != 1)
                            return amendments.First().Number;
                        else
                            return amendments.Find(x => !amendments.Select(y => y.PreviousId).Contains(x.Id)).Number;
                    }
                }
            }
            return null;
        }
        #endregion

        #region eCPT Helper V5
        public static string GetPlannedSexOfParticipantsFromCodeListV5(this List<TransCelerate.SDR.Core.DTO.StudyV5.CodeDto> plannedSexofParticipants)
        {
            if (plannedSexofParticipants != null && plannedSexofParticipants.Any())
            {
                if (plannedSexofParticipants.Any())
                {
                    if (plannedSexofParticipants.Count == 1)
                    {
                        return GetCptMappingValue(Constants.SdrCptMasterDataEntities.SexofParticipants, plannedSexofParticipants[0].Code) ?? plannedSexofParticipants[0].Decode;
                    }
                    var cptMappingForPlannedSexofParticipants = plannedSexofParticipants.Select(x => new
                    {
                        code = x.Code,
                        decode = x.Decode,
                        cptValue = GetCptMappingValue(Constants.SdrCptMasterDataEntities.SexofParticipants, x.Code)
                    });
                    if ((cptMappingForPlannedSexofParticipants.Any(x => x.cptValue == Constants.PlannedSexOfParticipants.Male) && cptMappingForPlannedSexofParticipants.Any(x => x.cptValue == Constants.PlannedSexOfParticipants.Female)) || cptMappingForPlannedSexofParticipants.Any(x => x.cptValue == Constants.PlannedSexOfParticipants.MaleOrFemale))
                    {
                        return Constants.PlannedSexOfParticipants.MaleOrFemale;
                    }
                    else if (cptMappingForPlannedSexofParticipants.Any(x => x.cptValue == Constants.PlannedSexOfParticipants.Male))
                    {
                        return Constants.PlannedSexOfParticipants.Male;
                    }
                    else if (cptMappingForPlannedSexofParticipants.Any(x => x.cptValue == Constants.PlannedSexOfParticipants.Female))
                    {
                        return Constants.PlannedSexOfParticipants.Female;
                    }
                    else
                        return String.Empty;
                }
            }
            return null;
        }
        public static TransCelerate.SDR.Core.DTO.StudyV5.StudyDefinitionDocumentVersionDto GetStudyProtocolVersionsV5(this Core.DTO.StudyV5.StudyVersionDto studyVersion, List<TransCelerate.SDR.Core.DTO.StudyV5.StudyDefinitionDocumentVersionDto> studyProtocolVersions)
        {
            //if (!String.IsNullOrWhiteSpace(studyVersion.DocumentVersionIds))
            //{
            //    return (studyProtocolVersions.Find(x => x.Id == studyVersion.DocumentVersionIds));
            //}
            if (studyVersion.DocumentVersionIds != null && studyVersion.DocumentVersionIds.Any())
            {
                return studyProtocolVersions.Find(x => studyVersion.DocumentVersionIds.Contains(x.Id));
            }
            return null;
        }
        public static string GetNumberOfParticipantsV5(this Core.DTO.StudyV5.StudyDesignPopulationDto population)
        {
            if (population?.PlannedEnrollmentNumber is Core.DTO.StudyV5.RangeDto plannedEnrollmentNumberRange)
            {
                if (Convert.ToInt32(plannedEnrollmentNumberRange.MaxValue) == Convert.ToInt32(plannedEnrollmentNumberRange.MinValue))
                    return plannedEnrollmentNumberRange.MaxValue.ToString();
                else
                    return $"{plannedEnrollmentNumberRange.MinValue} to {plannedEnrollmentNumberRange.MaxValue}";
            }
            else if (population?.PlannedEnrollmentNumber is Core.DTO.StudyV5.QuantityDto plannedEnrollmentNumberQuantity)
            {
                return plannedEnrollmentNumberQuantity.Value.ToString();
            }
            else if (population is not null && population.PlannedEnrollmentNumber is null && population.Cohorts?.Any() == true)
            {
                var plannedEnrollmentRanges = population.Cohorts
                    .Where(x => x?.PlannedEnrollmentNumber is Core.DTO.StudyV5.RangeDto)
                    .Select(x => x.PlannedEnrollmentNumber as Core.DTO.StudyV5.RangeDto)
                    .ToList();

                if (plannedEnrollmentRanges.Any())
                {
                    var minValue = plannedEnrollmentRanges.Min(x => Convert.ToInt32(x.MinValue));
                    var maxValue = plannedEnrollmentRanges.Max(x => Convert.ToInt32(x.MaxValue));

                    if (minValue != maxValue)
                    {
                        return $"{minValue} to {maxValue}";
                    }
                    else
                    {
                        return maxValue.ToString();
                    }
                }
            }
            return null;
        }
        public static string GetPlannedSexOfParticipantsV5(this Core.DTO.StudyV5.StudyDesignPopulationDto population)
        {
            if (population is not null && population.PlannedSex is not null && population.PlannedSex.Any())
            {
                var plannedSexOfParticipants = population.PlannedSex;
                if (population.Cohorts is not null && population.Cohorts.Any())
                {
                    plannedSexOfParticipants.AddRange(population.Cohorts.Where(x => x.PlannedSex is not null && x.PlannedSex.Any()).SelectMany(x => x.PlannedSex).ToList());
                }
                return plannedSexOfParticipants.GetPlannedSexOfParticipantsFromCodeListV5();
            }
            return null;
        }
        public static string GetAgeV5(this Core.DTO.StudyV5.StudyDesignPopulationDto population, bool isMax)
        {
            if (population is not null && population.PlannedAge is not null)
            {
                var plannedAges = new List<Core.DTO.StudyV5.RangeDto> { population.PlannedAge };
                if (population.Cohorts is not null && population.Cohorts.Any())
                {
                    plannedAges.AddRange(population.Cohorts.Where(x => x.PlannedAge is not null).Select(x => x.PlannedAge).ToList());
                }
                return isMax ? plannedAges.Max(x => x.MaxValue).ToString() : plannedAges.Min(x => x.MinValue).ToString();
            }
            return null;
        }
        public static ObjectivesEndpointsAndEstimandsDto GetObjectivesEndpointsAndEstimandsDtoV5(List<TransCelerate.SDR.Core.DTO.StudyV5.ObjectiveDto> objectives, IMapper mapper)
        {
            ObjectivesEndpointsAndEstimandsDto objectivesEndpointsAndEstimandsDto = new();
            if (objectives != null && objectives.Any())
            {
                var objectiveMapping = SdrCptMapping.SdrCptMasterDataMapping.Where(x => x.Entity == Constants.SdrCptMasterDataEntities.ObjectiveLevel).FirstOrDefault().Mapping;
                var primaryObjective = objectives.Where(x => x.Level?.Code == objectiveMapping.Where(x => x.CDISC == Constants.IdType.STUDY_PRIMARY_OBJECTIVE).Select(x => x.Code).FirstOrDefault());
                var secondaryObjective = objectives.Where(x => x.Level?.Code == objectiveMapping.Where(x => x.CDISC == Constants.IdType.STUDY_SECONDARY_OBJECTIVE).Select(x => x.Code).FirstOrDefault());
                if (primaryObjective != null && primaryObjective.Any())
                {
                    objectivesEndpointsAndEstimandsDto.PrimaryObjectives = mapper.Map<List<ObjectivesDto>>(primaryObjective);
                }
                if (secondaryObjective != null && secondaryObjective.Any())
                {
                    objectivesEndpointsAndEstimandsDto.SecondaryObjectives = mapper.Map<List<ObjectivesDto>>(secondaryObjective);
                }
            }
            return objectivesEndpointsAndEstimandsDto;
        }

        public static string GetAmendmentNumber(this List<Core.DTO.StudyV5.StudyAmendmentDto> amendments)
        {
            if (amendments is not null && amendments.Any())
            {
                // If there is only one amendment, take the first one
                if (amendments.Count == 1)
                    return amendments.First().Number;
                else
                {
                    // If there are more than one amendment, but the previous Id is not referred for more than one or none amendment, then take 1st amendment
                    if (amendments.Where(x => String.IsNullOrWhiteSpace(x.PreviousId)).Count() != 1)
                        return amendments.First().Number;
                    else
                    {
                        // If there are more than one amendment, that is not referred in previousId, then take 1st amendment
                        if (amendments.Where(x => !amendments.Select(y => y.PreviousId).Contains(x.Id)).Count() != 1)
                            return amendments.First().Number;
                        else
                            return amendments.Find(x => !amendments.Select(y => y.PreviousId).Contains(x.Id)).Number;
                    }
                }
            }
            return null;
        }

        public static string GetPrimaryPurposeFromIntentTypes(this List<TransCelerate.SDR.Core.DTO.StudyV5.CodeDto> intentTypes)
        {
            if (intentTypes != null && intentTypes.Any())
            {
                if (intentTypes.Count == 1)
                {
                    return GetCptMappingValue(Constants.SdrCptMasterDataEntities.TrialIntentType, intentTypes.FirstOrDefault()?.Code) ?? intentTypes.FirstOrDefault()?.Decode;
                }
                else
                {
                    return $"{String.Join(", ", intentTypes.Select(x => ECPTHelper.GetCptMappingValue(Constants.SdrCptMasterDataEntities.TrialIntentType, x.Code) ?? x.Decode).ToArray(), 0, intentTypes.Count - 1)}" +
                           $" and {intentTypes.Select(x => ECPTHelper.GetCptMappingValue(Constants.SdrCptMasterDataEntities.TrialIntentType, x.Code) ?? x.Decode).LastOrDefault()}";
                }
            }

            return null;
        }

        #endregion
    }
}
