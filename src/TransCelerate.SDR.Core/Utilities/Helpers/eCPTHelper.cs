using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using TransCelerate.SDR.Core.DTO.eCPT;
using TransCelerate.SDR.Core.Utilities.Common;
using static TransCelerate.SDR.Core.Utilities.Common.Constants;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public  class eCPTHelper
    {
        public static string GetPlannedSexOfParticipants(List<TransCelerate.SDR.Core.DTO.StudyV2.StudyDesignPopulationDto> studyDesignPopulations)
        {
            if (studyDesignPopulations != null && studyDesignPopulations.Any())
            {
                List<TransCelerate.SDR.Core.DTO.StudyV2.CodeDto> plannedSexofParticipants = studyDesignPopulations.SelectMany(x => x.PlannedSexOfParticipants).ToList();
                if (plannedSexofParticipants.Any())
                {
                    if (plannedSexofParticipants.Count == 1)
                    {
                        return GetCptMappingValue(Constants.SdrCptMasterDataEntities.SexofParticipants, plannedSexofParticipants[0].Code) ?? plannedSexofParticipants[0].Decode;
                    }
                    var cptMappingForPlannedSexofParticipants = plannedSexofParticipants.Select(x => new
                    {
                        code= x.Code,
                        decode=x.Decode,
                        cptValue= GetCptMappingValue(Constants.SdrCptMasterDataEntities.SexofParticipants, x.Code)
                    });
                    if ((cptMappingForPlannedSexofParticipants.Any(x =>x.cptValue==Constants.PlannedSexOfParticipants.Male) && cptMappingForPlannedSexofParticipants.Any(x => x.cptValue == Constants.PlannedSexOfParticipants.Female)) || cptMappingForPlannedSexofParticipants.Any(x => x.cptValue == Constants.PlannedSexOfParticipants.MaleOrFemale))
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
        public static string CheckForMaxMin(List<string> ageOfSubjects, bool isMax)
        {
            if (ageOfSubjects.Count == 1)
            {
                return ageOfSubjects[0];
            }
            if(ageOfSubjects.Where(x=>int.TryParse(x,out int number)).Count()!=ageOfSubjects.Count)
            {
                return string.Empty;
            }
            return isMax ? ageOfSubjects.Where(x => int.TryParse(x, out int number)).Max(y => int.Parse(y)).ToString() : ageOfSubjects.Where(x => int.TryParse(x, out int number)).Min(y => int.Parse(y)).ToString();
        }

        public static string GetCptMappingValue(string entity, string code)
        {
            string cptMappingValue = null;
            if(code != null)
            {
                var mappings = SdrCptMapping.SdrCptMasterDataMapping.Where(x => x.Entity == entity).FirstOrDefault().Mapping;
                cptMappingValue = mappings.Where(x => x.Code.ToLower() == code.ToLower()).Any() ?
                                  mappings.Where(x => x.Code.ToLower() == code.ToLower()).Select(x => !String.IsNullOrWhiteSpace(x.CPT) ? x.CPT : x.CDISC).FirstOrDefault() : null;
            }
            return cptMappingValue;
        }
        public static TransCelerate.SDR.Core.DTO.StudyV2.StudyProtocolVersionDto GetOrderedStudyProtocols(List<TransCelerate.SDR.Core.DTO.StudyV2.StudyProtocolVersionDto> studyProtocolVersions)
        {
            var protocolsWithDateAndVersions = studyProtocolVersions.Where(x => DateTime.TryParse(x.ProtocolEffectiveDate, out var date) && decimal.TryParse(x.ProtocolVersion, out decimal version)).ToList();

            if (!protocolsWithDateAndVersions.Any())
                return studyProtocolVersions.FirstOrDefault();
            else if (protocolsWithDateAndVersions.Count == 1)
                return protocolsWithDateAndVersions.FirstOrDefault();
            else
                return protocolsWithDateAndVersions.OrderByDescending(x => DateTime.Parse(x.ProtocolEffectiveDate)).ThenByDescending(x => decimal.Parse(x.ProtocolVersion)).FirstOrDefault();
        }
        public static ObjectivesEndpointsAndEstimandsDto GetObjectivesEndpointsAndEstimandsDto(List<TransCelerate.SDR.Core.DTO.StudyV2.ObjectiveDto> objectives,IMapper mapper)
        {
            ObjectivesEndpointsAndEstimandsDto objectivesEndpointsAndEstimandsDto=new ObjectivesEndpointsAndEstimandsDto();
            if(objectives!=null && objectives.Any())
            {
                var objectiveMapping = SdrCptMapping.SdrCptMasterDataMapping.Where(x => x.Entity == Constants.SdrCptMasterDataEntities.ObjectiveLevel).FirstOrDefault().Mapping;
                var primaryObjective = objectives.Where(x => x.ObjectiveLevel?.Code == objectiveMapping.Where(x => x.CDISC == Constants.IdType.STUDY_PRIMARY_OBJECTIVE).Select(x => x.Code).FirstOrDefault()).FirstOrDefault();
                var secondaryObjective = objectives.Where(x => x.ObjectiveLevel?.Code == objectiveMapping.Where(x => x.CDISC == Constants.IdType.STUDY_SECONDARY_OBJECTIVE).Select(x => x.Code).FirstOrDefault()).FirstOrDefault();
                if(primaryObjective!=null )
                {
                    objectivesEndpointsAndEstimandsDto.PrimaryObjectives = mapper.Map<ObjectivesDto>(primaryObjective); 
                }
                if (secondaryObjective!= null)
                {
                    objectivesEndpointsAndEstimandsDto.SecondaryObjectives = mapper.Map<ObjectivesDto>(secondaryObjective);
                }
            }
            return objectivesEndpointsAndEstimandsDto;
        }     
    }
}
