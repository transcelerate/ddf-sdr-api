using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Utilities.Common;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class eCPTHelper
    {
        public static string GetPlannedSexOfParticipants(List<string> plannedSexOfParticipants)
        {

            if (plannedSexOfParticipants != null && plannedSexOfParticipants.Any())
            {
                if (plannedSexOfParticipants.Any(x => Constants.Male.Contains(x.ToLower())) && plannedSexOfParticipants.Any(x => Constants.Female.Contains(x.ToLower())))
                {
                    return "Male or Female";
                }
                else if (plannedSexOfParticipants.Any(x => Constants.Male.Contains(x.ToLower())))
                {
                    return "Male";

                }
                else if(plannedSexOfParticipants.Any(x=>Constants.Female.Contains(x.ToLower())))
                {
                    return "Female";
                }
            }
            return string.Empty;
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
    }
}
